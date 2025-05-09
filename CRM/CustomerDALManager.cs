using System.Data;
using System.Data.SqlTypes;
using CRM;
using MySql.Data.MySqlClient;

namespace DAL;
public class CustomerDALManager
{
    public static IDbConnection dbConnection()
    {
        IDbConnection conn = new MySqlConnection();
        conn.ConnectionString = @"server=localhost;port=3306;user=root;password=manager;database=ecommerce";
        return conn;
    }

    public static List<Customer> getAllCustomerFromDB()
    {
        List<Customer> customers = new List<Customer>();
        IDbConnection conn = CustomerDALManager.dbConnection();
        IDbCommand cmd = new MySqlCommand();
        string query = "select * from Customer";
        cmd.CommandText = query;
        cmd.Connection = conn;

        IDataReader reader = null;


        try
        {
            conn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = int.Parse(reader["CustomerId"].ToString());
                string loginId = reader["LoginId"].ToString();
                string password = reader["Password"].ToString();
                string name = reader["CustomerName"].ToString();
                string email = reader["Email"].ToString();
                string contactNo = reader["ContactNo"].ToString();
                string location = reader["Location"].ToString();


                Customer customer = new Customer
                {
                    CustomerId=id,
                    LoginId=loginId,
                    Password=password,
                    CustomerName=name,
                    Email=email,
                    ContactNo=contactNo,
                    Location=location
                };

                customers.Add(customer);
            }
            conn.Close();


        }
        catch (MySqlException exp)
        {
            string msg = exp.Message;
            Console.WriteLine(msg);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        return customers;
    }

    public static Customer getCustomerById(int customerid)
    {

        Customer customer = null;
        IDbConnection conn = ProductDALManager.dbConnection();
        IDbCommand cmd = new MySqlCommand();
        string query = "select * from Customer where CustomerId=@customerid";
        cmd.Parameters.Add(new MySqlParameter("@customerid", customerid));
        cmd.CommandText = query;
        cmd.Connection = conn;

        IDataReader reader = null;


        try
        {
            conn.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            int id = int.Parse(reader["CustomerId"].ToString());
            string loginId = reader["LoginId"].ToString();
            string password = reader["Password"].ToString();
            string name = reader["CustomerName"].ToString();
            string email = reader["Email"].ToString();
            string contactNo = reader["ContactNo"].ToString();
            string location = reader["Location"].ToString();


             customer = new Customer
            {
                CustomerId = id,
                LoginId = loginId,
                Password = password,
                CustomerName = name,
                Email = email,
                ContactNo = contactNo,
                Location = location
            };
            conn.Close();


        }
        catch (MySqlException exp)
        {
            string msg = exp.Message;
            Console.WriteLine(msg);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }

        return customer;

    }


    public static bool insertCustomer(Customer customer)
    {
        bool status = false;
        IDbConnection conn = CustomerDALManager.dbConnection();
        IDbCommand cmd = new MySqlCommand();
        string query = "insert into Customer values(@customertid, @loginId, @password, @CustomerName, @Email, @Contact, @location)";
        cmd.Parameters.Add(new MySqlParameter("@customertid", customer.CustomerId));
        cmd.Parameters.Add(new MySqlParameter("@loginId", customer.LoginId));
        cmd.Parameters.Add(new MySqlParameter("@password", customer.Password));
        cmd.Parameters.Add(new MySqlParameter("@CustomerName", customer.CustomerName));
        cmd.Parameters.Add(new MySqlParameter("@Email", customer.Email));
        cmd.Parameters.Add(new MySqlParameter("@Contact", customer.ContactNo));
        cmd.Parameters.Add(new MySqlParameter("@location", customer.Location));

        cmd.CommandText = query;
        cmd.Connection = conn;

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            status = true;
        }
        catch (MySqlException exp)
        {
            string msg = exp.Message;
            Console.WriteLine(msg);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return status;
    }


    public static bool UpdateCustomer(Customer customer)
    {
        bool status = false;
        IDbConnection conn = CustomerDALManager.dbConnection();
        IDbCommand cmd = new MySqlCommand();
        string query = "update Customer set LoginId=@loginid, Password=@password, CustomerName=@name, Email=@email, ContactNo=@contact,Location=@location where CustomerId=@id";
        cmd.Connection = conn;
        cmd.CommandText = query;

        cmd.Parameters.Add(new MySqlParameter("@id", customer.CustomerId));
        cmd.Parameters.Add(new MySqlParameter("@loginid", customer.LoginId));
        cmd.Parameters.Add(new MySqlParameter("@password", customer.Password));
        cmd.Parameters.Add(new MySqlParameter("@name", customer.CustomerName));
        cmd.Parameters.Add(new MySqlParameter("@email", customer.Email));
        cmd.Parameters.Add(new MySqlParameter("@contact", customer.ContactNo));
        cmd.Parameters.Add(new MySqlParameter("@location", customer.Location));


        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            status = true;
        }
        catch (MySqlException exp)
        {
            string msg = exp.Message;
            Console.WriteLine(msg);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return status;
    }



    public static bool deleteCustomerById(int id)
    {
        bool status = false;
        IDbConnection conn = CustomerDALManager.dbConnection();
        IDbCommand cmd = new MySqlCommand();
        string query = "delete from Customer where CustomerId=@id";
        cmd.Parameters.Add(new MySqlParameter("@id", id));
        cmd.CommandText = query;
        cmd.Connection = conn;

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            status = true;
        }
        catch (MySqlException exp)
        {
            string msg = exp.Message;
            Console.WriteLine(msg);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return status;
    }



}