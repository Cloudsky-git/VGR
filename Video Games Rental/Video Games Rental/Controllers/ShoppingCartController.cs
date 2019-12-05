using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Video_Games_Rental.Models;

namespace Video_Games_Rental.Controllers
{
    public class ShoppingCartController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.games.Find(id),1)
                };

                Session[strCart] = lsCart;
            }

            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = isExistingCheck(id);
                if (check == -1)
                    lsCart.Add(new Cart(db.games.Find(id), 1));
                else
                    lsCart[check].Quantity++;
                Session[strCart] = lsCart;
            }
            return RedirectToAction("Index");

        }

        private int isExistingCheck(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i=0; i<lsCart.Count; i++)
            {
                if (lsCart[i].Product.game_id == id) return i;
            }
            return -1;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = isExistingCheck(id);
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("Index");
        }

        public ActionResult CheckOut(FormCollection frc)
        {           
            return View("CheckOut");
        }

        public ActionResult ProcessOrder(FormCollection frc)
        {
            //List<Cart> lsCart = (List<Cart>)Session[strCart];
            //user_details user = new user_details()
            //{
            //    name = frc["cusName"],
            //    surname = frc["cusSurname"],
            //    address_line1 = frc["cusAddress1"],
            //    address_line2 = frc["cusAddress2"],
            //    postal_code = frc["cusPostal"],

            //};

            //db.user_details.Add(user);
            //db.SaveChanges();

            //AspNetUser userADD = new AspNetUser()
            //{             
            //    Email = frc["cusMail"],
            //    PhoneNumber = frc["cusPhone"],
            //};

            //db.AspNetUsers.Add(userADD);
            //db.SaveChanges();


            //foreach (Cart cart in lsCart)
            //{
            //    order order = new order()
            //    {

            //    };
            //}

            return View("OrderSuccess");
        }
    }
}