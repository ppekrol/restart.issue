//-----------------------------------------------------------------------
// <copyright file="TransactionalStorageConfigurator.cs" company="Hibernating Rhinos LTD">
//     Copyright (c) Hibernating Rhinos LTD. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace RestartIssue.Web
{
    using System;
    using System.IO;

    using Microsoft.Isam.Esent.Interop;

    public class TransactionalStorageConfigurator
    {
        public const int MaxSessions = 1024;

        public InstanceParameters ConfigureInstance(JET_INSTANCE jetInstance, string path)
        {
            path = Path.GetFullPath(path);
            var logsPath = path;

            var instanceParameters = new InstanceParameters(jetInstance)
            {
                Recovery = true,
                NoInformationEvent = false,
                CreatePathIfNotExist = true,
                EnableIndexChecking = true,
                TempDirectory = Path.Combine(logsPath, "temp"),
                SystemDirectory = Path.Combine(logsPath, "system"),
                LogFileDirectory = Path.Combine(logsPath, "logs"),
                BaseName = "RVN",
                EventSource = "Raven",
                MaxSessions = MaxSessions,
                AlternateDatabaseRecoveryDirectory = path
            };

            return instanceParameters;
        }
    }
}
