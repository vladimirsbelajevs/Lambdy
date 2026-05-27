using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lambdy.Builders.SubBuilders.Raw.Interfaces;
using Lambdy.Constants.Sql;
using Lambdy.Parameters;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.TreeNodes.ExpressionNodes;

namespace Lambdy.Builders.SubBuilders.Raw
{
    internal class RawBuilder<TModel> : IRawBuilder<TModel>
        where TModel : class
    {
        private readonly ILambdyBuilder<TModel> _parentBuilder;
        private readonly ParameterTracker _parentParameterTracker;
        private readonly RawBuilderClauseReferences _clauseReferences;

        internal RawBuilder(
            ILambdyBuilder<TModel> parentBuilder,
            ParameterTracker parameterTracker,
            RawBuilderClauseReferences references)
        {
            _parentBuilder = parentBuilder ?? throw new ArgumentNullException(nameof(parentBuilder));
            _parentParameterTracker = parameterTracker ?? throw new ArgumentNullException(nameof(parameterTracker));
            _clauseReferences = references ?? throw new ArgumentNullException(nameof(references));
        }

        public ILambdyBuilder<TModel> From(string sqlFragment)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            
            sqlFragment = SubstringSqlClause(sqlFragment, SqlClauses.From);

            _clauseReferences
                .FromClause
                .Node = new RawNode(sqlFragment);

            return _parentBuilder;
        }

        public ILambdyBuilder<TModel> Join(string sqlFragment)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            
            _clauseReferences
                .JoinClause
                .Nodes
                .Add(new RawNode(sqlFragment));

            return _parentBuilder;
        }

        public ILambdyBuilder<TModel> Where(string sqlFragment)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            
            _clauseReferences
                .WhereClause
                .Nodes
                .Add(new RawNode(sqlFragment));

            return _parentBuilder;
        }

        public ILambdyBuilder<TModel> Where(
            string sqlFragment,
            object parameters)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));
            
            Where(sqlFragment);
            AppendParametersFromObject(parameters);

            return _parentBuilder;
        }

        public ILambdyBuilder<TModel> OrderBy(string sqlFragment)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            
            sqlFragment = SubstringSqlClause(sqlFragment, SqlClauses.OrderBy);

            _clauseReferences
                .OrderClause
                .Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = new RawNode(sqlFragment)
                }
            };

            return _parentBuilder;
        }

        public ILambdyBuilder<TModel> OrderBy(
            string sqlFragment,
            object parameters)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));
            
            OrderBy(sqlFragment);
            AppendParametersFromObject(parameters);

            return _parentBuilder;
        }

        private string SubstringSqlClause(string sqlFragment, string clause)
        {
            if (sqlFragment is null) throw new ArgumentNullException(nameof(sqlFragment));
            if (clause is null) throw new ArgumentNullException(nameof(clause));
            
            var index = sqlFragment.IndexOf(
                clause,
                StringComparison.InvariantCultureIgnoreCase);

            if (index >= 0)
            {
                var substringFrom = index + clause.Length + 1;
                sqlFragment = sqlFragment.Substring(substringFrom);
            }

            return sqlFragment;
        }

        private void AppendParametersFromObject(object parameters)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(parameters))
            {
                var value = property.GetValue(parameters);
                if (value != null)
                {
                    _parentParameterTracker
                        .AddParameter(property.Name, value);
                }
            }
        }
    }
}
