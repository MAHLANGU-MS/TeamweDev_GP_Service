using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TeamweDev_GP_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public User Login(string email, string password)
        {
            var sysuser = (from s in db.Users
                           where s.Email.Equals(email) &&
                           s.Password.Equals(password)
                           select s).FirstOrDefault();

            if (sysuser != null)
            {
                var tempUser = new User
                {
                    Id = sysuser.Id,
                    Email = sysuser.Email,
                    UserType = sysuser.UserType,
                };
                return tempUser;
            }
            else
            {
                return null;
            }
        }

        public int register(User user)
        {
            var sysUser = (from s in db.Users
                           where s.Email.Equals(user.Email) &&
                           s.Password.Equals(user.Password)
                           select s).FirstOrDefault();

            if (sysUser == null)
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = user.Password,
                    UserType = user.UserType
                };

                db.Users.InsertOnSubmit(newUser);
                try
                {
                    db.SubmitChanges();
                    //success
                    return 1;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    //error
                    return -1;
                }
            }
            else
            {
                //user exists
                return 0;
            }
        }

        public List<Product> getAllProducts()
        {
            var Prods = new List<Product>();

            var Prod = (from p in db.Products
                        where p.PrQuantity > 0
                        select p).DefaultIfEmpty();

            foreach (Product p in Prod)
            {
                var product = new Product
                {
                    Id = p.Id,
                    PrName = p.PrName,
                    PrCategory = p.PrCategory,
                    PrRating = p.PrRating,
                    PrPrice = p.PrPrice,
                    PrOldPrice = p.PrOldPrice,
                    PrImage = p.PrImage
                };

                Prods.Add(product);
            }

            return Prods;
        }

        public Product getProduct(int ID)
        {
            var Prod = (from p in db.Products
                        where p.Id.Equals(ID)
                        select p).FirstOrDefault();

            if (Prod != null)
            {
                var tempProd = new Product
                {
                    Id = Prod.Id,
                    PrCategory = Prod.PrCategory,
                    PrDescription = Prod.PrDescription,
                    PrImage = Prod.PrImage,
                    PrName = Prod.PrName,
                    PrOldPrice = Prod.PrOldPrice,
                    PrPrice = Prod.PrPrice,
                    PrQuantity = Prod.PrQuantity,
                    PrRating = Prod.PrRating,
                };
                return tempProd;
            }
            else
            {
                return null;
            }
        }

        public List<Product> getAllProductsAlphabetically()
        {
            var Prods = new List<Product>();

            var Prod = (from p in db.Products
                        where p.PrQuantity > 0 
                        orderby p.PrName ascending
                        select p).DefaultIfEmpty();

            foreach (Product p in Prod)
            {
                var product = new Product
                {
                    Id = p.Id,
                    PrName = p.PrName,
                    PrCategory = p.PrCategory,
                    PrRating = p.PrRating,
                    PrPrice = p.PrPrice,
                    PrOldPrice = p.PrOldPrice,
                    PrImage = p.PrImage
                };

                Prods.Add(product);
            }

            return Prods;
        }

        public List<Product> getAllProductsByPrice()
        {
            var Prods = new List<Product>();

            var Prod = (from p in db.Products
                        where p.PrQuantity > 0
                        orderby p.PrPrice ascending
                        select p).DefaultIfEmpty();

            foreach (Product p in Prod)
            {
                var product = new Product
                {
                    Id = p.Id,
                    PrName = p.PrName,
                    PrCategory = p.PrCategory,
                    PrRating = p.PrRating,
                    PrPrice = p.PrPrice,
                    PrOldPrice = p.PrOldPrice,
                    PrImage = p.PrImage
                };

                Prods.Add(product);
            }

            return Prods;
        }

        public List<Product> getAllProductsPriceFiltered(int min, int max)
        {
            var Prods = new List<Product>();

            var Prod = (from p in db.Products
                        where p.PrQuantity > 0 &&
                        p.PrPrice > min &&
                        p.PrPrice < max
                        orderby p.PrPrice ascending
                        select p).DefaultIfEmpty();

            foreach (Product p in Prod)
            {
                var product = new Product
                {
                    Id = p.Id,
                    PrName = p.PrName,
                    PrCategory = p.PrCategory,
                    PrRating = p.PrRating,
                    PrPrice = p.PrPrice,
                    PrOldPrice = p.PrOldPrice,
                    PrImage = p.PrImage
                };

                Prods.Add(product);
            }

            return Prods;
        }

        public List<Product> getAllProductsCategoryFiltered(char aORnORe)
        {
            var Prods = new List<Product>();

            var Prod = (from p in db.Products
                        where p.PrQuantity < 0
                        select p).DefaultIfEmpty();

            if (aORnORe.Equals('a') || aORnORe.Equals('A'))
            {
                Prod = (from p in db.Products
                            where p.PrQuantity > 0 &&
                            p.PrCategory.Equals("audio")
                            orderby p.PrPrice ascending
                            select p).DefaultIfEmpty();
            }
            else if (aORnORe.Equals('e') || aORnORe.Equals('E'))
            {
                Prod = (from p in db.Products
                             where p.PrQuantity > 0 &&
                             p.PrCategory.Equals("ebook")
                             orderby p.PrPrice ascending
                             select p).DefaultIfEmpty();
            }
            else if (aORnORe.Equals('n') || aORnORe.Equals('N'))
            {
                Prod = (from p in db.Products
                             where p.PrQuantity > 0 &&
                             p.PrCategory.Equals("novel")
                             orderby p.PrPrice ascending
                             select p).DefaultIfEmpty();
            }

            

            foreach (Product p in Prod)
            {
                var product = new Product
                {
                    Id = p.Id,
                    PrName = p.PrName,
                    PrCategory = p.PrCategory,
                    PrRating = p.PrRating,
                    PrPrice = p.PrPrice,
                    PrOldPrice = p.PrOldPrice,
                    PrImage = p.PrImage
                };

                Prods.Add(product);
            }

            return Prods;
        }
    }
}
