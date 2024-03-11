using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace MakingTimeTable_ver0._2
{
    public partial class Login : Form
    {
        public Login()
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var result = client.Get("가입자 명단/" + tboxUsername.Text);
            Upload upd = result.ResultAs<Upload>();

            if(upd == null || tboxPassword.Text != upd.password)
            {
                MessageBox.Show("아이디 혹은 비밀번호가 잘못되었습니다.");
                return;
            }
            else
            {
                MessageBox.Show("로그인 성공");
            }
            throw new Exception();
        }

        private void lblSignup_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.ShowDialog();
        }

        private void lblForgot_Click(object sender, EventArgs e)
        {

        }
    }
}
