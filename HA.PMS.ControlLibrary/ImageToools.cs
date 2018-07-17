using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace HA.PMS.ToolsLibrary
{
    public class ImageToools
    {
         #region 变量申明
        private string AllowExt = ".jpe|.jpeg|.jpg|.png|.tif|.tiff|.bmp|.gif|.jfif";
        private Hashtable htmimes = new Hashtable();
        private bool isMarker = false;//是否打水印
        private string MarkerStr = "";//打水印的文字内容
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageToools()
        {
            #region
            htmimes[".jpe"] = "image/jpeg";
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            //htmimes[".bmp"] = "image/bmp";
            htmimes[".bmp"] = "image/jpeg";
            htmimes[".gif"] = "image/gif";
	        htmimes[".jfif"] = "image/jpeg";
            #endregion
        }
        #endregion

        #region 属性
        /// <summary>
        /// 设置是否打水印
        /// </summary>
        public bool setIsMarker
        {
            set
            {
                isMarker = value;
            }
        }
        /// <summary>
        /// 设置水印内容
        /// </summary>
        public string setMarkerStr
        {
            set
            {
                MarkerStr = value;
            }
        }
        #endregion

        #region 检测扩展名的有效性
        /// <summary>
        /// 检测扩展名的有效性
        /// </summary>
        /// <param name="extend">文件扩展名</param>
        /// <returns>如果扩展名有效,返回true,否则返回false</returns>
        public bool CheckValidExtend(string extend)
        {
            bool flag = false;
            string[] aExt = AllowExt.Split('|');
            foreach (string filetype in aExt)
            {
                if (filetype.ToLower() == extend)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion

        #region 保存图片
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        void SaveImage(System.Drawing.Image image, string savePath, ImageCodecInfo ici)
        {
            try
            {
                //设置 原图片 对象的 EncoderParameters 对象
                EncoderParameters parameters = new EncoderParameters(1);
                string ext = savePath.ToLower().Substring(savePath.LastIndexOf("."));
                parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)80));
                if (isMarker)
                {
                    Stream stream = SaveImage(image, ici);
                    Marker(MarkerStr, stream, savePath);
                    try
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                    catch { }
                    return;
                }
                if (File.Exists(savePath))
                {
                    try
                    {
                        File.Delete(savePath);
                    }
                    catch { }
                }
                string path = savePath.Substring(0,savePath.LastIndexOf(@"\"));
                if(!Directory.Exists(path)){
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch { }
                }
                image.Save(savePath, ici, parameters);
                parameters.Dispose();
            }
            catch (System.Exception err)
            {
                throw new Exception(string.Format("保存图片出错：{0}", err.Message));
            }

        }

        /// <summary>
        /// 保存图片 to Stream
        /// </summary>
        /// <param name="image"></param>
        /// <param name="ici"></param>
        /// <returns></returns>
        public Stream SaveImage(Image image, ImageCodecInfo ici)
        {
            Stream re = new System.IO.MemoryStream();
            try
            {
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)80));
                image.Save(re, ici, parameters);
                parameters.Dispose();
            }
            catch (System.Exception err)
            {
                throw new Exception(string.Format("保存图片出错：{0}", err.Message));
            }
            return re;
        }
        #endregion

        #region 获取图像编码解码器的所有相关信息
        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        #endregion

        #region 生成缩略图（传入源图片的路径，直接保存到指定路径）
        /// <summary>
        /// 生成缩略图（传入源图片的路径，直接保存到指定路径）
        /// </summary>
        /// <param name="SourcePath">源图片的路径</param>
        /// <param name="ThumbnailPath">生成的缩略图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool CreateThumbnail(string SourcePath, string ThumbnailPath, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            bool b = false;
            string sExt = null;//源的文件格式

            #region 对参数判断
            //判断源文件的路径是否有问题
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null!");

            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();//获取源图片的格式
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromFile(SourcePath);
                b = CreateThumbnail(oldImage, ThumbnailPath, sExt, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            #endregion

            return b;
        }
        #endregion

        #region 生成缩略图（传入源图片的路径，返回 Image 对象）
        /// <summary>
        /// 生成缩略图（传入源图片的路径，返回 Image 对象）
        /// </summary>
        /// <param name="SourcePath">源图片的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image CreateThumbnail(string SourcePath, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            string sExt = null;//源的文件格式

            #region 对参数判断
            //判断源文件的路径是否有问题
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null!");

            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();//获取源图片的格式
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            System.Drawing.Image newImage = null;

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = Image.FromFile(SourcePath);
                newImage = CreateThumbnail(oldImage, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err2)
            {
                throw new Exception(string.Format("处理图片异常：{0}", err2.Message));
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 生成缩略图（传入源图片流，直接保存到指定路径）
        /// <summary>
        /// 生成缩略图（传入源图片流，直接保存到指定路径）
        /// </summary>
        /// <param name="SourceStream">源图片源流</param>
        /// <param name="sExt">图片格式，已点开始，如（.jpg , .bmp）</param>
        /// <param name="ThumbnailPath">缩略图保存的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool CreateThumbnail(System.IO.Stream SourceStream, string sExt, string ThumbnailPath, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            bool b = false;

            #region 对参数判断
            //对传入的流的判断
            if (SourceStream == null)
            {
                throw new NullReferenceException(string.Format("传入的流为空"));
            }
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromStream(SourceStream);
                b = CreateThumbnail(oldImage, ThumbnailPath, sExt, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                SourceStream.Close();
            }
            #endregion

            return b;

        }
        #endregion

        #region 生成缩略图（传入源图片流，返回 Image 对象）
        /// <summary>
        /// 生成缩略图（传入源图片流，返回 Image 对象）
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailWidth">缩略图宽</param>
        /// <param name="ThumbnailHeight">缩略图高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image CreateThumbnail(System.IO.Stream SourceStream, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            System.Drawing.Image newImage = null;

            #region 对参数判断
            //对传入的流的判断
            if (SourceStream == null)
            {
                throw new NullReferenceException(string.Format("传入的流为空"));
            }
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = Image.FromStream(SourceStream);
                newImage = CreateThumbnail(oldImage, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err2)
            {
                throw new Exception(string.Format("处理图片异常：{0}", err2.Message));
            }
            finally
            {
                oldImage.Dispose();
                SourceStream.Close();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 生成缩略图（传入源 Image 对象，保存缩略图到指定位置）
        /// <summary>
        /// 生成缩略图（传入源 Image 对象，保存缩略图到指定位置）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图保存的格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool CreateThumbnail(System.Drawing.Image SourceImage, string ThumbnailPath, string sExt, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            bool b = false;

            #region 对参数的判断
            if (ThumbnailPath.ToString() == System.String.Empty)
                throw new ArgumentException("ThumbnailPath is null");

            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            System.Drawing.Image newImage = null;

            #region 处理图片
            try
            {
                newImage = CreateThumbnail(SourceImage, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                SourceImage.Dispose();
            }
            #endregion

            #region 将此 Image 对象以指定格式并用指定的编解码参数保存到指定文件
            try
            {
                SaveImage(newImage, ThumbnailPath, GetCodecInfo((string)htmimes[sExt]));
                b = true;
            }
            catch (System.Exception err3)
            {
                throw new Exception(string.Format("保存图片异常：{0}", err3.Message));
            }
            finally
            {
                newImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 生成缩略图（传入源 Image 对象，返回 Image 对象）
        /// <summary>
        /// 生成缩略图（传入源 Image 对象，返回 Image 对象）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image CreateThumbnail(System.Drawing.Image SourceImage, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            System.Drawing.Image newImage = null;

            #region 对参数判断
            if (SourceImage == null)
                throw new NullReferenceException("SourceImage is null");
            if ((ThumbnailWidth < 1) || (ThumbnailHeight < 1))
                throw new ArgumentException("ThumbnailWidth or ThumbnailHeight is less than 1");
            #endregion

            #region 生成缩略图的 Image 对象
            try
            {
                newImage = ThumbnailImage(SourceImage, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err2)
            {
                throw new Exception(string.Format("创建缩略图出错：{0}", err2.Message));
            }
            finally
            {
                SourceImage.Dispose();
            }
            #endregion

            #region 切图
            try
            {
                newImage = ShearImage(newImage, ThumbnailWidth, ThumbnailHeight);
            }
            catch (System.Exception err4)
            {
                throw new Exception(string.Format("切图异常:{0}", err4.Message));
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 生成缩略图（主题方法）
        /// <summary>
        /// 生成缩略图（主题方法）
        /// 生成的缩略图是按比例缩小的，但这个比例是只要有高、宽中有一样达到了缩放的标准就按照这个比例缩放
        /// </summary>
        /// <param name="SourceImage">要处理的Image对象源</param>
        /// <param name="ThumbnailImageWidth">缩略图的宽</param>
        /// <param name="ThumbnailImageHeight">缩略图的高</param>
        /// <returns>返回生成的缩略图的Image对象</returns>
        private System.Drawing.Image ThumbnailImage(System.Drawing.Image SourceImage, Int32 ThumbnailImageWidth, Int32 ThumbnailImageHeight)
        {
            System.Drawing.Bitmap bitmap = null;

            #region 定义变量（源图片的高宽和新图片的高宽）
            Int32 Width = SourceImage.Width;//原图的宽
            Int32 Height = SourceImage.Height;//原图的高
            Int32 newWidth = Width;//新图的宽
            Int32 newHeight = Height;//新图的高
            float fnWidth = 0.00f;
            float fnHeight = 0.00f;
            #endregion

            ThumbnailImageWidth = ThumbnailImageWidth + 2;
            ThumbnailImageHeight = ThumbnailImageHeight + 2;

            #region 计算出图的缩放比例(这个比例是只要有高、宽中有一样达到了缩放的标准就按照这个比例缩放)
            if (Width != ThumbnailImageWidth || Height != ThumbnailImageHeight)
            {
                
                float fsWidth = Convert.ToSingle(Width);
                
                float fsHeight = Convert.ToSingle(Height);
                float ratio = 0.00f;
                //先让宽等于要缩放的宽
                fnWidth = Convert.ToSingle(ThumbnailImageWidth);
                ratio = fnWidth / fsWidth;
                fnHeight = fsHeight * ratio;
                //如果高小于缩放的高
                if (Convert.ToInt32(fnHeight) < ThumbnailImageHeight)
                {
                    fnHeight = Convert.ToSingle(ThumbnailImageHeight);
                    ratio = fnHeight / fsHeight;
                    fnWidth = fsWidth * ratio;
                }

                /*newWidth = ThumbnailImageWidth;
                newHeight = Height * ThumbnailImageWidth / Width;
                //当高已经小于缩略图的高时
                if (newHeight < ThumbnailImageHeight)
                {
                    newHeight = ThumbnailImageHeight;
                    newWidth = Width * ThumbnailImageHeight / Height;
                }*/
                newWidth = Convert.ToInt32(Math.Floor(fnWidth));
                newHeight = Convert.ToInt32(Math.Floor(fnHeight));
            }
            #endregion

            #region 处理图片
            //用指定的大小和格式初始化 Bitmap 类的新是实例
            bitmap = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //从指定的 Image 对象创建新的 Graphice 对象
            System.Drawing.Graphics gp = Graphics.FromImage(bitmap);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gp.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            try
            {
                //清楚整个绘图并以透明背景色填充
                gp.Clear(System.Drawing.Color.Transparent);
                //gp.Clear(System.Drawing.Color.FromArgb(255,255,255));
                gp.DrawImage(SourceImage, new RectangleF(0, 0, fnWidth, fnHeight));
            }
            catch (System.Exception err)
            {
                throw err;
            }
            finally
            {
                SourceImage.Dispose();
                gp.Dispose();
            }
            #endregion

            return bitmap;
        }
        #endregion

        #region 切图（在把缩略图按照指定的大小进行取中剪切）
        /// <summary>
        /// 切图（在把缩略图按照指定的大小进行取中剪切）
        /// </summary>
        /// <param name="SourceImage">Image 对象源</param>
        /// <param name="Width">生成的新图的宽</param>
        /// <param name="Height">生成的新图的高</param>
        /// <returns>返回生成的新图的 Image 对象</returns>
        public System.Drawing.Image ShearImage(System.Drawing.Image SourceImage, Int32 Width, Int32 Height)
        {
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Graphics graphice = null;
            Int32 X = 0;
            Int32 Y = 0;
            try
            {
                if (Width > SourceImage.Width)
                    Width = SourceImage.Width;
                if (Height > SourceImage.Height)
                    Height = SourceImage.Height;
                if (Width < SourceImage.Width)
                {
                    X = (SourceImage.Width - Width) / 2;
                }
                if (Height < SourceImage.Height)
                {
                    Y = (SourceImage.Height - Height) / 2;
                }
                if (X + Width > SourceImage.Width)
                    X = 0;
                if (X < 0)
                    X = 0;
                if (Y + Height > SourceImage.Height)
                    Y = 0;
                if (Y < 0)
                    Y = 0;
            }
            catch { }
            try
            {
                bitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                graphice = System.Drawing.Graphics.FromImage(bitmap);
                graphice.Clear(System.Drawing.Color.Transparent);
                graphice.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphice.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphice.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //graphice.Clear(System.Drawing.Color.FromArgb(255, 255, 255));
                graphice.DrawImage(SourceImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, System.Drawing.GraphicsUnit.Pixel);
            }
            catch (System.Exception err)
            {
                throw new Exception(string.Format("切图出错：{0}", err.Message));
            }
            finally
            {
                SourceImage.Dispose();
                graphice.Dispose();
            }
            return bitmap;
        }
        #endregion


        #region 切图（切取图片指定区域，传入源图片的路径，直接保存到指定路径）
        /// <summary>
        /// 切图（切取图片指定区域，传入源图片的路径，直接保存到指定路径）
        /// </summary>
        /// <param name="SourcePath">源图片的路径</param>
        /// <param name="ThumbnailPath">生成的缩略图的路径</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ShearImageDesignateSize(string SourcePath, string ThumbnailPath, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            bool b = false;
            string sExt = null;//源的文件格式

            #region 对参数判断
            //判断源文件的路径是否有问题
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null!");

            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();//获取源图片的格式
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromFile(SourcePath);
                b = ShearImageDesignateSize(oldImage, ThumbnailPath, sExt, X, Y, Width, Height);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 切图（切取图片指定区域，传入源图片的路径，返回 Image 对象）
        /// <summary>
        /// 切图（切取图片指定区域，传入源图片的路径，返回 Image 对象）
        /// </summary>
        /// <param name="SourcePath">源图片的路径</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ShearImageDesignateSize(string SourcePath, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            string sExt = null;//源的文件格式

            #region 对参数判断
            //判断源文件的路径是否有问题
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null!");

            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();//获取源图片的格式
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            System.Drawing.Image newImage = null;

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = Image.FromFile(SourcePath);
                newImage = ShearImageDesignateSize(oldImage, X, Y, Width, Height);
            }
            catch (System.Exception err2)
            {
                throw new Exception(string.Format("处理图片异常：{0}", err2.Message));
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 切图（切取图片指定区域，传入源图片流，直接保存到指定路径）
        /// <summary>
        /// 切图（切取图片指定区域，传入源图片流，直接保存到指定路径）
        /// </summary>
        /// <param name="SourceStream">源图片源流</param>
        /// <param name="sExt">图片格式，已点开始，如（.jpg , .bmp）</param>
        /// <param name="ThumbnailPath">缩略图保存的路径</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ShearImageDesignateSize(System.IO.Stream SourceStream, string sExt,string ThumbnailPath, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            bool b = false;

            #region 对参数判断
            //对传入的流的判断
            if (SourceStream == null)
            {
                throw new NullReferenceException(string.Format("传入的流为空"));
            }
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromStream(SourceStream);
                b = ShearImageDesignateSize(oldImage, ThumbnailPath, sExt, X, Y, Width, Height);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
                SourceStream.Close();
            }
            #endregion

            return b;

        }
        #endregion

        #region 切图（切取图片指定区域，传入源图片流，返回 Image 对象）
        /// <summary>
        /// 切图（切取图片指定区域，传入源图片流，返回 Image 对象）
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ShearImageDesignateSize(System.IO.Stream SourceStream, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            System.Drawing.Image newImage = null;

            #region 对参数判断
            //对传入的流的判断
            if (SourceStream == null)
            {
                throw new NullReferenceException(string.Format("传入的流为空"));
            }
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = Image.FromStream(SourceStream);
                newImage = ShearImageDesignateSize(oldImage, X, Y, Width, Height);
            }
            catch (System.Exception err2)
            {
                throw new Exception(string.Format("处理图片异常：{0}", err2.Message));
            }
            finally
            {
                oldImage.Dispose();
                SourceStream.Close();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 切图（切取图片指定区域，传入源 Image 对象，保存缩略图到指定位置）
        /// <summary>
        /// 切图（切取图片指定区域，传入源 Image 对象，保存缩略图到指定位置）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图保存的格式</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ShearImageDesignateSize(System.Drawing.Image SourceImage, string ThumbnailPath, string sExt, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            bool b = false;

            #region 对参数的判断
            if (ThumbnailPath.ToString() == System.String.Empty)
                throw new ArgumentException("ThumbnailPath is null");

            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            System.Drawing.Image newImage = null;

            #region 处理图片
            try
            {
                newImage = ShearImageDesignateSize(SourceImage, X, Y, Width, Height);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                SourceImage.Dispose();
            }
            #endregion

            #region 将此 Image 对象以指定格式并用指定的编解码参数保存到指定文件
            try
            {
                SaveImage(newImage, ThumbnailPath, GetCodecInfo((string)htmimes[sExt]));
                b = true;
            }
            catch (System.Exception err3)
            {
                throw new Exception(string.Format("保存图片异常：{0}", err3.Message));
            }
            finally
            {
                newImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 切图（切取图片指定区域，传入源 Image 对象，返回 Image 对象）
        /// <summary>
        /// 切图（切取图片指定区域，传入源 Image 对象，返回 Image 对象）
        /// </summary>
        /// <param name="SourceImage">Image 对象源</param>
        /// <param name="X">切图取的 X 坐标</param>
        /// <param name="Y">切图取的 Y 坐标</param>
        /// <param name="Width">切图取的宽</param>
        /// <param name="Height">切图取的高</param>
        /// <returns>返回生成的新图的 Image 对象</returns>
        public Image ShearImageDesignateSize(Image SourceImage, Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Graphics graphice = null;
            if (X < 0)
                X = 0;
            if (Y < 0)
                Y = 0;
            int sWidth = SourceImage.Width;
            int sHeight = SourceImage.Height;
            if (X + Width > sWidth)//超出了图片的X坐标
            {
                if (Width > sWidth)
                    X = 0;
                else
                    X = sWidth - Width;
            }
            if (Y + Height > sHeight)//超出了图片Y坐标
            {
                if (Height > sHeight)
                    Y = 0;
                else
                    Y = sHeight - Height;
            }
            try
            {
                bitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                graphice = System.Drawing.Graphics.FromImage(bitmap);
                graphice.Clear(System.Drawing.Color.Transparent);
                graphice.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphice.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphice.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //graphice.Clear(System.Drawing.Color.FromArgb(255, 255, 255));
                graphice.DrawImage(SourceImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, System.Drawing.GraphicsUnit.Pixel);
            }
            catch (System.Exception err)
            {
                throw new Exception(string.Format("切图出错：{0}", err.Message));
            }
            finally
            {
                SourceImage.Dispose();
                graphice.Dispose();
            }
            return bitmap;
        }
        #endregion



        #region 生成等比例的缩略图（传入源图路径，高宽都不得超过指定值，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，高宽都不得超过指定值，保存到指定位置 ）
        /// </summary>
        /// <param name="SourcePath">源图路径</param>
        /// <param name="ThumbnailPath">保存缩略图路径</param>
        /// <param name="ThumbnailWidth">缩略图宽</param>
        /// <param name="ThumbnailHeight">缩略图高</param>
        /// <returns></returns>
        public bool ToProportionThumbnailImage(string SourcePath, string ThumbnailPath, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage(SourcePath, ThumbnailPath,ThumbnailWidth, ThumbnailHeight, false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图路径，宽都不得超过指定值，高随宽定，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，宽都不得超过指定值，高随宽定，保存到指定位置 ）
        /// </summary>
        /// <param name="SourcePath">源图路径</param>
        /// <param name="ThumbnailPath">保存缩略图路径</param>
        /// <param name="ThumbnailWidth">缩略图宽</param>
        /// <returns></returns>
        public bool ToProportionThumbnailImage(string SourcePath, string ThumbnailPath, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourcePath,ThumbnailPath, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图路径，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，保存到指定位置 ）
        /// </summary>
        /// <param name="SourcePath">源图路径</param>
        /// <param name="ThumbnailPath">保存缩略图路径</param>
        /// <param name="ThumbnailWidth">缩略图宽</param>
        /// <param name="ThumbnailHeight">缩略图高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(string SourcePath, string ThumbnailPath, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            bool b = false;
            string sExt = null;

            #region 对参数判断
            //判断源文件的路径是否有问题
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null!");

            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();//获取源图片的格式
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromFile(SourcePath);
                b = ToProportionThumbnailImage(oldImage, ThumbnailPath, sExt, ThumbnailWidth, ThumbnailHeight,isW);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 生成等比例的缩略图（传入源图路径，高宽都不得超过指定值，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，高宽都不得超过指定值，返回 Image 对象）
        /// </summary>
        /// <param name="SourcePath">源图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(string SourcePath, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage(SourcePath, ThumbnailWidth, ThumbnailHeight, false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图路径，宽都不得超过指定值，高随宽定，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，高宽都不得超过指定值，返回 Image 对象）
        /// </summary>
        /// <param name="SourcePath">源图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(string SourcePath, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourcePath, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图路径，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，返回 Image 对象）
        /// </summary>
        /// <param name="SourcePath">源图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(string SourcePath, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            System.Drawing.Image newImage = null;

            #region 对参数判断
            string sExt = null;
            sExt = SourcePath.Substring(SourcePath.LastIndexOf(".")).ToLower();
            if (SourcePath.ToString() == System.String.Empty)
                throw new NullReferenceException("SourcePath is null");
            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]", "SourceImagePath");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromFile(SourcePath);
                newImage = ToProportionThumbnailImage(oldImage, ThumbnailWidth, ThumbnailHeight,isW);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 生成等比例的缩略图（传入源图片流，高宽都不得超过指定值，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图片流，高宽都不得超过指定值，保存到指定位置 ） 
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图的保存格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(System.IO.Stream SourceStream, string ThumbnailPath, string sExt, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage( SourceStream, ThumbnailPath, sExt, ThumbnailWidth, ThumbnailHeight,false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图片流，宽都不得超过指定值，高随宽定，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图片流，宽都不得超过指定值，高随宽定，保存到指定位置 ） 
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图的保存格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(System.IO.Stream SourceStream, string ThumbnailPath, string sExt, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourceStream, ThumbnailPath, sExt, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图片流，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源图片流，保存到指定位置 ） 
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图的保存格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(System.IO.Stream SourceStream, string ThumbnailPath, string sExt, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            bool b = false;

            #region 对参数判断
            if (SourceStream == null)
                throw new ArgumentException("SourceStream is null");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromStream(SourceStream);
                b = ToProportionThumbnailImage(oldImage, ThumbnailPath, sExt, ThumbnailWidth, ThumbnailHeight,isW);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 生成等比例的缩略图（传入源图流，高宽都不得超过指定值，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图流，高宽都不得超过指定值，返回 Image 对象）
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.IO.Stream SourceStream, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage( SourceStream,  ThumbnailWidth,  ThumbnailHeight, false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图流，宽都不得超过指定值，高随宽定，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图流，宽都不得超过指定值，高随宽定，返回 Image 对象）
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.IO.Stream SourceStream, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourceStream, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源图流，返回 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源图流，返回 Image 对象）
        /// </summary>
        /// <param name="SourceStream">源图片流</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.IO.Stream SourceStream, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            System.Drawing.Image newImage = null;

            #region 对参数判断
            if (SourceStream == null)
                throw new ArgumentException("SourceStream is null");
            #endregion

            #region 处理图片
            System.Drawing.Image oldImage = null;
            try
            {
                oldImage = System.Drawing.Image.FromStream(SourceStream);
                newImage = ToProportionThumbnailImage(oldImage, ThumbnailWidth, ThumbnailHeight,isW);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                oldImage.Dispose();
            }
            #endregion

            return newImage;
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，高宽都不得超过指定值，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，高宽都不得超过指定值，保存到指定位置 ）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图保存的格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns></returns>
        public bool ToProportionThumbnailImage(System.Drawing.Image SourceImage, string ThumbnailPath, string sExt, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage( SourceImage,  ThumbnailPath,  sExt,  ThumbnailWidth,  ThumbnailHeight, false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，宽都不得超过指定值，高随宽定，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，宽都不得超过指定值，高随宽定，保存到指定位置 ）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图保存的格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <returns></returns>
        public bool ToProportionThumbnailImage(System.Drawing.Image SourceImage, string ThumbnailPath, string sExt, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourceImage, ThumbnailPath, sExt, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，保存到指定位置 ）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，保存到指定位置 ）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailPath">保存缩略图的路径</param>
        /// <param name="sExt">缩略图保存的格式</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns></returns>
        public bool ToProportionThumbnailImage(System.Drawing.Image SourceImage, string ThumbnailPath, string sExt, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            bool b = false;

            #region 对参数判断
            if (ThumbnailPath.ToString() == System.String.Empty)
                throw new ArgumentException("ThumbnailPath is null");

            if (!CheckValidExtend(sExt))
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + AllowExt + " ]");
            #endregion

            System.Drawing.Image newImage = null;

            #region 处理图片
            try
            {
                newImage = ToProportionThumbnailImage(SourceImage, ThumbnailWidth, ThumbnailHeight,isW);
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                SourceImage.Dispose();
            }
            #endregion

            #region 将此 Image 对象以指定格式并用指定的编解码参数保存到指定文件
            try
            {
                SaveImage(newImage, ThumbnailPath, GetCodecInfo((string)htmimes[sExt]));
                b = true;
            }
            catch (System.Exception err3)
            {
                throw new Exception(string.Format("保存图片异常：{0}", err3.Message));
            }
            finally
            {
                newImage.Dispose();
            }
            #endregion

            return b;
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，高宽都不得超过指定值，返回生成的 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，高宽都不得超过指定值，返回生成的 Image 对象）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns></returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.Drawing.Image SourceImage, Int32 ThumbnailWidth, Int32 ThumbnailHeight)
        {
            return ToProportionThumbnailImage(SourceImage, ThumbnailWidth, ThumbnailHeight, false);
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，宽都不得超过指定值，高随宽定，返回生成的 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，宽都不得超过指定值，高随宽定，返回生成的 Image 对象）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <returns></returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.Drawing.Image SourceImage, Int32 ThumbnailWidth)
        {
            return ToProportionThumbnailImage(SourceImage, ThumbnailWidth, 0, true);
        }
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，返回生成的 Image 对象）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，返回生成的 Image 对象）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="isW">是否只按照宽缩放</param>
        /// <returns>返回生成的 Image 对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.Drawing.Image SourceImage, Int32 ThumbnailWidth, Int32 ThumbnailHeight,bool isW)
        {
            #region 对参数判断
            if (SourceImage == null)
                throw new ArgumentException("SourceImage is null");
            //if ((ThumbnailWidth < 1) || (ThumbnailHeight < 1))
            //    throw new ArgumentNullException("ThumbnailWidth or ThumbnailHeight is less than 1");
            #endregion

            #region 计算缩略图的高宽
            Int32 Width = SourceImage.Width;
            Int32 Height = SourceImage.Height;
            Int32 newWidth = SourceImage.Width;
            Int32 newHeight = SourceImage.Height;

            #region 计算 比例
            if (isW)
            {
                //计算出图的缩放比例(这个比例是宽来缩放的)
                if (Width > ThumbnailWidth)
                {
                    newWidth = ThumbnailWidth;
                    newHeight = Height * ThumbnailWidth / Width;
                }
            }
            else
            {
                //高宽都不得超过指定值
                if (Width > ThumbnailWidth)
                {
                    newWidth = ThumbnailWidth;
                    newHeight = Height * ThumbnailWidth / Width;
                    if (newHeight > ThumbnailHeight)
                    {
                        newWidth = newWidth * ThumbnailHeight / newHeight;
                        newHeight = ThumbnailHeight;
                    }
                }
                else if (Height > ThumbnailHeight)
                {
                    newHeight = ThumbnailHeight;
                    newWidth = Width * ThumbnailHeight / Height;
                    if (newWidth > ThumbnailWidth)
                    {
                        newHeight = newHeight * ThumbnailWidth / newWidth;
                        newWidth = ThumbnailWidth;
                    }
                }
            }
            #endregion

            //判断大小，如果都小于1就跳出
            if ((newWidth < 1) || (newHeight < 1))
                throw new Exception("计算出的缩略图高宽都有小于1的");
            #endregion

            #region 处理图片
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Graphics graphics = null;
            try
            {
                //用指定的大小和格式初始化 Bitmap 类的新实例
                bitmap = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //从指定的 Image 对象创建新 Graphics 对象
                graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(System.Drawing.Color.Transparent);
                //graphics.Clear(System.Drawing.Color.FromArgb(255,255,255));
                //在指定位置按指定的大小绘制怨 Image 对象
                graphics.DrawImage(SourceImage, new Rectangle(0, 0, newWidth, newHeight));
            }
            catch (System.Exception err)
            {
                throw new Exception("处理图片异常：" + err.Message);
            }
            finally
            {
                SourceImage.Dispose();
                graphics.Dispose();
            }
            #endregion

            return bitmap;
        }
        #endregion

        ///--2013/09/07
        #region 生成等比例的缩略图（传入源 Image 对象，保存到指定位置，生成最大缩略图）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，保存到指定位置，生成最大缩略图）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailImagePath">保存缩略图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(System.Drawing.Image SourceImage, string ThumbnailImagePath, float ThumbnailWidth, float ThumbnailHeight)
        {
            if (SourceImage.Height < ThumbnailHeight || SourceImage.Width < ThumbnailWidth)
            {
                return false;
            }
            else
            {
                DirectoryInfo ObjDirInfo = new DirectoryInfo(ThumbnailImagePath);
                string ShortageDir = ObjDirInfo.Parent.FullName;
                if (!Directory.Exists(ShortageDir))
                {
                    Directory.CreateDirectory(ShortageDir);
                }
                float intSourceImageWidth = SourceImage.Width, intSourceImageHeight = SourceImage.Height;
                if (intSourceImageWidth / ThumbnailWidth > intSourceImageHeight / ThumbnailHeight)
                {
                    float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                    float intStartPosationX = (intSourceImageHeight - ThumbnailHeight) / 2f;
                    using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(SourceImage, Convert.ToInt32(intStartPosationX), 0, Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                    {
                        ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                    }
                }
                else if (intSourceImageWidth / ThumbnailWidth < intSourceImageHeight / ThumbnailHeight)
                {
                    float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                    float intStartPosationY = (intSourceImageHeight - ThumbnailHeight) / 2f;
                    using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(SourceImage, 0, Convert.ToInt32(intStartPosationY), Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                    {
                        ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                    }
                }
                else
                {
                    ToProportionThumbnailImage(SourceImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                }
                return true;
            }
        } 
        #endregion

        #region 生成等比例的缩略图（传入源图路径，保存到指定位置，生成最大缩略图）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，保存到指定位置）
        /// </summary>
        /// <param name="SourceImagePath">传入源图路径</param>
        /// <param name="ThumbnailImagePath">保存缩略图的路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>返回是否生成成功</returns>
        public bool ToProportionThumbnailImage(string SourceImagePath, string ThumbnailImagePath, float ThumbnailWidth, float ThumbnailHeight)
        {
            using (System.Drawing.Image ObjSourceImage =System.Drawing.Image.FromFile(SourceImagePath))
            {
                if (ObjSourceImage.Height < ThumbnailHeight || ObjSourceImage.Width < ThumbnailWidth)
                {
                    return false;
                }
                else
                {
                    DirectoryInfo ObjDirInfo = new DirectoryInfo(ThumbnailImagePath);
                    string ShortageDir = ObjDirInfo.Parent.FullName;
                    if (!Directory.Exists(ShortageDir))
                    {
                        Directory.CreateDirectory(ShortageDir);
                    }
                    float intSourceImageWidth = ObjSourceImage.Width, intSourceImageHeight = ObjSourceImage.Height;
                    if (intSourceImageWidth / ThumbnailWidth > intSourceImageHeight / ThumbnailHeight)
                    {
                        float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                        float intStartPosationX = (intSourceImageHeight - ThumbnailHeight) / 2f;
                        using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(ObjSourceImage, Convert.ToInt32(intStartPosationX), 0, Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                        {
                            ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                        }
                    }
                    else if (intSourceImageWidth / ThumbnailWidth < intSourceImageHeight / ThumbnailHeight)
                    {
                        float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                        float intStartPosationY = (intSourceImageHeight - ThumbnailHeight) / 2f;
                        using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(ObjSourceImage, 0, Convert.ToInt32(intStartPosationY), Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                        {
                            ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                        }
                    }
                    else
                    {
                        ToProportionThumbnailImage(ObjSourceImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight)).Save(ThumbnailImagePath);
                    }
                    return true;
                }
            }
        } 
        #endregion

        #region 生成等比例的缩略图（传入源图路径，返回生成的 Image 对象，生成最大缩略图）
        /// <summary>
        /// 生成等比例的缩略图（传入源图路径，返回生成的 Image 对象，生成最大缩略图）
        /// </summary>
        /// <param name="SourceImagePath">传入源图路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>缩略图Image对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(string SourceImagePath, float ThumbnailWidth, float ThumbnailHeight)
        {
            using (System.Drawing.Image ObjSourceImage =System.Drawing.Image.FromFile(SourceImagePath))
            {
                if (ObjSourceImage.Height < ThumbnailHeight || ObjSourceImage.Width < ThumbnailWidth)
                {
                    return null;
                }
                else
                {
                    float intSourceImageWidth = ObjSourceImage.Width, intSourceImageHeight = ObjSourceImage.Height;
                    if (intSourceImageWidth / ThumbnailWidth > intSourceImageHeight / ThumbnailHeight)
                    {
                        float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                        float intStartPosationX = (intSourceImageHeight - ThumbnailHeight) / 2f;
                        using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(ObjSourceImage, Convert.ToInt32(intStartPosationX), 0, Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                        {
                            return ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                        }
                    }
                    else if (intSourceImageWidth / ThumbnailWidth < intSourceImageHeight / ThumbnailHeight)
                    {
                        float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                        float intStartPosationY = (intSourceImageHeight - ThumbnailHeight) / 2f;
                        using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(ObjSourceImage, 0, Convert.ToInt32(intStartPosationY), Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                        {
                            return ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                        }
                    }
                    else
                    {
                        return ToProportionThumbnailImage(ObjSourceImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                    }
                }
            }
        } 
        #endregion

        #region 生成等比例的缩略图（传入源 Image 对象，返回生成的 Image 对象，生成最大缩略图）
        /// <summary>
        /// 生成等比例的缩略图（传入源 Image 对象，返回生成的 Image 对象，生成最大缩略图）
        /// </summary>
        /// <param name="SourceImage">源 Image 对象</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <returns>缩略图Image对象</returns>
        public System.Drawing.Image ToProportionThumbnailImage(System.Drawing.Image SourceImage, float ThumbnailWidth, float ThumbnailHeight)
        {
            if (SourceImage.Height < ThumbnailHeight || SourceImage.Width < ThumbnailWidth)
            {
                return null;
            }
            else
            {
                float intSourceImageWidth = SourceImage.Width, intSourceImageHeight = SourceImage.Height;
                if (intSourceImageWidth / ThumbnailWidth > intSourceImageHeight / ThumbnailHeight)
                {
                    float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                    float intStartPosationX = (intSourceImageHeight - ThumbnailHeight) / 2f;
                    using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(SourceImage, Convert.ToInt32(intStartPosationX), 0, Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                    {
                        return ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                    }
                }
                else if (intSourceImageWidth / ThumbnailWidth < intSourceImageHeight / ThumbnailHeight)
                {
                    float intShearHeight = ThumbnailHeight * intSourceImageWidth / ThumbnailWidth;
                    float intStartPosationY = (intSourceImageHeight - ThumbnailHeight) / 2f;
                    using (System.Drawing.Image ObjTempImage = ShearImageDesignateSize(SourceImage, 0, Convert.ToInt32(intStartPosationY), Convert.ToInt32(intSourceImageWidth), Convert.ToInt32(intShearHeight)))
                    {
                        return ToProportionThumbnailImage(ObjTempImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                    }
                }
                else
                {
                    return ToProportionThumbnailImage(SourceImage, Convert.ToInt32(ThumbnailWidth), Convert.ToInt32(ThumbnailHeight));
                }

            }
        } 
        #endregion
        ///--
        #region 添加水印
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="graphics"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Graphics Marker(string sign,Graphics graphics,Int32 width,Int32 height)
        {
            sign += " © 69SKY.NET";
            Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            //Font font_2 = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            //SolidBrush brush_2 = new SolidBrush(Color.FromArgb(193, 190, 190));
            SolidBrush brush = new SolidBrush(Color.FromArgb(139, 138, 138));
            SizeF testSize = graphics.MeasureString(sign, font);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            if (testSize.Width > width - 50 || testSize.Height > height - 50)
                return graphics;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            
            //graphics.DrawString(sign, font_2, brush_2, 9, 10,sf);
            graphics.DrawString(sign, font, brush, 10, 10,sf);
            return graphics;
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="stream"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool Marker(string sign, Stream stream, string filepath)
        {
            Image image = null;
            System.Drawing.Bitmap bitmap = null;
            Graphics g = null;
            bool re = false;
            try
            {
                image = Image.FromStream(stream);
                bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                try
                {
                    g = Graphics.FromImage(bitmap);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    ImageToools handleimage = new ImageToools();
                    g = handleimage.Marker(sign, g, image.Width, image.Height);
                }
                catch { }
                File.Delete(filepath);
                bitmap.Save(filepath,image.RawFormat);
                re = true;
            }
            catch
            {
            }
            finally
            {
                try
                {
                    image.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
                catch { }
            }
            return re;
        }
        #endregion
    }
}
