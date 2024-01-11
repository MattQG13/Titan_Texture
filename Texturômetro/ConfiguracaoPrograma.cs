using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Texturometer {
    public partial class ConfiguracaoPrograma : Form {
        bool salvo = true;
        TexturometroForms ft;

        public ConfiguracaoPrograma(TexturometroForms ft) {
            InitializeComponent();
            this.ft=ft;
        }

        private void ConfiguracaoPrograma_Load(object sender,EventArgs e) {
            addPortsCb();

            try {
                string[] ports = SerialPort.GetPortNames();
                int index = Array.IndexOf(ports,Properties.Settings.Default.PortaCOM);
                cbCOM.SelectedIndex=index; 
            } finally{ }

            cbBaud.SelectedItem=Properties.Settings.Default.Baudrate.ToString();

            salvo=true;
        }

        private void addPortsCb() {
            string[] ports = SerialPort.GetPortNames();
            string[] names = SerialPort.GetPortNames();

            for(int i = 0;i<ports.Length;i++) {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%("+ports[i]+")%'");

                foreach(ManagementObject device in searcher.Get()) {
                    names[i]=device["Name"].ToString();
                }
            }

            for(int i = 0;i<ports.Length;i++) {
                cbCOM.Items.Add(names[i]);
            }
        }
        private string nomePorta(string port) {
            string[] ports = SerialPort.GetPortNames();
            string[] names = SerialPort.GetPortNames();

            for(int i = 0;i<ports.Length;i++) {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%("+ports[i]+")%'");
                
                foreach(ManagementObject device in searcher.Get()) {
                    names[i]=device["Name"].ToString();
                }
            }

            for(int i = 0;i<names.Length;i++) {
                if(names[i]==port) {
                    return ports[i];
                }
            }
            return null;
        }

        private void ChangedConfig (object sender,EventArgs e) {
            salvo = false;
        }

        private void ConfiguracaoPrograma_FormClosing(object sender,FormClosingEventArgs e) {
            if(salvo==false) {
                var result = MessageBox.Show("Salvar alterações?","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                if(result==DialogResult.Cancel) { e.Cancel=true; }
                if(result==DialogResult.Yes) { e.Cancel=true; btnSalvar.PerformClick(); }
            }
        }

        private void btnSalvar_Click(object sender,EventArgs e) {

            Properties.Settings.Default.PortaCOM=nomePorta(cbCOM.Text);
            Properties.Settings.Default.Baudrate=Convert.ToInt32(cbBaud.Text);
            Properties.Settings.Default.Save();
            salvo=true;
            this.Close();
            ft.reconfigura();
        }
    }
}
