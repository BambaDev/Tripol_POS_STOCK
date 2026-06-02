using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pos.Function
{
	internal class NotificationListener
	{
		private string connectionString = "data source=.\\sqlexpress;integrated security=SSPI;initial catalog=Pos;TrustServerCertificate=true;Trusted_Connection=true";

		public NotificationListener()
		{
			// Start the listener infrastructure
			SqlDependency.Start(connectionString);
		}

		~NotificationListener()
		{
			// Stop the listener when the object is destroyed
			SqlDependency.Stop(connectionString);
		}

		public void StartListening()
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Command to monitor for changes
				using (SqlCommand command = new SqlCommand("SELECT Id,Type,NotifiableType,Data,ReadAt,NotifiableId,CreatedAt FROM Notification", connection))
				{
					// Create a dependency and associate it with the SqlCommand.
					SqlDependency dependency = new SqlDependency(command);

					// Subscribe to the SqlDependency event.
					dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

					// Execute the command to establish the notification subscription.
					using (SqlDataReader reader = command.ExecuteReader())
					{
						// Process any initial results here...
						Console.WriteLine("Process any initial results here...");
					}
				}
			}
		}

		// Event handler for SqlDependency
		private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
		{
			// This event will fire when changes are detected
			if (e.Type == SqlNotificationType.Change)
			{
				// Handle the change notification here, then restart the listening process
				Console.WriteLine("Data changed.");
				MessageBox.Show("Data changed.");
				StartListening();
			}
		}
	}
}
