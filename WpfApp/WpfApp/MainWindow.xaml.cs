using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void DataInsertedEventHandler();  
        public event DataInsertedEventHandler DataInserted;
        public MainWindow()
        {
            InitializeComponent();
            Loadgrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=RIZVE_AHMAD\SQLEXPRESS;Initial Catalog=Office;Integrated Security=True;Encrypt=False");

        public void Loadgrid()
        {
            SqlCommand cmd = new SqlCommand("select * from Employees", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            EmpDataGrid.ItemsSource = dt.DefaultView;
        }
        

        private void btnAddEmp(object sender, RoutedEventArgs e)
        {
            AddEmp addEmp = new AddEmp(this);
            addEmp.Show();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Get the selected row
                var selectedRow = EmpDataGrid.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    // Create child window instance and pass selected row data
                    var UpdateEmp = new UpdateEmp(this, selectedRow);
                    UpdateEmp.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving row data: " + ex.Message);
            }
        }
    }
}
