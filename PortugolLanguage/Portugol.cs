using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;
using PortugolLanguage.AstNodes;

namespace PortugolLanguage
{
    public class Portugol : InterpretedLanguageGrammar
    {
        public Portugol()
            : base(false)
        {
            LanguageFlags = LanguageFlags.CreateAst;

            var numero = new NumberLiteral("Numero", NumberOptions.AllowSign);
            var identificador = new IdentifierTerminal("Identificador");

            var expressao = new NonTerminal("Expressao", typeof (AstNode));
            var termo = new NonTerminal("Termo", typeof (AstNode));
            var chamadaFuncao = new NonTerminal("Chamada funcao", typeof (ChamadaDeFuncao));
            var operacaoBinaria = new NonTerminal("Operacao binaria", typeof (OperacaoBinaria));
            var operacaoComParentese = new NonTerminal("Operacao com parentese", typeof (AstNode));
            var se = new NonTerminal("Se", typeof (CondicaoSe));
            var operador = new NonTerminal("Operador", typeof(AstNode));
            var operadorLogico = new NonTerminal("Operador logico", typeof (AstNode));
            var argumentos = new NonTerminal("Argumentos", typeof (AstNode));
            var sePart = new NonTerminal("Se parte", typeof (AstNode));
            var entaoPart = new NonTerminal("Entao parte", typeof (AstNode));
            var senaoPart = new NonTerminal("Senao parte", typeof (AstNode));

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
            RegisterOperators(5, "=", "<", ">", "<=", ">=", "<>");
            RegisterOperators(10, "+", "-");
            RegisterOperators(20, "*", "/", "%", "^");
            MarkPunctuation("(", ")");
            RegisterBracePair("(", ")");

            MarkTransient(expressao, operador, termo, operadorLogico, operacaoComParentese);
            Root = expressao;
            LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
