using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MCServerManager_v2._0
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();

            minRAMBox.AppendText(MainWindow.minRAM);
            maxRAMBox.AppendText(MainWindow.maxRAM);
            permGenBox.AppendText(MainWindow.permGen);
            serverNameBox.AppendText(MainWindow.serverName);
            jvmArgsBox.AppendText(MainWindow.jvmArgs);
            backupFrequencyBox.AppendText(MainWindow.backupFrequency.ToString());
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            MainWindow.minRAM = minRAMBox.Text;
            MainWindow.maxRAM = maxRAMBox.Text;
            MainWindow.permGen = permGenBox.Text;
            MainWindow.serverName = serverNameBox.Text.Replace(".jar", "");
            MainWindow.jvmArgs = jvmArgsBox.Text;
            MainWindow.backupFrequency = Convert.ToInt32(backupFrequencyBox.Text);

            MainWindow.WriteConfigs();

            MainWindow.backupThread.Abort();
            MainWindow.backupThread.Start();

            this.Close();
        }
    }
}
