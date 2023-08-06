using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WebService
{
    class DBHelper
    {
        private static string _nameDB;
        private static string _connectionString;

        public static string NameDB
        {
            get
            {
                return _nameDB;
            }

            set
            {
                _nameDB = value;
                _connectionString = string.Format("Data Source={0};", _nameDB);
            }
        }

        public static List<Employee> GetAllEmployees()
        {
            Console.WriteLine("Чтение записей из базы...");

            List<Employee> empls = new List<Employee>();

            try
            {
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "SELECT * FROM Employee";

                        SQLiteDataReader reader = command.ExecuteReader();
                        if(reader.HasRows)
                            while (reader.Read()) 
                            {
                                Console.WriteLine("Считан сотрудник: " + "Id: " + reader["Id"] + " Name: " + reader["Name"] + " Surname: " + reader["Surname"]);

                                Passport pass = new Passport()
                                {
                                    Type = (string)reader["Type"],
                                    Number = (string)reader["Number"]
                                };

                                Employee emp = new Employee()
                                {
                                    Id = Convert.ToInt32((Int64)reader["Id"]),//в reader почему-то INTEGER читаются как Int64, поэтому приводим к int
                                    Name = (string)reader["Name"],
                                    Surname = (string)reader["Surname"],
                                    Phone = (string)reader["Phone"],
                                    CompanyId = Convert.ToInt32((Int64)reader["CompanyId"]),
                                    Passport = pass
                                };

                                empls.Add(emp);
                            }
                        else
                            Console.WriteLine("В базе пока нет ни одного сотрудника.");
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при чтении записей из базы. {0}", ex);
            }
            return empls;
        }

        public static List<Employee> GetEmployees(int companyId)
        {
            Console.WriteLine("Чтение записей c CompanyId = {0}...", companyId);

            List<Employee> empls = new List<Employee>();

            try
            {
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        string cmdText = string.Format("SELECT * FROM Employee WHERE CompanyId = {0}", companyId);
                        command.CommandText = cmdText;

                        SQLiteDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                Console.WriteLine("Считан сотрудник: " + "Id: " + reader["Id"] + " Name: " + reader["Name"] + " Surname: " + reader["Surname"]);

                                Passport pass = new Passport()
                                {
                                    Type = (string)reader["Type"],
                                    Number = (string)reader["Number"]
                                };

                                Employee emp = new Employee()
                                {
                                    Id = Convert.ToInt32((Int64)reader["Id"]),//в reader почему-то INTEGER читаются как Int64, поэтому приводим к int
                                    Name = (string)reader["Name"],
                                    Surname = (string)reader["Surname"],
                                    Phone = (string)reader["Phone"],
                                    CompanyId = Convert.ToInt32((Int64)reader["CompanyId"]),//в reader почему-то INTEGER читаются как Int64, поэтому приводим к int
                                    Passport = pass
                                };

                                empls.Add(emp);
                            }
                        else
                            Console.WriteLine("В базе пока нет ни одного сотрудника.");
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при чтении записей из базы. {0}", ex);
            }
            return empls;
        }

        public static bool AddEmployee(Employee empl)
        {
            bool ret = false;
            try
            {
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        string cmdText = string.Format("INSERT INTO 'Employee' ('Id', 'Name', 'Surname', 'Phone', 'CompanyId','Type','Number') " +
                           "VALUES ({0},'{1}','{2}','{3}',{4},'{5}','{6}')",
                           empl.Id, empl.Name, empl.Surname, empl.Phone, empl.CompanyId, empl.Passport.Type, empl.Passport.Number);
                        command.CommandText = cmdText;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                Console.WriteLine("Сотрудник {0} {1} Id = {2} добавлен в базу.", empl.Name, empl.Surname, empl.Id);
                ret = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении записи в базу. {0}", ex);
            }
            return ret;
        }

        public static bool DeleteEmployee(int id)
        {
            bool ret = false;

            try { 
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        string cmdText = string.Format("DELETE FROM Employee WHERE Id = '{0}'", id);

                        command.CommandText = cmdText;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                Console.WriteLine("Cотрудник c Id {0} удалён из базы.", id);
                ret = true;
            }
            catch(System.Exception ex)
            {
                Console.WriteLine("Ошибка при удалении записи из базы. {0}", ex);
            }
            return ret;
        }

        public static bool SetEmployeeFields(Employee empl)
        {
            bool ret = false;
            try
            {
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        string cmdText = string.Format("UPDATE 'Employee' SET " +
                            "Name = '{0}', Surname = '{1}', Phone = '{2}', CompanyId = {3}, Type = '{4}', Number = '{5}'" +
                            " WHERE Id = {6}",
                            empl.Name, empl.Surname, empl.Phone, empl.CompanyId, empl.Passport.Type, empl.Passport.Number, empl.Id);
                        command.CommandText = cmdText;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                Console.WriteLine("Сотрудник с Id = {0} изменён в базе.", empl.Id);
                ret = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при модификации данных в базе. {0}", ex);
            }
            return ret;
        }

        public static void AddEmployeeTest()
        {
            try
            {
                using (var connection = new SQLiteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "INSERT INTO 'Employee' ('Id', 'Name', 'Surname', 'Phone', 'CompanyId','Type','Number') " +
                            "VALUES (1,'Alex','Fedotov','8-951-358-60-12','1','1','123456789')";
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                Console.WriteLine("Тестовый сотрудник добавлен в базу.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении записи в базу. {0}", ex);
            }
        }

        public static void DeleteEmployeeTest()
        {
            using (var connection = new SQLiteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DELETE FROM Employee WHERE Name = 'Alex'";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            Console.WriteLine("Тестовый сотрудник удалён из базы.");
        }

        public static bool CreateDataBase()
        {
            var ret = false;
            try
            {
                if (CheckDataBaseExist())
                {
                    return ret;
                }

                CreateDB();
                ret = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при создании базы данных! {0}", ex);

                File.Delete(_nameDB);
            }
            return ret;
        }

        private static void CreateDB()
        {
            try
            {
                using (var connection = new SQLiteConnection())
                {
                    Console.WriteLine("Cоздание БД...");

                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "CREATE TABLE IF NOT EXISTS Employee (Id INTEGER PRIMARY KEY," +
                            "Name TEXT NOT NULL, " +
                            "Surname TEXT NOT NULL," +
                            "Phone TEXT NOT NULL," +
                            "CompanyId INTEGER NOT NULL," +
                            "Type TEXT NOT NULL," +
                            "Number TEXT NOT NULL)";
                        command.ExecuteNonQuery();
                        
                        Console.WriteLine("БД успешно создана.");
                    }

                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Ошибка при создании БД");
                throw ex;
            }
        }

        private static bool CheckDataBaseExist()
        {
            if (File.Exists(_nameDB))
            {
                Console.WriteLine("БД {0} найдена. ", _nameDB);
                return true;
            }
            else
            {
                Console.WriteLine("БД {0} не найдена.", _nameDB);
                return false;
            }
        }
    }
}
