namespace ProyectoMotos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new NavigationPage(new Inicio());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}