using Microsoft.Data.SqlClient;
using System;

namespace Spektro_API_Azure.Service
{
    public class ErrorLogger
    {
        public static void LogToDb(string errorText, string className) 
        {
            string commandString = "insert into [dbo].[ErrorLogs] (ErrorText, DateOfError, PlaceOfError) values (@text, GETDATE(), @classname)";

            try
            {
                using (SqlConnection connection = new SqlConnection(SecretStrings.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(commandString, connection))
                    {
                        cmd.Parameters.AddWithValue("@text", errorText);
                        cmd.Parameters.AddWithValue("@classname", className);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                //Change email later.
                DynamicEmailSender.SendCustomEmail(firstname: null, lastname: null, email: "screadern@gmail.com", date: DateTime.Now, subject: "API could not record error.", errorText: e.ToString(), mailtype: "ERROR");
            }
        }
    }
}
