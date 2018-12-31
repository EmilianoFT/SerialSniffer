using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SerialSniffer;
namespace SerialSniffer
{
    public partial class frmMain : Form
    {
        CommunicationManager comm = new CommunicationManager();
        CommunicationManager comm2 = new CommunicationManager();
        string transType = string.Empty;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            comm.SetRComm(comm2);
            comm2.SetRComm(comm);
            LoadValues();
            SetDefaults();
            SetControlState();
        }

        private void cmdOpen_Click(object sender, EventArgs e)
        {
            comm.PortName = cboPort.Text;
            comm.Parity = cboParity.Text;
            comm.StopBits = cboStop.Text;
            comm.DataBits = cboData.Text;
            comm.BaudRate = cboBaud.Text;
            comm.DisplayWindow = rtbDisplay;
            comm.OpenPort();

            comm2.PortName = cboPort2.Text;
            comm2.Parity = cboParity2.Text;
            comm2.StopBits = cboStop2.Text;
            comm2.DataBits = cboData2.Text;
            comm2.BaudRate = cboBaud2.Text;
            comm2.DisplayWindow = rtbDisplay2;
            comm2.OpenPort();

            cmdOpen.Enabled = false;
            cmdClose.Enabled = true;
        }

        /// <summary>
        /// Method to initialize serial port
        /// values to standard defaults
        /// </summary>
        private void SetDefaults()
        {
            cboPort.SelectedIndex = 0;
            cboBaud.SelectedText = "9600";
            cboParity.SelectedIndex = 0;
            cboStop.SelectedIndex = 1;
            cboData.SelectedIndex = 1;

            cboPort2.SelectedIndex = 0;
            cboBaud2.SelectedText = "9600";
            cboParity2.SelectedIndex = 0;
            cboStop2.SelectedIndex = 1;
            cboData2.SelectedIndex = 1;
        }

        /// <summary>
        /// methos to load our serial
        /// port option values
        /// </summary>
        private void LoadValues()
        {
            comm.SetPortNameValues(cboPort);
            comm.SetParityValues(cboParity);
            comm.SetStopBitValues(cboStop);

            comm2.SetPortNameValues(cboPort2);
            comm2.SetParityValues(cboParity2);
            comm2.SetStopBitValues(cboStop2);
        }

        /// <summary>
        /// method to set the state of controls
        /// when the form first loads
        /// </summary>
        private void SetControlState()
        {
            rdoText.Checked = true;
            cmdClose.Enabled = false;
        }

        private void rdoHex_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHex.Checked == true)
            {
                comm2.CurrentTransmissionType = comm.CurrentTransmissionType = SerialSniffer.CommunicationManager.TransmissionType.Hex;
            }
            else
            {
                comm2.CurrentTransmissionType = comm.CurrentTransmissionType = SerialSniffer.CommunicationManager.TransmissionType.Text;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {      
            comm.ClosePort();
            comm2.ClosePort();

            cmdOpen.Enabled = true;
            cmdClose.Enabled = false;
        }
    }
}