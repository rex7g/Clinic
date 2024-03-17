
using Clinic.Paginas.Login;

namespace Clinic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new AppShell();
           MainPage=new LoginPage();
        }
    }
}
