using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Policy;

namespace SharpScript.Sandboxing
{
    public class SandboxSecurityParameters
    {
        /// <summary>
        /// The permission set for the AppDomain. If UseZoneSecurity is true, this is ignored.
        /// </summary>
        public PermissionSet AppDomainPermissions = new PermissionSet(System.Security.Permissions.PermissionState.None);

        public List<Assembly> AvailableAssemblies = new List<Assembly>();

        public StrongName[] FullTrustAssemblies;

        /// <summary>
        /// If true, the Zone security evidence is used to create the sandbox. The default is false
        /// </summary>
        public bool UseZoneSecurity = false;

        public Zone ZoneSecurityEvidence { get; set; } = new Zone(SecurityZone.MyComputer);
    }
}