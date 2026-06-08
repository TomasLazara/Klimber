using DevelopmentChallenge.Data.Classes;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Tests BDD para Feature: Calcular perímetro de formas geométricas
    /// Mapeo 1:1 con scenarios en 03_calcular_perimetro.feature
    /// </summary>
    [TestFixture]
    public class CalcularPerimetroTests
    {
        [TestCase]
        public void CalcularPerimetroDeCuadrado()
        {
            // Given un Cuadrado con lado 5
            var cuadrado = new Cuadrado(5);

            // When calculo el perímetro
            var perimetro = cuadrado.CalcularPerimetro();

            // Then el perímetro es 20
            Assert.AreEqual(20, perimetro);
        }

        [TestCase]
        public void CalcularPerimetroDeCirculo()
        {
            // Given un Círculo con diámetro 6
            var circulo = new Circulo(6);

            // When calculo el perímetro
            var perimetro = circulo.CalcularPerimetro();

            // Then el perímetro es 18.85
            Assert.AreEqual(18.85, perimetro, 0.01);
        }

        [TestCase]
        public void CalcularPerimetroDeTriangulo()
        {
            // Given un Triángulo con lado 4
            var triangulo = new Triangulo(4);

            // When calculo el perímetro
            var perimetro = triangulo.CalcularPerimetro();

            // Then el perímetro es 12
            Assert.AreEqual(12, perimetro);
        }

        [TestCase]
        public void CalcularPerimetroDeTrapecioIsosceles()
        {
            // Given un Trapecio con base mayor 10, base menor 4 y altura 4
            var trapecio = new Trapecio(10, 4, 4);

            // When calculo el perímetro
            var perimetro = trapecio.CalcularPerimetro();

            // Then el perímetro es 24
            Assert.AreEqual(24, perimetro);
        }
    }
}
