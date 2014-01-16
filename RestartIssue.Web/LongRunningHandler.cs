namespace RestartIssue.Web
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Threading;

    public class LongRunningHandler : IHttpAsyncHandler
    {
        private readonly CancellationToken token;

        public LongRunningHandler(CancellationToken token)
        {
            this.token = token;
        }

        public Task ProcessRequestAsync(HttpContext context)
        {
            return Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                            return;

                        Thread.Sleep(250);
                    }
                },
                token);
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequestAsync(context).Wait();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            context.Response.BufferOutput = false;
            return ProcessRequestAsync(context)
                .ContinueWith(task => cb(task));
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            var task = result as Task;
            if (task != null)
                task.Wait(); // ensure we get proper errors
        }
    }
}