using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Threading;

namespace Nationala_2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Jocuri.mdf;Integrated Security=True");
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader r;
        StreamReader reader;
        string line;

        public void tabel1()
        {
            try
            {
                con.Open();
                reader = new StreamReader("Utilizatori.txt");
                while((line=reader.ReadLine())!=null)
                {
                    string[] bucati = line.Split(';');
                    cmd = new SqlCommand(String.Format("INSERT INTO Utilizatori VALUES('{0}','{1}','{2}');",bucati[0],bucati[1],bucati[2]), con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void tabel2()
        {
            try
            {
                con.Open();
                reader = new StreamReader("Rezultate.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(';');
                    string data = bucati[3];
                    string[] bucatele = data.Split('.');
                    string val = bucatele[1];
                    val += ".";
                    val += bucatele[0];
                    val += ".";
                    val += bucatele[2];
                   // MessageBox.Show(val);

                    cmd = new SqlCommand(String.Format("INSERT INTO Rezultate VALUES({0},'{1}',{2},'{3}');", bucati[0], bucati[1], bucati[2],val), con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                tabel1();
                tabel2();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
