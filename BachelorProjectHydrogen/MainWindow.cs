using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BachelorProjectHydrogen.ClientSocket;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.ComCommands;
using static BachelorProjectHydrogen.ComPort;
using static BachelorProjectHydrogen.StringTransformations;
using static BachelorProjectHydrogen.OtherTransformations;
using System.IO.Ports;


namespace BachelorProjectHydrogen
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
            
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);

        }
       
        bool checkBox1Changed = false;
        bool checkBox2Changed = false;
        public static string manualCommand = "";
        public static string sendStepsCommandString = "00000111";

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1Changed = true;
            panel1.Visible = checkBox1.Checked;
            textBox3.Visible = true;
            button10.Visible = true;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2Changed = true;
            panel2.Visible = checkBox2.Checked;
            textBox3.Visible = true;
            button10.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // add cycles input
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string text = textBox1.Text;
            //cycleAmount = Convert.ToInt32(text);
            //Console.WriteLine(cycleAmount);
        }

        private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // add target pressure input
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            targetPressure = Convert.ToInt32(text);
        }

     
        /*
        private void textBox3_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("Only numbers allowed");
            }
        }
        */
       

        // add cycle amount button
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            cycleAmount = Convert.ToInt32(text);
            textBox1.Text = "";
            Console.WriteLine("Set cycles: ");
            Console.WriteLine(cycleAmount);
            tableLayoutPanel2.Visible = true;
           
        }

        // add target pressure button
        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            targetPressure = Convert.ToDouble(text);
            textBox2.Text = "";
            Console.WriteLine("Set target pressure: ");
            Console.WriteLine(targetPressure);
            tableLayoutPanel2.Visible=true;
        }

        // exit button
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // time sync button
        private void button3_Click(object sender, EventArgs e)
        {

            WriteToComPort(TimeSyncComCommand());
            
        }

        // add all components btn
        private void button4_Click(object sender, EventArgs e)
        {
           
            InitializationCommand();
            WriteToComPort(InitializationFinishedCom());

        }

        // reset all components btn
        private void button6_Click(object sender, EventArgs e)
        {
            WriteToComPort(ResetAll());
            
        }

        // start compression cycle btn
        private void button5_Click(object sender, EventArgs e)
        {
            if(checkBox1Changed)
            {

                SendAllCommandsToComPortAndServer(fromSecondsToMs(timeInterval));
                
            }
            else
            {
                
                SendAllCommandsToComPortAndServerWithTargetPres(fromSecondsToMs(timeInterval));
            }

        }

        private void AppMode_Click(object sender, EventArgs e)
        {

        }

        // connect to com port 
        private void button9_Click(object sender, EventArgs e)
        {
            
            ConnectToComPort(_serialPort, "COM8");
            if (connectedToComPort)
            {
                textBox5.Visible = true;
            }

        }

        // set time intervals button
        private void button10_Click(object sender, EventArgs e)
        {
            string text = textBox3.Text;
            timeInterval = Convert.ToInt32(text);
            textBox3.Text = "";
            Console.WriteLine("Time intervals: ");
            Console.WriteLine(timeInterval);
        }

        // connect to socket button
        private void button11_Click(object sender, EventArgs e)
        {
            ConnectToServerSocket();
            textBox4.Visible = true;
        }

        // com port connected textbox
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        // send to com port manual command btn

        private void button14_Click(object sender, EventArgs e)
        {

            //WriteToComPort(ConvertBinaryString(sendStepsCommandString + manualCommand));
            //Send(sendStepsCommandString + manualCommand);           
            
        }



        // create manual command button
        private void button17_Click(object sender, EventArgs e)
        {

            ManualCommand f2 = new ManualCommand();
            f2.Show();
 
        }

        // create cyclogram button
        private void button7_Click(object sender, EventArgs e)
        {
            ManualCyclograms cyclo = new ManualCyclograms();
            cyclo.Show();
        }

    
    }
}
