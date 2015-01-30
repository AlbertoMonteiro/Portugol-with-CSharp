using Irony.Interpreter;
using Irony.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PortugolLanguage.Test
{
    [TestClass]
    public class DadoUmaGramaticaPortugol
    {
        private Portugol grammar;
        private Parser parser;
        private ScriptApp scriptApp;

        [TestInitialize]
        public void Setup()
        {
            grammar = new Portugol();
            parser = new Parser(grammar);
            scriptApp = new ScriptApp(parser.Language);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSomaSimples()
        {
            const string EXPRESSION = "1+3";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(4d, value);
        }

        [TestMethod]
        public void RespeitaProcedencia()
        {
            const string EXPRESSION = "10*1+2";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(12d, value);
        }


        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSomaComNumeroNegativo()
        {
            const string EXPRESSION = "-1+3";
            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(2d, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSubtracaoSimples()
        {
            const string EXPRESSION = "3 - 1.5";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(1.5, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeDivisaoSimples()
        {
            const string EXPRESSION = "7 / 2";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(3.5, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeMultiplicacaoSimples()
        {
            const string EXPRESSION = "1.25 * 5.80";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(7.25, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDePotencia()
        {
            const string EXPRESSION = "2 ^ 3";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(8d, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeResto()
        {
            const string EXPRESSION = "15 % 2";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(1d, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoComTodasAsOperacoesJuntas()
        {
            //5
            const string EXPRESSION = "(((3 + 5) + (6 / 2) - 1) / 2) ^ 2";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(25d, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoSeComAAfirmacaoVerdadeira()
        {
            const string EXPRESSION = @"SE 1 > 0 
                                E 2 > 1 
                                E 1 = 1 
                                E 3 >= 2 
                                E 4 >= 4
                                E 4 <= 4
                                E 5 <> 4
                                E (1 + 1 = 2) ENTAO                          
                                    30000
                                SENAO
                                    60000";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(30000d, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoSeComAAfirmacaoFalsaUsandoOu()
        {
            const string EXPRESSION = @"SE 1 > 1 
                                        OU -2 > -1 ENTAO                         
                                            30000
                                        SENAO
                                            60000";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(60000d, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoSeComAAfirmacaoFalsa()
        {
            const string EXPRESSION = @"SE 1 < 0 e 2 < 1 
                                        ENTAO 30000 
                                        SENAO 60000";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(60000d, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoComBlocosDeComentarios()
        {
            const string EXPRESSION = @"/*Bloco de comentário*/
                                        SE 1 < 0 e 2 < 1 ENTAO
                                            30000
                                        SENAO
                                            60000";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(60000d, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoComBlocosDeSe()
        {
            const string EXPRESSION = @"SE 1 < 0 e 2 < 1 ENTAO
                                    30000
                                SENAO
                                    SE (2 + 2) > 3 ENTAO
                                        27
                                    SENAO
                                        28";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(27d, value);
        }

        [TestMethod]
        public void SePassarUmAlgoritmoVazioOCompiladorDeveRetornarNull()
        {
            const string EXPRESSION = "";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = parseTree.HasErrors();

            Assert.IsTrue(value);
        }

        [TestMethod]
        public void PossoUsarAFuncaoRandomico()
        {
            const string EXPRESSION = "Randomico(4)";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            var valores = new[] { 0, 1, 2, 3 };
            CollectionAssert.Contains(valores, value);
        }

        [TestMethod]
        public void PossoFazerComparacaoComDatas()
        {
            const string EXPRESSION = @"SE 06.10.2011 > 05.10.2010 ENTAO
                                            1
                                        SENAO
                                            0";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void PossoObterADiferencaEntreDuasDatas()
        {
            const string EXPRESSION = @"06.10.2011 - 05.10.2010";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(366, value);
        }

        [TestMethod]
        public void PossoCompararADiferencaEntreDuasDatas()
        {
            const string EXPRESSION = @"SE 06.10.2011 - 05.10.2010 >= 366
                                        OU 06.10.2011 - 05.10.2010 - 1 = 365 ENTAO
                                            1
                                       SENAO
                                            2";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void PossoRetornarUmValorNegativo()
        {
            const string EXPRESSION = @"SE 0 - 1 = -1 ENTAO
                                        -1
                                       SENAO
                                       + 1";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);


            Assert.AreEqual(-1, value);
        }

        [TestMethod]
        public void PossoFazerUmaExpressaoComTresSeAninhados()
        {
            const string EXPRESSION = @"SE 2 = 1 + 1 ENTAO
                                            SE  5 = 3 + 2 ENTAO 
                                                SE 8 = 4 + 4 ENTAO
                                                    2000
                                                SENAO
                                                    0   
                                            SENAO
                                                0
                                        SENAO
                                            0";

            var tree = parser.Parse(EXPRESSION);
            var value = scriptApp.Evaluate(tree);

            Assert.AreEqual(2000d, value);
        }
    }
}
