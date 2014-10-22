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

        static Process backup = new Process();

        //Initialise string variables
        public static string minRAM = "";
        public static string maxRAM = "";
        public static string permGen = "";
        public static string serverName = "";
        public static string jvmArgs = "";
        public static int backupFrequency = 0;

        public static Thread backupThread = new Thread(new ThreadStart(BackupThread));
        public static Thread serverOutputThread = new Thread(new ThreadStart(ServerOutputThread));

        public MainWindow()
        {
            ReadConfigs();

            InitializeComponent();

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

            backupThread.Start();
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
                jvmArgs = configReader.ReadString();
                backupFrequency = configReader.ReadInt32();

                serverName = serverName.Replace(".jar", "");

                configReader.Close();
                configStream.Close();
            }
            catch(FileNotFoundException)
            {
                GenerateConfigs();
            }
            catch(Exception e)
            {
                FileStream crashStream = new FileStream("crash.txt", FileMode.Create);
                BinaryWriter crashWriter = new BinaryWriter(crashStream);

                crashWriter.Write(e.ToString());

                crashWriter.Close();
                crashStream.Close();

                Environment.Exit(0);
            }
        }

        public static void WriteConfigs()
        {
            FileStream configStream = new FileStream("configs.dat", FileMode.Create);
            BinaryWriter configWriter = new BinaryWriter(configStream);

            configWriter.Write(Convert.ToInt32(minRAM));
            configWriter.Write(Convert.ToInt32(maxRAM));
            configWriter.Write(Convert.ToInt32(permGen));
            configWriter.Write(serverName);
            configWriter.Write(jvmArgs);
            configWriter.Write(backupFrequency);

            configWriter.Close();
            configStream.Close();
        }

        public static void GenerateConfigs()
        {
            minRAM = "1024";
            maxRAM = "1024";
            permGen = "128";
            serverName = "server.jar";
            backupFrequency = 60;

            WriteConfigs();

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
                string serverArgs = String.Format("-jar {0}.jar -Xms{1}m -Xmx{2}m -XX:MaxPermSize={3}m {4}", serverName, minRAM, maxRAM, permGen, jvmArgs);

                try
                {
                    //Set the process arguments
                    mcServer.StartInfo.FileName = javaLocation;
                    mcServer.StartInfo.Arguments = serverArgs;
                    mcServer.StartInfo.UseShellExecute = false;
                    mcServer.StartInfo.RedirectStandardInput = true;
                    mcServer.StartInfo.RedirectStandardOutput = true;
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

        private static void Backup()
        {
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

        private static void BackupThread()
        {
            Thread.Sleep(backupFrequency * 60000);

            mcServer.StandardInput.WriteLine("say Server is going down temporarily for a backup");
            Thread.Sleep(5000);

            mcServer.StandardInput.WriteLine("save-all");
            Thread.Sleep(3000);
            mcServer.StandardInput.WriteLine("stop");
            Thread.Sleep(10000);

            Backup();

            backup.WaitForExit();

            StartServer();
        }

        public static void ServerOutputThread()
        {
            Thread.Sleep(5000);

            while(true)
            {
                string text = mcServer.StandardOutput.ReadToEnd();

                serverOutput.AppendText(text);
            }
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupThread.Abort();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
    }
}
