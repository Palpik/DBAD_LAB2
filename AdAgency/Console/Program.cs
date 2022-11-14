using System;
using System.Linq;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Domain;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(@"D:\ВУЗ\5 сем Разработка Приложение баз данных для ИС\lab2\AdAgency\Console\appsettings.json");
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AdAgencyContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            using (AdAgencyContext db = new AdAgencyContext(options))
            {
                bool flag = true;
                while(flag)
                {
                    Console.Write("Номер задания > ");
                    string c = Console.ReadLine();
                    switch(c)
                    {
                        case "1": Task1(db);
                        break;
                        case "2": Task2(db);
                        break;
                        case "3": Task3(db);
                        break;
                        case "4": Task4(db);
                        break;
                        case "5": Task5(db);
                        break;
                        case "6": Task6(db);
                        break;
                        case "7": Task7(db);
                        break;
                        case "8": Task8(db);
                        break;
                        case "9": Task9(db);
                        break;
                        case "10": Task10(db);
                        break;
                        case "exit": flag = false;
                        break;
                        default: break;
                    }
                }
            }
            Console.ReadKey();
        }
        
        public static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }

        public static void Task1(AdAgencyContext db)
        {
            var query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            string comment = "1. Результат выполнения запроса на выборку записей из одной таблицы на стороне отношения один: \r\n";
            Print(comment, query);
        }

        public static void Task2(AdAgencyContext db)
        {
            var query = db.AdTypes.Where(x => x.Id % 2 == 1).Select(x => new { Id = x.Id, Название = x.Name, Описание = x.Description});
            string comment = "2. Результат выполнения запроса на выборку записей из таблицы AdTypes, удовлетворяющих заданному условию \"id % 2 = 1\" : \r\n";
            Print(comment, query);
        }
        public static void Task3(AdAgencyContext db)
        {
            var query = db.AdPlaces
            .GroupBy(gr => new {gr.TypeId})
            .Select(grouped => new { TypeId = grouped.Key.TypeId, AverageCost = (double)grouped.Average(g => g.Cost) } )
            .OrderBy(x => x.TypeId);
            string comment = "3. Результат выполнение запроса на группировку и выборку записей из таблицы AdPlaces, с выводом средней стоимости : \r\n";
            Print(comment, query);
        }

        public static void Task4(AdAgencyContext db)
        {
            var query = from o in db.Orders
            join p in db.AdPlaces on o.PlaceId equals p.Id
            select new {OrderId = o.Id, Place = p.Place};
            string comment = "4. Результат выполнения запроса на выборку записей из двух таблиц: \r\n";
            Print(comment, query.Take(2000));
        }
        public static void Task5(AdAgencyContext db)
        {
            var avg = db.AdPlaces.Average(x => x.Cost);
            var query = from o in db.Orders
            join p in db.AdPlaces on o.PlaceId equals p.Id
            where p.Cost < avg
            select new {OrderId = o.Id, Place = p.Place};
            string comment = "5. Результат выполнения запроса на выборку записей из двух таблиц с ограничением: \r\n";
            Print(comment, query.Take(2000));
        }

        public static void Task6(AdAgencyContext db)
        {
            var query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            string comment = "6. Результат выполнения запроса на выборку записей из таблицы AdTypes до вставки записи: \r\n";
            Print(comment,query);
            Console.Write("Type = ");
            string type = Console.ReadLine();
            Console.Write("Description = ");
            string desc = Console.ReadLine();
            AdType at = new AdType
            {
                Name = type,
                Description = desc
            };
            db.AdTypes.Add(at);
            db.SaveChanges();
            query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            comment = "6. Результат выполнения запроса на выборку записей из таблицы AdTypes после вставки записи: \r\n";
            Print(comment,query);
        }

        public static void Task7(AdAgencyContext db)
        {
            string comment = "7. Выборка последних 200 записей из таблицы AdPlaces до втсавки записи: \r\n";
            var query = from t in db.AdPlaces
                        select new { Id = t.Id, Place = t.Place, Description = t.Description, Cost = t.Cost };
            Print(comment, query.OrderByDescending(x => x.Id).Take(200));
            Console.Write("Place = ");
            string place = Console.ReadLine();
            Console.Write("Description = ");
            string desc = Console.ReadLine();
            Console.Write("Cost = ");
            decimal cost = Convert.ToDecimal(Console.ReadLine());
            int typeId = db.AdTypes.OrderByDescending(x => x.Id).First().Id;
            AdPlace ap = new AdPlace
            {
                Place = place,
                Description = desc,
                Cost = cost,
                TypeId = typeId
            };
            db.AdPlaces.Add(ap);
            db.SaveChanges();
            comment = "7. Выборка последних 200 записей из таблицы AdPlaces после втсавки записи: \r\n";
            query = from t in db.AdPlaces
                        select new { Id = t.Id, Place = t.Place, Description = t.Description, Cost = t.Cost };
            Print(comment, query.OrderByDescending(x => x.Id).Take(200));
        }

        public static void Task8(AdAgencyContext db)
        {
            var query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            string comment = "8. Выборка записей из таблицы AdTypes до удаления записей: \r\n\r\n\r\n";
            Print(comment, query);
            var lastType = db.AdTypes.OrderByDescending(x => x.Id).First();
            var places = db.AdPlaces.Include(x => x.Type).Where(x => x.Type.Name == lastType.Name);
            db.AdPlaces.RemoveRange(places);
            db.AdTypes.Remove(lastType);
            db.SaveChanges();
            comment = "8. Выборка всех записей из таблицы AdTypes после удаления записи: \r\n\r\n\r\n";
            query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            Print(comment, query);
        }
        public static void Task9(AdAgencyContext db)
        {
            string comment = "9. Выборка Последних 200 записей из таблицы AdPlaces после удаления записи: \r\n\r\n\r\n";
            var query = from t in db.AdPlaces
                        select new { Id = t.Id, Place = t.Place, Description = t.Description, Cost = t.Cost };
            Print(comment, query.OrderByDescending(x => x.Id).Take(200));
            var lastPlace = db.AdPlaces.OrderByDescending(x => x.Id).First();
            var orders = db.Orders.Include(x => x.Place).Where(x => x.Place.Place == lastPlace.Place);
            db.Orders.RemoveRange(orders);
            db.AdPlaces.Remove(lastPlace);
            db.SaveChanges();
            comment = "9. Выборка Последних 200 записей из таблицы AdPlaces после удаления записи: \r\n\r\n\r\n";
            query = from t in db.AdPlaces
                        select new { Id = t.Id, Place = t.Place, Description = t.Description, Cost = t.Cost };
            Print(comment, query.OrderByDescending(x => x.Id).Take(200));
        }

        public static void Task10(AdAgencyContext db)
        {
            var query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            string comment = "10. Выборка записей из таблицы AdTypes до обновления записей: \r\n\r\n\r\n";
            Print(comment,query);
            var types = db.AdTypes.Where(x => x.Id % 2 == 0);
            foreach(var myt in types)
            {
                myt.Description = "запись с четным Id";
            }
            db.SaveChanges();
            comment = "10. Выборка записей из таблицы AdTypes после изменения записей: \r\n\r\n\r\n";
            query = from t in db.AdTypes
                        select new { Id = t.Id, Название = t.Name, Описание = t.Description };
            Print(comment, query);
        }
    }
    
}