using Day1New.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Day1New.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index() {

            if (HttpContext.Session.GetString("token") !=null)
            {
               return RedirectToAction("Index", "Dash");
            }

            else
            {
                HttpContext.Session.Clear();
                TempData.Clear();
                return View("Index1");
            }

        }
       

        public Dictionary<string,string> Auth(IFormCollection form)
        {


            List<temployModel> customers = new List<temployModel>();
            MySqlConnection connection = new MySqlConnection(ConstantModel.ConnectionString);

            string query = $"SELECT firstname, lastname, email, password FROM users where email=\"{form["email"]}\"";
            Console.WriteLine(query);
            MySqlCommand command = new MySqlCommand(query);

            command.Connection = connection;
            connection.Open();
            MySqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                temployModel Data = new temployModel();
                Data.FirstName = sdr["firstname"].ToString();
                Data.Lastname = sdr["lastname"].ToString();
                Data.Email = sdr["email"].ToString();
                Data.Password = sdr["password"].ToString();
                customers.Add(Data);
            }
            connection.Close();

            Dictionary<string, string> response = new Dictionary<string, string>();

            MD5Controller md5 = new MD5Controller();

            if (customers.Count == 0 || String.Compare( md5.GetMD5SumHash(form["password"]) , customers[0].Password , true) != 0 )
            {
                response["status"] = "-1";
                return response;
            }

            else
            {
                response["status"] = "0";

                //TempData["token"] = customers[0].Email;
                HttpContext.Session.SetString("fname", customers[0].FirstName);
                HttpContext.Session.SetString("lname", customers[0].Lastname);
                HttpContext.Session.SetString("token", customers[0].Email);

                return response;


            }


        }

        public string jj()
        {
            return " ";

        }


    }
}
