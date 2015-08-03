using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CZJC.WebAPI.OWIN
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            Init窗体();
        }

        private void Init窗体()
        {
            var ds = Get服务();
            txt服务IP地址.Text = ds.Hosts;
        }
        private dynamic Get服务()
        {
            var cfg = System.Configuration.ConfigurationManager.AppSettings;
            var re = new
            {
                Hosts = cfg["HostUrl"].Replace(",", "\r\n"),
            };
            return re;
        }
        private void bt确定_Click(object sender, EventArgs e)
        {
            if (txt服务IP地址.Text.Trim() != "")
            {
                Save配置();
                SN.SNConfig.Init();

                this.Close();
            }
        }
        private void Save配置()
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //写入<add>元素的Value
            Set服务(config,txt服务IP地址.Text);

            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);

            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        private void Set服务(Configuration config, string hosts)
        {
            var cfg = config.AppSettings.Settings;
            cfg["HostUrl"].Value = hosts.Trim().Trim(',').Replace("\r\n", ",");
        }
    }
}
