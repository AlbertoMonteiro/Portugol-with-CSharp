using Irony.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PortugolLanguage.Test
{
    [TestClass]
    public class DadoUmaGramaticaPortugol
    {
        private Portugol grammar;
        private Parser parser;

        [TestInitialize]
        public void Setup()
        {
            grammar = new Portugol();
            parser = new Parser(grammar);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSomaSimples()
        {
            const string EXPRESSION = "1+3";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node) parseTree.Root.AstNode).Eval();
            Assert.AreEqual(4, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSomaComNumeroNegativo()
        {
            const string EXPRESSION = "-1+3";
            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeSubtracaoSimples()
        {
            const string EXPRESSION = "3 - 1.5";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(1.5, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeDivisaoSimples()
        {
            const string EXPRESSION = "7 / 2";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(3.5, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeMultiplicacaoSimples()
        {
            const string EXPRESSION = "1.25 * 5.80";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(7.25, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDePotencia()
        {
            const string EXPRESSION = "2 ^ 3";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(8, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoDeResto()
        {
            const string EXPRESSION = "15 % 2";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void PossoExecutarUmaInstrucaoComTodasAsOperacoesJuntas()
        {
            const string EXPRESSION = "(((3 + 5) + (6 / 2) - 1) / 2) ^ 2";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(25, value);
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

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(30000, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoSeComAAfirmacaoFalsaUsandoOu()
        {
            const string EXPRESSION = @"SE 1 > 1 
                                        OU -2 > -1 ENTAO                         
                                            30000
                                        SENAO
                                            60000";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(60000, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoSeComAAfirmacaoFalsa()
        {
            const string EXPRESSION = @"SE 1 < 0 e 2 < 1 ENTAO
                                            30000
                                        SENAO
                                            60000";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(60000, value);
        }

        [TestMethod]
        public void PossoFazerUmaInstrucaoComBlocosDeComentarios()
        {
            const string EXPRESSION = @"//Comentário
                                       /*Bloco de
                                        Comentario
                                        */
                                        SE 1 < 0 e 2 < 1 ENTAO
                                            30000
                                        SENAO
                                            60000";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(60000, value);
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

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(27, value);
        }

        [TestMethod]
        public void SePassarUmAlgoritmoVazioOCompiladorDeveRetornarNull()
        {
            const string EXPRESSION = "";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = parseTree.HasErrors();

            Assert.IsTrue(value);
        }

//        [TestMethod]
//        public void DeveLancarFunctionNotFoundExceptionSePassarUmaFuncaoOuPalavraNaoReconhecida()
//        {
//            const string EXPRESSION = @"SE salarioContratual > 1000 ENTAO
//                                            30000
//                                        SENAO
//                                            60000";

//            ParseTree parseTree = parser.Parse(EXPRESSION);

//            try
//            {
//                compiler.Compile().Eval(null);
//            }
//            catch (FunctionNotFoundException ex)
//            {
//                Assert.AreEqual("Função não identificada: salarioContratual", ex.Message);
//            }
//        }

        [TestMethod]
        public void PossoFazerComparacaoComDatas()
        {
            const string EXPRESSION = @"SE 06.10.2011 > 05.10.2010 ENTAO
                                            1
                                        SENAO
                                            0";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void PossoObterADiferencaEntreDuasDatas()
        {
            const string EXPRESSION = @"06.10.2011 - 05.10.2010";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

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

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(1, value);
        }

//        [TestMethod]
//        public void DeveLancarTermExpectedExceptionCasoFalteAlgumOperandoNoAlgoritmo()
//        {
//            const string EXPRESSION = @"1 +";

//            ParseTree parseTree = parser.Parse(EXPRESSION);

//            try
//            {
//                compiler.Compile().Eval(null);
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOf(typeof(TermExpectedException), ex);
//            }
//        }

//        [TestMethod]
//        public void DeveLancarTermExpectedExceptionCasoOAlgoritmoNaoEncontreAInstrucaoSENAO()
//        {
//            const string EXPRESSION = @"SE 1 + 1 = 2 ENTAO
//                                        2";

//            ParseTree parseTree = parser.Parse(EXPRESSION);

//            try
//            {
//                compiler.Compile().Eval(null);
//            }
//            catch (Exception ex)
//            {
//                Assert.IsInstanceOf(typeof(TermExpectedException), ex);
//                Assert.AreEqual("Instrução Esperada: 'SENAO'", ex.Message);
//            }
//        }

        [TestMethod]
        public void PossoRetornarUmValorNegativo()
        {
            const string EXPRESSION = @"SE 0 - 1 = -1 ENTAO
                                        -1
                                       SENAO
                                       + 1";

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

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

            ParseTree parseTree = parser.Parse(EXPRESSION);
            var value = ((Node)parseTree.Root.AstNode).Eval();

            Assert.AreEqual(2000, value);
        }
    }
}
