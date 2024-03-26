using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for AddEmp.xaml
    /// </summary>
    public partial class AddEmp : Window
    {
        private MainWindow _parentWindow;
        public AddEmp(MainWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            _parentWindow.DataInserted += OnDataInserted;

        }

        SqlConnection con = new SqlConnection(@"Data Source=RIZVE_AHMAD\SQLEXPRESS;Initial Catalog=Office;Integrated Security=True;Encrypt=False");

        public void Resetdata()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtDesignation.Clear();
        }

        private void btnReset(object sender, RoutedEventArgs e)
        {
            Resetdata();
        }
        public bool isvalid()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDesignation.Text == string.Empty)
            {
                MessageBox.Show("Gender is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }
        private void btnAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isvalid())
                {

                    SqlCommand cmd = new SqlCommand("INSERT INTO Employees VALUES(@Name, @Email, @Designation)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    OnDataInserted();
                    MessageBox.Show("Successfull", "Saved", MessageBoxButton.OK);
                    Resetdata();

                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void OnDataInserted() 
        {
            _parentWindow.Loadgrid();  
        }
    }
}
