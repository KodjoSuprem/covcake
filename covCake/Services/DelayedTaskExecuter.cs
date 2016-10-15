using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace covCake.Services
{
    public class DelayedTaskExecuter
    {

        public static void ExecuteLaterAsync(int delaySecond, Action<object, bool> action)
        {
            ExecuteLaterAsync(delaySecond, action, null);
        }
        public static void ExecuteLaterAsync(int delaySecond, Action<object, bool> action, object objectToPass)
        {
 
            ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(false), new WaitOrTimerCallback(action), objectToPass, delaySecond * 1000, true);

        }
    }
      
}
