using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace IronShoes
{
    //点击修改TCP服务器的地址
    public partial class Datavive: Form
    {
        private TCPHellper client = new TCPHellper();
        public Datavive()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;//设置该属性 为false
            client.Completed += new Action<System.Net.Sockets.TcpClient, EnSocketAction>((c, enAction) =>
            {
                IPEndPoint iep = c.Client.RemoteEndPoint as IPEndPoint;
                string key = string.Format("{0}:{1}", iep.Address.ToString(), iep.Port);
                switch (enAction)
                {
                    case EnSocketAction.Connect:
                        Console.WriteLine("已经与{0}建立连接", key);
                        break;
                    case EnSocketAction.SendMsg:
                        Console.WriteLine("{0}：向{1}发送了一条消息", DateTime.Now, key);
                        break;
                    case EnSocketAction.Close:
                        Console.WriteLine("服务端连接关闭");
                        break;
                    default:
                        break;
                }
            });
            //接收
            try {
           
            client.Received += new Action<string, string>((key, msg) =>
            {
                entity.Trank tk = DataSource.getDataSource(msg);
                label41.Text=tk.TrimId;
                label4.Text=tk.InfoNum ;
                label39.Text=tk.TrimTime;
                label37.Text=tk.Voltage;
                label35.Text=tk.Pith;
                label33.Text=tk.Roll;
                label31.Text=tk.Yaw;
                label7.Text=tk.Longitude;
                label5.Text=tk.Latitude;
                label11.Text=tk.Distance;
                label9.Text=tk.Temp;
                string[] sTrings = tk.StatValue.Split(',');
                label15.Text = sTrings[0];
                label27.Text = sTrings[1];
                label13.Text = sTrings[2];
                label25.Text = sTrings[3];
                label23.Text = sTrings[4];
                label19.Text = sTrings[5];
                label21.Text = sTrings[6];
                label17.Text = sTrings[7];
                label44.Text = sTrings[8];
                label29.Text = tk.Loca;
                label49.Text=tk.Trno;
                label47.Text=tk.Putsta;
                label52.Text=tk.BrakestatusCode;
                label45.Text = tk.Electricity;
                string[] sTrings2 = tk.GPRS1.Split(',');
                label55.Text = sTrings2[0];
                label51.Text = sTrings2[1];
                Console.WriteLine(tk.TrimId);
            });
            }
            catch (Exception ex)
            {
            }
            client.ConnectAsync("127.0.0.1", 6873);

        }

        private void Datavive_Load(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }
    }
}
