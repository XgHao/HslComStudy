using HslCommunication;
using HslCommunication.BasicFramework;
using HslCommunication.Profinet.Siemens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HslComStudy
{
    public partial class Siemens : Form
    {
        /// <summary>
        /// 一个西门子的客户端类，使用S7协议来进行数据交互
        /// </summary>
        private SiemensS7Net siemensTcpNet = null;

        public Siemens(SiemensPLCS siemensPLCS)
        {
            InitializeComponent();
            siemensTcpNet = new SiemensS7Net(siemensPLCS);
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Siemens_Load(object sender, EventArgs e)
        {
            //禁用面板
            panel2.Enabled = false;
            //新增曲线
            userCurve1.SetLeftCurve("A", Array.Empty<float>(), Color.Tomato);

            if (!Program.ShowAuthorInfomation)
            {

            }
        }

        /// <summary>
        /// 关闭窗体时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Siemens_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        /// <summary>
        /// 统一的读取结果的数据解析，显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="address"></param>
        /// <param name="textBox"></param>
        private void ReadResultRender<T>(OperateResult<T> result, string address, TextBox textBox)
        {
            if (result.IsSuccess)
            {
                textBox.AppendText($"{DateTime.Now:[HH:mm:ss]}--[{address}]: {result.Content}{Environment.NewLine}");
            }
            else
            {
                MessageBox.Show($"{DateTime.Now:[HH:mm:ss]}--[{address}]读取失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }

        /// <summary>
        /// 统一的数据写入的结果显示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="address"></param>
        private void WriteResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show($"{DateTime.Now:[HH:m:ss]}--[{address}]写入成功");
            }
            else
            {
                MessageBox.Show($"{DateTime.Now:[HH:mm:ss]}--[{address}]写入失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }

        #region 连接与断开

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            //连接

            if (!IPAddress.TryParse(txt_IP.Text, out IPAddress address))
            {
                MessageBox.Show("IP错误");
                return;
            }

            //设置ip
            siemensTcpNet.IpAddress = address.ToString();

            try
            {
                //连接
                OperateResult connect = siemensTcpNet.ConnectServer();
                if (connect.IsSuccess)
                {
                    MessageBox.Show("连接成功");
                    btn_Connect.Enabled = false;
                    btn_DisConnect.Enabled = true;
                    panel2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DisConnect_Click(object sender, EventArgs e)
        {
            //断开连接
            siemensTcpNet.ConnectClose();
            btn_Connect.Enabled = true;
            btn_DisConnect.Enabled = false;
            panel2.Enabled = false;
        }

        #endregion

        #region 单数据读取测试

        /// <summary>
        /// 读取Bool变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Bool_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadBool(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取byte变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Byte_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadByte(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取short变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Short_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt16(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取ushort变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_UShort_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt16(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取int变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Int_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt32(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取uint变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_UInt_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt32(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取long变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Long_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadInt64(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取ulong变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_ULong_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadUInt64(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取float变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Float_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadFloat(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取double变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Double_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadDouble(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        /// <summary>
        /// 读取string变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_String_Click(object sender, EventArgs e)
        {
            ReadResultRender(siemensTcpNet.ReadString(txt_ReadSingle_Address.Text), txt_ReadSingle_Address.Text, txt_Result_Single);
        }

        #endregion

        #region 单数据写入测试

        /// <summary>
        /// 写入bool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Bool_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, bool.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 写入byte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Byte_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, byte.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入short
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Short_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, short.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入ushort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_UShort_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, ushort.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入int
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Int_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, int.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入uint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_UInt_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, uint.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入long
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Long_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, long.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入ulong
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_ULong_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, ulong.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入float
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Float_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, float.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入double
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_Double_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, double.Parse(txt_WriteSingle_Value.Text));
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 写入string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Write_String_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult result = siemensTcpNet.Write(txt_WriteSingle_Address.Text, txt_WriteSingle_Value.Text);
                WriteResultRender(result, txt_WriteSingle_Address.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 批量读取测试

        /// <summary>
        /// 订货号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Order_Click(object sender, EventArgs e)
        {
            OperateResult<string> result = siemensTcpNet.ReadOrderNumber();

            if (result.IsSuccess)
            {
                txt_Result_Many.Text = $"订货号：{result.Content}";
            }
        }

        /// <summary>
        /// 批量读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReadMany_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> result = siemensTcpNet.Read(txt_ReadMany_Address.Text, ushort.Parse(txt_ReadMany_Lenth.Text));
                if (result.IsSuccess)
                {
                    txt_Result_Many.Text = $"结果：{SoftBasic.ByteToHexString(result.Content)}";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 报文读取测试

        /// <summary>
        /// 读取报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Read_Message_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> result = siemensTcpNet.ReadFromCoreServer(SoftBasic.HexStringToBytes(txt_Read_Message.Text));
                if (result.IsSuccess)
                {
                    txt_Result_Message.Text = $"结果：{SoftBasic.ByteToHexString(result.Content)}";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 定时器读取测试

        #endregion

    }
}
