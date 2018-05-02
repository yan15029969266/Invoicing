using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystemAPI.Tests
{
    public class Test
    {
        //base64编码的文本 转为    图片
        public static void Base64StringToImage(string Base64)
        {
            try
            {
                //FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                //StreamReader sr = new StreamReader(ifs);

                //String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(Base64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                bmp.Save(@"F:\Test\base64" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                //sr.Close();
                //ifs.Close();
                //if (File.Exists(txtFileName))
                //{
                //    File.Delete(txtFileName);
                //}
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

        //图片 转为    base64编码的文本
        public static void ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                //this.pictureBox1.Image = bmp;
                FileStream fs = new FileStream(@"F:\Test\base64.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
                // MessageBox.Show("转换成功!");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ImgToBase64String 转换失败\nException:" + ex.Message);
            }
        }

    }
}
