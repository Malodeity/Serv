using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Data;
namespace Serv.Models
{
    public class DataAccess
    {
        static string connectString = "Server=localhost;Port=3306;Database=UberEats;Uid=root;Pwd=root;";
        static MySqlDataReader read;

        //REGISTERING A CUSTOMER
        public string AddCustomer(Customer cust)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Customer(CustName,CustSurname,CustEmail,CustCell, CustPassword) " +
                    "VALUES('" + cust.CustName + "','" + cust.CustSurname + "','" + cust.CustEmail + "','" + cust.CustCell + "','" + cust.CustPassword + "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@CustName", cust.CustName);
                        comma.Parameters.AddWithValue("@CustSurname", cust.CustSurname);
                        comma.Parameters.AddWithValue("@CustEmail", cust.CustEmail);
                        comma.Parameters.AddWithValue("@CustCell", cust.CustCell);
                        comma.Parameters.AddWithValue("@CustPassword", cust.CustPassword);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                        comma.Connection.Close();
                    }
                }
                return null;

            }
        }

        //RETRIEVING ALL THE CUSTOMERS ON THE DATABASE

        public Customer[] GetAllCust()
        {
            string sql = "SELECT * FROM UberEats.Customer;";
            List<Customer> clients = new List<Customer>();
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;
                try
                {
                    comma.Connection.Open();
                    Customer cust = new Customer();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        cust = new Customer(Convert.ToInt32(read["CustId"]),
                                            Convert.ToString(read["CustName"]),
                                            Convert.ToString(read["CustSurname"]),
                                            Convert.ToString(read["CustEmail"]),
                                            Convert.ToString(read["CustCell"]),
                                            Convert.ToString(read["CustPassword"])

                                            );
                        clients.Add(cust);
                    }
                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }
                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }
                return clients.ToArray();
            }
        }




        //CUSTOMER LOG IN



        public Customer CustomerLogin(string custEmail, string custPassword)
        {
            string sql = "SELECT CustId,CustName,CustSurname,CustEmail,CustCell,CustPassword FROM UberEats.Customer WHERE CustEmail='" + custEmail + "'AND CustPassword='" + custPassword + "';";

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;

                try
                {
                    comma.Connection.Open();
                    comma.Parameters.Add(new MySqlParameter("@CustEmail", custEmail));
                    comma.Parameters.Add(new MySqlParameter("@CustPassword", custPassword));

                    read = comma.ExecuteReader();

                    while (read.Read())
                    {
                        return new Customer(Convert.ToInt32(read["CustId"]), Convert.ToString(read["CustName"]), Convert.ToString(read["CustSurname"]), Convert.ToString(read["CustEmail"]), Convert.ToString(read["CustCell"]), Convert.ToString(read["CustPassword"]));
                    }
                    read.Close();
                }
                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }
                return null;
            }
        }


        //UPDATE USER INFOMATION
        public Customer CustomerUpdate(Customer cust, int id)
        {
            string sql = "UPDATE UberEats.Customer SET CustName='" + cust.CustName + "',CustSurname='" + cust.CustSurname + "',CustEmail='" + cust.CustEmail+ "',CustCell='" + cust.CustCell + "' WHERE CustId=" + id + ";";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                using (MySqlCommand comma = new MySqlCommand(sql, connect))
                {

                    comma.Connection = connect;
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.Add(new MySqlParameter("@_CustName", cust.CustName));
                        comma.Parameters.Add(new MySqlParameter("@_CustSurname", cust.CustSurname));
                        comma.Parameters.Add(new MySqlParameter("@_CustEmail", cust.CustEmail));
                        comma.Parameters.Add(new MySqlParameter("@_CustCell", cust.CustCell));
                        comma.Parameters.Add(new MySqlParameter("@_CustPassword", cust.CustPassword));
                       

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            cust = new Customer(Convert.ToString(read["CustName"]),
                                                Convert.ToString(read["CustSurname"]),
                                                Convert.ToString(read["CellEmail"]),
                                                Convert.ToString(read["CustCell"]),
                                                Convert.ToString(read["CustPassword"])           

                                           );
                        }
                        read.Close();

                    }
                    catch (MySqlException exception)
                    {
                        exception.ToString();

                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return cust;
            }
        }


        //GET ALL THE RESTAURANTS
        public Restaurant[] GetRestaurant()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                string querys = "SELECT ResId,ResName,ResAddress,ResCity,ResImage FROM UberEats.Restaurant;";
                using (MySqlCommand comma = new MySqlCommand(querys, connect))
                {
                    try
                    {
                        comma.Connection.Open();
                       Restaurant res = new Restaurant();

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            res = new Restaurant(Convert.ToInt32(read["ResId"]),
                                                Convert.ToString(read["ResName"]),
                                                Convert.ToString(read["ResAddress"]),
                                                Convert.ToString(read["ResCity"]),
                                                 (byte[])(read["ResImage"])
                                                
                                               );
                            restaurants.Add(res);

                        }
                        read.Close();
                        MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                        reader.Read();
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return restaurants.ToArray();
            }

        }

        //GET PRODUCTS

        public Product[] GetProduct()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;

                string querys = "SELECT ProdId,ProdName,ProdDesc,ProdPrice,ProdImage FROM UberEats.Product;";
                using (MySqlCommand comma = new MySqlCommand(querys, connect))
                {
                    try
                    {
                        comma.Connection.Open();
                        Product prod = new Product();

                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            prod = new Product(Convert.ToInt32(read["ProdId"]),
                                                Convert.ToString(read["ProdName"]),
                                                Convert.ToString(read["ProdDesc"]),
                                                Convert.ToDecimal(read["ProdPrice"]),
                                                 (byte[])(read["ProdImage"])

                                               );
                            products.Add(prod);

                        }
                        read.Close();
                        MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                        reader.Read();
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return products.ToArray();
            }

        }



        //ADDING AN ORDER TO THE DATABASE


        public string AddOrder(Order odr)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Order(OrderProdName,OrderQuantity,OrderAddress,OrderTotalAmnt) " +
                    "VALUES('" + odr.OrderProdName + "','" + odr.OrderQuantity + "','" + odr.OrderAddress + "','" + odr.OrderTotalAmnt + "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@OrderProdName", odr.OrderProdName);
                        comma.Parameters.AddWithValue("@OrderQuantity", odr.OrderQuantity);
                        comma.Parameters.AddWithValue("@OrderAddress", odr.OrderAddress);
                        comma.Parameters.AddWithValue("@OrderTotalAmnt", odr.OrderTotalAmnt);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                        comma.Connection.Close();
                    }
                }
                return null;

            }
        }

        //ADDING PAYMENT

        public string AddPayment(Payment pay)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Payment(PayCustId,PayCardNum,PayExp,PayCVV) " +
                    "VALUES('" + pay.PayCustId + "','" + pay.PayCardNum + "','" + pay.PayExp + "','" + pay.PayCVV+ "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@PayCustId", pay.PayCustId);
                        comma.Parameters.AddWithValue("@PayCardNum", pay.PayCardNum);
                        comma.Parameters.AddWithValue("@PayExp", pay.PayExp);
                        comma.Parameters.AddWithValue("@PayCVV", pay.PayCVV);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException ex)
                    {
                        ex.ToString();
                        comma.Connection.Close();
                    }
                }
                return null;

            }
        }

    }
}
