using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Adfos.Entities;

namespace Adfos.Logging
{
    public class Log
    {
        public Log()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Adfos"];
            ConnectionString = connectionString == null ? string.Empty : connectionString.ConnectionString;
            LogFilePath = ConfigurationManager.AppSettings["PathLog"] ?? "C:\\Logs\\";
            LogFilePath += DateTime.Today.ToString("yyyyMMdd") + "_Log.csv";
        }

        public string ConnectionString { get; set; }

        public string LogFilePath { get; set; }

        [Obsolete]
        public void WriteEntry(LogEntry logEntry)
        {
            Database(logEntry);
            WindowsLog(logEntry);
            TextFile(logEntry);
        }

        public void WindowsLog(LogEntry logEntry)
        {
            try
            {
                using (var eventLog = new EventLog("Adfos SRL"))
                {
                    var serializer = new XmlSerializer(logEntry.GetType());
                    var detail = new StringWriter();
                    serializer.Serialize(detail, logEntry);
                    eventLog.Source = logEntry.Source;
                    eventLog.WriteEntry(detail.ToString(), logEntry.Type);
                }
            }
            catch (Exception ex)
            {
                var logError = new LogEntry
                {
                    Source = ex.Source,
                    Type = EventLogEntryType.Error,
                    Number = -1,
                    Code = ex.HResult,
                    Message = ex.GetExceptionMessages(),
                    TraceInfo = ex.StackTrace,
                    Ip = logEntry.Ip,
                    userId = logEntry.userId
                };
                TextFile(logError);
                TextFile(logEntry);
            }
          
        }

        public void Database(LogEntry logEntry)
        {
            try
            {                
                using (var connection = new SqlConnection(ConnectionString))
                    {
                        var command = new SqlCommand("spLogInsert", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.Add(new SqlParameter("@pSource", logEntry.Source));
                        command.Parameters.Add(new SqlParameter("@pType",(int) logEntry.Type));
                        command.Parameters.Add(new SqlParameter("@pNumber", logEntry.Number));
                        command.Parameters.Add(new SqlParameter("@pCode", logEntry.Code));
                        command.Parameters.Add(new SqlParameter("@pMessage", logEntry.Message));
                        command.Parameters.Add(new SqlParameter("@pTraceInfo", logEntry.TraceInfo));
                        command.Parameters.Add(new SqlParameter("@pIp", logEntry.Ip));
                        command.Parameters.Add(new SqlParameter("@pUserId", logEntry.userId));

                        connection.Open();
                        command.ExecuteScalar();
                        connection.Close();
                    }
            }
            catch (Exception ex)
                {
                    var logError = new LogEntry
                    {
                        Source = ex.Source,
                        Type = EventLogEntryType.Error,
                        Number = -1,
                        Code = ex.HResult,
                        Message = ex.GetExceptionMessages(),
                        TraceInfo = ex.StackTrace,
                        Ip = logEntry.Ip,
                        userId = logEntry.userId
                    };
                    WindowsLog(logError);
                    WindowsLog(logEntry);
                }
        }

        public void TextFile(LogEntry logEntry)
        {
            var logFileInfo = new FileInfo(LogFilePath);
            var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            var isnew = !logFileInfo.Exists;
            var fileStream = isnew ? logFileInfo.Create() : new FileStream(LogFilePath, FileMode.Append);
            var sb = TextEntryLog(logEntry);
            var log = new StreamWriter(fileStream);
            if (isnew)
            {
                log.WriteLine(TextHeader().ToString());
            }
            log.WriteLine(sb.ToString());
            log.Close();
        }

        private static StringBuilder TextEntryLog(LogEntry logEntry)
        {
            var sb = new StringBuilder();
            sb.Append(logEntry.Source).Append("|");
            sb.Append(logEntry.Type).Append("|");
            sb.Append(logEntry.Number).Append("|");
            sb.Append(logEntry.Code).Append("|");
            sb.Append(logEntry.Message.Replace(Environment.NewLine," ")).Append("|");
            sb.Append(logEntry.TraceInfo.Replace(Environment.NewLine," ")).Append("|");
            sb.Append(logEntry.Ip).Append("|");
            sb.Append(logEntry.userId).Append("|");
            sb.Append(DateTime.Now);
            return sb;
        }

        private static StringBuilder TextHeader()
        {
            var sb = new StringBuilder();
            sb.Append("Source").Append("|");
            sb.Append("Type").Append("|");
            sb.Append("Number").Append("|");
            sb.Append("Code").Append("|");
            sb.Append("Message").Append("|");
            sb.Append("TraceInfo").Append("|");
            sb.Append("Ip").Append("|");
            sb.Append("UserId").Append("|");
            sb.Append("CreationDate");
            return sb;
        }

    }
}
