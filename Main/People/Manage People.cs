using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Main
{
    public partial class Manage_People : Form
    {
        public Manage_People()
        {
            InitializeComponent();
        }
        private DataTable _PeopleTable;

        private void _RefreshList()
        {
            _PeopleTable = PersonServices.GetAllPeople();
            dataGridView1.DataSource = _PeopleTable;
            Records_Count.Text = "Records Count: " + dataGridView1.Rows.Count.ToString();
        }

        private string _FilterColumnName()
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Person ID": return "PersonID";
                case "National No.": return "NationalNO";
                case "First Name": return "FirstName";
                case "Second Name": return "SecondName";
                case "Third Name": return "ThirdName";
                case "Last Name": return "LastName";
                case "Nationality": return "Nationality";
                case "Gender": return "Gendor";
                case "Phone": return "Phone";
                case "Email": return "Email";
                default: return null;
            }
        }

        // Escapes quotes (string literal) and LIKE wildcards so user text is matched literally.
        private static string _EscapeFilterValue(string text)
        {
            var sb = new System.Text.StringBuilder();
            foreach (char c in text)
            {
                if (c == '\'')
                    sb.Append("''");
                else if (c == '[' || c == ']' || c == '*' || c == '%')
                    sb.Append('[').Append(c).Append(']');
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private void _ApplyFilter()
        {
            if (_PeopleTable == null)
                return;

            if (comboBox1.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                _PeopleTable.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                string column = _FilterColumnName();
                string searchText = _EscapeFilterValue(textBox1.Text);
                _PeopleTable.DefaultView.RowFilter = $"Convert({column}, 'System.String') LIKE '%{searchText}%'";
            }

            Records_Count.Text = "Records Count: " + dataGridView1.Rows.Count.ToString();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
                textBox1.Text = string.Empty;
            }
            else
            {
                textBox1.Visible = true;
            }
            _ApplyFilter();
        }

        private void Manage_People_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            _RefreshList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ShowAddEdit(int personID)
        {
            using (Add_Edit frm = new Add_Edit(personID))
                frm.ShowDialog();
            _RefreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ShowAddEdit(-1);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowAddEdit(-1);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (selectedRow.Cells["PersonID"].Value != DBNull.Value && selectedRow.Cells["PersonID"].Value != null)
                {
                    int personID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);

                    _ShowAddEdit(personID);
                }
            }
            else
            {
                MessageBox.Show("Please select a row first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void ShowDetailsToolStripMenu_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells["PersonID"].Value != DBNull.Value && selectedRow.Cells["PersonID"].Value != null)
                {
                    int personID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);
                    Person_Details frm = new Person_Details(personID);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a row first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells["PersonID"].Value != DBNull.Value && selectedRow.Cells["PersonID"].Value != null)
                {
                    int personID = Convert.ToInt32(selectedRow.Cells["PersonID"].Value);
                    var confirmResult = MessageBox.Show("Are you sure to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        if (PersonServices.DeletePerson(personID))
                        {
                            MessageBox.Show("Person deleted successfully.", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete person because he is associated with other records.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        _RefreshList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
