using Joselito_Technocell.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Joselito_Technocell.Helpers
{
    public class UserHelper : IDisposable
    {
        private static Joselito_TechnocellDbContext userContext = new Joselito_TechnocellDbContext();
        private static Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        public static User User(string userName)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            return userManager.FindByName(userName);
        }

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Check to see if Role Exists, if not create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }
        public static List<IdentityRole> getRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));
            var model = new List<IdentityRole>();
            model = roleManager.Roles.ToList();
            return model;
        }
        public static void addRol(string roleNeme, string email)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));
            var userASP = userManager.FindByName(email);
            var rol = roleManager.FindByName(roleNeme);
            bool a = true;
            if (roleManager.RoleExists(roleNeme))
            {
                var roles = userASP.Roles.ToArray();
                foreach (var item in roles)
                {
                    if (item.RoleId == rol.Id)
                    {
                        a = false;
                    }
                }
                if (a)
                {
                    userManager.AddToRole(userASP.Id, roleNeme);
                }
            }

        } 
        public static void removeRol(string roleNeme, string email)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));
            var userASP = userManager.FindByName(email);
            var rol = roleManager.FindByName(roleNeme);
            bool a = false;
            if (roleManager.RoleExists(roleNeme))
            {
                var roles = userASP.Roles.ToArray();
                foreach (var item in roles)
                {
                    if (item.RoleId == rol.Id)
                    {
                        a = true;
                    }
                }
                if (a)
                {
                    userManager.RemoveFromRole(userASP.Id, roleNeme);
                }
            }

        }
        public static void CheckSuperUser()
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CreateUserASP(email, "AD-ROOT", password);
                return;
            }

            userManager.AddToRole(userASP.Id, "AD-ROOT");
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));

            var userASP = new User
            {
                Email = email,
                UserName = email,
                FirstName = "Elvin",
                LastName = "Apellido",
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(User userASP, string password)
        {

            userASP.UserName = userASP.Email;
            var userManager = new UserManager<User>(new UserStore<User>(userContext));

            userManager.Create(userASP, password);
        }

        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            var user = db.Users.Where(tp => tp.UserName == email).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            var random = new Random();
            var newPassword = string.Format("{0}{1}{2:04}*",
                user.FirstName.Trim().ToUpper().Substring(0, 1),
                user.LastName.Trim().ToLower(),
                random.Next(10000));

            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Taxes Password Recovery";
            var body = string.Format(@"
                <h1>Taxes Password Recovery</h1>
                <p>Yor new password is: <strong>{0}</strong></p>
                <p>Please change it for one, that you remember easyly",
                newPassword);

            await MailHelper.SendMail(email, subject, body);
        }

        public static bool UpdateUserName(string currentUserName, string newUserName)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var userASP = userManager.FindByEmail(currentUserName);
            if (userASP == null)
            {
                return false;
            }
            userASP.UserName = newUserName;
            userASP.Email = newUserName;
            var response = userManager.Update(userASP);
            return response.Succeeded;
        }

        public static bool DeleteUser(string userName)
        {
            var userManager = new UserManager<User>(new UserStore<User>(userContext));
            var userASP = userManager.FindByEmail(userName);
            if (userASP == null)
            {
                return false;
            }
            var response = userManager.Delete(userASP);
            return response.Succeeded;
        }

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }

}
