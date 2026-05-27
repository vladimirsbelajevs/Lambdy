using System;

namespace Lambdy
{
    public static class LambdyBuilderCoreExtensions
    {
        public static ILambdyBuilder<TTarget> Cast<TTarget>(
            this ILambdyBuilderCore lambdyBuilder)
            where TTarget : class
        {
            if (lambdyBuilder is null) throw new ArgumentNullException(nameof(lambdyBuilder));
            return (ILambdyBuilder<TTarget>) lambdyBuilder;
        }
    }
}
