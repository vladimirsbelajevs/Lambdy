using System;

namespace Lambdy
{
    public static class LambdyBuilderCoreExtensions
    {
        public static ILambdyBuilder<TTarget> Cast<TTarget>(
            this ILambdyBuilderCore lambdyBuilder)
            where TTarget : class
        {
            lambdyBuilder.ThrowIfNull(nameof(lambdyBuilder));
            return (ILambdyBuilder<TTarget>) lambdyBuilder;
        }
    }
}
