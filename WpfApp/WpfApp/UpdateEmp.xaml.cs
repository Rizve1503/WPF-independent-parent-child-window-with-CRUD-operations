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
    /// Interaction logic for UpdateEmp.xaml
    /// </summary>
    public partial class UpdateEmp : Window
    {
        private MainWindow _parentWindow;
        public UpdateEmp(MainWindow parentWindow, DataRowView selectedRow)
        {
            InitializeComponent();

            _parentWindow = parentWindow;
            _parentWindow.DataInserted += OnDataChange;

            txtId.Text = selectedRow["Id"].ToString();
            txtName.Text = selectedRow["Name"].ToString();
            txtEmail.Text = selectedRow["Email"].ToString();
            txtDesignation.Text = selectedRow["Designation"].ToString();

        }

        SqlConnection con = new SqlConnection(@"Data Source=RIZVE_AHMAD\SQLEXPRESS;Initial Catalog=Office;Integrated Security=True;Encrypt=False");

        public bool isvalid()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Email is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDesignation.Text == string.Empty)
            {
                MessageBox.Show("Designation is required", "Faild!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update table1 set Name = '" + txtName.Text + "', Email = '" + txtEmail.Text + "', Designation = '" + txtDesignation.Text + "' where Id = '" + txtId.Text + "'  ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfull", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                this.Close();
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from Employees where Id =" + txtId.Text + " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfull", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                OnDataChange();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            finally
            {
                this.Close();
            }
        }
        private void OnDataChange()
        {
            _parentWindow.Loadgrid();
        }
    }
}
