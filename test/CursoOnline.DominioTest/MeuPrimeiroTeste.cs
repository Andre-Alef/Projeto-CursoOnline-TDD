using Xunit;

namespace CursoOnline.DominioTest
{
    public class MeuPrimeiroTeste
    {
        [Fact]
        public void Teste()
        {
            var var1 = 1;
            var var2 = 2;

            Assert.Equal(var1, var2);
        }
    }
}
