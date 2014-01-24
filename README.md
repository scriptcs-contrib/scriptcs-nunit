ScriptCs.NUnit
==============

Script pack for running nunit tests from scriptcs

## Installation

From the folder where you are writing a script, run:

    scriptcs -install scriptcs.nunit
    
This will install `ScriptCs.NUnit` and the necessary dependencies.

## Usage

You can now write and run *NUnit* unit tests in your scriptcs CSX file.

### Tests for CSX

    [TestFixture]
    public class Tests
    {
      [Test]
    	public void ScriptcsShouldBeAwesome()
    	{
    		var scriptcsIsAwesome = true;
    		Assert.IsTrue(scriptcsIsAwesome);
    	}
     }
    
    var nunit = Require<NUnitRunner>();
    nunit.RunAllUnitTests(); 
    
This will simply run all the unit tests in the given script context. It will also find tests referenced by `#load`.
This way you can test your CSX code, with CSX unit tests.

### Tests from external file 
You can also reference an external assembly containing *NUnit* tests and run these. 

For example, consider a `MyTests.dll` which contains some unit tests in the `UnitTests` class. 
You can copy that DLL to the `bin` folder and do the following (passing in the `Assembly` instance):

    #r "MyUnitTests.dll"
    
    var nunit = Require<NUnitRunner>();
    nunit.RunAllUnitTests(typeof(MyUnitTests.UnitTests).Assembly); 
  
### Writing tests for code from external DLL 
Using the same technique, you can import an external DLL, and write unit tests for that DLL in CSX. For example, consider a hypothetical `MyAssembly.dll` with a `MyClass` class.
You can do the following:

    #r "MyAssembly.dll"
    using MyAssembly;
    
    [TestFixture]
    public class Tests
    {
      [Test]
    	public void MyClassShouldNotBeNull()
    	{
    		var myClass = new MyClass();
    		Assert.NotNull(myClass);
    	}
     }
    
    var nunit = Require<NUnitRunner>();
    nunit.RunAllUnitTests(); 
    
## Other considerations

The `RunAllUnitTests()` method actually returns an instance of `NUnit.Core.TestResult` which contains all info about the tests run - such as assert count, individual failures/successes, execution time and many more details regarding each test. With this, you can programmatically discover the status, and - for example - program automated tasks to depend on unit tests.

**Important!**
This script pack only works in debug mode. It means, you have to execute your CSX like this:

    scriptcs start.csx -debug
    
Script pack also exposes an overload:

    TestResult RunAllUnitTests(Action<string> callback, Assembly testAssembly = null)
    
where you can redirect the test output from Console (default behavior) to anywhere you want.

Script pack imports the following namespaces into your script (no need to do `using` for them):

 - NUnit.Framework
 - NUnit.Core
 - System.Reflection
