public class Option<T> {
	private enum OptionState {
		Some,
		None
	}

	private OptionState state;
	private T? innerItem;

	public static Option<T> Some(T result)
	{
		var newContainer = new Option<T>();
		newContainer.state = OptionState.Some;
		newContainer.innerItem = result;
		return newContainer;
	}

	public static Option<T> None()
	{
		var newContainer = new Option<T>();
		newContainer.state = OptionState.None;
		return newContainer;
	}

	public bool IsSome()
	{
		return state == OptionState.Some;
	}

	public bool IsNone()
	{
		return state == OptionState.None;
	}

	public override string ToString()
	{
		return state + (this.IsSome() ? "(" + innerItem + ")" : "");
	}

	public T Unwrap()
	{
		if (this.IsNone()) throw new InvalidOperationException();
		else return innerItem;
	}

	public T UnwrapOr(T defaultVal)
	{
		if (this.IsNone()) return defaultVal;
		else return innerItem;
	}

	public Option<U> Map<U>(Func<T, U> mapFunc)
	{
	    if(this.IsNone()) return Option<U>.None();
	    else return Option<U>.Some(mapFunc(innerItem));
	}

	public U MapOr<U>(U defaultVal, Func<T, U> mapFunc)
    {
        if(this.IsNone()) return defaultVal;
        else return mapFunc(innerItem);
    }

    public U MapOrElse<U>(Func<T, U> mapSomeFunc, Func<U> mapNoneFunc)
    {
        if(this.IsNone()) return mapNoneFunc();
        else return mapSomeFunc(innerItem);
    }

    public void Inspect(Action<T> func)
    {
        if(this.IsNone()) return;
        else func(innerItem);
    }
}
