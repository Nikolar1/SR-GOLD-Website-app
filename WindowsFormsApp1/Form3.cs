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
    public partial class Form3 : Form
    {
        public string trenutnastrana = "Knjigovodstvena agencija";
        public IMongoCollection<Objava> coll;
        public Form3()
        {
            InitializeComponent();
            Pokrenidb();
            listView1 = new ListView();
            var lista = Napisilistu();
            foreach (var item in lista)
            {
                listView1.Items.Add(item.Naslov);
            }
        }

        public void Pokrenidb()
        {
            var client = new MongoClient("mongodb://kay:j4Ak4OryhSgqiCjG@SR-GOLD-Bazapodataka-shard-00-00.mongodb.net:27017,SR-GOLD-Bazapodataka-shard-00-01.mongodb.net:27017,SR-GOLD-Bazapodataka-shard-00-02.mongodb.net:27017/admin?ssl=true&replicaSet=SR-GOLD-Bazapodataka-shard-0&authSource=admin");
            var db = client.GetDatabase("Objave");
            coll = db.GetCollection<Objava>("objave");
            Napisilistu();
        }

        private List<Objava> Napisilistu()
        {
            var listaobjava = coll
                .Find(o => o.Strana == trenutnastrana)
                .ToListAsync()
                .Result;
            return listaobjava;
        }



        private void Pocetna_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
