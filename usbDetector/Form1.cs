using System.Runtime.InteropServices;
using System.Windows.Forms;
using UsbDetector;

namespace usbDetector
{
    public partial class Form1 : Form
    {
        private ListBox listBox1;

        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        private const int DBT_DEVTYP_VOLUME = 0x00000002;
        private const int WM_DEVICECHANGE = 0x219;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    switch ((int)m.WParam)
                    {
                        case DBT_DEVICEARRIVAL:
                            int devType = Marshal.ReadInt32(m.LParam, 4);
                            if (devType == DBT_DEVTYP_VOLUME)
                            {
                                listBox1.Items.Add("Usb pod³¹czone");
                                DevBroadcastVolume vol = (DevBroadcastVolume)Marshal.PtrToStructure(m.LParam, typeof(DevBroadcastVolume));
                                listBox1.Items.Add("Maska: " + vol.Mask);
                                listBox1.Items.Add("Litera: " + GetLetter(vol.Mask));
                                listBox1.Items.Add("----------");
                            }
                            break;
                        case DBT_DEVICEREMOVECOMPLETE:
                            listBox1.Items.Add("Usb od³¹czono");
                            listBox1.Items.Add("----------");
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public char GetLetter(int mask)
        {
            int ch = 0;
            for (; ch < 26; ch++)
            {
                if ((mask & 0x1) == 0x1)
                {
                    break;
                }
                mask >>= 1;
            }

            ch += 0x41;
            return (char)ch;
        }

        public Form1()
        {
            InitializeComponent();
            
            listBox1 = new ListBox();
            listBox1.Size = new System.Drawing.Size(250, 250);

            Controls.Add(listBox1);
        }
    }
}
