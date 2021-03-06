﻿using System;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace PortugolLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Portugol REPL";
            Console.WriteLine(@"Exemplo de código:
    
    Randomico(Randomico(100)) * 2 +  
    SE 5 <= 2 ENTAO 3 SENAO 
    SE 1 = 1 ENTAO 10 SENAO 20
===================
Operadores binários: +, -, * , /
Operadores lógicos: =, <, >, <=, >=, <>
Condição SE: SE condicao ENTAO expressao SENAO expressao
Chamada de função: NomeDaFuncao, NomeDaFuncao(1,2)...
===================


A unica função implementada foi a Randomico que recebe nenhum ou 1 parametro");

            Console.WriteLine("\nEntre com sua expressão\n");
            Console.SetCursorPosition(Console.CursorLeft + 4, Console.CursorTop - 1);
            Grammar grammar = new Portugol();

            var parser = new Parser(grammar);
            string expressao = Console.ReadLine();
            var tree = parser.Parse(expressao);
            /*var scriptApp = new ScriptApp(parser.Language);
            var result = scriptApp.Evaluate(tree);*/

            
            if (!tree.HasErrors() && tree.Root.AstNode != null)
            {
                var astNode = (AstNode)tree.Root.AstNode;
                var result = astNode.Evaluate(null);
                Console.Write("Resultado:\n    {0}\n", result);
            }
            else
                foreach (var parserMessage in tree.ParserMessages)
                    Console.WriteLine(parserMessage);
            Console.WriteLine("Precione qualquer tecla para sair...");
            Console.ReadLine();
        }
    }
}
