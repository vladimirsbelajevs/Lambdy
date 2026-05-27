using System;

namespace Lambdy
{
    public static class LambdyBuilderCoreExtensions
    {
        public static ILambdyBuilder<TTarget> Cast<TTarget>(
            this ILambdyBuilderCore lambdyBuilder)
            where TTarget : class
        {
            ArgumentNullException.ThrowIfNull(lambdyBuilder);
            return (ILambdyBuilder<TTarget>) lambdyBuilder;
        }
    }
}
