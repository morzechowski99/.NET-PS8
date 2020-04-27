using Microsoft.Extensions.Configuration;
using PS8.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PS8.DAL
{
    public class ProductSqlDB : IProductDB
    {
        SqlConnection con;
        string connectionString;
        public ProductSqlDB(IConfiguration _configuration)
        {
            connectionString = _configuration.GetConnectionString("myCompanyDB");
            con = new SqlConnection(connectionString);
        }
        public List<Product> List()
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("sp_productList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    int id = Int32.Parse(reader["Id"].ToString());
                    string name = reader["name"].ToString();
                    decimal price = Decimal.Parse(reader["price"].ToString());
                    
                    products.Add(new Product
                    {
                        id = id,
                        name = name,
                        price = price,
                        
                    });

                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();

            }
            return products;
        }
        public Product Get(int _id)
        {
            SqlCommand cmd = new SqlCommand("sp_productById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
            cmd.Parameters["@ProductID"].Value = _id;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Product p;
                reader.Read();
                if (reader.HasRows == true)
                {
                    int idd = Int32.Parse(reader["Id"].ToString());
                    string name = reader["name"].ToString();
                    decimal price = Decimal.Parse(reader["price"].ToString());
                   
                    p = new Product { id = idd, name = name, price = price };
                    reader.Close();

                    return p;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        public void Update(Product _product)
        {
            SqlCommand cmd = new SqlCommand("sp_productChange", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 20));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
           
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            cmd.Parameters["@name"].Value = _product.name;
            cmd.Parameters["@price"].Value = _product.price;
           
            cmd.Parameters["@id"].Value = _product.id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException exc)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Delete(int _id)
        {
            SqlCommand cmd = new SqlCommand("sp_productDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
            cmd.Parameters["@ProductID"].Value = _id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException exc)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Add(Product _product)
        {
            SqlCommand cmd = new SqlCommand("sp_productAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NChar, 20));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.Money));
            
            cmd.Parameters["@name"].Value = _product.name;
            cmd.Parameters["@price"].Value = _product.price;
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException exc)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}
