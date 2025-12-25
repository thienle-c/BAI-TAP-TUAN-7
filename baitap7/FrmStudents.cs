using baitap7.model;
using SchoolDB.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitap7
{
    public partial class FrmStudents : Form
    {
        private SchoolContext db = new SchoolContext();

        public FrmStudents()
        {
            InitializeComponent();
            LoadData();
            BindControls();                             
            dgvStudents.CellClick += dgvStudents_CellClick;
        }

        private void LoadData()
        {
            bsStudents.DataSource = db.Students.ToList();
            dgvStudents.DataSource = bsStudents;
        }

        private void BindControls()
        {
            txtFullName.DataBindings.Clear();
            txtAge.DataBindings.Clear();
            cboMajor.DataBindings.Clear();

            txtFullName.DataBindings.Add("Text", bsStudents, "FullName");
            txtAge.DataBindings.Add("Text", bsStudents, "Age");
            cboMajor.DataBindings.Add("Text", bsStudents, "Major");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student s = new Student
            {
                FullName = txtFullName.Text,
                Age = int.Parse(txtAge.Text),
                Major = cboMajor.Text
            };

            db.Students.Add(s);
            db.SaveChanges();

            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student s = bsStudents.Current as Student;
            if (s == null) return;

            db.Students.Remove(s);
            db.SaveChanges();

            LoadData();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;

            txtFullName.Text = dgvStudents.CurrentRow.Cells["FullName"].Value.ToString();
            txtAge.Text = dgvStudents.CurrentRow.Cells["Age"].Value.ToString();
            cboMajor.Text = dgvStudents.CurrentRow.Cells["Major"].Value.ToString();
            if (e.RowIndex < 0) return;

            currentIndex = e.RowIndex;
            DisplayStudent(currentIndex);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Student s = bsStudents.Current as Student;
            if (s == null) return;

            db.SaveChanges();
            LoadData();
        }

        private void DisplayStudent(int index)
        {
            if (dgvStudents.Rows.Count == 0) return;

            if (index < 0) index = 0;
            if (index >= dgvStudents.Rows.Count)
                index = dgvStudents.Rows.Count - 1;

            currentIndex = index;

            dgvStudents.ClearSelection();
            dgvStudents.Rows[currentIndex].Selected = true;

            txtFullName.Text = dgvStudents.Rows[currentIndex].Cells["FullName"].Value.ToString();
            txtAge.Text = dgvStudents.Rows[currentIndex].Cells["Age"].Value.ToString();
            cboMajor.Text = dgvStudents.Rows[currentIndex].Cells["Major"].Value.ToString();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            bsStudents.MoveNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bsStudents.MovePrevious();
        }


    }
}
