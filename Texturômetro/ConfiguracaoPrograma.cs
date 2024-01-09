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

namespace Texturometer {
    public partial class ConfiguracaoPrograma : Form {
        public ConfiguracaoPrograma() {
            InitializeComponent();
        }

        private void ConfiguracaoPrograma_Load(object sender,EventArgs e) {
            cbCOM.Items.AddRange(SerialPort.GetPortNames());

            try { cbCOM.SelectedItem=Properties.Settings.Default.PortaCOM; } finally { }
            cbBaud.SelectedItem=Properties.Settings.Default.Baudrate.ToString();
            
        }
    }
}
