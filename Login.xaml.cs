using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Supermarket.DBHelper;
using MySql.Data.MySqlClient;
using MySqlHelper = MySql.Data.MySqlClient.MySqlHelper;

namespace Supermarket
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #region 事件
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                if (SLogin())
                {
                    MessageBoxResult dr = MessageBox.Show("登录成功", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                    }
                   
                }
                else {
                    MessageBoxResult dr = MessageBox.Show("登录失败", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
            }

        }
        #endregion

        #region 方法
        /// <summary>
        /// 非空验证
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (userName.Text.Trim() == "" || passWord.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名和密码");
                return false;
            }
            return true;
        }


        #region 登录
        public bool SLogin()
        {
            bool flag = false;
            DbMySqlHelper mySqlHelper = new DbMySqlHelper();
            string userString = userName.Text.Trim();
            string pwdString = passWord.Text.Trim();
            //创建sql
            try
            {
                string sql = string.Format("select * from ec_user where username = '{0}' and passwd = '{1}'", userString, pwdString);
                DataTable mysqlData  = mySqlHelper.getMySqlRead(sql);

                if (mysqlData.Rows.Count >= 1)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常 " +ex.Message);
                flag = false;
            }
            
            return flag;
        } 
        #endregion
        #endregion
    }
}
