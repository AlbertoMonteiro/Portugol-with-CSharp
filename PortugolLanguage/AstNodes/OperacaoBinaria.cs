using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage.AstNodes
{
    public class OperacaoBinaria : AstNode
    {
        private dynamic arg1, arg2;
        private Func<double, double, object> operador;

        private readonly Dictionary<string, Func<double, double, object>> dic = new Dictionary<string, Func<double, double, object>>
        {
            { "+", (a, b) => a + b },
            { "-", (a, b) => a - b },
            { "*", (a, b) => a * b },
            { "/", (a, b) => a / b },
            { "%", (a, b) => a % b },
            { "^", (a, b) => Math.Pow(a, b) },
            { "=", (a, b) => a == b },
            { "<", (a, b) => a < b },
            { ">", (a, b) => a > b },
            { "<=", (a, b) => a <= b },
            { ">=", (a, b) => a >= b },
            { "<>", (a, b) => a != b },
            { "e", (a, b) => Convert.ToBoolean(a) && Convert.ToBoolean(b) },
            { "E", (a, b) => Convert.ToBoolean(a) && Convert.ToBoolean(b) },
            { "ou", (a, b) => Convert.ToBoolean(a) || Convert.ToBoolean(b) },
            { "OU", (a, b) => Convert.ToBoolean(a) || Convert.ToBoolean(b) },
        };


        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("EN");
            base.Init(context, treeNode);
            operador = dic[treeNode.ChildNodes[1].FindTokenAndGetText()];

            arg1 = ((AstNode) treeNode.ChildNodes[0].AstNode).Evaluate(null);
            arg2 = ((AstNode)treeNode.ChildNodes[2].AstNode).Evaluate(null);
        }

        protected override object DoEvaluate(ScriptThread thread)
        {
            return operador(Convert.ToDouble(arg1),Convert.ToDouble(arg2));
        }
    }
}