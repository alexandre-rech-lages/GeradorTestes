using eAgenda.Infra.Arquivos;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.IO;
using System.Windows.Forms;

namespace GeradorTeste.WinApp
{
    internal static class Program
    {
        static ISerializador serializador = new SerializadorDadosEmJsonDotnet();

        static DataContext contexto = new DataContext(serializador);


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException +=
               new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(@"C:\temp\dados.json") == false)
                ConfigurarAplicacao();

            Application.Run(new TelaPrincipalForm(contexto));

            contexto.GravarDados();
        }

        private static void ConfigurarAplicacao()
        {
            ConfigurarQuestoesMatematica();

            ConfigurarQuestoesPortugues();
        }

        private static void ConfigurarQuestoesPortugues()
        {
            var portugues = new Disciplina
            {
                Numero = 5,
                Nome = "Português"
            };

            var vogais = new Materia
            {
                Numero = 6,
                Nome = "Vogais",
                Serie = SerieMateriaEnum.Primeira
            };

            vogais.ConfigurarDisciplina(portugues);

            var consoantes = new Materia
            {
                Numero = 4,
                Nome = "Consoantes",
                Serie = SerieMateriaEnum.Primeira
            };

            consoantes.ConfigurarDisciplina(portugues);

            Questao q1 = NovaQuestaoPortugues(consoantes, 41, 17, 'C', 'A');
            Questao q2 = NovaQuestaoPortugues(consoantes, 42, 21, 'E', 'C');
            Questao q3 = NovaQuestaoPortugues(consoantes, 43, 25, 'G', 'E');
            Questao q4 = NovaQuestaoPortugues(consoantes, 44, 29, 'I', 'G');

            contexto.Disciplinas.Add(portugues);

            contexto.Materias.Add(vogais);
            contexto.Materias.Add(consoantes);

            contexto.Questoes.Add(q1);
            contexto.Questoes.Add(q2);
            contexto.Questoes.Add(q3);
            contexto.Questoes.Add(q4);
        }

        private static Questao NovaQuestaoPortugues(Materia materia, int idQuestao, int idAlternativa, char letra, char resposta)
        {
            var questao = new Questao
            {
                Numero = idQuestao,
                Enunciado = $"Depois da letra {letra} qual é a próxima letra do alfabeto?"
            };

            questao.ConfigurarMateria(materia);

            Alternativa[] alternativas = new Alternativa[4];

            for (int i = 0; i < 4; i++)
            {
                alternativas[i] = new Alternativa
                {
                    Numero = idAlternativa + i + 1,
                    Resposta = ((char)(resposta + (i + 1))).ToString()
                };
            }

            foreach (var item in alternativas)
            {
                item.Letra = questao.GerarLetraAlternativa();
                questao.AdicionarAlternativa(item);
            }

            questao.AtualizarAlternativaCorreta(alternativas[2]);

            return questao;
        }

        private static void ConfigurarQuestoesMatematica()
        {
            var matematica = new Disciplina
            {
                Numero = 1,
                Nome = "Matemática"
            };

            contexto.Disciplinas.Add(matematica);

            var adicaoUnidades = new Materia
            {
                Numero = 1,
                Nome = "Adição de Unidades",
                Serie = SerieMateriaEnum.Primeira
            };

            adicaoUnidades.ConfigurarDisciplina(matematica);

            var adicaoDezenas = new Materia
            {
                Numero = 2,
                Nome = "Adição de Dezenas",
                Serie = SerieMateriaEnum.Primeira
            };

            adicaoDezenas.ConfigurarDisciplina(matematica);

            var adicaoCentenas = new Materia
            {
                Numero = 3,
                Nome = "Adição de Centenas",
                Serie = SerieMateriaEnum.Segunda
            };

            adicaoCentenas.ConfigurarDisciplina(matematica);

            var adicaoMilhar = new Materia
            {
                Numero = 4,
                Nome = "Adição de Milhar",
                Serie = SerieMateriaEnum.Segunda
            };

            adicaoMilhar.ConfigurarDisciplina(matematica);

            contexto.Materias.Add(adicaoUnidades);
            contexto.Materias.Add(adicaoDezenas);
            contexto.Materias.Add(adicaoCentenas);
            contexto.Materias.Add(adicaoMilhar);

            var materias = new Materia[] { adicaoUnidades, adicaoDezenas, adicaoCentenas, adicaoMilhar };

            int contadorAlternativa = 1;
            int resposta = 0;

            for (int i = 1; i < 40; i++)
            {
                if (i % 10 == 0)
                {
                    resposta = 1;
                    continue;
                }

                int fator, posicaoMateria;                

                if (i <= 10)
                {
                    fator = 1; posicaoMateria = 0;
                }

                else if (i <= 20)
                {
                    fator = 10; posicaoMateria = 1;
                }

                else if (i <= 30)
                {
                    fator = 100; posicaoMateria = 2;
                }

                else
                {
                    fator = 1000; posicaoMateria = 3;
                }

                Questao q = NovaQuestaoMatematica(materias[posicaoMateria], i, contadorAlternativa, resposta++, fator);

                contadorAlternativa += 4;

                contexto.Questoes.Add(q);

                
            }
        }

        private static Questao NovaQuestaoMatematica(Materia materia, int idQuestao, int idAlternativa, int resposta, int fator)
        {
            var questao = new Questao
            {
                Numero = idQuestao,
                Enunciado = $"Quanto é {fator * resposta} + {fator * resposta } ?"
            };

            questao.ConfigurarMateria(materia);

            Alternativa[] alternativas = new Alternativa[4];

            for (int i = 0; i < 4; i++)
            {
                alternativas[i] = new Alternativa
                {
                    Numero = idAlternativa + i + 1,
                    Resposta = (resposta * 2 * i).ToString()
                };
            }

            foreach (var item in alternativas)
            {
                item.Letra = questao.GerarLetraAlternativa();
                questao.AdicionarAlternativa(item);
            }

            questao.AtualizarAlternativaCorreta(alternativas[2]);

            return questao;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            contexto.GravarDados();
        }
    }
}
