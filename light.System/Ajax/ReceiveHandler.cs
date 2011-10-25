using System;
using System.IO;
using System.Web;
using light;
using light;
using light.Entities;
using light.Data;
using light.Entities;

namespace light.Ajax
{
   public class ReceiveHandler : IHttpHandler
   {
      protected string rootPath = null;

      public void ProcessRequest(HttpContext context)
      {
         lock (this) //is working?
         {
            string cmd = context.Request.QueryString["m"];
            if (!string.IsNullOrEmpty(cmd))
            {
               rootPath = context.Server.MapPath("~/");
               switch (cmd)
               {
                  case "avatar":
                     ProcessAvatar(context);
                     break;
                  case "activity_cover":
                     ProcessActivityCover(context);
                     break;
                  default:
                     break;
               }
            }
         }
      }

      /// <summary>
      /// 开始上传
      /// </summary>
      /// <param name="hpfBase">数据请求基类</param>
      /// <param name="savePath">要保存的路径</param>
      /// <param name="fileName">旧文件名称,便于删除(注:如果存在文件夹路径,程序将自动去除,只留下文件名)</param>
      /// <param name="state">上传状态.  0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.</param>
      /// <returns>文件名</returns>
      public bool SaveFile(HttpPostedFile file, string fileName)
      {
         try
         {
            if (file == null || file.ContentLength <= 0) { return false; }
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            file.SaveAs(fileName);
            return true;
         }
         catch
         {
            return false;
         }
      }
      
      private void ProcessActivityCover(HttpContext context)
      {
         try
         {
            HttpPostedFile fileUpload = context.Request.Files["Filedata"];
            int uid = CU.ToInt(context.Request.QueryString["uid"]);
            int aid = CU.ToInt(context.Request.QueryString["aid"]);
            string name = context.Request.QueryString["n"];

            if (uid > 0 && !string.IsNullOrEmpty(name))
            {
               if (fileUpload.ContentLength > 0)
               {
                  string fileName = fileUpload.FileName;
                  string originalPath = @"s\o\180x120\";
                  string coverPath = @"s\c\180x120\";
                  string ofileName = rootPath + originalPath + "\\" + fileName;
                  if (SaveFile(fileUpload, ofileName))
                  {
                     string cfileName = rootPath + coverPath + "\\" + name;
                     IU.Resize(ofileName, cfileName, 180, 120);
                     File.Delete(ofileName);

                     string url = "/s/c/180x120/" + name;

                     if (aid > 0 && FileDB.ExistUploadInfo(uid, aid, 2))
                     {
                        //不创建上传信息
                     }
                     else
                     {
                        FileDB.CreateAttachment(new AttachmentEntity()
                        {
                           uid = uid,
                           referid = aid,
                           url = "/s/c/180x120/" + name,
                           physicalpath = cfileName,
                           type = 2
                        });
                     }

                     context.Response.StatusCode = 200;
                     context.Response.Write("/s/c/180x120/" + name);
                     return;
                  }
               }
            }
         }
         catch { }
         //内部服务器错误
         context.Response.StatusCode = 500;
         context.Response.Write("内部服务器错误");
      }

      private void ProcessAvatar(HttpContext context)
      {
         HttpPostedFile fileUpload = context.Request.Files["Filedata"];

         if (fileUpload != null && fileUpload.ContentLength > 0)
         {
            int id = CU.ToInt(context.Request.QueryString["i"]);
            string name = context.Request.QueryString["n"];

            UserEntity user = UserAccount.Current;
            if (user != null && user.id == id)
            {
               if (name == null) name = user.www;

               string extension = Path.GetExtension(fileUpload.FileName).ToLower();
               string fileName = name + extension;

               string originalPath = @"s\o\";
               string datepath = DateTime.Now.ToString("yyyyMM");
               string ofileName = rootPath + originalPath + datepath + "\\" + fileName;
               if (SaveFile(fileUpload, ofileName))
               {
                  string iconFileName = rootPath + originalPath + datepath + "\\" + name + ".png";
                  int width, height;
                  if (IU.Resize(ofileName, iconFileName, 300, 300, out width, out height))
                  {
                     if (ofileName != iconFileName) File.Delete(ofileName);
                  }
                  else { /*TODO: log failed!*/ }

                  FileController.CreateAttachment(new AttachmentEntity()
                  {
                     uid = id,
                     url = "/s/o/" + datepath + "/" + fileName,
                     physicalpath = iconFileName,
                     type = 1
                  });

                  context.Response.StatusCode = 200;
                  string data = "{url:'" + "/s/o/" + datepath + "/" + name + ".png" + "',width:" + width + ",height:" + height + "}";
                  context.Response.Write(JU.BuildJSON(true, data));
                  return;
               }
            }
         }

         //内部服务器错误
         context.Response.StatusCode = 500;
         context.Response.Write(JU.Build(false, "图片上传失败"));
      }

      public bool IsReusable
      {
         get { return true; }
      }
   }


}