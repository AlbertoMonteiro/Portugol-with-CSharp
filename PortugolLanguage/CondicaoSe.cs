using System;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class CondicaoSe : AstNode
    {
        private bool resultadoCondicao;
        private double resultadoVerdadeiro, resultadoFalso;


        public override void Init(AstContext context, ParseTreeNode treeNode)
        {
            base.Init(context, treeNode);

            var childNode = (AstNode)treeNode.ChildNodes[1].AstNode;
            resultadoCondicao = Convert.ToBoolean(childNode.Evaluate(null));

            var value = (AstNode) treeNode.ChildNodes[3].AstNode;
            resultadoVerdadeiro = Convert.ToInt32(value.Evaluate(null));

            var parseTreeNode = treeNode.ChildNodes[5];

            var astNode = (AstNode)parseTreeNode.AstNode;
            var o1 = astNode.Evaluate(null);
            resultadoFalso = Convert.ToInt32(o1);
        }

        protected override object DoEvaluate(ScriptThread thread)
        {
            return resultadoCondicao ? resultadoVerdadeiro : resultadoFalso;
        }
    }
}