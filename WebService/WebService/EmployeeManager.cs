using System;
using System.Collections.Generic;

namespace WebService
{
    class EmployeeManager
    {
        public static void initEmployeeTable()
        {
            DBHelper.NameDB = "test.sqlite";
            DBHelper.CreateDataBase();
        }

        //добавить сотрудника, в ответ отправить его Id
        public static int NewEmployee(Employee empl)
        {
            Console.WriteLine("");
            Console.WriteLine("Попытка добавить сотрудника Name = {0}, Surname = {1} в базу.", empl.Name,empl.Surname);
            //получить список сотрудников
            List<Employee> empls = DBHelper.GetAllEmployees();

            foreach(Employee curEmp in empls)
            {
                if (empl == curEmp) {
                    Console.WriteLine("Сотрудник с Name = {0}, Surname = {1} уже есть в базе", empl.Name, empl.Surname);
                    return curEmp.Id;
                }
            }

            //сформировать Id нового сотрудника
            int newEmployeeId = empls.Count + 1;

            //назначить Id новому сотруднику
            empl.Id = newEmployeeId;

            //добавить нового сотрудника в базу
            if (DBHelper.AddEmployee(empl))
                return empl.Id;
            else
                return -1;//0
        }

        //удалить сотрудника по Id
        public static bool DelEmployee(int id)
        {
            Console.WriteLine("");
            if (DBHelper.DeleteEmployee(id))
                return true;
            else
                return false;
        }

        //вывести список сотрудников для указанной компании
        public static List<Employee> GetEmployees(int companyId)
        {
            Console.WriteLine("");
            return DBHelper.GetEmployees(companyId);
        }

        //изменить сотрудника по его Id, при этом изменены должны быть только те поля,которые указаны в запросе
        public static bool ChangeEmployee(Employee empl)
        {
            Console.WriteLine("");
            Console.WriteLine("Попытка изменить сотрудника с Id = {0} в базе.", empl.Id);

            //получить список сотрудников
            List<Employee> empls = DBHelper.GetAllEmployees();

            foreach (Employee curEmp in empls)
            {
                if (empl.Id == curEmp.Id)
                {
                    //изменение полей сотрудника, если в запросе что-то было передано
                    if (empl.Name == null) empl.Name = curEmp.Name;
                    if (empl.Surname == null) empl.Surname = curEmp.Surname;
                    if(empl.Phone == null) empl.Phone = curEmp.Phone;
                    if (empl.CompanyId < 0) empl.CompanyId = curEmp.CompanyId;
                    if (empl.Passport.Number == null) empl.Passport.Number = curEmp.Passport.Number;
                    if (empl.Passport.Type == null) empl.Passport.Type = curEmp.Passport.Type;

                    if (DBHelper.SetEmployeeFields(empl))
                        return true;
                    else
                        return false;
                }
            }
            Console.WriteLine("Сотрудник с Id = {0} не найден в базе.", empl.Id);
            return false;
        }
    }
}
