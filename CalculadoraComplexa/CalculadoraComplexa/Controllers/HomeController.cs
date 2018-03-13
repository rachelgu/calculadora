using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraComplexa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet] // esta anotação é facultativa, pois, por defeito 
        public ActionResult Index()
        {
            limpaCampos();
            ViewBag.Visor = "0";
            return View();
        }
        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor){

            // identificar o valor da variavel 'bt'
            string ausar = "";
            switch (bt)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if ((bool)Session["depoisOp"]) limpaCampos();

                    ausar = "num1";
                    if (!Session["operacao"].Equals("")) { ausar = "num2"; }
                    if (Session[ausar].Equals("0")) Session[ausar] = bt;
                    else Session[ausar] += bt;
                    break;
                case "+":
                case "-":
                case "x":
                case "/":
                    Session["depoisOp"] = false;
                    Session["operacao"] = bt;
                    break;
                case "=":
                    if (Session["num2"].Equals("0") || Session["operacao"].Equals("")) break;
                    // escolher operação a fazer com o operador anterior
                    switch(Session["operacao"])
                    {
                      
                        case "+":
                            Session["num1"] = (Double.Parse((string)Session["num1"]) + Double.Parse((string)Session["num2"])).ToString();
                            break;
                        case "-":
                            Session["num1"] = (Double.Parse((string)Session["num1"]) - Double.Parse((string)Session["num2"])).ToString();
                            break;
                        case "*":
                            Session["num1"] = (Double.Parse((string)Session["num1"]) * Double.Parse((string)Session["num2"])).ToString();
                            break;
                        case "/":
                            Session["num1"] = (Double.Parse((string)Session["num1"]) / Double.Parse((string)Session["num2"])).ToString();
                            break;
                    }

                    Session["num2"] = "0";
                    Session["operacao"] = "";
                    //para concluir a operação
                    Session["depoisOp"] = true;
                    break;
                case ",":
                    if ((bool)Session["depoisOp"]) limpaCampos();
                    string a = Convert.ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    ausar = "num1";
                    if (!Session["operacao"].Equals("")) { ausar = "num2"; }
                    if(!((string)Session[ausar]).Contains(a))Session[ausar] += a;
                    break;
                case "+/-":
                    ausar = "num1";
                    if (!Session["operacao"].Equals("")) { ausar = "num2"; }
                    Session[ausar] = (Double.Parse((string)Session[ausar]) * -1).ToString();
                    break;
                case "c":
                    limpaCampos();
                    break;
            }

            if (Session["operacao"].Equals("")) ViewBag.visor = Session["num1"];
            else ViewBag.visor = Session["num2"];
            return View();
        }

        private void limpaCampos()
        {
            Session["num1"] = "0";
            Session["num2"] = "0";
            Session["operacao"] = "";
            Session["depoisOp"] = false;
        }
    }

}