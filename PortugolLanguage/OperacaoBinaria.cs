using System;
using System.Linq;
using System.Globalization;
using System.Threading;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class OperacaoBinaria : Node
    {
        private dynamic arg1, arg2;
        private string operador;

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);
            operador = treeNode.MappedChildNodes[1].FindTokenAndGetText();

            arg1 = GetValue(treeNode, 0);
            arg2 = GetValue(treeNode, 2);
        }

        public override dynamic Eval()
        {
            double resultado;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("EN");
            var operadores = new[] { "+", "-", "*", "/" };

            if (operadores.Contains(operador.ToLower()))
            {
                return OperadoresComuns();
            }
            try
            {
                switch (operador.ToLower())
                {
                    case "=": resultado = arg1 == arg2;
                        break;
                    case "<": resultado = arg1 < arg2;
                        break;
                    case ">": resultado = arg1 > arg2;
                        break;
                    case "<=": resultado = arg1 <= arg2;
                        break;
                    case ">=": resultado = arg1 >= arg2;
                        break;
                    case "<>": resultado = arg1 != arg2;
                        break;
                    case "e": resultado = arg1 && arg2;
                        break;
                    case "ou": resultado = arg1 || arg2;
                        break;
                    default: resultado = 0;
                        break;
                }
            }
            catch (Exception)
            {
                resultado = 0;
            }
            return resultado;
        }

        private dynamic OperadoresComuns()
        {
            arg1 = Convert.ToDouble(arg1);
            arg2 = Convert.ToDouble(arg2);
            switch (operador.ToLower())
            {
                case "+":
                    return arg1 + arg2;
                case "-":
                    return arg1 - arg2;
                case "*":
                    return arg1 * arg2;
                case "/":
                    return arg1 / arg2;
                default:
                    return 0;
            }
        }
    }
}