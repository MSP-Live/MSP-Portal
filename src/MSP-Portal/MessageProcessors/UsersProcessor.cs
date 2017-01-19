using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }

        public string ChangePassword(int id, string[] args)
        {
            DBContext.PortalContext db = new DBContext.PortalContext();
            Entities.User user = db.UsersCollection.First(x=> x.Id == id);
            if (user != null)
            {
                if (PasswordChecker(args[0]))
                {
                    user.Password = args[0];
                    return string.Empty;
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

        private bool PasswordChecker(string password)
        {
            System.Text.RegularExpressions.Regex checker = new System.Text.RegularExpressions.Regex("");
            return checker.IsMatch(password);
        }
    }
}
