using System;
using System.Linq;
using System.Globalization;
using System.Threading;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class OperacaoBinaria : AstNode
    {
        private dynamic arg1, arg2;
        private string operador;

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("EN");
            base.Init(context, treeNode);
            operador = treeNode.ChildNodes[1].FindTokenAndGetText();

            arg1 = ((AstNode) treeNode.ChildNodes[0].AstNode).Evaluate(null);
            arg2 = ((AstNode)treeNode.ChildNodes[2].AstNode).Evaluate(null);
        }

        protected override object DoEvaluate(ScriptThread thread)
        {
            var operadores = new[] { "+", "-", "*", "/", "%", "^" };

            if (operadores.Contains(operador.ToLower()))
                return OperadoresComuns();

            try
            {
                switch (operador.ToLower())
                {
                    case "=": return arg1 == arg2;
                    case "<": return arg1 < arg2;
                    case ">": return arg1 > arg2;
                    case "<=": return arg1 <= arg2;
                    case ">=": return arg1 >= arg2;
                    case "<>": return arg1 != arg2;
                    case "e": return Convert.ToBoolean(arg1) && Convert.ToBoolean(arg2);
                    case "ou": return Convert.ToBoolean(arg1) || Convert.ToBoolean(arg2);
                    default: return 0;
                }
            }
            catch (Exception) { return 0; }
        }


        private dynamic OperadoresComuns()
        {
            arg1 = Convert.ToDouble(arg1);
            arg2 = Convert.ToDouble(arg2);
            switch (operador.ToLower())
            {
                case "+": return arg1 + arg2;
                case "-": return arg1 - arg2;
                case "*": return arg1 * arg2;
                case "/": return arg1 / arg2;
                case "%": return arg1 % arg2;
                case "^": return Math.Pow(arg1,arg2);
                default: return 0;
            }
        }
    }
}