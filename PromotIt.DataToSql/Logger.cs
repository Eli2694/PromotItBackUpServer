using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.DataToSql
{
    public class Logger
    {
        public static string fileName { get { return "C:\\Users\\User\\source\\repos\\PromotIt\\" + DateTime.Now.ToString("dd-MM-yyyy") + "file" + fileNumbering + ".log"; } set { fileNumbering = int.Parse(value); } }

        public static int fileNumbering = 1;
        private static void Log(string str)
        {
           string log = DateTime.Now + " - " + str + "\n";

           bool exists =  CheckIfFileExists(fileName);
           if(exists)
           {
                bool fileSizeOverOneMega = CheckFileSize(fileName);
                if(fileSizeOverOneMega)
                {
                    ChangeFileName(fileName);
                    Log(log);

                }
                else
                {
                    log = DateTime.Now + " - " + str + "\n";
                    File.AppendAllText(fileName, log);
                }
           }
           else
           {
               log = DateTime.Now + " - " + str + "\n";
               File.AppendAllText(fileName, log);
            }   
            
        }

        private static void ChangeFileName(string str)
        {
            //string prevFileName = str;
            //string prevFildEnding = "file" + fileNumbering;
            //fileNumbering++;
            //string newFileEnding = "file" + fileNumbering;
            //fileName = prevFileName.Replace(prevFildEnding, newFileEnding);

            fileNumbering++;
            fileName = fileNumbering.ToString();
        }

        private static bool CheckFileSize(string str)
        {
            FileInfo fileInfo = new FileInfo(str);
            long fileSize = fileInfo.Length;
            if (fileSize >= 1048576)
            {
                return true;
            }
            return false;   
        }

        private static bool CheckIfFileExists(string str)
        {
            if(File.Exists(str))
            {
                return true;
            }
            return false;
        }

        public static void LogError(string str)
        {
            Log("Error - " + str);
        }
        public static void LogEvent(string str)
        {
            Log("Event - " + str);
        }
    }
}
