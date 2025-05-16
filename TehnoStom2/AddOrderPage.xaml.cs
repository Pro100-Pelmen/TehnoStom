using System.Collections.ObjectModel;
using TehnoStom2.DbContexts;
using TehnoStom2.Entities;

namespace TehnoStom2;

public partial class AddOrderPage : ContentPage
{
    private ObservableCollection<Order> _orders = new();

    public AddOrderPage()
	{
		InitializeComponent();
        AppDbContext dbContext = new AppDbContext();
        LoadOrders();
    }

    private void LoadOrders()
    {
        AppDbContext dbContext = new AppDbContext();
        _orders = new ObservableCollection<Order>(dbContext.Orders.ToList());
        OrdersCollectionView.ItemsSource = _orders;
    }

    private void OnOrderSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Order selectedOrder)
        {
            var detailPage = new OrderDetailPage(selectedOrder);
            Navigation.PushAsync(detailPage);

            OrdersCollectionView.SelectedItem = null;
        }
    }

    private void AddOrder(object sender, EventArgs e)
    {
        var title = TitleEntry.Text;
        var technicalRequirements = TechnicalRequirementsEntry.Text;
        var specialization = SpecializationEntry.Text;
        var score = Convert.ToInt32(ScoreEntry.Text);

        AppDbContext dbContext = new AppDbContext();

        var specializationId = dbContext.Specializations.FirstOrDefault(x => x.Title == specialization);
        if (specializationId == null)
        {
            AppShell.Current.DisplayAlert("Ошибка", "Такой специальности нет", "Ок");
            return;
        }

        var order = new Order
        {
            Title = title,
            TechnicalRequirements = technicalRequirements,
            SpecializationId = specializationId.Id,
            Score = score,
        };

        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
        LoadOrders();
    }

    private void GoBack(object sender, EventArgs e)
    {
        var loginPage = new LoginPage();
        Navigation.PushAsync(loginPage);
    }

    private void GoToRegistrationPage(object sender, EventArgs e)
    {
        var registrationPage = new RegistrationPage();
        Navigation.PushAsync(registrationPage);
    }
}