using System.Threading.Tasks;
using TehnoStom2.Entities;
using TehnoStom2.DbContexts;

namespace TehnoStom2;

public partial class EmploeerOrderDetailPage : ContentPage
{
    private User _user;
    private Order _order;
    public EmploeerOrderDetailPage(Order order, User user)
    {
		InitializeComponent();
        _user = user;
        _order = order;
        DisplayOredrAndUserInfo(order, user);
        IsButtonVisible(order);
    }

    private void IsButtonVisible(Order order)
    {
        if (order.Status == "Ожидание")
        {
            TakeOrderButton.IsVisible = true;
            return;
        }
        else if (order.Status == "Доработка") 
        {
            CompleteOrderButton.IsVisible = true;
            return;
        }
        else if (order.Status == "В работе")
        {
            CancelOrderButton.IsVisible = true;
            CompleteOrderButton.IsVisible = true;
        }    
    }

    private void DisplayOredrAndUserInfo(Order order, User user)
    {
        OrderTitleLabel.Text = order.Title;
        OrderScoreLabel.Text = Convert.ToString(order.Score);
        OrderStatusLabel.Text = $"Статус: {order.Status}";
        OrderSpecializationLabel.Text = $"Требуемая специализация: {order.Specialization?.Title}";
        OrderRequirementsLabel.Text = $"Технические требования:\n{order.TechnicalRequirements}";
    }

    private void TakeOrder(object sender, EventArgs e)
    {
        AppDbContext dbContext = new AppDbContext();

        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).Status = "В работе";
        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).UserId = _user.Id;

        dbContext.SaveChanges();

        TakeOrderButton.IsVisible = false;
        CancelOrderButton.IsVisible = true;
        CompleteOrderButton.IsVisible = true;
    }

    private void CancelOrder(object sender, EventArgs e)
    {
        AppDbContext dbContext = new AppDbContext();

        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).Status = "Ожидание";
        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).UserId = 1;

        dbContext.SaveChanges();

        TakeOrderButton.IsVisible = true;
        CancelOrderButton.IsVisible = false;
        CompleteOrderButton.IsVisible = false;
    }

    private void CompleteOrder(object sender, EventArgs e)
    {
        AppDbContext dbContext = new AppDbContext();

        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).Status = "Ожидание подтверждения";

        dbContext.SaveChanges();


        CancelOrderButton.IsVisible = false;
        CompleteOrderButton.IsVisible = false;
    }

    private void GoBack(object sender, EventArgs e)
    {
        var emploeerPage = new EmployeerMainPage(_user);
        Navigation.PushAsync(emploeerPage);
    }
}