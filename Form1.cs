//login and sign up with hashing password
//i create here database and table  automatic with loading of form
//if yoy want create it out side here i put code of dumb in file in the folder

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                //creating database
                String connectionToDatabase1 = @"server=localhost;userid=root;password=root;database=test";
                String query = @"CREATE DATABASE IF NOT EXISTS atefDiab;";
                MySqlConnection conn1 = new MySqlConnection(connectionToDatabase1);


                MySqlCommand cmd1 = new MySqlCommand(query, conn1);
                cmd1.Connection = conn1;
                cmd1.Connection.Open();
                cmd1.CommandText = query;

                cmd1.ExecuteNonQuery();



                //table
                //creating database
                String connectionToDatabase2 = @"server=localhost;userid=root;password=root;database=atefDiab";
                String query2 = @" CREATE TABLE IF NOT EXISTS users ( id int(11) NOT NULL auto_increment,username varchar(200) NOT NULL,password varchar(200) default NULL, PRIMARY KEY  (id)) ";
                MySqlConnection conn2 = new MySqlConnection(connectionToDatabase2);


                MySqlCommand cmd2 = new MySqlCommand(query, conn2);
                cmd2.Connection = conn2;
                cmd2.Connection.Open();
                cmd2.CommandText = query2;

                cmd2.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string password = textBox2.Text.GetMD5Hash();
            string username = textBox1.Text;
            string connectionToDatabase = "server=localhost;userid=root;password=root;database=atefDiab";
            MySqlConnection conn = new MySqlConnection(connectionToDatabase);
            MySqlCommand cmd = new MySqlCommand();





            try
            {
                cmd.Connection = conn;
                cmd.Connection.Open();
               cmd.CommandText = "select * from users where username=@username and password=@password";
               cmd.Prepare();
               cmd.Parameters.AddWithValue("@username", username);
               cmd.Parameters.AddWithValue("@password", password);
               int atef = (int)cmd.ExecuteScalar();
                if(atef>0)
               MessageBox.Show("successfuly login !!");
                else
                    MessageBox.Show("can not  login !!");
               

            }
            catch 
            {
                MessageBox.Show(" invalid username or password try again");

            }
            finally
            {
                if (conn != null)
                    conn.Close();

            }


        
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            string password = textBox4.Text.GetMD5Hash();
            string username = textBox3.Text;
            string connectionToDatabase = "server=localhost;userid=root;password=root;database=atefDiab";
            MySqlConnection conn = new MySqlConnection(connectionToDatabase);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.Connection.Open();
                cmd.CommandText = "insert into users(username,password) values(@username,@password)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int atef = cmd.ExecuteNonQuery();
                if (atef != 0)
                {
                    MessageBox.Show("successfully registerd ");
                    MessageBox.Show("try login now in form below it ");

                }
                else
                    MessageBox.Show("can not register try again !! ");


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("the error is " + ex.ToString());

            }
            finally
            {
                if (conn != null)
                    conn.Close();

            }


        }


    }
}
