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

namespace SignalRTest2
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
            this.m_Proxy.On<string,string,string>("pushMessage", (strMessage,name,price) => SetText(strMessage,name,price));
            this.m_Connection.Start().Wait();
            this.textBox1.Text = "State" + this.m_Connection.State.ToString() + "; transport=" + this.m_Connection.Transport.Name;
        }

        delegate void SetTextCallback(string strText, string strText2,string strText3);
        private void SetText(string strText,string strText2,string strText3)
        {
            if (this.textBox1.InvokeRequired && this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { strText,strText2,strText3 });
            }
            else
            {
                this.textBox1.Text = strText;
                this.textBox2.Text = strText2;
                this.textBox3.Text = strText3;
            }
        }
    }
}
