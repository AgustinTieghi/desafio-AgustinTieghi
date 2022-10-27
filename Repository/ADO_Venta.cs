using System.Data.SqlClient;
using System.Data;
using Hola.Models;
using Hola.Controllers;
namespace Hola.Repository
{
    public class ADO_Venta
    {
        public static List<Venta> GetVenta()
        {
            var listaVenta = new List<Venta>();
            var query = @"SELECT * FROM venta";
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
                            var venta = new Venta();

                            venta.Id = Convert.ToInt32(dr.GetValue(0));
                            venta.Comentarios = dr.GetValue(1).ToString();
                            venta.IdUsuario = Convert.ToInt32(dr.GetValue(2));


                            listaVenta.Add(venta);

                        }
                        dr.Close();

                    }
                }

            }
            return listaVenta;

        }
        public static void EliminarVenta(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM venta where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void CargarVenta(ListaVenta vn)
        {
            long IdVenta;
            string query = "Insert into Venta ( Comentarios, idUsuario) OUTPUT INSERTED.id values(@Comentarios, @idUsuario) SELECT SCOPE_IDENTITY()";
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using SqlCommand cmd3 = new SqlCommand(query, connection);
                {
                    var paramComen = new SqlParameter();
                    paramComen.ParameterName = "Comentarios";
                    paramComen.SqlDbType = SqlDbType.VarChar;
                    paramComen.Value = vn.Comentarios;

                    var paramIdUsu = new SqlParameter();
                    paramIdUsu.ParameterName = "IdUsuario";
                    paramIdUsu.SqlDbType = SqlDbType.VarChar;
                    paramIdUsu.Value = vn.IdUsuario;

                    cmd3.Parameters.Add(paramComen);
                    cmd3.Parameters.Add(paramIdUsu);
                    IdVenta = Convert.ToInt64(cmd3.ExecuteScalar());

                }

                foreach (ProductoVendido product in vn.Productos)
                {
                    var cmd = new SqlCommand("INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta)  VALUES   (@Stock,@IdProducto,@IdVenta) ", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = product.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = product.IdProducto;
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = IdVenta;
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE Producto SET Stock = Stock - @Stock WHERE Id = @IdProducto", connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = product.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = product.IdProducto;
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }


    }
}
