using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace WebService
{
    class RequestListener
    {
        HttpListener _listener;

        public RequestListener(IEnumerable<string> prefixes)
        {
            _listener = new HttpListener();
            foreach (string s in prefixes)
            {
                _listener.Prefixes.Add(s);
            }
        }

        public void Listen()
        {
            _listener.Start();
            while (true)
            {
                var context = _listener.GetContext();
                lock (_listener)
                {
                    ProcessRequest(context);
                }
            }
        }

        static void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.HttpMethod == "GET")//запрос сотрудников с CompanyId
            {
                var rurl = context.Request.RawUrl;
                var IdRegex = new Regex(@"(CompanyId=)(?<CompanyId>(\w|\d)*)");

                var IdMatch = IdRegex.Match(rurl);

                int CompanyId = -1;
                if (IdMatch.Success)
                {
                    var stationIdString = IdMatch.Result("${CompanyId}");
                    if (int.TryParse(stationIdString, out CompanyId))
                    {
                        if(CompanyId != -1)
                        {
                            List<Employee> employees = EmployeeManager.GetEmployees(CompanyId);
                            if (employees.Any())
                            {
                                response.StatusCode = 200;

                                //сериализация коллекции
                                string json = JsonConvert.SerializeObject(employees);

                                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                                response.ContentLength64 = buffer.Length;
                                System.IO.Stream output = response.OutputStream;
                                output.Write(buffer, 0, buffer.Length);
                                output.Close();
                            }
                            else
                            {
                                response.StatusCode = 404;//не найдены сотрудники с таким CompanyId
                            }
                        }else
                        {
                            response.StatusCode = 400;//в теле запроса не найден CompanyId
                        }
                    }
                }
                response.Close();
                return;
            }

            if (request.HttpMethod == "POST")//добавить нового сотрудника в базу
            {
                var stream = request.InputStream;
                var sr = new StreamReader(stream);
                var res = sr.ReadToEnd();
                Console.WriteLine("");
                Console.WriteLine(res);
                
                Employee empFrJson = JsonConvert.DeserializeObject<Employee>(res);
                int employeeId = EmployeeManager.NewEmployee(empFrJson);//добавить нового сотрудника в базу и получить его Id

                if (employeeId > 0)
                { //сотрудник успешно добавлен в базу

                    //сформировать ответ
                    var outJson = new { Id = employeeId };
                    string json = JsonConvert.SerializeObject(outJson);

                    response.StatusCode = 200;

                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                else//не удалось добавить сотрудника в базу
                {
                    response.StatusCode = 500;
                }
                response.Close();
                return;
            }

            if (request.HttpMethod == "PATCH")//изменить сотрудника в базе
            {
                var stream = request.InputStream;
                var sr = new StreamReader(stream);
                var res = sr.ReadToEnd();
                Console.WriteLine("");
                Console.WriteLine(res);

                //проверка что десереализация прошла успешно
                Employee empFrJson = JsonConvert.DeserializeObject<Employee>(res);

                if(EmployeeManager.ChangeEmployee(empFrJson))
                {
                    response.StatusCode = 200;
                }
                else
                {
                    response.StatusCode = 404;//не найден сотрудник с таким Id
                }
                response.Close();
                return;
            }

            if (request.HttpMethod == "DELETE")//удалить сотрудника по его Id
            {
                var stream = request.InputStream;
                var sr = new StreamReader(stream);
                var res = sr.ReadToEnd();
                Console.WriteLine("");
                Console.WriteLine(res);

                var companyIdType = new { Id = 0 };
                var frJson = JsonConvert.DeserializeAnonymousType(res, companyIdType);

                if(EmployeeManager.DelEmployee(frJson.Id))
                {
                    response.StatusCode = 200;
                }
                else
                {
                    response.StatusCode = 404;//не найден сотрудник с таким Id
                }
                response.Close();
                return;
            }
            
        }
    }
}