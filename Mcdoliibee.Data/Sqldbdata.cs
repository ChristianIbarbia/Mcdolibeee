using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Mcdoliibee.Models;

namespace Mcdoliibee.Data
{
    public class Sqldbdata
    {
        private readonly string _connectionString =
            "Data Source=DESKTOP-S34S8RD;Initial Catalog=MenuMcdollibee;Integrated Security=True;";

        public bool CreateMenu(menu menu)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Menu (ItemName, Category, Code) VALUES (@ItemName, @Category, @Code)";
                    var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ItemName", menu.ItemName);
                    command.Parameters.AddWithValue("@Category", menu.Category);
                    command.Parameters.AddWithValue("@Code", menu.Code);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool UpdateMenu(menu menu)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Menu SET ItemName = @ItemName, Category = @Category WHERE Code = @Code";
                    var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ItemName", menu.ItemName);
                    command.Parameters.AddWithValue("@Category", menu.Category);
                    command.Parameters.AddWithValue("@Code", menu.Code);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool DeleteMenu(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Menu WHERE Code = @Code";
                    var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Code", code);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public List<menu> GetAllMenus()
        {
            var menus = new List<menu>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ItemName, Category, Code FROM Menu";
                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var menu = new menu
                            {
                                ItemName = reader["ItemName"].ToString(),
                                Category = reader["Category"].ToString(),
                                Code = reader["Code"].ToString()
                            };
                            menus.Add(menu);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return menus;
        }
    }
}