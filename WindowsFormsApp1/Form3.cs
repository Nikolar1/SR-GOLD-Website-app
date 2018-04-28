using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public string trenutnastrana = "Knjig";
        public IMongoCollection<Objava> coll;
        public Objava SelObjava;
        public Form3()
        {
            InitializeComponent();
            Pokrenidb();
            Unesiulistu();
        }

        public void Unesiulistu()
        {
            listView1.Items.Clear();
            var lista = Napisilistu();
                if (radioButton1.Checked == true)
                {
                    trenutnastrana = "Knjig";
                    lista = Napisilistu();
                    foreach (var item in lista)
                    {
                        string text = item.Datum.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        string ID = item._id.ToString();
                        string[] row = { item.Naslov, item.Cioda, text, ID };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        listView1.View = View.Details;
                }
                }
                if (radioButton2.Checked == true)
                {
                    trenutnastrana = "Poljo";
                    lista = Napisilistu();
                    foreach (var item in lista)
                    {
                        string text = item.Datum.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string ID = item._id.ToString();
                        string[] row = { item.Naslov, item.Cioda, text, ID };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        listView1.View = View.Details;
                }
                }
                if (radioButton3.Checked == true)
                {
                    trenutnastrana = "Posta";
                    lista = Napisilistu();
                    foreach (var item in lista)
                    {
                        string text = item.Datum.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string ID = item._id.ToString();
                        string[] row = { item.Naslov, item.Cioda, text, ID };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        listView1.View = View.Details;
                    }
                }
                if (radioButton4.Checked == true)
                {
                    trenutnastrana = "Ceno";
                    lista = Napisilistu();
                    foreach (var item in lista)
                    {
                        string text = item.Datum.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        string ID = item._id.ToString();
                        string[] row = { item.Naslov, item.Cioda, text, ID };
                        var listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                        listView1.View = View.Details;
                    }
                }
        }

        public void Pokrenidb()
        {
            var client = new MongoClient("mongodb://SRGOLD:j4Ak4OryhSgqiCjG@sr-gold-bazapodataka-shard-00-00-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-01-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-02-qhbns.mongodb.net:27017/test?ssl=true&replicaSet=SR-GOLD-Bazapodataka-shard-0&authSource=admin");
            var db = client.GetDatabase("Objave");
            coll = db.GetCollection<Objava>("objave");
            var lista = Napisilistu();
        }

        private List<Objava> Napisilistu()
        {
            var listaobjava = coll
                .Find(o => o.Strana == trenutnastrana)
                .ToListAsync()
                .Result;
            return listaobjava;
        }

        private List<Objava> Nadjiobjavu(ObjectId ID)
        {
            var listaobjava = coll
                .Find(o => o._id == ID)
                .ToListAsync()
                .Result;
            return listaobjava;
        }
       
        private void Pocetna_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Unesiulistu();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var a = listView1.SelectedItems[0].SubItems[3].Text;
                ObjectId b = ObjectId.Parse(s: a);
                var el = Nadjiobjavu(b);
                SelObjava = el[0];
                textBox1.Text = SelObjava.Text;
                textBox2.Text = SelObjava.Naslov;
                if (SelObjava.Cioda == "Da")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                dateTimePicker1.Value = SelObjava.Datum;
            }
            catch (System.Exception a)
            {
                textBox1.Text = "Molimo vas da kliknete na neki od naslova";
                textBox2.Text = "Program registruje klikove samo na naslove";
            }
}

        private void Obrisi_Click(object sender, EventArgs e)
        {
            coll.FindOneAndDelete(o => o._id == SelObjava._id);
            Unesiulistu();
        }

        private void Izmeni_Click(object sender, EventArgs e)
        {
            Objava Novo = SelObjava;
            Novo.Text = textBox1.Text;
            Novo.Naslov = textBox2.Text;
            Novo.Datum = dateTimePicker1.Value;
            if (checkBox1.Checked == true)
            {
                Novo.Cioda = "Da";
            }
            else
            {
                Novo.Cioda = "Ne";
            }
            coll.FindOneAndReplace(o => o._id == Novo._id, Novo);
            Unesiulistu();
        }

        private void Dodaj_Click(object sender, EventArgs e)
        {
            Objava Novo = new Objava();
            Novo._id = new ObjectId();
            Novo.Text = textBox1.Text;
            Novo.Naslov = textBox2.Text;
            Novo.Datum = dateTimePicker1.Value;
            if (checkBox1.Checked == true)
            {
                Novo.Cioda = "Da";
            }
            else
            {
                Novo.Cioda = "Ne";
            }
            if (radioButton1.Checked == true)
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
            Unesiulistu();
        }
    }
}
