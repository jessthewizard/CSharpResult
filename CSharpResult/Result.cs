public class Result<T, E> {
	private enum ResultState {
		Ok,
		Err
	}

	private ResultState state; 
	private T? okItem;
	private E? errItem;

	public static Result<T, E> Ok(T result)
	{
		var newContainer = new Result<T, E>();
		newContainer.state = ResultState.Ok;
		newContainer.okItem = result;
		return newContainer;
	}

	public static Result<T, E> Err(E result)
	{
		var newContainer = new Result<T, E>();
		newContainer.state = ResultState.Err;
		newContainer.errItem = result;
		return newContainer;
	}

	public bool IsOk()
	{
		return state == ResultState.Ok;
	}

	public bool IsErr()
	{
		return state == ResultState.Err;
	}

	public override string ToString()
	{
		return state + "(" + (this.IsOk() ? okItem : errItem) + ")";
	}

	public T Unwrap()
	{
		if (this.IsErr()) throw new InvalidOperationException();
		else return okItem;
	}

	public E UnwrapErr()
	{
		if (this.IsOk()) throw new InvalidOperationException();
		else return errItem;
	}

	public T UnwrapOr(T defaultVal)
	{
		if (this.IsErr()) return defaultVal;
		else return okItem;
	}

	public E UnwrapErrOr(E defaultVal)
	{
		if (this.IsOk()) return defaultVal;
		else return errItem;
	}

    public T UnwrapOrElse(Func<E, T> errFunc)
	{
		if (this.IsErr()) return errFunc(errItem);
		else return okItem;
	}

	public Result<U, E> Map<U>(Func<T, U> mapFunc)
	{
	    if(this.IsErr()) return Result<U, E>.Err(errItem);
	    else return Result<U, E>.Ok(mapFunc(okItem));
	}

	public Result<T, U> MapErr<U>(Func<E, U> mapFunc)
	{
	    if(this.IsOk()) return Result<T, U>.Ok(okItem);
	    else return Result<T, U>.Err(mapFunc(errItem));
	}

	public U MapOr<U>(U defaultVal, Func<T, U> mapFunc)
    {
        if(this.IsErr()) return defaultVal;
        else return mapFunc(okItem);
    }

    public U MapErrOr<U>(U defaultVal, Func<E, U> mapFunc)
    {
        if(this.IsOk()) return defaultVal;
        else return mapFunc(errItem);
    }

    public U MapOrElse<U>(Func<T, U> mapOkFunc, Func<E, U> mapErrFunc)
    {
        if(this.IsErr()) return mapErrFunc(errItem);
        else return mapOkFunc(okItem);
    }

    public void Inspect(Action<T> func)
    {
        if(this.IsErr()) return;
        else func(okItem);
    }

    public void InspectErr(Action<E> func)
    {
        if(this.IsOk()) return;
        else func(errItem);
    }
}
