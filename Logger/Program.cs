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
            static int _logSeq = 1;
            static int _logSeqLength = 5;
            static long _logSizelimit = 150;
            static string _fileNameInit = "Log";
            static string _filePath = @"C:\Temp\";
            static public string _fileName;

            public enum LogType
            {
                Error, Warning, Info
            }

            public string GetFileName()
            {
                string today = DateTime.Now.ToString("yyyyMMdd");
                _fileName = _filePath + _fileNameInit + "_" + today + _logSeq.ToString().PadLeft(_logSeqLength - _logSeq.ToString().Length, '0') + ".txt";
                long length = new FileInfo(_fileName).Length;
                if (length >= _logSizelimit)
                {
                    _logSeq++;
                }
                _fileName = _filePath + _fileNameInit + "_" + today + _logSeq.ToString().PadLeft(_logSeqLength - _logSeq.ToString().Length, '0') + ".txt";
                return _fileName;
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
                    //long length = streamWriter.BaseStream.Length;
                    //if (length >= Logger._logSeqLength)
                    // {

                    //}

                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            Logger lg = new Logger();
            //Logger._fileName = Logger._filePath + Logger._fileName + "_" + today + Logger._logSeq.ToString().PadLeft(Logger._logSeqLength - Logger._logSeq.ToString().Length, '0') + ".txt";
            string FileName = lg.GetFileName();
            FileLogger fl = new FileLogger();
            Message msg = new Message(Logger.LogType.Info, "Ilan", "Hello World");
            fl.Log(FileName, msg.ToString());
            Console.ReadLine();
        }
    }
}
