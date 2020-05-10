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
    public class UserDB : IUserDB
    {
        SqlConnection con;
        string connectionString;
        public UserDB(IConfiguration _configuration)
        {
            connectionString = _configuration.GetConnectionString("myCompanyDB");
            con = new SqlConnection(connectionString);
        }
        public List<User> List()
        {
            List<User> users = new List<User>();
            SqlCommand cmd = new SqlCommand("sp_userlist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    
                    string username = reader["username"].ToString();
                    string password = reader["password"].ToString();
                    

                    users.Add(new User
                    {
                       
                        userName = username,
                        password = password

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
            return users;
        }
    }
}
