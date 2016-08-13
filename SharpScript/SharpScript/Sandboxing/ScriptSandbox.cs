using System;
using System.Reflection;
using System.Security;
using System.Security.Policy;

namespace SharpScript.Sandboxing
{
    public class ScriptSandbox
    {
        public AppDomain SandboxDomain { get; }
        public ScriptingEngine SandboxedEngine { get; }

        /// <summary>
        /// Creates a ScriptSandbox with minimum permissions
        /// </summary>
        public ScriptSandbox() : this(new SandboxSecurityParameters(), Guid.NewGuid().ToString("N"))
        {
        }

        public ScriptSandbox(SandboxSecurityParameters securityParameters, string sandboxName)
        {
            SandboxDomain = CreateSandboxedAppDomain(securityParameters, sandboxName);
            foreach (Assembly availableAssembly in securityParameters.AvailableAssemblies)
            {
                SandboxDomain.Load(availableAssembly.FullName);
            }
        }

        private AppDomain CreateSandboxedAppDomain(SandboxSecurityParameters securityParameters, string sandboxName)
        {
            PermissionSet securityManager = null;
            if (securityParameters.UseZoneSecurity)
            {
                var evidence = new Evidence();
                evidence.AddHostEvidence(securityParameters.ZoneSecurityEvidence);
                securityManager = SecurityManager.GetStandardSandbox(evidence);
            }
            else
            {
                //Use explicit permission security
                securityManager = securityParameters.AppDomainPermissions;
            }

            var appDomainSetup = new AppDomainSetup()
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };

            return AppDomain.CreateDomain(sandboxName, null, appDomainSetup, securityManager, securityParameters.FullTrustAssemblies);
        }
    }
}