using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSP_Portal.Controllers;
using MSP_Portal.MessageProcessors;

namespace MSP_Portal.MessageProcessors
{
    public class UsersProcessor : MessagesController.MessageProcessor
    {
        public UsersProcessor()
        {
            Processors.Add("changepassword",
                new MessagesController.MessageProcess(ChangePassword));
            Processors.Add("changephonenumber",
                new MessagesController.MessageProcess(ChangePhoneNumber));
            Processors.Add("changecity",
                new MessagesController.MessageProcess(ChangeCity));
        }

        public string ChangePassword(int id, string[] args)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.User user = db.UsersCollection.First(x => x.Id == id);
                if (user != null)
                {
                    if (PasswordChecker(args[0]))
                    {
                        user.Password = args[0];
                        db.SaveChanges();
                        return "Password successful changed.";
                    }
                    else
                    {
                        throw new ArgumentException("Not correct password.");
                    }
                }
                else
                {
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        private bool PasswordChecker(string password)
        {
            System.Text.RegularExpressions.Regex checker = new System.Text.RegularExpressions.Regex("");
            return checker.IsMatch(password);
        }

        public string ChangePhoneNumber(int id, string[] args)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.User user = db.UsersCollection.First(x => x.Id == id);
                if (user != null)
                {
                    if (PhoneNumberChecker(args[0]))
                    {
                        user.PhoneNumber = args[0];
                        db.SaveChanges();
                        return "Phone number successful changed.";
                    }
                    else
                    {
                        throw new ArgumentException("Not correct phone number.");
                    }
                }
                else
                {
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        private bool PhoneNumberChecker(string phone)
        {
            System.Text.RegularExpressions.Regex checker = new System.Text.RegularExpressions.Regex("");
            return checker.IsMatch(phone);
        }

        public string ChangeCity(int id, string[] args)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.User user = db.UsersCollection.First(x => x.Id == id);
                if (user != null)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (string buffer in args)
                    {
                        builder.Append(buffer).Append(' ');
                    }
                    builder.Remove(builder.Length - 1, 1);
                    user.City = GetCityId(builder.ToString());
                    db.SaveChanges();
                    return "City successful changed.";
                }
                else
                {
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        private int GetCityId(string cityName)
        {
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                Entities.City city = db.CitiesCollection.FirstOrDefault(x => x.Name == cityName);
                if (city == null)
                {
                    city = new Entities.City() { Name = cityName };
                    db.CitiesCollection.Add(city);
                    db.SaveChanges();
                }
                return city.Id;
            }
        }
    }
}
