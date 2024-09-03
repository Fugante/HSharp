using Hsharp;


public abstract class Either<L, R> : IMonad<R>
{
    public abstract L LValue { get; }
    public abstract R RValue { get; }
    public bool IsLeft { get => this.GetType() == typeof(Left<L, R>); }
    public bool IsRight { get => this.GetType() == typeof(Right<L, R>); }

    public IFunctor<S> Map<S>(Func<R, S> f)
    {
        if (IsLeft)
        {
            return new Left<L, S>(LValue);
        }
        return new Right<L, S>(f(RValue));
    }

    public IApplicative<S> Apply<S>(IApplicative<Func<R, S>> f)
    {
        if (IsLeft || (f as Either<L, Func<R, S>>).IsLeft)
        {
            // TODO: Make sure both values are Monoids and concatenate them
            return new Left<L, S>(LValue);
        }
        return new Right<L, S>((f as Either<L, Func<R, S>>).RValue(RValue));
    }
}

public class Left<L, R> : Either<L, R>
{
    public override L LValue { get; }
    public override R RValue { get => throw new Exception(); }
    public Left(L l)
    {
        LValue = l;
    }
}

public class Right<L, R> : Either<L, R>
{
    public override L LValue { get => throw new Exception(); }
    public override R RValue { get; }
    public Right(R r)
    {
        RValue = r;
    }
}
