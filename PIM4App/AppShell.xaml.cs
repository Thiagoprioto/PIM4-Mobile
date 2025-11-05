using PIM4App.Views;


namespace PIM4App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ChamadosPage), typeof(ChamadosPage));
            Routing.RegisterRoute(nameof(NovoChamadoPage), typeof(NovoChamadoPage));
            Routing.RegisterRoute(nameof(FaqPage), typeof(FaqPage));
            Routing.RegisterRoute(nameof(DetalheChamadoPage), typeof(DetalheChamadoPage));
        }
    }
}
