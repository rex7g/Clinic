
using Clinic.Paginas.Login;
using Clinic.Servicios;

namespace Clinic
{
    public partial class App : Application
    {
        public IApiService ApiService;
        public App(IApiService service)
        {

            InitializeComponent();
            ApiService = service;

           // MainPage = new AppShell();
           MainPage=new OpcionPage(ApiService);
        }
    }
}
