using Autofac;

namespace RedSpartan.IntervalTraining.Bootstrap
{
    //TODO: I don't like statics so I'll need to change this to a non static at some point
    public static class AppContainer
    {
        public static IContainer Container { get; set; }
    }
}
