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
}
