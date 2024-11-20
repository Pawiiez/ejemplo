using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ProyectoMotos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private string connectionString = "Server=databasepoe.cfko0iqhcsi0.us-east-1.rds.amazonaws.com;Database=Pruebas;User ID=admin;Password=POE$2024";

        private async void OnLoginClicked(object sender, EventArgs e)
        {
           string username = usernameEntry.Text;
           string password = passwordEntry.Text;

           if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
           {
            await DisplayAlert("Error", "Por favor ingrese un usuario y una contraseña", "OK");
            return;
           }

            bool loginExitoso = await ValidarUsuarioAsync(username, password);

           if (loginExitoso)
           {
            await DisplayAlert("Éxito", "Login exitoso", "OK");
                await Navigation.PushAsync(new EliminarUsuario());
            }
           else
           {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
           }
        }


        private async Task<bool> ValidarUsuarioAsync(string usuario, string contrasena)
        {
           try
           { 
              using (var conexion = new MySqlConnection(connectionString))
              {
                await conexion.OpenAsync();

                string query = "SELECT COUNT(*) FROM Usuarios WHERE usuario = @Usuario AND contraseña = @Contrasena";
                  using (var cmd = new MySqlCommand(query, conexion))
                  {
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    var resultado = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(resultado) > 0;
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
}
