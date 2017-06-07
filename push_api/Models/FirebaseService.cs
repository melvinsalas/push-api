using System;
using System.Collections.Generic;
using FirebaseNet.Messaging;

namespace TokenApi.Models
{
	public class FirebaseService
	{
        public static FCMClient FCMClient { get; set; }

        internal static FCMClient GetInstance(string serverKey)
        {
            return new FCMClient(serverKey);
        }

        internal static Message GetMessage(TokenItem token, MessageItem item)
        {
			var message = new Message()
			{
				To = token.Token,
				Notification = new AndroidNotification()
				{
					Body = item.Body,
					Title = item.Title,
					Icon = "mdi_car_connected",
					Sound = "default"
				},
				Data = new Dictionary<string, string>
				{
					{ "activity", item.Activity },
					{ "id", item.ActivityId }
				}
			};
            return message;
        }
    }
}