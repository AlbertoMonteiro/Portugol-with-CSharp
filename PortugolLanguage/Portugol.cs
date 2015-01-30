using Irony.Interpreter;
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

            var expressao = new NonTerminal("Expressao", typeof (DummyNode));
            var termo = new NonTerminal("Termo", typeof (DummyNode));
            var chamadaFuncao = new NonTerminal("Chamada funcao", typeof (ChamadaDeFuncao));
            var operacaoBinaria = new NonTerminal("Operacao binaria", typeof (OperacaoBinaria));
            var operacaoComParentese = new NonTerminal("Operacao com parentese", typeof (DummyNode));
            var se = new NonTerminal("Se", typeof (CondicaoSe));
            var operador = new NonTerminal("Operador", typeof (OperadorNode));
            var operadorLogico = new NonTerminal("Operador logico", typeof (DummyNode));
            var argumentos = new NonTerminal("Argumentos", typeof (DummyNode));
            var sePart = new NonTerminal("Se parte", typeof (DummyNode));
            var entaoPart = new NonTerminal("Entao parte", typeof (DummyNode));
            var senaoPart = new NonTerminal("Senao parte", typeof (DummyNode));

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

            MarkTransient(expressao, termo, operadorLogico, operacaoComParentese);
            Root = expressao;
            LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}
