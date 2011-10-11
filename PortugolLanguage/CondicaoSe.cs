using System;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class CondicaoSe : Node
    {
        private bool resultadoCondicao;
        private double resultadoVerdadeiro, resultadoFalso;

        public override void Init(ParsingContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            dynamic eval = ((Node) AddChild("Arg", treeNode.MappedChildNodes[1])).Eval();
            resultadoCondicao = Convert.ToBoolean(eval);

            resultadoVerdadeiro = treeNode.MappedChildNodes[3].AstNode != null ?
                ((Node)AddChild("Arg", treeNode.MappedChildNodes[3])).Eval() :
                int.Parse(treeNode.MappedChildNodes[3].FindTokenAndGetText());

            resultadoFalso = treeNode.MappedChildNodes[5].AstNode != null ?
                ((Node)AddChild("Arg", treeNode.MappedChildNodes[5])).Eval() :
                int.Parse(treeNode.MappedChildNodes[5].FindTokenAndGetText());
        }

        public override dynamic Eval()
        {
            return resultadoCondicao ? resultadoVerdadeiro : resultadoFalso;
        }
    }
}