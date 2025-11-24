namespace CSharpResult.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void ResultErr_IntOkStringErr_IsErr()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Err(Test)", x.ToString());
    }

    [TestMethod]
    public void ResultErr_IntOkStringErr_IsOk()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual("Ok(10)", x.ToString());
    }

}
