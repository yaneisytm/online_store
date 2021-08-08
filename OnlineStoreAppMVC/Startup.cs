using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineStoreCORE;
using OnlineStoreDAL;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineStoreAppMVC.Startup))]
namespace OnlineStoreAppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<RolesApp>(new RoleStore<RolesApp>(context));

         
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new RolesApp();
                role.Name = "Admin";
                roleManager.Create(role);

            }
            var user = new ApplicationUser
            {

                UserName = "Admin",
                Email = "admin@mail.com",
                Surname = "Admin",
                PasswordHash = "adminpass",
            };


            var chkUser = UserManager.Create(user);

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");

            }
          

        }
    }
}
