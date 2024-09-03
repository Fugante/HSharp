namespace Hsharp;


public interface IFunctor<A>
{
    public abstract IFunctor<B> Map<B>(Func<A, B> f);
}

public interface IApplicative<A> : IFunctor<A>
{
    public abstract IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f);
}

public interface IMonad<A> : IApplicative<A>
{
    public abstract IMonad<B> Bind<B>(Func<A, IMonad<B>> f);
}
