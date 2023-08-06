using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using WebService;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace TestWebPjt
{
    class Program
    {
        enum MyEnum
        {
            FirstValue,
            SecondValue,
            ThirdValue
        }
        

        static void Main(string[] args)
        {
            int value = 5;
            MyEnum enumValue;

            enumValue = (MyEnum) value;

            Console.WriteLine(enumValue);


            /*Console.WriteLine("Test...");
            Console.ReadKey(true);

            var employee = new Employee()
            {
                //Id = 0,
                Name = "Alexander",
                Surname = "Fedotov",
                Phone = "89513586012",
                CompanyId = 1,
                Passport = new WebService.Passport { Type = "1", Number = "987654321" }
            };//создаём сотрудника

            var json = JsonConvert.SerializeObject(employee);

            // Адрес ресурса, к которому выполняется запрос
            string url = "http://localhost:8080";

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                string reply = webClient.UploadString(url, json);

                Console.WriteLine(reply);
            }
            //Console.ReadKey(true);


            var employee1 = new Employee()
            {
                //Id = 0,
                Name = "Boris",
                Surname = "Britva",
                Phone = "89513586013",
                CompanyId = 1,
                Passport = new WebService.Passport { Type = "1", Number = "123456789" }
            };//создаём сотрудника

            json = JsonConvert.SerializeObject(employee1);

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                string reply = webClient.UploadString(url, json);

                Console.WriteLine(reply);
            }
            //Console.ReadKey(true);

            var employee2 = new Employee()
            {
                //Id = 0,
                Name = "Tony",
                Surname = "Bullet",
                Phone = "89513586014",
                CompanyId = 2,
                Passport = new WebService.Passport { Type = "1", Number = "123987456" }
            };//создаём сотрудника

            json = JsonConvert.SerializeObject(employee2);

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                string reply = webClient.UploadString(url, json);

                Console.WriteLine(reply);
            }
            //Console.ReadKey(true);


            //три сотрудника в базе, считаем двух из них по CompanyId
            using (var webClient = new WebClient()) { 
                NameValueCollection myQueryStringCollection = new NameValueCollection();
                myQueryStringCollection.Add("CompanyId", "1");

                // Attach QueryString to the WebClient.
                webClient.QueryString = myQueryStringCollection;

                string inputStr = webClient.DownloadString(url);

                //вывод записей в консоль
                Console.WriteLine(inputStr);
            }

            //Console.ReadKey(true);


            var employeeP = new Employee()
            {
                Id = 2,
                Name = "Dag",
                Surname = "Head",
                Phone = "89513586015",
                //CompanyId = 1,
                Passport = new WebService.Passport { Type = "1", Number = "123456788" }
            };//с Id =2 уже другой сотрудыник

            json = JsonConvert.SerializeObject(employeeP);
            using (var webClient = new WebClient())
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
                webClient.UploadData(url, "PATCH", data);
            }

            //Console.ReadKey(true);

            using (var webClient = new WebClient())
            {
                var emp = new { Id = 1 };
                json = JsonConvert.SerializeObject(emp);

                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
                webClient.UploadData(url, "DELETE", data);
            }
            Console.ReadKey(true);*/
        }
    }
}
