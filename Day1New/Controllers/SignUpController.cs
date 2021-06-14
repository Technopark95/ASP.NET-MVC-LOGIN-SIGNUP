using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day1New.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Day1New.Controllers
{
    public class SignUpController : Controller
    {

       static public int InsertUser(string query)
        {
           
            MySqlConnection connection = new MySqlConnection(ConstantModel.ConnectionString);
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = connection;
            connection.Open();
            int result = command.ExecuteNonQuery();
            Console.WriteLine(result);
            connection.Close();
            return result;


        }

        public ActionResult Index()
        {
            return View();
        }
        public int PushUser(IFormCollection MyForm)
        {
            SignUpModel Sign = new SignUpModel();

            Sign.FirstName = MyForm["firstname"];
            Sign.LastName = MyForm["lastname"];
            Sign.Email = MyForm["email"];
            Sign.Password = MyForm["password"];

            string query = $@"insert into users values(""{Sign.FirstName}"",""{Sign.LastName}"",""{Sign.Email}"", MD5(""{Sign.Password}""))";

            return InsertUser(query);

        }

        [HttpPost]
        public async Task<int> CheckValidEmail()
        {

            StreamReader y = new StreamReader(Request.Body);
           string body = await y.ReadToEndAsync();
            string query = $@"SELECT email FROM users where email= ""{body}""";
            MySqlConnection connection = new MySqlConnection(ConstantModel.ConnectionString);
            Console.WriteLine(query);
            MySqlCommand command = new MySqlCommand(query);
            command.Connection = connection;
            connection.Open();
            MySqlDataReader sdr = command.ExecuteReader();
            int rowsfetched = 0;

            while (sdr.Read())
            {
                ++rowsfetched;
            }

            connection.Close();
            Console.WriteLine(rowsfetched);
            return rowsfetched;


        }

    }
}
