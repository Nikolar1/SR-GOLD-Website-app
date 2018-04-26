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
        public Form2()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://kay:j4Ak4OryhSgqiCjG@SR-GOLD-Bazapodataka-shard-00-00.mongodb.net:27017,SR-GOLD-Bazapodataka-shard-00-01.mongodb.net:27017,SR-GOLD-Bazapodataka-shard-00-02.mongodb.net:27017/admin?ssl=true&replicaSet=SR-GOLD-Bazapodataka-shard-0&authSource=admin");
            var db = client.GetDatabase("Objave");
            coll = db.GetCollection<Objava>("objave");
        }

        private void Pocetna_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Posalji_Click(object sender, EventArgs e)
        {

        }
    }
}
