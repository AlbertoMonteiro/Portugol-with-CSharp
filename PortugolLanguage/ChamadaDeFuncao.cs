using System;
using System.Collections.Generic;
using System.Linq;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class ChamadaDeFuncao : Node
    {
        private string nomeDaFunca;
        private readonly List<double> argumentos = new List<double>();

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            nomeDaFunca = treeNode.MappedChildNodes[0].FindTokenAndGetText();
            if (treeNode.MappedChildNodes.Count < 2) return;
            
            foreach (var mappedChildNode in treeNode.MappedChildNodes[1].MappedChildNodes)
                argumentos.Add(GetValue(mappedChildNode));
        }

        public override dynamic Eval()
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