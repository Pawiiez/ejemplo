namespace ProyectoMotos;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
	}

	public async void OnOpSesionClicked( object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());
    }

	public async void OnOpRegistrarClicked( object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Registro());
	}
}
