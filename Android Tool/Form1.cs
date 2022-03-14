using System;
using System.Drawing;
using System.Windows.Forms;
using JJLab.Android;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace Android_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        public static string xmlpath,port,loader,cloud,qcjob,mtkjob,args,cprel;
        public static bool blu,isdual,isfrp,davb,skp;
        //This will fuck richTextBox1 from background worker
        private void logs(string text,Color color)
        {
            Invoke(new MethodInvoker(delegate { richTextBox1.AppendText(text,color); }));
        }//
        #region Windows Form
        private void xm_Click(object sender, EventArgs e)
        {
            xm.BackColor = Color.Gray;
            mtkb.BackColor = Color.Silver;
            fbb.BackColor = Color.Silver;
            wtb.BackColor= Color.Silver;
            tabControl1.SelectedIndex = 0;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            ProcessStartInfo i = new ProcessStartInfo("cmd.exe")
            {
                Arguments = "/c taskkill /f /im python*",
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process.Start(i);
            button2.Enabled = true;
            richTextBox1.Clear();
        }
        
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xm.BackColor = Color.Gray;
            mtkb.BackColor = Color.Silver;
            fbb.BackColor = Color.Silver;
            wtb.BackColor = Color.Silver;
            tabControl1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex= 0;
            label1.Text = "Android Tool(By Kyaw Khant Zaw)";
            fwp.Text = "Double click to load firmware";
            label4.Text = "1. Select Operation" + "\n2. Click Start" + "\n3. Wait 5s and plug in preloader mode or brom mode";
            fbfwp.Text = "Double click to load firmware";
            logs("Python မရှိပါက MTK Tab ကို အသုံးပြု၍မရပါ။\nPython မရှိပါက Windows Tool Tab မှ Python ကို အရင် Install လုပ်ပါ။", Color.Red);
        }

        private void mtkb_Click(object sender, EventArgs e)
        { 
            xm.BackColor = Color.Silver;
            mtkb.BackColor = Color.Gray;
            fbb.BackColor = Color.Silver;
            wtb.BackColor = Color.Silver;
            tabControl1.SelectedIndex = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_ysl.mbn";                    
                    break;
                case 1:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_jason.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\jason\raw.xml";
                    break;
                case 2:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_ugglite.mbn";
                    break;
                case 3:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_ugg.mbn";
                    break;
                case 4:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_whyred.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\whyred\raw.xml";
                    break;
                case 5:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_tulip.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\tulip\raw.xml";
                    break;
                case 6:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_lavender.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\lavender\raw.xml";
                    break;
                case 7:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_ginkgo.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\ginkgo\raw.xml";
                    logs("Warning : \nEnable OEM Unlock in Developer Options first.",Color.Red);
                    break;
                case 8:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_oxygen.mbn";
                    break;
                case 9:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_nitrogen.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\nitrogen\raw.xml";
                    break;
                case 10:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_wayne.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\wayne\raw.xml";
                    break;
                case 11:
                    loader = Directory.GetCurrentDirectory() + @"\bin\fh\prog_emmc_firehose_platina.elf";
                    xmlpath = Directory.GetCurrentDirectory() + @"\bin\xml\platina\raw.xml";
                    break;
            }
        }

        private void fwp_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {                
                string f = fbd.SelectedPath.ToString();
                if (File.Exists(f + @"\images\NON-HLOS.bin") || File.Exists(f+@"NON-HLOS.bin"))
                {
                    f=f.Replace("\\images"," ");
                    fwp.Text = f.Trim();
                }
                else
                {
                    MessageBox.Show("Firmware select error.", "Error");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Xiaomi tab>EDL
        private void starter_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            MiAccErase.RunWorkerAsync();
        }
        //Xiaomi tab>Patch Firmware 
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            mpa.RunWorkerAsync();
        }
        //MTK Tab
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();            
            mtk.RunWorkerAsync();
        }
        //Fastboot tab > firmware browse
        private void fbfwp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string p = f.SelectedPath;
                if (!File.Exists(p + "\\flash_all.bat") && (!Directory.Exists(p + "\\images")))
                {
                    MessageBox.Show("Flash script not found!");
                }
                else
                {
                    fbfwp.Text = p;
                    createdatatable();
                    ParseFlashBat();
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.BackColor = Color.LightBlue;
                    dataGridView1.DataSource = dt;
                }
            }
        }
        //Fastboot tab > fastboot flash
        private void button4_Click(object sender, EventArgs e)
        {
            fbfler.RunWorkerAsync();
        }
        //Fastboot tab > blu
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (fastboot.isConnected())
            {
                logs("Unlocking Bootloader : ", Color.Black);
                if(fastboot.process("flashing unlock_critical").Contains("Finished"))
                {
                    logs("Success", Color.DarkGreen);
                }
                else
                {
                    logs("Fail",Color.Red);
                }
            }
            else
            {
                MessageBox.Show("No fastboot device.");
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\mtk\mtkclient\req.bat");
        }
        //Fastboot tab > FRP Reset
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (fastboot.isConnected())
            {                
                FBInfo();
                logs("\nErasing frp : ", Color.Black);
                if (isfrp)
                {
                    fastboot.erase("frp");
                }
                else
                {
                    fastboot.erase("config");
                }
                logs("Done", Color.DarkGreen);
                fastboot.process("reboot");
            }
            else
            {
                MessageBox.Show("No fastboot device");
            }
        }
        //Fastboot tab > Mi Sig Unlock
        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\MiUnlock.exe");
        }
        //Python
        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\python.exe").WaitForExit();
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName= @"bin\mtk\mtkclient\req.bat",
                WorkingDirectory= @"bin\mtk\mtkclient\",
            };
            Process.Start(psi);
        }
        //adb
        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\ADBInstaller.exe");
        }
        //MTK Driver
        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\MTKDriver.exe");
        }
        //usbdk
        private void button11_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\usbdk.msi");
        }
        //Driver signature off
        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start(@"bin\sig.bat");
        }
        //Assign qcjob if combobox2 change
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {            
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    qcjob = "Erase";
                    break;
                case 1:
                    qcjob = "Write";
                    break;
                    case 2:
                    qcjob = "EU";
                    break;
                case 3:
                    qcjob = "MiRoot";
                    break;
                case 4:
                    qcjob = "BLU";
                    break;
            }
        }

        private void fbb_Click(object sender, EventArgs e)
        {
            xm.BackColor = Color.Silver;
            mtkb.BackColor = Color.Silver;
            fbb.BackColor = Color.Gray;
            wtb.BackColor = Color.Silver;
            tabControl1.SelectedIndex = 2;
        }

        private void MiAccErase_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (qcjob)
            {
                case "Erase":
                    AccountErase();
                    break;
                case "Write":
                    AccountWrite();
                    break;
                case "EU":
                    EURom();
                    break;
                case "MiRoot":
                    MiAccRoot();
                    break;
                case "BLU":
                    BLU();
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    mtkjob = "bypass";
                    break;
                case 1:
                    mtkjob = "reset";
                    break;
                case 2:
                    mtkjob = "keepdata";
                    break;
                case 3:
                    mtkjob = "FRP";
                    break;
                case 4:
                    mtkjob = "BLU";
                    break;
                case 5:
                    mtkjob = "MiAcc";
                    break;
            }
        }

        private void mtk_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (mtkjob)
            {
                case "bypass":
                    logs("Operation - MTK Auth Bypass\n", Color.DarkGreen);
                    MTK("payload");
                    break;
                case "reset":
                    logs("Operation - MTK Factory Reset\n", Color.DarkGreen);
                    MTK("e userdata --preloader="+"\""+cprel+"\"");
                    break;
                case "keepdata":
                    logs("Operation - MTK Factory Reset(Keep Data)\n", Color.DarkGreen);
                    MTK("w para bin\\list.dat");
                    break;
                case "FRP":
                    logs("Operation - MTK FRP Erase\n", Color.DarkGreen);
                    MTK("e frp");
                    break;
                case "BLU":
                    logs("Operation - MTK Bootloader Unlock\n", Color.DarkGreen);
                    MTK("da seccfg unlock");
                    break;
                case "MiAcc":
                    logs("Operation - MTK Mi Account Erase\n", Color.DarkGreen);
                    MTK("e persist");
                    break;
            }            
        }

        private void mpa_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ModemPatch();
        }
        private void wtb_Click(object sender, EventArgs e)
        {
            xm.BackColor = Color.Silver;
            mtkb.BackColor = Color.Silver;
            fbb.BackColor = Color.Silver;
            wtb.BackColor = Color.Gray;
            tabControl1.SelectedIndex = 3;
        }
        #endregion
        #region Xiaomi Tab
        //Mi Account Remove Erase Method
        private void AccountErase()
        {            
            logs("Searching EDL port : ", Color.Black);
            if (emmcdl.Detect())
            {
                string pn = emmcdl.PortName;
                logs(pn, Color.DarkGreen);
                logs("\nSending Loader : ", Color.Black);
                if (emmcdl.SendLoader(pn, loader))
                {
                    logs("Success",Color.DarkGreen);
                    logs("\nErasing Account : ", Color.Black);
                    if(emmcdl.process("-p " + pn + " -f " + "\"" + loader + "\"" + " -e persist").Contains("Successfully"))
                    {                        
                        logs("Success\nRebooting", Color.DarkGreen);
                        fhloader.Reboot(pn);
                    }
                }
                else
                {
                    logs("Fail", Color.Red);
                }
            }
            else
            {
                logs("No Port Found", Color.Red);
            }
        }        

        //Mi Account Remove Write Method
        private void AccountWrite()
        {
            logs("Searching EDL port : ", Color.Black);
            if (emmcdl.Detect())
            {
                string pn = emmcdl.PortName;
                logs(pn, Color.DarkGreen);
                logs("\nSending Loader : ", Color.Black);
                if (emmcdl.SendLoader(pn, loader))
                {
                    logs("Success", Color.DarkGreen);
                    logs("\nWriting persist : ", Color.Black);
                    string[] add = emmcdl.GetAddress(pn, loader, "persist");
                    fhloader.WriteByAddress(pn, cloud, add);
                    if (fhloader.isOk)
                    {
                        logs("Success\nRebooting", Color.Green);
                        fhloader.Reboot(pn);
                    }
                    else
                    {
                        logs("Fail", Color.Red);
                    }
                }
                else
                {
                    logs("Fail", Color.Red);
                }
            }
            else
            {
                logs("No Port Found", Color.Red);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                davb = true;
            }
            else
            {
                davb = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                skp = true;
            }
            else
            {
                skp = false;
            }
        }

        //Mi Account Remove EU Rom
        private void EURom()
        {
            logs("Searching Device in twrp : ", Color.Black);
            if (Adb.HasConnectedDevice())
            {
                logs("Connected", Color.DarkGreen);
                logs("\nPatching System : ",Color.Black);
                Adb.EUBypass();
                logs("Finish\nRebooting", Color.DarkGreen);
                Adb.Reboot(null);
            }
            else
            {
                logs("No Device", Color.Red);
            }
        }
        //Mi Account Remove Root
        private void MiAccRoot()
        {
            logs("Searching Devices : ", Color.Black);
            if (Adb.HasConnectedDevice())
            {
                logs("Found", Color.DarkGreen);
                logs("\nChecking root : ", Color.Black);
                if (Adb.HasRoot())
                {
                    logs("Rooted",Color.DarkGreen);
                    logs("\nPatching system : ",Color.Black);
                    Adb.MiAccountSignInSignOut(@"bin\f.apk");
                    Adb.Reboot(null);
                }
                else
                {
                    logs("No root\nAborting",Color.Red);
                }
            }
            else
            {
                logs("No Device", Color.Red);
            }
        }

        

        private void ModemPatch()
        {
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\bin\\sfk.exe",Properties.Resources.sfk);
            logs("Patching NON-HLOS.bin : ", Color.Black);
            if (File.Exists(fwp.Text + @"\images\NON-HLOS.bin"))
            {                
                ProcessStartInfo patch = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = Directory.GetCurrentDirectory()+@"\bin\",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "/c sfk.exe replace " + fwp.Text + @"\images\NON-HLOS.bin" + " -binary " + "\""+"/43415244415050/4D415244415050/"+"\"" + " -dump -yes",
                };
                Process.Start(patch).WaitForExit();
                logs("Done", Color.DarkGreen);
            }
            else
            {
                logs("File not found", Color.Red);
            }
        }    

        private void BLU()
        {
            logs("Searching EDL Port : ", Color.Black);
            if (emmcdl.Detect())
            {                
                string p = emmcdl.PortName;
                logs(p, Color.DarkGreen);
                logs("\nSending Loader : ",Color.Black);
                if (emmcdl.SendLoader(p,loader))
                {
                    logs("Success", Color.DarkGreen);
                    logs("\nSending Unlock Data : ", Color.Black);
                    if (emmcdl.SendXML(p, loader, xmlpath))
                    {
                        logs("Finished\nRebooting\nMay need to use Mi Sig Unlock in Fastboot tab.", Color.DarkGreen);
                        fhloader.Reboot(p);
                    }
                    else
                    {
                        logs("Fail", Color.Red);
                    }
                }
                else
                {
                    logs("fail", Color.Red);
                }
            }
            else
            {
                logs("No Port Found", Color.Red);
            }
        }
        #endregion
        #region Fastboot Tab
        public void createdatatable()
        {
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("Operation", typeof(string));
            dt.Columns.Add("Partition", typeof(string));
            dt.Columns.Add("Filename", typeof(string));
        }
        private void fbfler_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (fastboot.isConnected())
            {
                FBInfo();
                if (blu)
                {
                    FBFlash();
                }
                else
                {
                    logs("Bootloader is locked!\nCan't Flash", Color.Red);
                }
            }
            else
            {
                logs("No fastboot devicce", Color.Red);
            }
        }
        private void ParseFlashBat()
        {
            richTextBox1.Clear();
            dataGridView1.Columns.Clear();
            if (fbfwp.Text.Contains("images"))
            {
                using (StreamReader r = new StreamReader(fbfwp.Text + "\\flash_all.bat"))
                {
                    string line;
                    int i = 0;
                    while ((line = r.ReadLine()) != null)
                    {
                        if (line.Contains("flash") && !line.Contains("NONE") && !line.Contains("tool"))
                        {
                            line = line.Replace("\"", " ");
                            string[] ls = line.Split('|');
                            string sf = ls[0].Replace(" %* ", " ");
                            string fs = sf.Replace(" %~dp0", " ");
                            string l = fs.Replace("fastboot ", " ");
                            string fn = l.TrimStart();
                            fn = fn.Replace("images", "=");
                            if (fn.Contains(":"))
                            {
                                fn = fn.Replace(":", " ");
                                fn = fn.TrimStart();
                            }
                            if (fn.Contains("\\"))
                            {
                                fn = fn.Replace("\\", " ");
                            }
                            string[] final = fn.Split('=');
                            string cmd = final[0].TrimStart().TrimEnd() + " \\images\\" + final[1].TrimStart().TrimEnd();                            
                            if (cmd.Contains("%~dp0 "))
                            {
                                cmd = cmd.Replace("%~dp0 ", " ");
                                string[] d = cmd.Split(' ');
                                dt.Rows.Add(d[0].Trim(), d[1].Trim(), fbfwp.Text + d[2].Trim());
                            }
                            else
                            {
                                string[] d = cmd.Split(' ');
                                dt.Rows.Add(d[0].Trim(), d[1].Trim(), fbfwp.Text + d[2].Trim());
                            }                            
                            i++;
                        }
                    }                    
                }
            }
            else
            {
                MessageBox.Show("Not Xiaomi Fastboot firmware folder type." + Environment.NewLine + "No images folder found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                fbfwp.Text = "Double click to load firmware";
            }
        }
        private void FBInfo()
        {
            if (fastboot.isConnected())
            {
                File.WriteAllText(@"bin\fb.cmd", Properties.Resources.fb);
                Process fd = new Process();
                fd.StartInfo.FileName = "cmd.exe";
                fd.StartInfo.Arguments = "/c fb.cmd";
                fd.StartInfo.UseShellExecute = false;
                fd.StartInfo.CreateNoWindow = true;
                fd.StartInfo.WorkingDirectory = @"bin";
                fd.StartInfo.RedirectStandardOutput = true;
                fd.Start();
                fd.WaitForExit();
                File.Delete(@"bin\fb.cmd");
                using (StreamReader r = new StreamReader(@"bin\f.txt"))
                {
                    string line;
                    int i = 0;
                    while ((line = r.ReadLine()) != null)
                    {
                        if (line.Contains("unlocked"))
                        {
                            line = line.Replace("(bootloader) ", " ").TrimStart();
                            string[] pd = line.Split(':');
                            logs("Unlocked : ", Color.Black);
                            if (pd[1].Contains("yes"))
                            {
                                logs(pd[1].TrimStart().TrimEnd().ToUpper(), Color.DarkGreen);
                                blu = true;
                            }
                            else
                            {
                                logs(pd[1].TrimStart().TrimEnd(), Color.Red);
                                blu = false;
                            }
                        }
                        if (line.Contains("product"))
                        {
                            line = line.Replace("(bootloader) ", " ").TrimStart();
                            string[] pd = line.Split(':');
                            logs(Environment.NewLine+"Product : ", Color.Black);
                            logs(pd[1].TrimStart().TrimEnd(), Color.DarkGreen);
                        }
                        if (line.Contains("slot-count"))
                        {
                            line = line.Replace("(bootloader) ", " ").TrimStart();
                            string[] pd = line.Split(':');
                            logs(pd[0] + " : ", Color.Black);
                            logs(pd[1].TrimStart().TrimEnd(), Color.DarkGreen);
                            string slc = pd[1].TrimStart().TrimEnd();
                            if (!slc.Contains("0"))
                            {
                                isdual = true;
                            }
                        }
                        if (!line.Contains("config:"))
                        {
                            isfrp = true;
                        }
                        i++;
                    }
                }
            }
            else
            {
                logs("No Fastboot device", Color.Red);
            }
        }
        private void FBFlash()
        {
            this.Cursor = Cursors.WaitCursor;
            logs("\n\nErasing boot : ", Color.Black);
            fastboot.erase("boot");
            logs("Done", Color.DarkGreen);
            logs(Environment.NewLine + "Erasing metadata : ", Color.Black);
            fastboot.erase("metadata");
            logs("Done", Color.DarkGreen);
            int i = 0;
            while (i < dt.Rows.Count)
            {                                
                var pt=dt.Rows[i][1];
                var fn = dt.Rows[i][2];
                logs("\n" + DateTime.Now, Color.DarkGreen);
                logs(" - Writing ", Color.Black);
                logs(pt.ToString() + ": ",Color.DarkBlue);
                if (skp)
                {
                    if (pt.ToString().Contains("preloader"))
                    {
                        logs("Preloader Skipped!",Color.Red);
                    }
                }
                else
                {
                    fastboot.flash(pt.ToString(), fn.ToString());
                    logs("Done", Color.DarkGreen);
                }
                if (pt.ToString().Contains("vbmeta"))
                {
                    if (davb)
                    {
                        fastboot.process("--disable-verity --disable-verification flash vbmeta " + fn.ToString());
                        logs("Done", Color.DarkGreen);
                    }
                    else
                    {
                        fastboot.flash(pt.ToString(), fn.ToString());
                        logs("Done", Color.DarkGreen);
                    }
                }
                i++;
            }
            if (isdual)
            {
                logs(Environment.NewLine + "Dual slot device detect :" + Environment.NewLine + "Setting active slot to A :", Color.Red);
                fastboot.process("set_active a");
            }            
            logs(Environment.NewLine + Environment.NewLine + "Developed By :", Color.Black);
            logs("Kyaw Khant Zaw", Color.DarkGreen);
            fastboot.process("reboot");
            this.Cursor = Cursors.Default;
        }
        #endregion
        #region MTK
        public void MTK(string Arg)
        {
            ProcessStartInfo i = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/c python mtk "+Arg,
                WorkingDirectory = Directory.GetCurrentDirectory()+@"\bin\MTK\mtkclient",
                CreateNoWindow =true,
                UseShellExecute = false,
                RedirectStandardOutput=true,                
            };
            Process m = new Process();
            m.StartInfo = i;
            m.OutputDataReceived += outputHandler;
            m.Start();
            m.BeginOutputReadLine();
        }
        private void outputHandler(object sendingProcess, DataReceivedEventArgs outline)
        {
            if (!string.IsNullOrEmpty(outline.Data) && !outline.Data.ToString().Contains("payload"))
            {
                if (outline.Data.Contains("Preloader"))
                {
                    string s = outline.Data.ToString();
                    s = s.Replace("Preloader -", " ");
                    Invoke(new MethodInvoker(delegate { richTextBox1.AppendText(s.TrimStart() + Environment.NewLine); }));
                }
                if (outline.Data.Contains("Successfully"))
                {
                    Invoke(new MethodInvoker(delegate { richTextBox1.AppendText(outline.Data.TrimStart() + Environment.NewLine); }));
                }
                if (outline.Data.Contains("wrote") || outline.Data.Contains("formatted") || outline.Data.Contains("error"))
                {
                    Invoke(new MethodInvoker(delegate { richTextBox1.AppendText(outline.Data.TrimStart() + Environment.NewLine); }));
                }
            }            
        }
        #endregion
    }
    #region RichTextBoxExtension
    public static class RichTextBoxExtension
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
    #endregion
}
