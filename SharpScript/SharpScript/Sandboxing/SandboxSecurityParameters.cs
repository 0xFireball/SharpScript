using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
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

        public StrongName[] FullTrustAssemblies = new StrongName[0];

        /// <summary>
        /// If true, the Zone security evidence is used to create the sandbox. The default is false
        /// </summary>
        public bool UseZoneSecurity = false;

        public Zone ZoneSecurityEvidence { get; set; } = new Zone(SecurityZone.MyComputer);

        //Allows Roslyn to run on the code
        public void AllowScripting()
        {
            var currentTrustedAsms = FullTrustAssemblies.ToList();

            var mscorlib = typeof(object).Assembly;
            var microsoftCSharp = typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).Assembly;
            var systemCore = typeof(System.Linq.Enumerable).Assembly;
            var systemCollectionsImmutable = typeof(System.Collections.Immutable.ImmutableArray).Assembly;
            var systemNetHttp = typeof(System.Net.Http.HttpClient).Assembly;
            var systemReflectionMetadata = typeof(System.Reflection.Metadata.AssemblyDefinition).Assembly;
            //var systemRuntimeSerialization = typeof(System.Runtime.Serialization.DataContractSerializer).Assembly;
            var systemXml = typeof(System.Xml.XmlDocument).Assembly;
            var systemXmlLinq = typeof(System.Xml.Linq.XDocument).Assembly;
            var microsoftCodeAnalysis = typeof(Microsoft.CodeAnalysis.Compilation).Assembly;
            var microsoftCodeAnalysisCSharp = typeof(Microsoft.CodeAnalysis.CSharp.CSharpCompilation).Assembly;
            var microsoftCodeAnalysisCSharpScripting = typeof(Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript).Assembly;
            var microsoftCodeAnalysisScripting = typeof(Microsoft.CodeAnalysis.Scripting.Script).Assembly;

            var coreAssemblies = new Assembly[]
            {
                mscorlib,
                microsoftCSharp,
                systemCore,
                systemCollectionsImmutable,
                systemNetHttp,
                systemReflectionMetadata,
                //systemRuntimeSerialization,
                systemXml,
                systemXmlLinq,
                microsoftCodeAnalysis,
                microsoftCodeAnalysisCSharp,
                microsoftCodeAnalysisCSharpScripting,
                microsoftCodeAnalysisScripting,
            };

            //Trust core
            currentTrustedAsms.AddRange(
                coreAssemblies.Select(referencedAsm => referencedAsm.Evidence.GetHostEvidence<StrongName>())
            );

            //Reference core
            AvailableAssemblies.AddRange(
                coreAssemblies
            );

            /*
            var referencedAssemblies = typeof(CSharpScript).Assembly.GetReferencedAssemblies()
                .Select(referencedAsmName => Assembly.Load(referencedAsmName))
                .Select(referencedAsm => referencedAsm.Evidence.GetHostEvidence<StrongName>());

            currentTrustedAsms.AddRange(referencedAssemblies);
            */

            currentTrustedAsms.Add(typeof(CSharpScript).Assembly.Evidence.GetHostEvidence<StrongName>());

            //Trust and reference SharpScript
            currentTrustedAsms.Add(typeof(ScriptSandbox).Assembly.Evidence.GetHostEvidence<StrongName>());
            AvailableAssemblies.Add(typeof(SandboxSecurityParameters).Assembly); //Current Assembly

            //Remove duplicates and save
            AvailableAssemblies = AvailableAssemblies.Distinct().ToList();
            FullTrustAssemblies = currentTrustedAsms.Distinct().ToArray();

            //Grant required permissions

            //Allow execution
            AppDomainPermissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            
        }
    }
}