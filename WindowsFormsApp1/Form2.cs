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
    public partial class Form2 : Form
    {
        public IMongoCollection<Objava> coll;
        public IMongoDatabase db;
        public Form2()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://SRGOLD:j4Ak4OryhSgqiCjG@sr-gold-bazapodataka-shard-00-00-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-01-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-02-qhbns.mongodb.net:27017/test?ssl=true&replicaSet=SR-GOLD-Bazapodataka-shard-0&authSource=admin");
            db = client.GetDatabase("Objave");
            coll = db.GetCollection<Objava>("objave");
        }

        private void Pocetna_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Posalji_Click(object sender, EventArgs e)
        {
            Objava Novo = new Objava();
            Novo._id = new ObjectId();
            Novo.Text = textBox1.Text;
            Novo.Naslov = textBox2.Text;
            Novo.Datum = dateTimePicker1.Value;
            if(checkBox1.Checked == true)
            {
                Novo.Cioda = "Da";
            }
            else{
                Novo.Cioda = "Ne";
            }
            if(radioButton1.Checked == true)
            {
                Novo.Strana = "Knjig";
            }
            if (radioButton2.Checked == true)
            {
                Novo.Strana = "Poljo";
            }
            if (radioButton3.Checked == true)
            {
                Novo.Strana = "Posta";
            }
            if (radioButton4.Checked == true)
            {
                Novo.Strana = "Ceno";
            }
            coll.InsertOne(Novo);
        }
    }
}
