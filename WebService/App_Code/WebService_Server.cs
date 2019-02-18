using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService_Server
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService_Server : System.Web.Services.WebService
{

    public WebService_Server()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod()]
    public string HelloWorld(string strMessage)
    {
        return "Hello Everybody!!  " + strMessage;
    }
    [WebMethod()]
    public void MessageLine(string token, string msg, string pictureUrl,int stickerPackageID,int stickerID)
    {
        // https://notify-bot.line.me/my/ เข้าเว็บ
        var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
        var postData = string.Format("message={0}", msg);
        /*if (stickerPackageID > 0 && stickerID > 0)
        {
            var stickerPackageId = string.Format("stickerPackageId={0}", stickerPackageID);
            var stickerId = string.Format("stickerId={0}", stickerID);
            postData += "&" + stickerPackageId.ToString() + "&" + stickerId.ToString();
        }
        if (pictureUrl != "")
        {
            var imageThumbnail = string.Format("imageThumbnail={0}", pictureUrl);
            var imageFullsize = string.Format("imageFullsize={0}", pictureUrl);
            postData += "&" + imageThumbnail.ToString() + "&" + imageFullsize.ToString();
        }*/
        var data = Encoding.UTF8.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;
        request.Headers.Add("Authorization", "Bearer " + token);

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
        //using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);

        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
    }

    [WebMethod()]
    public void MessageToServer(string token, string msg, string pictureUrl, int stickerPackageID, int stickerID)
    {
        MessageLine(token, msg, pictureUrl, stickerPackageID, stickerID);
    }

}
