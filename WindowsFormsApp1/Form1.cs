using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Nova_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.ShowDialog();
            this.Show();
            
        }

        private void Izmeni_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.ShowDialog();
            this.Show();
        }
    }
    public class Objava
    {
        public string _id { get; set; }
        public string Strana { get; set; }
        public string Cioda { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public DateTime Datum { get; set; }
    }
}
