namespace TehnoStom2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(EmployeerMainPage), typeof(EmployeerMainPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(AddOrderPage), typeof(AddOrderPage));
            Routing.RegisterRoute(nameof(OrderDetailPage), typeof(OrderDetailPage));
            Routing.RegisterRoute(nameof(EmploeerOrderDetailPage), typeof(EmploeerOrderDetailPage));
        }
    }
}
