using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public class Objava
        {
            public ObjectId _id { get; set; }
            public string Strana { get; set; }
            public string Cioda { get; set; }
            public string Naslov { get; set; }
            public string Text { get; set; }
            public DateTime Datum { get; set; }
            public string Slika { get; set; }
        }
        public string trenutnastrana = "Knjig";
        public IMongoCollection<Objava> coll;
        public Objava SelObjava;
        public List<Objava> glista;
        public Form3()
        {
            InitializeComponent();
            Pokrenidb();
            Unesiulistu();
            
        }

        public List<Objava> Filter()
        {
            List<Objava> lista = glista.FindAll(o => o.Strana == trenutnastrana);

            if (checkBox2.Checked)
            {
                for(int i=0; i < lista.Count;i++)
                {
                    if(lista[i].Naslov != textBox4.Text)
                    {
                        lista.Remove(lista[i]);
                        i--;
                    }
                }
            }
            if (checkBox3.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Datum.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) != dateTimePicker2.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                    {
                        lista.Remove(lista[i]);
                        i--;
                    }
                }
            }
            if (checkBox4.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Cioda != "Da")
                    {
                        lista.Remove(lista[i]);
                        i--;
                    }
                }
            }
            return lista;
        }

        public void Unesiulistu()
        {
            try
            {
                if (glista == null)
                {
                    Napisilistu();
                }
            
                listView1.Items.Clear();
                var lista = Filter();
                if (radioButton1.Checked == true)
                {
                    trenutnastrana = "Knjig";
                    lista = Filter();
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
                    lista = Filter();
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
                if (radioButton3.Checked == true)
                {
                    trenutnastrana = "Posta";
                    lista = Filter();
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
                if (radioButton4.Checked == true)
                {
                    trenutnastrana = "Ceno";
                    lista = Filter();
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
            }
            catch (Exception a)
            {
                Form1 f = new Form1();
                f.ShowDialog();
            }
        }
        

        public void Pokrenidb()
        {
            var client = new MongoClient("mongodb://SRGOLD:j4Ak4OryhSgqiCjG@sr-gold-bazapodataka-shard-00-00-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-01-qhbns.mongodb.net:27017,sr-gold-bazapodataka-shard-00-02-qhbns.mongodb.net:27017/test?ssl=true&replicaSet=SR-GOLD-Bazapodataka-shard-0&authSource=admin");
            var db = client.GetDatabase("Objave");
            coll = db.GetCollection<Objava>("objave");
            var lista = glista;
        }

        private void Napisilistu()
        {
                glista = coll
                        .Find(_ => true)
                        .ToListAsync()
                        .Result;
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
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes(textBox3.Text)));
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Unesiulistu();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (glista == null)
            {
                Napisilistu();
            }
            if (listView1.SelectedItems.Count != 0) { 
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
                if (SelObjava.Slika != null)
                {
                    textBox3.Text = SelObjava.Slika;
                }
                if (SelObjava.Slika == null)
                {
                    textBox3.Text = "Odaberite sliku";
                    pictureBox1.Image = null;
                }
                else
                {
                    textBox3.Text = "Slika Postoji";
                    var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(SelObjava.Slika)));
                    pictureBox1.Image =  img;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                textBox1.Text = "Molimo vas da kliknete na neki od naslova";
                textBox2.Text = "Program registruje klikove samo na naslove";
            }
}

        private void Obrisi_Click(object sender, EventArgs e)
        {
            coll.FindOneAndDelete(o => o._id == SelObjava._id);
            textBox3.Text = "Odaberite sliku";
            glista = null;
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
            if (textBox3.Text != "Odaberite sliku" && textBox3.Text != "Slika Postoji")
            {
                Novo.Slika = Convert.ToBase64String(File.ReadAllBytes(textBox3.Text));
                textBox3.Text = "Odaberite sliku";
            }
            coll.FindOneAndReplace(o => o._id == Novo._id, Novo);
            glista = null;
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
            if (textBox3.Text != "Odaberite sliku")
            {
                Novo.Slika = Convert.ToBase64String(File.ReadAllBytes(textBox3.Text));
                textBox3.Text = "Odaberite sliku";
            }
            coll.InsertOne(Novo);
            glista = null;
            Unesiulistu();
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            
                Unesiulistu();
          
        }
    }
}
