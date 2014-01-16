namespace RestartIssue.Web
{
    using Microsoft.Isam.Esent.Interop;

    public class MvcApplication : System.Web.HttpApplication
    {
        private JET_INSTANCE instance;

        protected void Application_Start()
        {
            // comment/uncomment handler in web.config to see a behavior

            RavenShutdownDetector.Instance.Initialize();
            RavenShutdownDetector.Instance.Token.Register(TearDown);

            Initialize();
        }

        private void TearDown()
        {
            Api.JetTerm2(instance, TermGrbit.Complete);
        }

        private void Initialize()
        {
            var t = new TransactionalStorageConfigurator();
            t.ConfigureInstance(instance, @"C:\temp\RestartIssue\"); // might want to change this

            Api.JetInit(ref instance);
        }
    }
}