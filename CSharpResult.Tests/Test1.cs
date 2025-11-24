namespace CSharpResult.Tests;

[TestClass]
public sealed class ResultTests
{
    [TestMethod]
    public void ResultToString_IsErr()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Err(Test)", x.ToString());
    }

    [TestMethod]
    public void ResultToString_IsOk()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual("Ok(10)", x.ToString());
    }

    [TestMethod]
    public void UnwrapResult_Is10()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual(10, x.Unwrap());
    }

    [TestMethod]
    public void UnwrapErrResult_IsTest()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Test", x.UnwrapErr());
    }

    [TestMethod]
    public void UnwrapOrResult_Is10()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual(10, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapErrOrResult_IsTest()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual("Test", x.UnwrapErrOr(""));
    }

    [TestMethod]
    public void UnwrapOrResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");
	    Assert.AreEqual(-1, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapErrOrResult_IsOk()
    {
	    var x = Result<int, string>.Ok(10);
	    Assert.AreEqual("Ok", x.UnwrapErrOr("Ok"));
    }

    [TestMethod]
    public void UnwrapErrResult_IsOk()
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
    public void UnwrapResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

	    try {
		    x.Unwrap();
	    } catch (InvalidOperationException) {
		    return;
	    }

	    throw new InvalidOperationException();
    }

    [TestMethod]
    public void MapResult_IsOk()
    {
	    var x = Result<int, string>.Ok(10);

        var y = x.Map<bool>((x) => { return x == 10; } );
        Assert.AreEqual(Result<bool, string>.Ok(true).ToString(), y.ToString());
    }

    [TestMethod]
    public void MapErrResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.MapErr<bool>((x) => { return x.Equals("Test"); } );
        Assert.AreEqual(Result<int, bool>.Err(true).ToString(), y.ToString());
    }

    [TestMethod]
    public void MapResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.Map<bool>((x) => { return x == 10; } );
        Assert.AreEqual(Result<bool, string>.Err("Test").ToString(), y.ToString());
    }

    [TestMethod]
    public void MapErrResult_IsOk()
    {
	    var x = Result<int, string>.Ok(10);

        var y = x.MapErr<bool>((x) => { return x.Equals("Test"); } );
        Assert.AreEqual(Result<int, bool>.Ok(10).ToString(), y.ToString());
    }

    [TestMethod]
    public void MapOrResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.MapOr<int>(1, (x) => { return x * 5; } );
        Assert.AreEqual(1, y);
    }

    [TestMethod]
    public void MapOrResult_IsOk()
    {
	    var x = Result<int, string>.Ok(1);

        var y = x.MapOr<int>(1, (x) => { return x * 5; } );
        Assert.AreEqual(5, y);
    }

    [TestMethod]
    public void MapErrOrResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.MapErrOr<bool>(false, (x) => { return x.Equals("Test"); } );
        Assert.AreEqual(true, y);
    }

    [TestMethod]
    public void MapErrOrResult_IsOk()
    {
	    var x = Result<int, string>.Ok(1);

        var y = x.MapErrOr<bool>(false, (x) => { return x.Equals("Test"); } );
        Assert.AreEqual(false, y);
    }

    [TestMethod]
    public void MapOrElseResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.MapOrElse<string>((x) => { return "int " + x; }, (x) => { return "string " + x; });
        Assert.AreEqual("string Test", y);
    }

    [TestMethod]
    public void MapOrElseResult_IsOk()
    {
	    var x = Result<int, string>.Ok(1);

        var y = x.MapOrElse<string>((x) => { return "int " + x; }, (x) => { return "string " + x; });
        Assert.AreEqual("int 1", y);
    }

    [TestMethod]
    public void OkToOptionResult_IsOk()
    {
	    var x = Result<int, string>.Ok(1);

        var y = x.OkToOption();
        Assert.AreEqual(Option<int>.Some(1).ToString(), y.ToString());
    }

    [TestMethod]
    public void OkToOptionResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.OkToOption();
        Assert.AreEqual(Option<int>.None().ToString(), y.ToString());
    }

    [TestMethod]
    public void ErrToOptionResult_IsOk()
    {
	    var x = Result<int, string>.Ok(1);

        var y = x.ErrToOption();
        Assert.AreEqual(Option<string>.None().ToString(), y.ToString());
    }

    [TestMethod]
    public void ErrToOptionResult_IsErr()
    {
	    var x = Result<int, string>.Err("Test");

        var y = x.ErrToOption();
        Assert.AreEqual(Option<string>.Some("Test").ToString(), y.ToString());
    }
}
