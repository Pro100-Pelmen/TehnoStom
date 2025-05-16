using TehnoStom2.DbContexts;
using TehnoStom2.Entities;

namespace TehnoStom2;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private void RegistrerUser(object sender, EventArgs e)
    {
        var firstName = FirstNameEntry.Text == null ? string.Empty : FirstNameEntry.Text.Trim();
        var middleName = MiddleNameEntry.Text == null ? string.Empty : MiddleNameEntry.Text.Trim();
        var lastName = LastNameEntry.Text == null ? string.Empty : LastNameEntry.Text.Trim();
        var login = LoginEntry.Text == null ? string.Empty : LoginEntry.Text.Trim();
        var password = PasswordEntry.Text == null ? string.Empty : PasswordEntry.Text.Trim();
        var phone = PhoneEntry.Text == null ? string.Empty : PhoneEntry.Text.Trim();
        var specialization = SpecializationEntry.Text == null ? string.Empty : SpecializationEntry.Text.Trim();
        var role = RoleEntry.Text == null ? string.Empty : RoleEntry.Text.Trim();

        AppDbContext dbContext = new AppDbContext();

        var isFirstNameEmpty = string.IsNullOrWhiteSpace(firstName);
        if (isFirstNameEmpty)
        {
            AppShell.Current.DisplayAlert("������", "��� ���?", "OK");
            return;
        }

        var isLastNameEmpty = string.IsNullOrWhiteSpace(lastName);
        if (isLastNameEmpty)
        {
            AppShell.Current.DisplayAlert("������", "������� ���?", "OK");
            return;
        }

        var isLoginEmpty = string.IsNullOrWhiteSpace(login);
        if (isLoginEmpty)
        {
            AppShell.Current.DisplayAlert("������", "� ����� ���?", "OK");
            return;
        }

        var isPaswordEmpty = string.IsNullOrWhiteSpace(password);
        if (isPaswordEmpty)
        {
            AppShell.Current.DisplayAlert("������", "� ������ ��� ������?", "OK");
            return;
        }

        var isPhoneEmpty = string.IsNullOrWhiteSpace(phone);
        if (isPhoneEmpty)
        {
            AppShell.Current.DisplayAlert("������", "������� ���?", "OK");
            return;
        }

        var isSpecializationEmpty = string.IsNullOrWhiteSpace(specialization);
        if (isSpecializationEmpty)
        {
            AppShell.Current.DisplayAlert("������", "������������� ���?", "OK");
            return;
        }

        var isRoleEmpty = string.IsNullOrWhiteSpace(role);
        if (isRoleEmpty)
        {
            AppShell.Current.DisplayAlert("������", "���� ���?", "OK");
            return;
        }

        var isLoginExist = dbContext.Users.Any(x => x.Login == login);
        if (isLoginExist)
        {
            AppShell.Current.DisplayAlert("������", "������������ ��� ���������� � ������ ������", "OK");
            return;
        }

        using (AppDbContext db = new AppDbContext())
        {
            try
            {
                // ��������� ������������� ���� ��� ������� �����
                var userRole = db.Roles.FirstOrDefault(x => x.Title == role);
                if (userRole == null)
                {
                    userRole = new Role { Title = role };
                    db.Roles.Add(userRole);
                    db.SaveChanges(); // ���������, ����� �������� ID
                }

                // ��������� ������������� ������������� ��� ������� �����
                var userSpecialization = db.Specializations.FirstOrDefault(x => x.Title == specialization);
                if (userSpecialization == null)
                {
                    userSpecialization = new Specialization { Title = specialization };
                    db.Specializations.Add(userSpecialization);
                    db.SaveChanges(); // ���������, ����� �������� ID
                }

                // ��������� ������������� ������
                if (db.Users.Any(x => x.Login == login))
                {
                    AppShell.Current.DisplayAlert("������", "������������ ��� ���������� � ������ ������", "OK");
                    return;
                }

                // ������� ������ ������������
                var newUser = new User
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Login = login,
                    Password = password, // ���������� ���������� ������!
                    Phone = phone,
                    SpecializationId = userSpecialization.Id,
                    RoleId = userRole.Id
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                AppShell.Current.DisplayAlert("�����", "������������ ���������������", "OK");
            }
            catch (Exception ex)
            {
                AppShell.Current.DisplayAlert("������", $"�� ������� ���������������� ������������: {ex.Message}", "OK");
            }
        }
    }

    private void GoBack(object sender, EventArgs e)
    {
        var addOrderPage = new AddOrderPage();
        Navigation.PushAsync(addOrderPage);
    }
}