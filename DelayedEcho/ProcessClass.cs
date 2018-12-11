using System;
using System.Threading.Tasks;


namespace DelayedEcho
{
    public delegate Task AsyncEventHandler(string message, EventArgs e);
    public class ProcessClass
    {
        public event AsyncEventHandler MessageDelayed;
       
        protected virtual async Task OnMessageDelayed(string message, EventArgs e)
        {
            AsyncEventHandler handler = MessageDelayed;
            if (handler != null)
            {
               await handler(message, e);
            }
        }

        public async Task EchoDelayed(string message, int delaySeconds)
        {
            await Task.Delay(delaySeconds * 1000);

            var returnMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + message;
            await OnMessageDelayed(returnMessage, EventArgs.Empty);
        }
    }
}
