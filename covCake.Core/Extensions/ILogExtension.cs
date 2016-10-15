using System;
using log4net;

namespace covCake
{
    
        public static class ILogExtension
        {
            public static void cError(this ILog logger, string msg)
            {
                cError(logger, msg, null, null);
            }
            public static void cError(this ILog logger, string msg, string label)
            {
                cError(logger, msg, label, null);
            }
            public static void cError(this ILog logger, string msg, Exception ex)
            {
                cError(logger, msg, null, ex);
            }
            public static void cError(this ILog logger, string msg, string label, Exception ex)
            {
                log4net.ThreadContext.Properties["user"] = CovCake.GetCurrentUserEmail().ToUpper(); ;

                if (!label.IsNullOrEmpty())
                    log4net.ThreadContext.Properties["label"] = label.ToUpper();
                if (ex != null)
                    logger.Error(msg, ex);
                else
                    logger.Error(msg);
                log4net.ThreadContext.Properties.Clear();//["label"] = label;
            }

            public static void cInfo(this ILog logger, string msg)
            {
                cInfo(logger, msg , null, null);
            }
            public static void cInfo(this ILog logger, string msg, string label)
            {
                cInfo(logger, msg, label, null);
            }
            public static void cInfo(this ILog logger, string msg, Exception ex)
            {
                cInfo(logger, msg, null, null);
            }
            public static void cInfo(this ILog logger, string msg, string label, Exception ex)
            {
                log4net.ThreadContext.Properties["user"] = CovCake.GetCurrentUserEmail().ToUpper(); ;
                if (!label.IsNullOrEmpty())
                    log4net.ThreadContext.Properties["label"] = label.ToUpper();
                if (ex != null)
                    logger.Info(msg, ex);
                else
                    logger.Info(msg);
                log4net.ThreadContext.Properties.Clear();//["label"] = label;
            }

        }
    }

