using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthDomain.Entities.Notification;

using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using FCMNet = FCM.Net;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly IServiceProvider _scope;
        private readonly IAppDbContext _context;
        private readonly IConfiguration _configuration;

        public NotificationService(IServiceProvider scope, IAppDbContext context, IConfiguration configuration)
        {
            _scope = scope;
            _context = context;
            _configuration = configuration;

        }

        public async Task Save(Notification notification, string token)
        {
            await _context.Set<Notification>().AddAsync(notification);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(token))
            {
                await FCMNotify(notification, token);
            }
        }

        private async Task FCMNotify(Notification notification, string userFcmToken)
        {
            var serverKey = _configuration.GetValue<string>("FCMServerKey");
            using (var sender = new FCMNet.Sender(serverKey))
            {
                FCMNet.Message msg;
                msg = new FCMNet.Message
                {
                    RegistrationIds = new List<string> { userFcmToken },

                    Notification = new FCMNet.Notification
                    {
                        Title = notification.SubjectAr,
                        Body = notification.BodyAr,
                        Sound = "sound.caf"
                    },
                    Priority = FCMNet.Priority.High,
                };

                var data=await sender.SendAsync(msg);
            }
        }

        //public static string Notification(Notification notification, string token)
        //{
        //    try
        //    {
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";

        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAAzeHsvc:APA91bGzQXZ2pWbom40rIb0CLcWI4CPmduQAz0GA3q1-LzQiuzQqnLi0VaUYHBPuKCrYxBupkIixhrxDGgqGCJhOsG7N4v2TLqtmQa0v-mpKDdf1_gTXYFjZPmFjzVtz7cF-IDpTQOEp"));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", "13816541943"));
        //        tRequest.ContentType = "application/json";
        //        var payload = new
        //        {
        //            to = token,
        //            priority = "high",
        //            content_available = true,
        //            notification = new
        //            {
        //                body = notification.Body,
        //                title = notification.Subject,
        //                sound = "sound.caf",
        //            }
        //        };
        //        var ser = new JavaScriptSerializer();
        //        var json = ser.Serialize(payload);
        //        byte[] originalArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.ContentLength = originalArray.Length;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(originalArray, 0, originalArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                        {
        //                            String sResponseFromServer = tReader.ReadToEnd();
        //                            return sResponseFromServer;
        //                        }
        //                    return "fail";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return "Error";
        //    }
        //}
    }
}
