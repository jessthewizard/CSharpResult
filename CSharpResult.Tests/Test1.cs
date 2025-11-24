namespace CSharpResult.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void ResultToString_IntOkStringErr_IsErr()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Err(Test)", x.ToString());
    }

    [TestMethod]
    public void ResultToString_IntOkStringErr_IsOk()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual("Ok(10)", x.ToString());
    }

    [TestMethod]
    public void UnwrapResult_IntOkStringErr_Is10()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual(10, x.Unwrap());
    }

    [TestMethod]
    public void UnwrapErrResult_IntOkStringErr_IsTest()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Test", x.UnwrapErr());
    }

    [TestMethod]
    public void UnwrapOrResult_IntOkStringErr_Is10()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual(10, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapErrOrResult_IntOkStringErr_IsTest()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Test", x.UnwrapErrOr(""));
    }

    [TestMethod]
    public void UnwrapOrResult_IntOkStringErr_IsErr()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual(-1, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapErrOrResult_IntOkStringErr_IsOk()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual("Ok", x.UnwrapErrOr("Ok"));
    }

    [TestMethod]
    public void UnwrapErrResult_IntOkStringErr_IsOk()
    {
	    var x = Result<int, string>.Ok(10);

	    try {
		    x.UnwrapErr();
	    } catch (InvalidOperationException) {
		    return;
	    }

	    throw new InvalidOperationException();
    }

    [TestMethod]
    public void UnwrapResult_IntOkStringErr_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

	    try {
		    x.Unwrap();
	    } catch (InvalidOperationException) {
		    return;
	    }

	    throw new InvalidOperationException();
    }
}
