using System;
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

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("EN");
            base.Init(context, treeNode);
            operador = treeNode.ChildNodes[1].Evaluate<Func<double, double, object>>();

            arg1 = ((AstNode) treeNode.ChildNodes[0].AstNode).Evaluate(null);
            arg2 = ((AstNode)treeNode.ChildNodes[2].AstNode).Evaluate(null);
        }

        protected override object DoEvaluate(ScriptThread thread)
        {
            return operador(Convert.ToDouble(arg1),Convert.ToDouble(arg2));
        }
    }
}