using ScriptCs.Contracts;

namespace ScriptCs.NUnit
{
    public class NUnitScriptPack : IScriptPack
    {
        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("NUnit.Framework");
            session.ImportNamespace("ScriptCs.NUnit");
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
