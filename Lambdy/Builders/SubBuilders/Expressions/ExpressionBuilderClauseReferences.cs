using Lambdy.TreeNodes.ClauseSectionNodes;

namespace Lambdy.Builders.SubBuilders.Expressions
{
    internal class ExpressionBuilderClauseReferences
    {
        public SelectClauseNode SelectClause { get; set; } = new SelectClauseNode();
        public FromClauseNode FromClause { get; set; } = new FromClauseNode();
        public JoinClauseNode JoinClause { get; set; } = new JoinClauseNode();
        public WhereClauseNode WhereClause { get; set; } = new WhereClauseNode();
        public OrderClauseNode OrderClause { get; set; } = new OrderClauseNode();
        public SkipTakeClauseNode SkipTakeClause  { get; set; } = new SkipTakeClauseNode();
    }
}