using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CZOA.WebAPI.OWIN
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
        #if Release
            btTest.Visible = false;
        #endif
            SN.SNConfig.Init();
            timerMsg.Start();
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Fun停止服务();
        }
        private void bt启动服务_Click(object sender, EventArgs e)
        {
            if (bt启动服务.Text.Equals("启动服务"))
            {
                if (Fun启动服务())
                {
                    SN.SNLog.AddMsg("服务启动");
                    bt启动服务.Text = "停止服务";
                }
            }
            else
            {
                Fun停止服务();
                SN.SNLog.AddMsg("服务停止");
                bt启动服务.Text = "启动服务";
            }
        }
        private void timerMsg_Tick(object sender, EventArgs e)
        {
            rtxtMsg.AppendText(Fun输出消息());
        }
        private bool Fun启动服务()
        {
            try
            {
                SN.SNConfig.Init(true);
                SN.SNOwin.Open(SN.SNConfig.HostUrl);
                return true;
            }
            catch (Exception ex)
            {
                SN.SNLog.AddMsg(ex.Message);
                return false;
            }
        }
        private void Fun停止服务()
        {
            SN.SNOwin.Close();
        }
        private string Fun输出消息()
        {
            return SN.SNLog.GetMsg();
        }

        private void btTest_Click(object sender, EventArgs e)
        {
        }

        private void bt服务设置_Click(object sender, EventArgs e)
        {
            var form = new Setting();
            form.ShowDialog(this);
        }
    }
}
