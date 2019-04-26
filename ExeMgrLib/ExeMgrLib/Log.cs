using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Utils
{
    public class Lg
    {
        static string FileName = "OrderLog.txt";
        static ILog logger = null;

        public static void Init(string fileName)
        {
            if (logger != null) return;
            if (!String.IsNullOrEmpty(fileName))
            {
                FileName = fileName;
            }
            ///LevelRangeFilter  
            log4net.Filter.LevelRangeFilter levfilter = new log4net.Filter.LevelRangeFilter();
            levfilter.LevelMax = log4net.Core.Level.Fatal;
            levfilter.LevelMin = log4net.Core.Level.Error;
            levfilter.ActivateOptions();
            //Appender1  
            log4net.Appender.FileAppender appender1 = new log4net.Appender.FileAppender();

            appender1.AppendToFile = true;
            appender1.File = @"c:/log/ERROR/Err.txt";
            appender1.ImmediateFlush = true;
            appender1.LockingModel = new log4net.Appender.FileAppender.MinimalLock();

            appender1.Name = "ErrAppender";
            appender1.AddFilter(levfilter);

            ///Appender2  
            log4net.Appender.FileAppender appender2 = new log4net.Appender.FileAppender();

            appender2.AppendToFile = true;
            appender2.File = @"c:/log/" + FileName+".txt";
            appender2.ImmediateFlush = true;
            appender2.LockingModel = new log4net.Appender.FileAppender.MinimalLock();

            appender1.Name = "AllAppender";
            ///layout  
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level - %message%newline");
            //  
            appender1.Layout = layout;
            appender2.Layout = layout;
            appender1.ActivateOptions();
            appender2.ActivateOptions();
            //
            var repository = log4net.LogManager.CreateRepository("MyRepository");
            
            log4net.Config.BasicConfigurator.Configure(repository, appender1);
            log4net.Config.BasicConfigurator.Configure(repository, appender2);
             logger = log4net.LogManager.GetLogger(repository.Name, "MyLog");
        }

        public static void Debug(string format, params object[] args)
        {
            string msg = GetMsg(format, args);
            //ILog logger = Init();
            logger.Debug(msg);
        }
        public static void Info(string format, params object[] args)
        {
            string msg = GetMsg(format, args);
            //ILog logger = Init();
            logger.Info(msg);
        }
        public static void Warn(string format, params object[] args)
        {
            string msg = GetMsg(format, args);
            //ILog logger = Init();
            logger.Warn(msg);
        }
        public static void Error(string format, params object[] args)
        {
            string msg = GetMsg(format, args);
            //ILog logger = Init();
            logger.Error(msg);
        }
        public static void Fatal(string format, params object[] args)
        {
            string msg = GetMsg(format, args);
            //ILog logger = Init();
            logger.Fatal(msg);
        }

        private static string GetMsg(string format, params object[] args)
        {
            args = args ?? new object[0];
            // 未传入参数时，允许format字符串包含 { } .
            if (args.Length == 0)
            {
                format = format.Replace("{", "{{").Replace("}", "}}");
            }

            var msg = string.Format(format, args);

            return msg;
        }
    }
}
