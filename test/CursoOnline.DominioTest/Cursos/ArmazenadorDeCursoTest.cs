using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
   
   public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCursos;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
             _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao =  fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50,1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000,2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
             _armazenadorDeCursos = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }
        // mock - Quando é realizada a verificação
        [Fact]
        public void DeveAdicionarCurso()
        {
          
            _armazenadorDeCursos.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    C => C.Nome == _cursoDto.Nome &&
                    C.Descricao == _cursoDto.Descricao
                    )), Times.AtLeast(1));
        }
        //stub apenas "setapear", configurar um comportamento
        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);


            Assert.Throws<ArgumentException>(() => _armazenadorDeCursos.Armazenar(_cursoDto)).ComMensagem("Nome do curso já consta no banco de dados");
        }
        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;
            Assert.Throws<ArgumentException>(() => _armazenadorDeCursos.Armazenar(_cursoDto)).ComMensagem("Publico alvo inválido");
        }
      
   

     
        
       
    }
}
