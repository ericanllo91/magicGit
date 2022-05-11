using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace mtg_app.Controllers
{
    public class UserController : Controller
    {
        CardService serviceCard = new CardService();
        static string connection = "Server=LAPTOP-ERIC\\SQLEXPRESS;Database=Test;Trusted_Connection=True";

        public IActionResult SaveName(string name)
        {
        //HttpContext.Session.SetString("Name", name);
        return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            //Set value in Session object.
            //HttpContext.Session.SetString("Usuario", "Eric");
            return View();
        }


         public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserViewModel oUsuario)
        {
            
            bool registrado;
            string mensaje;

            if (oUsuario.Password != oUsuario.Confirm)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(connection))
            {
                //regristrar mal escrito
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Username);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Password);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "User");
            }
            else {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(UserViewModel oUsuario)
        {
            //oUsuario.Password = ConvertirSha256(oUsuario.Password);


            using (SqlConnection cn = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Username);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if (oUsuario.Id != 0)
            {
                //string name = HttpContext.Session.GetString("Name");
                
                //oUsuario.Username = name;

                //Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }

           
        }



        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    
    }
}