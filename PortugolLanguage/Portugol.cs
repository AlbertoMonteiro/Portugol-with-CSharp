using Irony.Ast;
using Irony.Interpreter;
using Irony.Parsing;

namespace PortugolLanguage
{
    public class Portugol : InterpretedLanguageGrammar//Grammar
    {
        public Portugol()
            : base(false)
        {
            LanguageFlags = LanguageFlags.CreateAst;

            var numero = new NumberLiteral("Numero", NumberOptions.AllowSign);
            var identificador = new IdentifierTerminal("Identificador");

            var expressao = new NonTerminal("Expressao", typeof(Node1));
            var termo = new NonTerminal("Termo", typeof(Node2));
            var chamadaFuncao = new NonTerminal("Chamada funcao", typeof(ChamadaDeFuncao));
            var operacaoBinaria = new NonTerminal("Operacao binaria", typeof(OperacaoBinaria));
            var operacaoComParentese = new NonTerminal("Operacao com parentese", typeof(Node3));
            var se = new NonTerminal("Se", typeof(CondicaoSe));
            var operador = new NonTerminal("Operador", typeof(Node4));
            var operadorLogico = new NonTerminal("Operador logico", typeof(Node5));
            var argumentos = new NonTerminal("Argumentos", typeof(Node6));
            var sePart = new NonTerminal("Se parte", typeof(Node7));
            var entaoPart = new NonTerminal("Entao parte", typeof(Node8));
            var senaoPart = new NonTerminal("Senao parte", typeof(Node9));

            NonGrammarTerminals.Add(new CommentTerminal("comment1", "/*", "*/"));

            expressao.Rule = operacaoBinaria | operacaoComParentese | se | chamadaFuncao | termo;
            termo.Rule = numero;
            operacaoComParentese.Rule = ToTerm("(") + expressao + ")";
            operacaoBinaria.Rule = expressao + operador + expressao;
            operador.Rule = ToTerm("+") | "-" | "*" | "/" | "^" | "%" | "=" | "<" | ">" | "<=" | ">=" | "<>" | "E" | "OU";

            sePart.Rule = ToTerm("Se");
            entaoPart.Rule = ToTerm("Entao");
            senaoPart.Rule = ToTerm("Senao");

            se.Rule = sePart + expressao + entaoPart + expressao + senaoPart + expressao;
            
            argumentos.Rule = MakePlusRule(argumentos, ToTerm(","), expressao);

            chamadaFuncao.Rule = identificador | identificador + "(" + argumentos + ")";

            RegisterOperators(1, "E", "OU");
            RegisterOperators(5, "=" , "<" , ">" , "<=" , ">=" , "<>");
            RegisterOperators(10, "+", "-");
            RegisterOperators(20, "*", "/", "%", "^");
            MarkPunctuation("(", ")");
            RegisterBracePair("(", ")");

            MarkTransient(operador, expressao, termo, operadorLogico, operacaoComParentese);
            Root = expressao;
            LanguageFlags = LanguageFlags.CreateAst;
            //this.Root.AstNode = config.DefaultNodeCreator();
        }
    }
}
