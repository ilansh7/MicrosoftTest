using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        class Logger
        {
            public static int LogSeq = 1;
            public static int LogSeqLength = 5;
            public static string FileName = "Log";
            public static string FilePath = @"C:\Temp\";

            public enum LogType
            {
                Error, Warning, Info
            }
        }



        class Message
        {
            public string timestamp { get; set; }
            public Logger.LogType type { get; set; }
            public string user { get; set; }
            public string message { get; set; }

            public Message(Logger.LogType msgType, string msgUser, string msg)
            {
                timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                type = msgType;
                user = msgUser;
                message = msg;
            }

            public override string ToString()
            {
                return timestamp + "\t" + type + "\t" + user + "\t" + message;
            }
        }

        public abstract class LogBase
        {
            public abstract void Log(string fileName, string message);
        }

        public class FileLogger : LogBase
        {
            //public string filePath = @"C:\Temp\Log.txt";
            public override void Log(string fileName, string message)
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName, append: true))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            Logger lg = new Logger();
            Logger.FileName = Logger.FilePath + Logger.FileName + "_" + today + Logger.LogSeq.ToString().PadLeft(Logger.LogSeqLength - Logger.LogSeq.ToString().Length, '0') + ".txt";
            FileLogger fl = new FileLogger();
            Message msg = new Message(Logger.LogType.Info, "Ilan", "Hello World");
            fl.Log(Logger.FileName, msg.ToString());
            Console.ReadLine();
        }
    }
}
