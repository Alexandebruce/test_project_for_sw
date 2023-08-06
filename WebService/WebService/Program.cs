using System;
using System.Collections.Generic;

namespace WebService
{
    class Program
    {
        static void Main(string[] args)
        {
            //инициализация сервера
            string str = "http://+:8080/";
            var pre = new List<string>();
            pre.Add(str);
            RequestListener _requestListener = new RequestListener(pre);

            //инициализация базы
            DBHelper.NameDB = "testbase.sqlite";
            DBHelper.CreateDataBase();


            try { 
                _requestListener.Listen();//запуск сервера 
            }
            catch(System.Exception ex)
            {
                Console.WriteLine("Ошибка при старте web-сервера. {0}", ex);
                Console.ReadKey(true);
            }            
        }
    }
}
