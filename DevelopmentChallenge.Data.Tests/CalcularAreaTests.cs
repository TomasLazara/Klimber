using DevelopmentChallenge.Data.Classes;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Tests BDD para Feature: Calcular área de formas geométricas
    /// Mapeo 1:1 con scenarios en 02_calcular_area.feature
    /// </summary>
    [TestFixture]
    public class CalcularAreaTests
    {
        [TestCase]
        public void CalcularAreaDeCuadrado()
        {
            // Given un Cuadrado con lado 5
            var cuadrado = new Cuadrado(5);

            // When calculo el área
            var area = cuadrado.CalcularArea();

            // Then el área es 25
            Assert.AreEqual(25, area);
        }

        [TestCase]
        public void CalcularAreaDeCirculo()
        {
            // Given un Círculo con diámetro 6
            var circulo = new Circulo(6);

            // When calculo el área
            var area = circulo.CalcularArea();

            // Then el área es 28.27
            Assert.AreEqual(28.27, area, 0.01);
        }

        [TestCase]
        public void CalcularAreaDeTriangulo()
        {
            // Given un Triángulo con lado 4
            var triangulo = new Triangulo(4);

            // When calculo el área
            var area = triangulo.CalcularArea();

            // Then el área es 6.93
            Assert.AreEqual(6.93, area, 0.01);
        }

        [TestCase]
        public void CalcularAreaDeTrapecio()
        {
            // Given un Trapecio con base mayor 10, base menor 4 y altura 4
            var trapecio = new Trapecio(10, 4, 4);

            // When calculo el área
            var area = trapecio.CalcularArea();

            // Then el área es 28
            Assert.AreEqual(28, area);
        }
    }
}
