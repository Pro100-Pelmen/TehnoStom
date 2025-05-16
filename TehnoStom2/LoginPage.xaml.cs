using Microsoft.Extensions.Logging.Abstractions;
using TehnoStom2.DbContexts;
using TehnoStom2.Entities;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace TehnoStom2
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            AppDbContext dbContext = new AppDbContext();

            var adminExists = dbContext.Users.FirstOrDefault(x => x.Login == "Radmir1");
            if (adminExists == null)
            {
                dbContext.Roles.Add(new Role { Title = "Администратор" });
                dbContext.Roles.Add(new Role { Title = "Сотрудник" });
                dbContext.Specializations.Add(new Specialization { Title = "Администратор" });
                dbContext.Users.Add(new User { FirstName = "Радмир", MiddleName = "Рустемович", LastName = "Гайнутдинов", Login = "Radmir1", Password = "123", Phone = "89397257945", RoleId = 1, SpecializationId = 1 });
                dbContext.SaveChanges();
            }
        }

        private void LoginUser(object sender, EventArgs e)
        {
            var login = LoginEntry.Text;
            var password = PasswordEntry.Text;
 

            AppDbContext dbContext = new AppDbContext();

            var user = dbContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user == null)
            {
                AppShell.Current.DisplayAlert("Ошибка", "Пользователь или пароль неверны", "OK");
                return;
            }

            if (user.RoleId == 1)
            {
                var addOrderPage = new AddOrderPage();
                Navigation.PushAsync(addOrderPage);
                return;
            }

            var employeerMainPage = new EmployeerMainPage(user);
            Navigation.PushAsync(employeerMainPage);
        }

    }

}
