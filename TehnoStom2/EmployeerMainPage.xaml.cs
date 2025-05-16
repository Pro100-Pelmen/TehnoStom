using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using TehnoStom2.DbContexts;
using TehnoStom2.Entities;

using TehnoStom2.Entities;

namespace TehnoStom2;

public partial class EmployeerMainPage : ContentPage
{
    private readonly User _currentEmployee;
    private ObservableCollection<Order> _orders = new();

    public EmployeerMainPage(User user)
    {
        InitializeComponent();
        AppDbContext dbContext = new AppDbContext();
        _currentEmployee = user;

        DisplayEmployeeInfo();
        LoadTasks();
    }

    private void DisplayEmployeeInfo()
    {
        EmployeeInfoLabel.Text = $"{_currentEmployee.LastName} {_currentEmployee.FirstName}";
        EmployeeScoreLabel.Text = $"{_currentEmployee.Score}";
        EmployeeSpecializationLabel.Text = $"Специализация: {_currentEmployee.Specialization?.Title}";
    }

    private void LoadTasks()
    {
        try
        {
            LoadingIndicator.IsRunning = true;


            AppDbContext dbContext = new AppDbContext();

            _orders = new ObservableCollection<Order>(
                dbContext.Orders
                    .Include(t => t.Specialization)
                    .Where(t => t.SpecializationId == _currentEmployee.SpecializationId)
                    .ToList());

            OrderCollectionView.ItemsSource = _orders;
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", ex.Message, "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }

    private void OnOrderSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Order selectedOrder)
        {
            var detailPage = new EmploeerOrderDetailPage(selectedOrder, _currentEmployee);
            Navigation.PushAsync(detailPage);
            OrderCollectionView.SelectedItem = null;
        }
    }

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        LoadTasks();
        RefreshView.IsRefreshing = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadTasks();
    }

    private void LogOut(object sender, EventArgs e)
    {
        var loginPage = new LoginPage();
        Navigation.PushAsync(loginPage);
    }
}