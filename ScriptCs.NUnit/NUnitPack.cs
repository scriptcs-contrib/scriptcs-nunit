using ScriptCs.Contracts;

namespace ScriptCs.NUnit
{
    public class NUnitScriptPack : IScriptPack
    {
        public void Initialize(IScriptPackSession session)
        {
            session.AddReference("nunit.core.dll");
            session.AddReference("nunit.core.interfaces.dll");
            session.AddReference("nunit.framework.dll");

            session.ImportNamespace("NUnit.Framework");
            session.ImportNamespace("ScriptCs.NUnit");
            session.ImportNamespace("ScriptCs.NUnit");
            session.ImportNamespace("System");
            session.ImportNamespace("System.Linq");
            session.ImportNamespace("System.Reflection");
        }

        public IScriptPackContext GetContext()
        {
            return new NUnitRunner();
        }

        public void Terminate()
        {}
    }
}
