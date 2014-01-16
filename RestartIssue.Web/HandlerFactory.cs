namespace RestartIssue.Web
{
    using System.Web;

    public class HandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            return new LongRunningHandler(RavenShutdownDetector.Instance.Token);
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}