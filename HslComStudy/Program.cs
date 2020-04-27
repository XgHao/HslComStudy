using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HslComStudy
{
    static class Program
    {
        /// <summary>
        /// 1=中文
        /// 2=英文
        /// </summary>
        public static int Language = 1;

        /// <summary>
        /// 是否显示相关的信息
        /// </summary>
        public static bool ShowAuthorInfomation = true;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Siemens(HslCommunication.Profinet.Siemens.SiemensPLCS.S200Smart));
        }
    }
}
