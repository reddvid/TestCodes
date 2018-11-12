using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDGateSystem.Forms
{
    public partial class frmMain : Form
    {
        const string rfidKey = "1Fog66";
        string data;
        Array Ports;
        Array ports2;
        int[] baudRates = new int[] { 9600, 19200, 38400, 57600, 115200 };
        DateTime dateTime;
        SqlConnection sqlCon;
        string sId1;
        string sId2;
        string query;
        bool isConnected;
        delegate void SetTextCallback(string text);

        public frmMain()
        {
            InitializeComponent();

            isConnected = false;

            LoadComPorts();

            LoadBaudRates();
        }

        private void ChangeButtonState(bool isConnected)
        {
            if (isConnected)
            {
                Disconnect();

                btnConnect.Text = "Connect";

                isConnected = false;

                // Enable state of comboboxes
                cbPort1.Enabled = cbPort2.Enabled = cbBaudRate.Enabled = true;
            }
            else
            {
                Connect();

                btnConnect.Text = "Disconnect";

                isConnected = true;

                // Disable state of comboboxes
                cbPort1.Enabled = cbPort2.Enabled = cbBaudRate.Enabled = false;
            }
        }

        private void Connect()
        {
            if (cbPort1.SelectedItem != null || cbPort2.SelectedItem != null)
            {
                if (!serialPort1.IsOpen || !serialPort2.IsOpen)
                {
                    try
                    {
                        serialPort1.PortName = cbPort1.Text;
                        serialPort1.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                        serialPort1.Parity = System.IO.Ports.Parity.None;
                        serialPort1.StopBits = System.IO.Ports.StopBits.One;
                        serialPort1.DataBits = 8;
                        serialPort1.Open();

                        serialPort2.PortName = cbPort2.Text;
                        serialPort2.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                        serialPort2.Parity = System.IO.Ports.Parity.None;
                        serialPort2.StopBits = System.IO.Ports.StopBits.One;
                        serialPort2.DataBits = 8;
                        serialPort2.Open();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Select a valid COM port!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Disconnect()
        {
            try
            {
                serialPort1.Close();
                serialPort2.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void LoadBaudRates()
        {
            foreach (var rate in baudRates)
            {
                cbBaudRate.Items.Add(rate.ToString());
            }

            // Select default
            cbBaudRate.SelectedIndex = 0;
        }

        private void LoadComPorts()
        {
            Ports = System.IO.Ports.SerialPort.GetPortNames();
            ports2 = System.IO.Ports.SerialPort.GetPortNames();

            if (Ports.Length != 0 || ports2.Length != 0)
            {
                foreach (var p in Ports)
                {
                    cbPort1.Items.Add(p);
                }

                foreach (var p in ports2)
                {
                    cbPort2.Items.Add(p);
                }

                // Select defaults
                try
                {
                    cbPort1.SelectedIndex = 0;
                    cbPort2.SelectedIndex = 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                // SHow error in finding COM ports!
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ChangeButtonState(isConnected);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(this, "Are you sure you want to close the application?", "Quit", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            ReceivedTextHandler(serialPort1.ReadExisting());
        }

        private void ReceivedTextHandler(string received)
        {
            if (this.rtbData.InvokeRequired)
            {
                SetTextCallback stcb = new SetTextCallback(ReceivedTextHandler);
                this.Invoke(stcb, new object[] { (received) });
            }
            else
            {
                dateTime = DateTime.Now;
                this.rtbData.Text = $"\t{ received }\t{ dateTime.ToString("MMM,d,yyy h:mm:ss tt") }\n";
                rtbData.SelectionStart = rtbData.TextLength;
                rtbData.ScrollToCaret();
                rtbData.Focus();

                if (received == rfidKey)
                {
                    serialPort2.RtsEnable = true;
                }
                else
                {
                    serialPort2.RtsEnable = false;
                }
            }
        }

        private void cbPort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = cbPort1.Text;
            }
            else
            {
                MessageBox.Show("Make sure the port is not in use.");
            }
        }

        private void cbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.BaudRate = Convert.ToInt32(cbBaudRate.Text);
            }
            else
            {
                MessageBox.Show("Make sure the port is not in use.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbData.Text))
            {
                var result = MessageBox.Show(this, "Are you sure you want to clear data?", "Clearing data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    rtbData.Text = string.Empty;
                }
            }
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            StreamWriter sw;
            s.Filter = ".txt";
            s.CheckPathExists = true;
            s.Title = "Save Report File";
            s.ShowDialog(this);

            try
            {
                sw = System.IO.File.AppendText(s.FileName);
                sw.Write(rtbData.Text);
                await sw.FlushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Show register form
        }
    }
}
