using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.StringTransformations;
using static BachelorProjectHydrogen.Steps;

namespace BachelorProjectHydrogen
{
    public partial class ManualCommand : Form
    {
        public ManualCommand()
        {
            InitializeComponent();
        }

        public static string manualCommand = "";

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // 0 button
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            manualCommand += "0";
            textBox1.Text = manualCommand;
        }

        // 1 button
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            manualCommand += "1";
            textBox1.Text = manualCommand;
        }

        // clear one character from manual command textbox
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (manualCommand.Length > 0)
            {
                manualCommand = manualCommand.Remove(manualCommand.Length - 1, 1);
            }
            textBox1.Text = manualCommand;
        }
    

        // clear everything from txtbox
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            manualCommand = string.Empty;         
        }

        // send command to comport 
        private void button5_Click(object sender, EventArgs e)
        {

            WriteToComPort(ConvertBinaryString(manualCommand));
               
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
