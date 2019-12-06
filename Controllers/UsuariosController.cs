using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using TestePraticoWebApp.Models;

namespace TestePraticoWebApp.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Novo");
        }

        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Novo(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.UploadData("https://usuarios-3fb66.firebaseio.com/usuarios/usuarios.json?auth=zpzzeAOUCEyerOZ9gkjOe6lPHLtP2qfyHCoZLbxU",
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(usuario)));
                    }
                    TempData["MessageSuccess"] = "Registro salvo na base de dados!";
                    return RedirectToAction("Novo");
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = ex.Message;
                }
            }
            return View(usuario);
        }

        public JsonResult ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return Json(false);
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return Json(cpf.EndsWith(digito));
        }
    }
}