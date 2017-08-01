using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRTest
{
    public partial class Form1 : Form
    {
        HubConnection Connection;
        IHubProxy Proxy;
        public Form1()
        {
            InitializeComponent();
            this.initHub();
        }

        private void initHub()
        {
            this.Connection = new HubConnection("http://localhost:51070/signalr/hubs");
            this.Proxy = this.Connection.CreateHubProxy("myHub");
            this.Proxy.On<string, string, string>("pushMessage", (strMessage, name, price) => SetText(strMessage, name, price));
            this.Connection.Start().Wait();
            this.textBox1.Text = "State" + this.Connection.State.ToString() + "; transport=" + this.Connection.Transport.Name;
        }

        delegate void SetTextCallback(string strText, string strText2, string strText3);
        private void SetText(string strText, string strText2, string strText3)
        {            
            if (this.textBox1.InvokeRequired && this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { strText, strText2, strText3 });
            }
            else
            {
                DataTable dt = new DataTable();

                this.dataGridView1.Columns[0].HeaderText = strText;
                this.dataGridView1.Columns[1].HeaderText = strText2;
                this.dataGridView1.Columns[2].HeaderText = strText3;
                dataGridView1.DataSource = dt;
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Proxy.Invoke("broadcastMessage", this.textBox1.Text,this.textBox2.Text,this.textBox3.Text);

            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
