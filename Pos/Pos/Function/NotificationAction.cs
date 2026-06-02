using Pos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pos.Function
{
	internal class NotificationAction
	{
		public static void Create(string Type,string NotifiableType,string Data,int NotifiableId)
		{
			Models.Notification notification = new Models.Notification();

			notification.Type = Type;
			notification.NotifiableType = NotifiableType;
			notification.Data = Data;
			notification.NotifiableId = NotifiableId;
			notification.CreatedAt = DateTime.Now;
			notification.UpdatedAt = DateTime.Now;

			Shared.db.Notifications.Add(notification);

			Shared.db.SaveChanges();
		}
	}
}
