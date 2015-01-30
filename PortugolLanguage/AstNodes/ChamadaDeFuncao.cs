using System;
using System.Collections.Generic;
using System.Linq;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage.AstNodes
{
    public class ChamadaDeFuncao : AstNode
    {
        private string nomeDaFunca;
        private readonly List<double> argumentos = new List<double>();

        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            nomeDaFunca = treeNode.ChildNodes[0].FindTokenAndGetText();
            if (treeNode.ChildNodes.Count < 2) return;

            foreach (var mappedChildNode in treeNode.ChildNodes[1].ChildNodes)
                argumentos.Add(Convert.ToDouble(mappedChildNode.FindToken().Value));
        }

        protected override object DoEvaluate(ScriptThread thread)
        {
            if (nomeDaFunca.ToLower() == "randomico")
            {
                var r = new Random();
                if (argumentos.Any())
                    return r.Next((int)argumentos.ElementAt(0));
                return r.Next();
            }
            throw new ArgumentException("Funcao desconhecida");
        }
    }
}