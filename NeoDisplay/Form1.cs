//TriStam Display by TrinitiSoft and Stamen Vasilev
using MyProg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NeoDisplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string pathToVideo;
        private void Form1_Load(object sender, EventArgs e)
        {
            InitTimer();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            try
            {
                var MyIni = new IniFile(@"C:\db\setPathDisplay.ini");
                pathToVideo = MyIni.Read("video","paths");
            }
            catch (Exception)
            {

                MessageBox.Show(@"Проблем с път към VIDEO, проверете файла c:\db\setPathDisplay.ini");
            }
            axWindowsMediaPlayer1.URL = pathToVideo;
            axWindowsMediaPlayer1.settings.setMode("loop",true);
            axWindowsMediaPlayer1.uiMode = "none";

        }
        public class Info
        {
            public string product { get; set; }
            public string version { get; set; }
            public string securitycode { get; set; }
        }

        public class Customer
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Picture { get; set; }
        }

        public class Receipt
        {
            public string Qnt { get; set; }
            public string Item { get; set; }
            public string Price { get; set; }
            public string Discount { get; set; }
            public string LineTotal { get; set; }
            public string Comment { get; set; }
            public string Type { get; set; }
        }

        public class Package
        {
            public string Item { get; set; }
            public string Total { get; set; }
            public string Used { get; set; }
            public string Left { get; set; }
            public string ValidTill { get; set; }
        }

        public class RootObject
        {
            public Info info { get; set; }
            public Customer Customer { get; set; }
            public string ReceiptAdv1 { get; set; }
            public string ReceiptAdv2 { get; set; }
            public string ReceiptVideo { get; set; }
            public string ReceiptTotal { get; set; }
            public string Currency { get; set; }
            public string CurrencySymbol { get; set; }
            public List<Receipt> Receipt { get; set; }
            public List<Package> Package { get; set; }
        }
        public void dateTime()
        {
            label6.Text = DateTime.Now.ToShortDateString() + "/" + DateTime.Now.ToShortTimeString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ImportJson();
            dateTime();
        }
        private Timer timer1;
        public void InitTimer()//timer to handle datetime
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }
        public string pathToJson;
        public string json;
        public void ImportJson()
        {
            try
            {
                var MyIni = new IniFile(@"C:\db\setPathDisplay.ini");
                pathToJson = MyIni.Read("json","paths");
                json = File.ReadAllText(pathToJson);
            }
            catch (Exception)
            {

                MessageBox.Show(@"Проблем с път към JSON, проверете файла c:\db\setPathDisplay.ini");
            }
            
            
            RootObject data = JsonConvert.DeserializeObject<RootObject>(json);
            this.label1.Text = "Клиент: " + data.Customer.Name; //client name
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();
            this.textBox1.Text = "";
            this.listBox4.Items.Clear();
            this.listBox5.Items.Clear();
            this.listBox6.Items.Clear();
            this.listBox7.Items.Clear();
            this.listBox8.Items.Clear();
            for (int i = 0; i <= data.Receipt.Count - 1; i++)//for qnt
            {
                listBox1.Items.Add(data.Receipt[i].Qnt);
            }
            for (int i = 0; i <= data.Receipt.Count - 1; i++)//for item name
            {
                listBox2.Items.Add(data.Receipt[i].Item);
            }
            for (int i = 0; i <= data.Receipt.Count - 1; i++)//for item price
            {
                listBox3.Items.Add(data.Receipt[i].Price);
            }
            this.textBox1.Text = data.ReceiptTotal; //total price
            for (int i = 0; i <= data.Package.Count - 1; i++)//for package name
            {
                listBox4.Items.Add(data.Package[i].Item);
            }
            for (int i = 0; i <= data.Package.Count - 1; i++)//for packages total
            {
                listBox5.Items.Add(data.Package[i].Total);
            }
            for (int i = 0; i <= data.Package.Count - 1; i++)//for packages used
            {
                listBox6.Items.Add(data.Package[i].Used);
            }
            for (int i = 0; i <= data.Package.Count - 1; i++)//for packages left
            {
                listBox7.Items.Add(data.Package[i].Left);
            }
            for (int i = 0; i <= data.Package.Count - 1; i++)//for packages valid time
            {
                listBox8.Items.Add(data.Package[i].ValidTill);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}   
