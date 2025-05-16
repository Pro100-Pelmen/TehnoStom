using System.Threading.Tasks;
using TehnoStom2.DbContexts;
using TehnoStom2.Entities;

namespace TehnoStom2;

public partial class OrderDetailPage : ContentPage
{
    private Order _order;
	public OrderDetailPage(Order order)
    {
		InitializeComponent();
        _order = order;
        IsButtonVisible(order);
        DisplayOrderDetails(order);
    }

    private void DisplayOrderDetails(Order order)
    {
        TitleLabel.Text = order.Title;
        StatusLabel.Text = $"������: {order.Status}";
        ScoreLabel.Text = Convert.ToString(order.Score);
        SpecializationLabel.Text = $"�������������: {order.Specialization?.Title ?? "�� �������"}";
        RequirementsLabel.Text = $"����������:\n{order.TechnicalRequirements}";
    }

    private void ConfirmOrder(object sender, EventArgs e)
    {
        AppDbContext dbContext = new AppDbContext();

        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).Status = "���������";
        dbContext.Users.FirstOrDefault(x => x.Id == _order.UserId).Score += _order.Score;
        dbContext.SaveChanges();

        ConfirmOrderButton.IsVisible = false;
        NotConfirmOrderButton.IsVisible = false;
    }

    private void NotConfirmOrder(object sender, EventArgs e)
    {
        AppDbContext dbContext = new AppDbContext();

        dbContext.Orders.FirstOrDefault(x => x.Id == _order.Id).Status = "���������";
        dbContext.SaveChanges();
        IsButtonVisible(_order);

        ConfirmOrderButton.IsVisible = false;
        NotConfirmOrderButton.IsVisible = false;
    }

    private void IsButtonVisible(Order order)
    {
        if (order.Status == "���������" || order.Status == "��������" || order.Status == "� ������")
        {
            ConfirmOrderButton.IsVisible = false;
            NotConfirmOrderButton.IsVisible = false;
            return;
        }
        else
        {
            ConfirmOrderButton.IsVisible = true;
            return;
        }
    }

    private void GoBack(object sender, EventArgs e)
    {
        var addOrderPage = new AddOrderPage();
        Navigation.PushAsync(addOrderPage);
    }
}