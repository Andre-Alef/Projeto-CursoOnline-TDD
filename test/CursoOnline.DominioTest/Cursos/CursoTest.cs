using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {

        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly double _valor;
        private PublicoAlvo _publicoAlvo;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Contrutor sendo executado");


            var faker = new Faker();
            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100,1000);
            _descricao = faker.Lorem.Paragraph();

            
          
        }
        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

         

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo,cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
          
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome inválido");

          
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQueUm(double cargaHoraria)
        {

            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHoraria).Build())
                .ComMensagem("Carga horaria invalida");
            
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double Valor)
        {
          

            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComValor(Valor).Build())
                .ComMensagem("Valor invalido");
            
        }

      
    }


    

    
}
