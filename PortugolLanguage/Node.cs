using System;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage
{
    public abstract class Node : AstNode
    {
        public abstract dynamic Eval();

        protected dynamic GetValue(ParseTreeNode treeNode, int index)
        {
            return Convert.ToDouble(treeNode.MappedChildNodes[index].AstNode != null ?
                ((Node)AddChild("Arg", treeNode.MappedChildNodes[index])).Eval() :
                treeNode.MappedChildNodes[index].FindTokenAndGetText());
        }

        protected dynamic GetValue(ParseTreeNode treeNode)
        {
            return Convert.ToDouble(treeNode.AstNode != null ?
                ((Node)AddChild("Arg", treeNode)).Eval() :
                treeNode.FindTokenAndGetText());
        }
    }
}