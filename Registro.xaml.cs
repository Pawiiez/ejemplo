using MySql.Data.MySqlClient;

namespace ProyectoMotos;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
	}
    private string connectionString = "Server=databasepoe.cfko0iqhcsi0.us-east-1.rds.amazonaws.com;Database=Pruebas;User ID=admin;Password=POE$2024";
    private async void OnRegisteredClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingrese un usuario, un correo y una contraseña", "OK");
            return;
        }

        bool registerExitoso = await RegistrarUsuarioAsync(username, password);

        if (registerExitoso)
        {
            await DisplayAlert("Éxito", "Registro exitoso", "OK");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Error al registrarse", "OK");
        }
    }

    private async Task<bool> RegistrarUsuarioAsync(string usuario, string contrasena)
    {
        try
        {
            using (var conexion = new MySqlConnection(connectionString))
            {
                await conexion.OpenAsync();

                string query = "INSERT INTO Usuarios(usuario,contraseña) VALUES (@Usuario,@Contrasena)";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    var resultado = await cmd.ExecuteNonQueryAsync();
                    return (resultado) > 0;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error al conectar con la base de datos: " + ex.Message, "OK");
            return false;
        }
    }
}