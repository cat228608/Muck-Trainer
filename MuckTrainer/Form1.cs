using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace MuckTrainer
{
    public partial class Form1 : Form
    {
        public Mem m = new Mem();
        private BackgroundWorker BGW = new BackgroundWorker();
        private GlobalKeyboardHook _keyboardHook;

        string playerStatusPtrString = "UnityPlayer.dll+017A35C0,3B8,38,18,8,198,0,780";
        public const string PROCESS_NAME = "Muck.exe";

        public Form1()
        {
            InitializeComponent();

            _keyboardHook = new GlobalKeyboardHook();

            _keyboardHook.HookKey(Keys.NumPad1, () => { this.Invoke((MethodInvoker)delegate { chkGodMode.Checked = !chkGodMode.Checked; }); });
            _keyboardHook.HookKey(Keys.NumPad2, () => { this.Invoke((MethodInvoker)delegate { chkInfStamina.Checked = !chkInfStamina.Checked; }); });
            _keyboardHook.HookKey(Keys.NumPad3, () => { this.Invoke((MethodInvoker)delegate { chkNoHunger.Checked = !chkNoHunger.Checked; }); });
            _keyboardHook.HookKey(Keys.NumPad4, () => { this.Invoke((MethodInvoker)delegate { chkInfJumps.Checked = !chkInfJumps.Checked; }); });
            _keyboardHook.HookKey(Keys.NumPad5, () => { this.Invoke((MethodInvoker)delegate { chkSuperSpeed.Checked = !chkSuperSpeed.Checked; }); });
            _keyboardHook.HookKey(Keys.NumPad6, () => { this.Invoke((MethodInvoker)delegate { chkInfShield.Checked = !chkInfShield.Checked; }); });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var checkBox in this.Controls.OfType<CheckBox>())
            {
                checkBox.CheckedChanged += AnyCheckbox_CheckedChanged;
                UpdateCheckboxText(checkBox);
            }

            BGW.DoWork += BGW_DoWork;
            BGW.RunWorkerAsync();
        }

        private void AnyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox chk)
            {
                UpdateCheckboxText(chk);
            }
        }

        private void UpdateCheckboxText(CheckBox chk)
        {
            string baseText = chk.Tag?.ToString() ?? chk.Text.Split('[')[0].Trim();
            if (chk.Tag == null) chk.Tag = baseText;

            chk.Text = $"{baseText} {(chk.Checked ? "[ВКЛ]" : "[ВЫКЛ]")}";
        }

        private void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!m.Attach(PROCESS_NAME))
                {
                    this.Invoke((MethodInvoker)delegate { lblStatus.Text = "Статус: Ожидание игры..."; });
                    Thread.Sleep(1000);
                    continue;
                }

                this.Invoke((MethodInvoker)delegate { lblStatus.Text = "Статус: Игра найдена!"; });

                while (m.Attach(PROCESS_NAME))
                {
                    long playerStatusAddr = FindDMAAddy(playerStatusPtrString);

                    if (playerStatusAddr != 0)
                    {
                        if (chkGodMode.Checked) m.WriteFloat(playerStatusAddr + 0x50, 600f);
                        if (chkInfStamina.Checked) m.WriteFloat(playerStatusAddr + 0x64, 100f);
                        if (chkNoHunger.Checked) m.WriteFloat(playerStatusAddr + 0x6C, 100f);
                        if (chkInfShield.Checked) m.WriteFloat(playerStatusAddr + 0x5C, 100f);

                        long playerMovementPtr = m.ReadInt64(playerStatusAddr + 0x18);
                        if (playerMovementPtr != 0)
                        {
                            if (chkInfJumps.Checked) m.WriteByte(playerMovementPtr + 0x7C, 1);

                            if (chkSuperSpeed.Checked)
                            {
                                m.WriteFloat(playerMovementPtr + 0x6C, 3500f);
                            }
                            else
                            {
                                float currentSpeed = m.ReadFloat(playerMovementPtr + 0x6C);
                                if (currentSpeed > 400f)
                                {
                                    m.WriteFloat(playerMovementPtr + 0x6C, 350f);
                                }
                            }
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }

        private long FindDMAAddy(string pointerString)
        {
            try
            {
                string[] parts = pointerString.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                string[] offsetsStr = parts.Last().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var module = m.GetModule(parts[0]);
                if (module == null) return 0;
                long address = module.BaseAddress.ToInt64();
                address += Convert.ToInt64(offsetsStr[0], 16);
                for (int i = 1; i < offsetsStr.Length; i++)
                {
                    address = m.ReadInt64(address);
                    if (address == 0) return 0;
                    address += Convert.ToInt64(offsetsStr[i], 16);
                }
                return address;
            }
            catch { return 0; }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _keyboardHook?.Dispose();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }

}
