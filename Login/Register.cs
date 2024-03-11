using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakingTimeTable_ver0._2
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        FirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = "0tS4xrpqmY7BfWImjf9CnCeTNHGzPD43MyoyF9Oh",
            BasePath = "https://makingtimetableregister-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(fbc);
            }
            catch
            {
                MessageBox.Show("문제 발생");
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool checkError(bool error, string errormessage)
        {
            if (error)
            {
                lblError.Text = errormessage;
                return true;
            }
            else
            {
                lblError.Text = "";
                return false;
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(checkError(tboxName.Text == "", "Please fill out your name"))
            {
                return;
            }
            if(checkError(tboxUniversity.Text == "", "Please fill out your university"))
            {
                return;
            }
            if(checkError(tboxUsername.Text == "", "Please fill out your username"))
            {
                return;
            }
            if(checkError(tboxUsername.Text.Length < 6 || tboxUsername.Text.Length >= 20, "Username must be at least 6 or less than 20 characters."))
            {
                return;
            }
            if (checkError(tboxPassword.Text == "", "Please fill out your password"))
            {
                return;
            }
            if (checkError(tboxPassword.Text.Length < 6 || tboxPassword.Text.Length >= 20, "Password must be at least 6 or less than 20 characters."))
            {
                return;
            }
            if(checkError(tboxPassword.Text != tboxConfirmPassword.Text, "Password does not match"))
            {
                return;
            }

            var result = client.Get("가입자 명단/" + tboxUsername.Text);
            Upload upd1 = result.ResultAs<Upload>();
            Upload upd2 = new Upload()
            {
                name = tboxName.Text,
                university = tboxUniversity.Text,
                username = tboxUsername.Text,
                password = tboxPassword.Text
            };
            var send = client.Set("가입자 명단/" + tboxUsername.Text, upd2);
            MessageBox.Show("회원가입 완료");
            this.Close();
        }
    }
}
