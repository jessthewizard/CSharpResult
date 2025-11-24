namespace CSharpResult.Tests;

[TestClass]
public sealed class OptionTests
{
    [TestMethod]
    public void OptionToString_IsNone()
    {
	    var x = Option<int>.None();
	    Assert.AreEqual("None", x.ToString());
    }

    [TestMethod]
    public void OptionToString_IsSome()
    {
	    var x = Option<int>.Some(10);
	    Assert.AreEqual("Some(10)", x.ToString());
    }

    [TestMethod]
    public void UnwrapOption_Is10()
    {
	    var x = Option<int>.Some(10);
	    Assert.AreEqual(10, x.Unwrap());
    }

    [TestMethod]
    public void UnwrapOrOption_Is10()
    {
	    var x = Option<int>.Some(10);
	    Assert.AreEqual(10, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapOrOption_IsNone()
    {
	    var x = Option<int>.None();
	    Assert.AreEqual(-1, x.UnwrapOr(-1));
    }

    [TestMethod]
    public void UnwrapOption_IsNone()
    {
	    var x = Option<int>.None();

	    try {
		    x.Unwrap();
	    } catch (InvalidOperationException) {
		    return;
	    }

	    throw new InvalidOperationException();
    }

    [TestMethod]
    public void MapOption_IsSome()
    {
	    var x = Option<int>.Some(10);

        var y = x.Map<bool>((x) => { return x == 10; } );
        Assert.AreEqual(Option<bool>.Some(true).ToString(), y.ToString());
    }

    [TestMethod]
    public void MapOption_IsNone()
    {
	    var x = Option<int>.None();

        var y = x.Map<bool>((x) => { return x == 10; } );
        Assert.AreEqual(Option<bool>.None().ToString(), y.ToString());
    }

    [TestMethod]
    public void MapOrOption_IsNone()
    {
	    var x = Option<int>.None();

        var y = x.MapOr<int>(1, (x) => { return x * 5; } );
        Assert.AreEqual(1, y);
    }

    [TestMethod]
    public void MapOrOption_IsSome()
    {
	    var x = Option<int>.Some(1);

        var y = x.MapOr<int>(1, (x) => { return x * 5; } );
        Assert.AreEqual(5, y);
    }

    [TestMethod]
    public void MapOrElseOption_IsNone()
    {
	    var x = Option<int>.None();

        var y = x.MapOrElse<string>((x) => { return "int " + x; }, () => { return "none"; });
        Assert.AreEqual("none", y);
    }

    [TestMethod]
    public void MapOrElseOption_IsSome()
    {
	    var x = Option<int>.Some(1);

        var y = x.MapOrElse<string>((x) => { return "int " + x; }, () => { return "none"; });
        Assert.AreEqual("int 1", y);
    }

    [TestMethod]
    public void OkOrOption_IsNone()
    {
	    var x = Option<int>.None();

        var y = x.OkOr<bool>(false);
        Assert.AreEqual("Err(False)", y.ToString());
    }

    [TestMethod]
    public void OkOrOption_IsSome()
    {
	    var x = Option<int>.Some(1);

        var y = x.OkOr<bool>(false);
        Assert.AreEqual("Ok(1)", y.ToString());
    }

    [TestMethod]
    public void OkOrElseOption_IsNone()
    {
	    var x = Option<int>.None();

        var y = x.OkOrElse<bool>(() => { return false; });
        Assert.AreEqual("Err(False)", y.ToString());
    }

    [TestMethod]
    public void OkOrElseOption_IsSome()
    {
	    var x = Option<int>.Some(1);

        var y = x.OkOrElse<bool>(() => { return false; });
        Assert.AreEqual("Ok(1)", y.ToString());
    }
}
