using System;
using System.Collections.Generic;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage.AstNodes
{
    public class OperadorNode : AstNode
    {
        private string operador;

        private Dictionary<string, Func<double, double, object>> dic = new Dictionary<string, Func<double, double, object>>
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
            base.Init(context, treeNode);
            operador = treeNode.ChildNodes[0].FindTokenAndGetText();
        }

        protected override object DoEvaluate(ScriptThread thread)
        {

            return dic[operador];
        }
    }
}