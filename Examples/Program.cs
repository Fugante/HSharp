using Hsharp;


Just<int> m2 = new(2);
var m4 = (Just<int>)m2.Map(n => n * n);
Nothing<int> n = new();
Nothing<Func<int, string>> nf = new();
Just<Func<int, string>> mf = new(n => n.ToString());
Console.WriteLine(m4);
Console.WriteLine(n.Map(n => n * n));
Console.WriteLine(m2.Apply(mf));
Console.WriteLine(n.Apply(mf));
Console.WriteLine(m2.Apply(nf));
Console.WriteLine(
    m2.Bind<double>(n => n == 0 ? new Nothing<double>() : new Just<double>(n / 4))
);

