using MySql.Data.MySqlClient;

namespace ProyectoMotos;

public partial class EliminarUsuario : ContentPage
{
	public EliminarUsuario()
	{
		InitializeComponent();
	}

    private string connectionString = "Server=databasepoe.cfko0iqhcsi0.us-east-1.rds.amazonaws.com;Database=Pruebas;User ID=admin;Password=POE$2024";
    private async void OnDropedClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor ingrese un usuario y una contraseña correctas", "OK");
            return;
        }

        bool DropExitoso = await EliminarUsuarioAsync(username, password);

        if (DropExitoso)
        {
            await DisplayAlert("Éxito", "Se elimino el usuario", "OK");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Error al eliminar", "OK");
        }
    }

    private async Task<bool> EliminarUsuarioAsync(string usuario, string contrasena)
    {
        try
        {
            using (var conexion = new MySqlConnection(connectionString))
            {
                await conexion.OpenAsync();

                string query = "DELETE FROM Usuarios WHERE usuario= @Usuario AND contraseña= @Contrasena";
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
