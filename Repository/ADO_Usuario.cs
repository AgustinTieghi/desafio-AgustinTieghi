using static Hola.Controllers.UsuarioController;
using Hola.Models;
using Hola.Repository;
using System.Data;
using System.Data.SqlClient;

namespace Hola.Repository
{
    public class ADO_Usuario
    {
        public static List<Usuario> GetUsuarios()
        {
            var listaUsuarios = new List<Usuario>();
            var query = @"SELECT * FROM usuario";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var usuario = new Usuario();

                            usuario.Id = Convert.ToInt32(dr.GetValue(0));
                            usuario.Nombre = dr.GetValue(1).ToString();
                            usuario.Apellido = dr.GetValue(2).ToString();
                            usuario.NombreUsuario = dr.GetValue(3).ToString();
                            usuario.Contraseña = dr.GetValue(4).ToString();
                            usuario.Mail = dr.GetValue(5).ToString();

                            listaUsuarios.Add(usuario);

                        }
                        dr.Close();

                    }
                }

            }
            return listaUsuarios;

        }

        public static void EliminarUsuario(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM usuario where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void ModificarUsuario(Usuario us)
        {
            string query = "UPDATE Usuario SET Nombre = @NombreUsu, Apellido = @Apellidos, NombreUsuario = @NombreUsuarioUsu, Contraseña = @ContraseñaUsu, Mail = @MailUsu " + " WHERE Id = @IdUsu";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                var paramID = new SqlParameter();
                paramID.ParameterName = "IdUsu";
                paramID.SqlDbType = SqlDbType.BigInt;
                paramID.Value = us.Id;

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "NombreUsu";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = us.Nombre;

                var paramApellido = new SqlParameter();
                paramApellido.ParameterName = "Apellidos";
                paramApellido.SqlDbType = SqlDbType.VarChar;
                paramApellido.Value = us.Apellido;

                var paramUsername = new SqlParameter();
                paramUsername.ParameterName = "NombreUsuarioUsu";
                paramUsername.SqlDbType = SqlDbType.VarChar;
                paramUsername.Value = us.NombreUsuario;

                var paramPass = new SqlParameter();
                paramPass.ParameterName = "ContraseñaUsu";
                paramPass.SqlDbType = SqlDbType.VarChar;
                paramPass.Value = us.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "MailUsu";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = us.Mail;

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramNombre);
                cmd.Parameters.Add(paramApellido);
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPass);
                cmd.Parameters.Add(paramMail);

                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }

        public static Usuario AgregarUsuario(Usuario us)
        {
            string query = "Insert into Usuario  (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                "Values (@NombreUsu, @Apellidos, @NombreUsuarioUsu, @ContraseñaUsu, @MailUsu)";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd3 = new SqlCommand(query, connection);

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "NombreUsu";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = us.Nombre;

                var paramApellido = new SqlParameter();
                paramApellido.ParameterName = "Apellidos";
                paramApellido.SqlDbType = SqlDbType.VarChar;
                paramApellido.Value = us.Apellido;

                var paramUsername = new SqlParameter();
                paramUsername.ParameterName = "NombreUsuarioUsu";
                paramUsername.SqlDbType = SqlDbType.VarChar;
                paramUsername.Value = us.NombreUsuario;

                var paramPass = new SqlParameter();
                paramPass.ParameterName = "ContraseñaUsu";
                paramPass.SqlDbType = SqlDbType.VarChar;
                paramPass.Value = us.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "MailUsu";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = us.Mail;

                cmd3.Parameters.Add(paramNombre);
                cmd3.Parameters.Add(paramApellido);
                cmd3.Parameters.Add(paramUsername);
                cmd3.Parameters.Add(paramPass);
                cmd3.Parameters.Add(paramMail);

                cmd3.ExecuteReader();
                return us;
                connection.Close();
            }

        }
    }
}


