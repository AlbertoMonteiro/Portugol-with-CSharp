using Irony.Parsing;

namespace PortugolLanguage
{
    public class Portugol : Grammar
    {
        public Portugol()
            : base(false)
        {
            LanguageFlags = LanguageFlags.CreateAst;

            var numero = new NumberLiteral("Numero");
            var identificador = new IdentifierTerminal("Identificador");

            var expressao = new NonTerminal("Expressao");
            var termo = new NonTerminal("Termo");
            var chamadaFuncao = new NonTerminal("Chamada funcao", typeof(ChamadaDeFuncao));
            var operacaoBinaria = new NonTerminal("Operacao binaria", typeof(OperacaoBinaria));
            var operacaoComParentese = new NonTerminal("Operacao com parentese");
            var se = new NonTerminal("Se", typeof(CondicaoSe));
            var operador = new NonTerminal("Operador");
            var operadorLogico = new NonTerminal("Operador logico");
            var argumentos = new NonTerminal("Argumentos");
            var sePart = new NonTerminal("Se parte");
            var entaoPart = new NonTerminal("Entao parte");
            var senaoPart = new NonTerminal("Senao parte");

            expressao.Rule = operacaoBinaria | operacaoComParentese | se | chamadaFuncao | termo;
            termo.Rule = numero;
            operacaoComParentese.Rule = ToTerm("(") + expressao + ")";
            operacaoBinaria.Rule = expressao + operador + expressao;
            operador.Rule = ToTerm("+") | "-" | "*" | "/" | "=" | "<" | ">" | "<=" | ">=" | "<>" | "E" | "OU";

            sePart.Rule = ToTerm("Se");
            entaoPart.Rule = ToTerm("Entao");
            senaoPart.Rule = ToTerm("Senao");

            se.Rule = sePart + expressao + entaoPart + expressao + senaoPart + expressao;
            
            argumentos.Rule = MakePlusRule(argumentos, ToTerm(","), expressao);

            chamadaFuncao.Rule = identificador | identificador + "(" + argumentos + ")";

            RegisterOperators(1, "E", "OU");
            RegisterOperators(5, "=" , "<" , ">" , "<=" , ">=" , "<>");
            RegisterOperators(10, "+", "-");
            RegisterOperators(20, "*", "/");
            MarkPunctuation("(", ")");
            RegisterBracePair("(", ")");

            MarkTransient(operador, expressao, termo, operadorLogico, operacaoComParentese);

            Root = expressao;
        }
    }
}