using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.ClientSocket;
using static BachelorProjectHydrogen.ManualCyclogramMethods;
using static BachelorProjectHydrogen.StringTransformations;
using static BachelorProjectHydrogen.OtherTransformations;
using System.IO;
using System.Security;

namespace BachelorProjectHydrogen
{
    public partial class ManualCyclograms : Form
    {
        public ManualCyclograms()
        {
            InitializeComponent();
        }

        
        public static string commandString = "";
        public static string timeIntervalString = "";
        public static string iteration;
        public static int stepTimeIntervals;
        public static int stepTime;
        public static Timer timer;
        public static int howManyCyclesToRun;
        public static string zeros = "00000000000000";
        


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        // add rows and columns button
        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = textBox1.Text;
            rows = Convert.ToInt32(text);
            cols = 18;
            textBox1.Text = "";          
            addButtons(cols, rows);
            tableLayoutPanel1.Visible = true;
            
        }

        private void addButtons(int cols, int rows)
        {

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    
                    tableLayoutPanel1.Controls.Add(GetTextBox(), i, j);
                }
            }
        }
        private TextBox GetTextBox()
        {

            var textBox = new TextBox();
            tableLayoutPanel1.Controls.Add(textBox);
            textBox.BackColor = Color.FromArgb(45, 45, 45);
            textBox.ForeColor = Color.WhiteSmoke;
            textBox.BorderStyle = BorderStyle.None;

            textBox.TextChanged += (s, e) => commandString += textBox.Text;
            
            return textBox;
        }

        // send cyclogram to com port btn

        private void button3_Click(object sender, EventArgs e)
        {
            
            SendStringToComAndSocketServer(SeperateString(commandString));
            
        }

        // save cyclogram in text file btn
        private void button4_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textBox3.Visible = true;
            
            button2.Visible = true;
        }

        // save cyclogram in file

        private void button2_Click(object sender, EventArgs e)
        {
            iteration = textBox3.Text;
            
            SaveToLogFile(SeperateString(commandString), iteration);
        }

        // back to main form poga
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // set time intervals btn
        private void button6_Click(object sender, EventArgs e)
        {
            string stepTimeIntervalsString = textBox4.Text;
            stepTimeIntervals = Convert.ToInt32(stepTimeIntervalsString);
            
        }

        // open file folder
        private void button7_Click(object sender, EventArgs e)
        {
 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GetFromFile();
                    addButtonsFromFile(18, CountLines());
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine($"Error message: {ex.Message}" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

        }

        private void addButtonsFromFile(int cols, int rows)
        {
            
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    tableLayoutPanel1.Controls.Add(GetTextBox(),i,j);                   
                    
                }
            }
        }

        private void GetFromFile()
        {
            var sr = new StreamReader(openFileDialog1.FileName);
            string allCommandsFromFile = "";
            List<string> commands = new List<string>();
            for (int i = 0; i < CountLines(); i++)
            {
                allCommandsFromFile = sr.ReadLine();
                commands.Add(allCommandsFromFile);                
            }
 
            
            int charAmount = allCommandsFromFile.Count()/CountLines();
            
            
            for (int i = 0; i < allCommandsFromFile.Length; i += charAmount )
            {
                
                commands.Add(allCommandsFromFile.Substring(i, charAmount));                            
            }
            
            SendStringToComAndSocketServer(commands);
            
        }

        private int CountLines()
        {
            var sr = new StreamReader(openFileDialog1.FileName);
            int lineCount = 0;
            while (sr.ReadLine() != null)
            {
                
                lineCount++;
            }
            return lineCount;
        }

        // add new row button

        private void button9_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {  
            for (int i = 0; i < cols; i++)
            {
                tableLayoutPanel1.Controls.Add(GetTextBox());
                
            }
        }
        private TextBox GetTimeTextBox()
        {
            var textBoxTime = new TextBox();
            tableLayoutPanel2.Controls.Add(textBoxTime);
            textBoxTime.BackColor = Color.FromArgb(45,45,45);
            textBoxTime.ForeColor = Color.WhiteSmoke;
            textBoxTime.BorderStyle = BorderStyle.None;
            textBoxTime.TextChanged += (s, e) => timeIntervalString += textBoxTime.Text;

            return textBoxTime;
        }
        private void AddTimeColumn()
        {
            for(int i = 0; i < rows; i++)
            {
                tableLayoutPanel2.Controls.Add(GetTimeTextBox());
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            button7.Click += new EventHandler(button7_Click);
            
        }

        // set time intervals for each step
        private void button8_Click(object sender, EventArgs e)
        {
            AddTimeColumn();
            label2.Visible = true;

        }

        private void ManualCyclograms_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<string> timeList = new List<string>();
            List<string> commandList = new List<string>();
            timeList = SeperateTimeString(timeIntervalString);
            commandList = SeperateString(commandString);
            TimeList(timeList, commandList);
            
        }

      
    

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private async void SendStringToComAndSocketServer(List<string> commands)
        {
            foreach (string s in commands)
            {

                WriteToComPort(ConvertBinaryString(s));
                SendDataToSocketServer(s);
                await Task.Delay(fromSecondsToMs(stepTimeIntervals));
            }
        }
    }
}
