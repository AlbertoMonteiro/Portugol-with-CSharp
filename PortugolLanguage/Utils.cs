using System;
using System.Linq.Expressions;
using System.Reflection;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage
{
    public static class Utils
    {
        public static T InfoOf<T>(Expression<Action> expression)
            where T : MemberInfo
        {
            var body = expression.Body;
            var methodCallExpression = body as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method as T;
            return null;
        }

        public static T Evaluate<T>(this ParseTreeNode node)
        {
            var astNode = (AstNode)node.AstNode;
            return astNode.Evaluate(null) is T ? (T)astNode.Evaluate(null) : default(T);
        }
    }
}