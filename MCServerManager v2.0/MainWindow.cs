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
using System.Diagnostics;
using System.Threading;

namespace MCServerManager_v2._0
{
    public partial class MainWindow : Form
    {
        //Create the process to run the JRE
        static Process mcServer = new Process();

        //Initialise string variables
        static string minRAM = "";
        static string maxRAM = "";
        static string permGen = "";
        static string serverName = "";

        public MainWindow()
        {
            InitializeComponent();

            ReadConfigs();

            Process mkdir = new Process();

            mkdir.StartInfo.FileName = "cmd.exe";
            mkdir.StartInfo.UseShellExecute = false;
            mkdir.StartInfo.RedirectStandardInput = true;

            try
            {
                mkdir.Start();

                mkdir.StandardInput.WriteLine("mkdir Server");
                mkdir.StandardInput.WriteLine("exit");
            }
            catch(Exception e)
            {
                programLog.AppendText(e.ToString());
            }
        }

        public static void ReadConfigs()
        {
            try
            {
                FileStream configStream = new FileStream("configs.dat", FileMode.Open);
                BinaryReader configReader = new BinaryReader(configStream);

                minRAM = configReader.ReadInt32().ToString();
                maxRAM = configReader.ReadInt32().ToString();
                permGen = configReader.ReadInt32().ToString();
                serverName = configReader.ReadString();
            }
            catch (FileNotFoundException)
            {
                GenerateConfigs();
            }
        }

        public static void GenerateConfigs()
        {
            int minRAM = 1024;
            int maxRAM = 1024;
            int permGen = 128;
            string serverName = "server.jar";

            FileStream configStream = new FileStream("configs.dat", FileMode.Create);
            BinaryWriter configWriter = new BinaryWriter(configStream);

            configWriter.Write(minRAM);
            configWriter.Write(maxRAM);
            configWriter.Write(permGen);
            configWriter.Write(serverName);

            configWriter.Close();
            configStream.Close();

            ReadConfigs();
        }

        public static void StartServer()
        {
            bool started = false;
            int n = 10;

            while (started == false)
            {
                started = true;

                //Create strings to hold the jar location, java location and jvm arguments
                string javaVersion = "jre" + n;
                string javaLocation = "C:\\Program Files\\Java\\" + javaVersion + "\\bin\\javaw.exe";
                string serverArgs = String.Format("-jar server.jar -Xms{0}m -Xmx{1}m -XX:MaxPermSize={2}m", minRAM, maxRAM, permGen);

                try
                {
                    //Set the process arguments
                    mcServer.StartInfo.FileName = javaLocation;
                    mcServer.StartInfo.Arguments = serverArgs;
                    mcServer.StartInfo.UseShellExecute = false;
                    mcServer.StartInfo.RedirectStandardInput = true;
                    mcServer.StartInfo.WorkingDirectory = "Server";

                    //Start the server
                    mcServer.Start();
                }
                catch (Win32Exception)
                {
                    started = false;
                    n--;
                }
            }
        }

        public void Backup()
        {
            //Create new process
            Process backup = new Process();

            //Tell process what program to run and with what arguments
            backup.StartInfo.FileName = "xcopy.exe";
            backup.StartInfo.Arguments = String.Format("Server\\* Backup /e /h /y /i");

            //Start the process
            try
            {
                backup.Start();
            }
            catch (Exception e)
            {
                programLog.AppendText(e.ToString());
            }
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options();
        }
    }
}
