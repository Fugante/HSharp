namespace Hsharp;


public abstract class Maybe<A> : IMonad<A>
{
    public abstract A Value { get; }
    public bool IsJust
    {
        get => this.GetType() == typeof(Just<A>);
    }
    public bool IsNothing
    {
        get => this.GetType() == typeof(Nothing<A>);
    }

    public IFunctor<B> Map<B>(Func<A, B> f)
    {
        if (IsNothing)
        {
            return new Nothing<B>();
        }
        return new Just<B>(f(Value));
    }

    public IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f)
    {
        if ((f as Maybe<Func<A, B>>).IsNothing || IsNothing)
        {
            return new Nothing<B>();
        }
        return new Just<B>((f as Maybe<Func<A, B>>).Value(Value));
    }

    public IMonad<B> Bind<B>(Func<A, IMonad<B>> f)
    {
        if (IsNothing)
        {
            return new Nothing<B>();
        }
        return f(Value);
    }
}

public class Just<A> : Maybe<A>
{
    public override A Value { get; }
    public Just(A a)
    {
        Value = a;
    }

    public override string ToString()
    {
        return $"Just {Value.ToString()}";
    }
}

public class Nothing<A> : Maybe<A>
{
    public override A Value
    {
        get => throw new MemberAccessException();
    }
    public override string ToString()
    {
        return "Nothing";
    }
}
