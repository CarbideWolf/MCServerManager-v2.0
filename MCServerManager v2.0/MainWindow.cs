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

        public static bool started = false;

        public static Thread backupThread = new Thread(new ThreadStart(BackupThread));
        public static Thread serverOutputThread;

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
            int n = 10;

            serverOutput.Text = "";

            programLog.AppendText("Starting server...\r\n");

            while (started == false)
            {
                started = true;

                //Create strings to hold the jar location, java location and jvm arguments
                string javaVersion = "jre" + n;
                string javaLocation = "C:\\Program Files\\Java\\" + javaVersion + "\\bin\\javaw.exe";
                string serverArgs = String.Format("-jar {0}.jar -Xms{1}m -Xmx{2}m -XX:MaxPermSize={3}m {4} nogui", serverName, minRAM, maxRAM, permGen, jvmArgs);

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
            programLog.AppendText("Backing up files...\r\n");

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

            mcServer.WaitForExit();

            Backup();

            backup.WaitForExit();

            StartServer();
        }

        public void ServerOutputThread()
        {
            while (true)
            {
                serverOutput.AppendText(mcServer.StandardOutput.ReadLine() + "\r\n");
            }
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serverOutputThread = new Thread(new ThreadStart(ServerOutputThread));

            StartServer();

            serverOutputThread.Start();

            Application.DoEvents();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupThread.Abort();

            this.Close();
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if(started == true)
            {
                programLog.AppendText("Stopping server...\r\n");

                mcServer.StandardInput.WriteLine("stop");

                mcServer.WaitForExit();

                serverOutputThread.Abort();
            }

            Backup();

            backupThread.Abort();

            this.Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(started == true)
            {
                programLog.AppendText("Sending command: \"" + commandInput.Text + "\"...\r\n");

                if (commandInput.Text.Equals("stop"))
                {
                    mcServer.StandardInput.WriteLine("stop");

                    mcServer.WaitForExit();

                    serverOutputThread.Abort();
                }
                else
                {
                    mcServer.StandardInput.WriteLine(commandInput.Text);
                }

                commandInput.Text = "";
            }
        }

        private void saveAllButton_Click(object sender, EventArgs e)
        {
            if(started == true)
            {
                programLog.AppendText("Saving all server data...\r\n");

                mcServer.StandardInput.WriteLine("save-all");
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            if(started == true)
            {
                started = false;

                programLog.AppendText("Restarting server...\r\n");

                mcServer.StandardInput.WriteLine("say Server is going down temporarily for a backup");
                Thread.Sleep(5000);

                mcServer.StandardInput.WriteLine("save-all");
                Thread.Sleep(3000);
                mcServer.StandardInput.WriteLine("stop");

                mcServer.WaitForExit();

                Backup();

                backup.WaitForExit();

                StartServer();

                Application.DoEvents();

                programLog.AppendText("Finished restarting server...\r\n");
            }
        }

        private void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "ban <name> [reason ...]";
        }

        private void banIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "ban-ip <address|name> [reason ...]";
        }

        private void banListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "banlist [ips|players]";
        }

        private void pardonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "pardon <name>";
        }

        private void pardonIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "pardon-ip <address>";
        }

        private void difficultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "difficulty <new difficulty>";
        }

        private void gamemodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "gamemode <mode> [player]";
        }

        private void teleportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "tp [target player] <destination player> OR /tp [target player] <x> <y> <z> [<y-rot> <x-rot>]";
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "time <set|add|query> <value>";
        }

        private void toggleDownfallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "toggledownfall";
        }
        private void blockDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "blockdata <x> <y> <z> <dataTag>";
        }

        private void entityDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "entitydata <entity> <dataTag>";
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "debug <start|stop|chunk> [<x> <y> <z>]";
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "execute <entity> <x> <y> <z> <command> OR /execute <entity> <x> <y> <z> detect <x> <y> <z> <block> <data> <command>";
        }

        private void spawnpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "spawnpoint [player] [<x> <y> <z>]";
        }

        private void spreadPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "spreadplayers <x> <z> <spreadDistance> <maxRange> <respectTeams true|false> <player ...>";
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "title <player> <title|subtitle|clear|reset|times> ...";
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "gamemode <mode> [player]";
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "defaultgamemode <mode>";
        }

        private void giveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "give <player> <item> [amount] [data] [dataTag]";
        }

        private void particleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "particle <name> <x> <y> <z> <xd> <yd> <zd> <speed> [count] [mode]";
        }

        private void summonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "summon <EntityName> [x] [y] [z] [dataTag]";
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "help [page|command name]";
        }

        private void kickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "kick <player> [reason ...]";
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "kill [player|entity]";
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "list";
        }

        private void meToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "me <action ...>";
        }

        private void sayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "say <message ...>";
        }

        private void tellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "tell <player> <private message ...>";
        }

        private void tellRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "tellraw <player> <raw json message>";
        }

        private void achievementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "achievement <give|take> <stat_name|*> [player]";
        }

        private void effectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "effect <player> <effect> [seconds] [amplifier] [hideParticles]";
        }

        private void enchantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "enchant <player> <enchantment ID> [level]";
        }

        private void xPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "xp <amount> [player] OR /xp <amount>L [player]";
        }

        private void giveOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "op <player>";
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "deop <player>";
        }

        private void playsoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "playsound <sound> <player> [x] [y] [z] [volume] [pitch] [minimumVolume]";
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "save-all";
        }

        private void saveOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "save-off";
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "save-on";
        }

        private void scoreboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "scoreboard <objectives|players|teams> ...";
        }

        private void triggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "trigger <objective> <add|set> <value>";
        }

        private void seedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "seed";
        }

        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "stats <entity|block> ...";
        }

        private void testForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "testfor <player> [dataTag]";
        }

        private void testForBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "testforblock <x> <y> <z> <TileName> [dataValue] [dataTag]";
        }

        private void testForBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "testforblocks <x1> <y1> <z1> <x2> <y2> <z2> <x> <y> <z> [mode]";
        }

        private void whitelistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "whitelist <on|off|list|add|remove|reload>";
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "clone <x1> <y1> <z1> <x2> <y2> <z2> <x> <y> <z> [mode]";
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "fill <x1> <y1> <z1> <x2> <y2> <z2> <TileName> [dataValue] [oldBlockHandling] [dataTag]";
        }

        private void replaceItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "replaceitem <entity|block> ...";
        }

        private void setBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "setblock <x> <y> <z> <TileName> [dataValue] [oldBlockHandling] [dataTag]";
        }

        private void setWorldSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "setworldspawn [<x> <y> <z>]";
        }

        private void worldBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandInput.Text = "worldborder <set|center|damage|warning|get> ...";
        }
    }
}
