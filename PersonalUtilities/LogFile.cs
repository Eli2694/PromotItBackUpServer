using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUtilities
{
    public class LogFile : ILogger
    {
        public static string filePath = "C:\\Users\\User\\source\\repos\\PromotIt\\";
        public static string fullFileName { get { return filePath + DateTime.Now.ToString("dd-MM-yyyy") + "file" + index + ".log"; } set { index = int.Parse(value); } }

        public static int index = 1;

        public int maxFileSize = 5000000;
        public void Init()
        {

        }
      
        public void Log(LogItem item)
        {

            using (StreamWriter streamWriter = new StreamWriter(fullFileName,true))
            {
                string log;

                if (item.exceptionSource == null)
                {
                    log = item.type + " - " + item.dateTime + " - " + item.message;
                }
                else
                {
                    log = item.type + " - " + item.dateTime + " - " + item.exceptionSource.StackTrace.ToString() + ", " + item.message;
                }

                streamWriter.WriteLine(log);
                streamWriter.Close();

            }

        }
        public void LogCheckHoseKeeping()
        {
            CheckFileSize();
        }

        private void CreateNewFile()
        {
            
            while (true)
            {
                string newFileName = filePath + DateTime.Now.ToString("dd-MM-yyyy") + "file" + index + ".log";

                if (!File.Exists(fullFileName))
                {

                    File.Create(fullFileName).Close();
                    return;
                }
                else
                {
                    FileInfo newFileInfo = new FileInfo(fullFileName);

                    if (newFileInfo.Length >= maxFileSize)
                    {
                        index++;
                        continue;
                    }

                    if (newFileInfo.Length < maxFileSize)
                    {
                        return;
                    }

                    fullFileName = newFileName;

                    File.AppendAllText(fullFileName, "FileOpen");
                    
                }
            }
        }

        private void CheckFileSize()
        {
            if (!File.Exists(fullFileName))
            {
                CreateNewFile();
                return;
            }

            FileInfo fileInfo = new FileInfo(fullFileName);
            if (fileInfo.Length >= maxFileSize)
            {
                CreateNewFile();
            }
        }
    }
}
