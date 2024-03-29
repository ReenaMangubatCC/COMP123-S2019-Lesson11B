﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11B
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is the event handler for the MainForm's FormClosing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// This is the event handler for the Exit Menu Item's click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sectionCDatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.sectionCDatabaseDataSet.StudentTable);

        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            var StudentList =
                from student in this.sectionCDatabaseDataSet.StudentTable
                select student;

            foreach(var student in StudentList.ToList())
            {
                Debug.WriteLine("Student ID: " + student.StudentID + "Last Name: " + student.LastName);
            }
        }

        private void StudentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //local scope aliases
            var rowIndex = StudentDataGridView.CurrentCell.RowIndex;
            var rows = StudentDataGridView.Rows;
            var columnCount = StudentDataGridView.ColumnCount;
            var cells = rows[rowIndex].Cells;

            rows[rowIndex].Selected = true;

            string outputString = string.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }

            SelectionLabel.Text = outputString;

            Program.student.id = int.Parse(cells[(int)StudentField.ID]. Value.ToString());
            Program.student.StudentID = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // configure the file dialog
            StudentSaveFileDialog.FileName = "Student.txt";
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            // open file dialog
            var result = StudentSaveFileDialog.ShowDialog();

            // open file dialog - Modal Form
            if (result != DialogResult.Cancel)
            {
                using (StreamWriter outputString = new StreamWriter(
                    File.Open("Student.txt", FileMode.Create)))
                {
                    // write stuff to the file
                    outputString.WriteLine(Program.student.id);
                    outputString.WriteLine(Program.student.StudentID);
                    outputString.WriteLine(Program.student.FirstName);
                    outputString.WriteLine(Program.student.LastName);

                    // clean up
                    outputString.Close();
                    outputString.Dispose();
                }

                MessageBox.Show("File Saved", "Saving...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.studentInfoForm.Show();
            this.Hide();
        }
    }
}
