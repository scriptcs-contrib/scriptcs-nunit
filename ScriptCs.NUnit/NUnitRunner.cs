using System;
using System.Linq;
using System.Reflection;
using NUnit.Core;
using ScriptCs.Contracts;

namespace ScriptCs.NUnit
{
    public class NUnitRunner : IScriptPackContext
    {
        public TestResult RunAllUnitTests(Assembly testAssembly = null)
        {
            if (testAssembly == null)
            {
                testAssembly = Assembly.GetCallingAssembly();
            }

            return RunAllUnitTests(msg => Console.WriteLine(msg), testAssembly);
        }

        public TestResult RunAllUnitTests(Action<string> callback, Assembly testAssembly = null)
        {
            if (testAssembly == null)
            {
                testAssembly = Assembly.GetCallingAssembly();
            }

            CoreExtensions.Host.InitializeService();
            TestExecutionContext.CurrentContext.TestPackage = new TestPackage(string.Format("TestPackage for {0}", testAssembly.GetName().FullName));

            var builder = new NamespaceTreeBuilder(new TestAssembly(testAssembly, testAssembly.GetName().FullName));
            var fixtures = testAssembly.GetTypes().Where(i => TestFixtureBuilder.CanBuildFrom(i)).Select(i => TestFixtureBuilder.BuildFrom(i)).ToList();
           
            builder.Add(fixtures);
            return builder.RootSuite.Run(new ConsoleListener(callback), TestFilter.Empty);
        }
    }
}