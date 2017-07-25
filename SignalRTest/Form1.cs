using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            this.Proxy = this.Connection.CreateHubProxy("MyHub");
            this.Connection.Start().Wait();
            this.textBox1.Text = "State" + this.Connection.State.ToString() + "; transport=" + this.Connection.Transport.Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Proxy.Invoke("broadcastMessage", this.textBox1.Text,this.textBox2.Text,this.textBox3.Text);

            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }

    }
}
