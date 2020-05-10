using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

    public class DB_MySQL
    {
        String connString;
        string host;
        int port;
        string database;
        string username;
        string password;

        //Объект для подключения к БД

        MySqlConnection obj;
        //Объект для выполнения SQL-запроса
        MySqlCommand objComand;

        /*
         * openSessionMySQL - открываем соединение с бд
         * statusOpenSession - проверка подключения к бд
         * CreatingNewRowTimetable - отдельный метод для внесения данных в таблицу с расписанием
         * DeletRow_id - метод удаляющий данные в любой таблице по id
         * DeletRowTimeT_FlightNumber - удаляет запись из таблицы с расписанием по номеру рейса
         * getTimeTimetable - получить расписание. НЕ ДОДЕЛАЛ!!!
         */

        public DB_MySQL(string Host, int Port, string Database, string Username, string Password)
        {
            host = Host;
            port = Port;
            database = Database;
            username = Username;
            password = Password;
        }

        public void openSessionMySQL()
        {
            connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;
            obj = new MySqlConnection(connString);
            try
            {
                Console.WriteLine("Try open session");
                obj.Open();
            }
            catch
            {
                Console.WriteLine("Error. Session not open!");
            }
        }

        public void closeSessionMySQL()
        {
                obj.Close();
        }

    public bool statusOpenSession()
        {
           return obj.Ping();
        }

        public void CreatingNewRowTimetable(string FlightDate, string ScheduledTime, string CodeA, int AirlineCode, string FlightNumber, string FlagArrivalDeparture, string TypeAircraft, string AParking, string ParkingSector, string NameAirline)
        {
            try
            {
                string sql = "INSERT INTO `Timetable`  (`FlightDate`, `ScheduledTime`, `CodeA`,  `AirlineCode`, `FlightNumber`, `FlagArrivalDeparture`, `TypeAircraft`, `AParking`,`ParkingSector`,`NameAirline`) VALUES ('" + FlightDate + "', '" + ScheduledTime + "', '" + CodeA + "','" + AirlineCode + "', '" + FlightNumber + "', '" + FlagArrivalDeparture + "', '" + TypeAircraft + "', '" + AParking + "', '" + ParkingSector + "', '" + NameAirline + "')";
                objComand = new MySqlCommand(sql, obj);
                objComand.ExecuteScalar();

                Console.WriteLine("Create new note");
            }
            catch
            {
                Console.WriteLine("Error. The add request was not executed");
            }
        }

        public void DeletRow_id(int id, string NameTable)
        {
            try
            {
                string sql = "DELETE FROM `" + NameTable + "` WHERE id =" + id;
                objComand = new MySqlCommand(sql, obj);
                objComand.ExecuteScalar();

                Console.WriteLine("Note is delete");
            }
            catch
            {
                Console.WriteLine("Error. The add request was not executed");
            }
        }

    public void clearTableDB(string NameTable)
    {
         try
         {
            string sql = "TRUNCATE TABLE `" + NameTable + "`";
            objComand = new MySqlCommand(sql, obj);
            objComand.ExecuteScalar();
         }
         catch
         {

         }
    }

    public void DeletRowTimeT_FlightNumber(int FlightNumber)
    {
       try
       {
          string sql = "DELETE FROM `Timetable` WHERE FlightNumber =" + FlightNumber;
          objComand = new MySqlCommand(sql, obj);
          objComand.ExecuteScalar();

          Console.WriteLine("Note is delete");
       }
       catch
       {
          Console.WriteLine("Error. The add request was not executed");
       }

    }

    public string[] getTimeTimetable(int id)
    {
        string[] mass = new string[10];

        string sql = "SELECT FlightDate, ScheduledTime, TypeAircraft, ct.Fuselage FROM `Timetable` as cn" +
" LEFT JOIN `Type_Plane` as ct on ct.SocrT=cn.TypeAircraft" +
            "   WHERE cn.id =" + id;
        objComand = new MySqlCommand(sql, obj);

        var readerP1 = objComand.ExecuteReader();

        while (readerP1.Read())
        {
           mass[0] = readerP1[0].ToString();
           mass[1] = readerP1[1].ToString();
           mass[2] = readerP1[2].ToString();
           mass[3] = readerP1[3].ToString();
        }
        readerP1.Close();
        
        return mass;
    }

    public string getKolodki(string id)
    {
        string mass="";

        string sql = "SELECT TypeOfKolodka FROM `Kolodki`  WHERE TypeOfPlane = 737" ;
        objComand = new MySqlCommand(sql, obj);

        var readerP1 = objComand.ExecuteReader();

        while (readerP1.Read())
        {
            mass = readerP1[0].ToString();
        }
        readerP1.Close();

        return mass;
    }

}
