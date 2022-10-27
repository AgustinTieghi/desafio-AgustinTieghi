using static Hola.Controllers.ProductoVendidoController;
using Hola.Models;
using Hola.Repository;
using System.Data.SqlClient;
using System.Data;

namespace Hola.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> GetProductoV()
        {
            var listaPV = new List<ProductoVendido>();
            var query = @"SELECT * FROM productoVendido";
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
                            var pv = new ProductoVendido();

                            pv.Id = Convert.ToInt32(dr.GetValue(0));
                            pv.Stock = Convert.ToInt32(dr.GetValue(1));
                            pv.IdProducto = Convert.ToInt32(dr.GetValue(2));
                            pv.IdVenta = Convert.ToInt32(dr.GetValue(3));


                            listaPV.Add(pv);

                        }
                        dr.Close();

                    }
                }

            }
            return listaPV;
        }

        public static void EliminarProducto(int id)
        {
            string connectionString = "Server=AgusPC; Database=SistemaGestion;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "DELETE FROM productoVendido where id = @IdUs";
                var param = new SqlParameter();
                param.ParameterName = "IdUs";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;

                cmd2.Parameters.Add(param);
                cmd2.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

