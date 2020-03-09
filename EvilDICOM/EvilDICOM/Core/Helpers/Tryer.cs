using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EvilDICOM.Core.Helpers
{
    public class Tryer
    {
        public Tryer()
        {

        }

        public void Try(Action a, int maxTries = 3, int msDelayBtwTries = 5000, Action<Exception> failureAction = null)
        {
            //Try at most 3 times to send
            int count = 0;
            while (true)
            {
                try
                {
                    a?.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    //Daemon might have crashed
                    Thread.Sleep(msDelayBtwTries);
                    if (++count == maxTries)
                    {
                        failureAction?.Invoke(e);
                    }
                }
            }
        }

        public static bool Try(Action a, string msg = "")
        {
            try
            {
                a();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
