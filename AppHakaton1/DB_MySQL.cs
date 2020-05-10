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

        public void CreatingNewRowTimetable(string FlightDate, TimeSpan ScheduledTime, string CodeA, int AirlineCode, int FlightNumber, string FlagArrivalDeparture, string TypeAircraft, string AParking, string ParkingSector, string NameAirline)
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

        public void getTimeTimetable(int FlightNumber)
        {
            var result = new List<string>();
            try
            {
                string sql = "SELECT `AirlineCode` FROM `Timetable` WHERE FlightNumber =" + FlightNumber;
                objComand = new MySqlCommand(sql, obj);

                //Console.WriteLine(objComand.ExecuteScalar().ToString());
                var reader = objComand.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                reader.Close();

                string[] a = result.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. The add request was not executed = " + ex.Message);
            }
        }
    }
