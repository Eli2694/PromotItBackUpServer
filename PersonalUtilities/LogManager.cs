﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUtilities
{
    public class LogManager
    {
        static Queue<LogItem> itemsQueue;
        Task queueTask = null;
        bool stop = false;

        protected LogManager()
        {
            itemsQueue = new Queue<LogItem>();
            PopItemsFromQueue();
            HouseKeeping();
        }

        public enum LogProvider
        {
            File,
            DB,
            Console
        }

        static  ILogger MyLog { get; set; }
        public static ILogger Target(LogProvider aProvider)
        {
            if(MyLog == null) 
            {
                // Create a one-time instance of LogManager to run the constructor of the class
                LogManager instance = new LogManager();

                // Determine where to write the log
                if (aProvider == LogProvider.File)
                {

                    MyLog = new LogFile();
                }
                else if (aProvider == LogProvider.DB)
                {

                    MyLog = new LogDB();
                }
                else if (aProvider == LogProvider.Console)
                {
                    MyLog = new LogConsole();
                }
                else
                {
                    MyLog = new LogNone();
                }

                return MyLog;
                
            }

            return MyLog;
        }

        public static void AddLogItemToQueue(string msg, Exception exc, string LogType)
        {
            LogItem item= new LogItem();
            item.message = msg;
            item.exception = exc;
            item.type = LogType;
            item.dateTime = DateTime.Now;
            itemsQueue.Enqueue(item);
        }
        void PopItemsFromQueue()
        {
            queueTask = Task.Run(() =>
            {
                while (!stop)
                {
                    if (itemsQueue.Count > 0)
                    {
                        LogItem item = itemsQueue.Dequeue();
                        // Write Log
                        MyLog.Log(item);

                    }

                    System.Threading.Thread.Sleep(1000 * 60 * 2);
                }

            });
        }

        void HouseKeeping()
        {
            queueTask = Task.Run(() =>
            {
                while (!stop)
                {                 
                    MyLog.LogCheckHoseKeeping();
                    System.Threading.Thread.Sleep(1000 * 60 * 60);
                }

            });
        }

    }
}
