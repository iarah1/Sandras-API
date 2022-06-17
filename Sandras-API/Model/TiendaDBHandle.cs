using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sandras_API.Model
{
    public class TiendaDBHandle
    {
        private SqlConnection con;
        private void connection()
        {

            string constring = @"Server=localhost; Database=TiendaVirtual;User Id=useraspnet;Password=Fr4m3w0rk;";
            con = new SqlConnection(constring);
        }

        public List<Product> GetProducts(int pageIndex, int categoryId, string search)
        {
            connection();
            List<Product> products = new List<Product>();

            SqlCommand cmd = new SqlCommand("spGetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.Parameters.AddWithValue("@Search", search);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                products.Add(
                    new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ItemCode = Convert.ToString(dr["ItemCode"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"]),
                        ImageUrl = Convert.ToString(dr["ImageUrl"])
                    });
            }

            return products;
        }

        public List<Product> GetListProducts(string productIds)
        {
            connection();
            List<Product> products = new List<Product>();

            SqlCommand cmd = new SqlCommand("spGetListProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productIds", productIds);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                products.Add(
                    new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ItemCode = Convert.ToString(dr["ItemCode"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"]),
                        ImageUrl = Convert.ToString(dr["ImageUrl"])
                    });
            }

            return products;
        }


        public int ProcesarPedido(Cliente cliente, List<PedidoDetalle> detalle)
        {
            int PedidoId = InsertPedido(cliente);

            if (PedidoId > 0)
            {
                foreach (var d in detalle)
                {
                    int DetalleId = InsertDetallePedido(PedidoId, d);
                }
            }

            return PedidoId;
        }

        private int InsertPedido(Cliente cliente)
        {
            string _cliente = cliente.Nombres + " " + cliente.Apellidos;

            int PedidoId = 0;
            connection();
            SqlCommand cmd = new SqlCommand("spCrearPedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cliente", _cliente);
            cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
            cmd.Parameters.AddWithValue("@Total", cliente.TotalPedido);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PedidoId = dr.GetInt32(0);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            //close data reader
            dr.Close();

            //close connection
            con.Close();

            return PedidoId;
        }

        private int InsertDetallePedido(int PedidoId, PedidoDetalle detalle)
        {
            int DetalleId = 0;
            connection();
            SqlCommand cmd = new SqlCommand("spAddDetallePedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PedidoId", PedidoId);
            cmd.Parameters.AddWithValue("@ProductId", detalle.ProductId);
            cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DetalleId = dr.GetInt32(0);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            //close data reader
            dr.Close();

            //close connection
            con.Close();

            return DetalleId;
        }

        public int NewFeedback(Feedback feedback)
        {
            connection();
            int result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("spNewFeedback", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeedbackTypeId", feedback.FeedbackTypeId);
                cmd.Parameters.AddWithValue("@Nombre", feedback.Nombre);
                cmd.Parameters.AddWithValue("@Email", feedback.Email);
                cmd.Parameters.AddWithValue("@Comentarios", feedback.Nota);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                sd.Fill(dt);
                con.Close();

                result = 1;
            }
            catch
            {
                result = -1;
            }

            return result;

        }
    }
}
