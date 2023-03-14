using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient; 


namespace C_Project
{

    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
        static void AddData(string queryString,
                string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        static int ReadData(string queryString, string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                int numRows = dt.Rows.Count;

                return (numRows - 1); 
            }
            
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string qs1 = $"INSERT INTO Students (name, email, age) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')";
            AddData(qs1, cs);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            string qs2 = $"SELECT * FROM Students";
            var number = ReadData(qs2, cs);

            MessageBox.Show($"You are number {number} to sign up!"); 

        }
    }
}




