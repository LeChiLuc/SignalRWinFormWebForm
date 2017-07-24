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
        HubConnection m_Connection;
        IHubProxy m_Proxy;
        public Form1()
        {
            InitializeComponent();
            this.initHub();
        }

        private void initHub()
        {
            this.m_Connection = new HubConnection("http://localhost:51070/signalr/hubs");
            this.m_Proxy = this.m_Connection.CreateHubProxy("MyHub");
            this.m_Connection.Start().Wait();
            this.textBox1.Text = "State" + this.m_Connection.State.ToString() + "; transport=" + this.m_Connection.Transport.Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.m_Proxy.Invoke("broadcastMessage", this.textBox1.Text,this.textBox2.Text,this.textBox3.Text);
        }
    }
}
