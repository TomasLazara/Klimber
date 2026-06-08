using System.Collections.Generic;
using DevelopmentChallenge.Data.Classes;
using DevelopmentChallenge.Data.Enums;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Tests BDD para Feature: Generar reporte de formas geométricas
    /// Mapeo 1:1 con scenarios en 04_generar_reporte.feature
    /// </summary>
    [TestFixture]
    public class GenerarReporteTests
    {
        private ReporteFormateador _formateador;

        [SetUp]
        public void SetUp()
        {
            _formateador = new ReporteFormateador();
        }

        [TestCase]
        public void GenerarReporteConListaVaciaEnCastellano()
        {
            // Given una lista vacía de formas
            var formas = new List<FormaGeometrica>();

            // When genero el reporte en Castellano
            var reporte = _formateador.GenerarReporte(formas, Idioma.Castellano);

            // Then el reporte contiene "Lista vacía de formas"
            Assert.That(reporte, Does.Contain("Lista vacía de formas"));
        }

        [TestCase]
        public void GenerarReporteConListaVaciaEnIngles()
        {
            // Given una lista vacía de formas
            var formas = new List<FormaGeometrica>();

            // When genero el reporte en Ingles
            var reporte = _formateador.GenerarReporte(formas, Idioma.Ingles);

            // Then el reporte contiene "Empty list of shapes"
            Assert.That(reporte, Does.Contain("Empty list of shapes"));
        }

        [TestCase]
        public void GenerarReporteConListaVaciaEnItaliano()
        {
            // Given una lista vacía de formas
            var formas = new List<FormaGeometrica>();

            // When genero el reporte en Italiano
            var reporte = _formateador.GenerarReporte(formas, Idioma.Italiano);

            // Then el reporte contiene "Elenco vuoto di forme"
            Assert.That(reporte, Does.Contain("Elenco vuoto di forme"));
        }

        [TestCase]
        public void GenerarReporteConUnCuadradoEnCastellano()
        {
            // Given un Cuadrado con lado 5
            var formas = new List<FormaGeometrica> { new Cuadrado(5) };

            // When genero el reporte en Castellano
            var reporte = _formateador.GenerarReporte(formas, Idioma.Castellano);

            // Then el reporte contiene "1 Cuadrado"
            Assert.That(reporte, Does.Contain("1 Cuadrado"));
            // And el reporte contiene "Area 25"
            Assert.That(reporte, Does.Contain("Area 25"));
            // And el reporte contiene "Perimetro 20"
            Assert.That(reporte, Does.Contain("Perimetro 20"));
            // And el reporte contiene "TOTAL"
            Assert.That(reporte, Does.Contain("TOTAL"));
            // And el reporte contiene "1 formas Perimetro 20 Area 25"
            Assert.That(reporte, Does.Contain("1 formas Perimetro 20 Area 25"));
        }

        [TestCase]
        public void GenerarReporteConMultiplesCuadradosEnIngles()
        {
            // Given un Cuadrado con lado 5
            // And un Cuadrado con lado 1
            // And un Cuadrado con lado 3
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(1),
                new Cuadrado(3)
            };

            // When genero el reporte en Ingles
            var reporte = _formateador.GenerarReporte(formas, Idioma.Ingles);

            // Then el reporte contiene "3 Squares"
            Assert.That(reporte, Does.Contain("3 Squares"));
            // And el reporte contiene "Area 35"
            Assert.That(reporte, Does.Contain("Area 35"));
            // And el reporte contiene "Perimeter 36"
            Assert.That(reporte, Does.Contain("Perimeter 36"));
            // And el reporte contiene "TOTAL"
            Assert.That(reporte, Does.Contain("TOTAL"));
            // And el reporte contiene "3 shapes Perimeter 36 Area 35"
            Assert.That(reporte, Does.Contain("3 shapes Perimeter 36 Area 35"));
        }

        [TestCase]
        public void GenerarReporteConMultiplesTiposDeFormasEnIngles()
        {
            // Given un Cuadrado con lado 5
            // And un Cuadrado con lado 2
            // And un Círculo con diámetro 3
            // And un Círculo con diámetro 2.75
            // And un Triángulo con lado 4
            // And un Triángulo con lado 9
            // And un Triángulo con lado 4.2
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(2),
                new Circulo(3),
                new Circulo(2.75),
                new Triangulo(4),
                new Triangulo(9),
                new Triangulo(4.2)
            };

            // When genero el reporte en Ingles
            var reporte = _formateador.GenerarReporte(formas, Idioma.Ingles);

            // Then el reporte contiene "2 Squares"
            Assert.That(reporte, Does.Contain("2 Squares"));
            // And el reporte contiene "2 Circles"
            Assert.That(reporte, Does.Contain("2 Circles"));
            // And el reporte contiene "3 Triangles"
            Assert.That(reporte, Does.Contain("3 Triangles"));
            // And el reporte contiene "TOTAL"
            Assert.That(reporte, Does.Contain("TOTAL"));
            // And el reporte contiene "7 shapes"
            Assert.That(reporte, Does.Contain("7 shapes"));
        }

        [TestCase]
        public void GenerarReporteConMultiplesTiposDeFormasEnCastellano()
        {
            // Given un Cuadrado con lado 5
            // And un Cuadrado con lado 2
            // And un Círculo con diámetro 3
            // And un Círculo con diámetro 2.75
            // And un Triángulo con lado 4
            // And un Triángulo con lado 9
            // And un Triángulo con lado 4.2
            var formas = new List<FormaGeometrica>
            {
                new Cuadrado(5),
                new Cuadrado(2),
                new Circulo(3),
                new Circulo(2.75),
                new Triangulo(4),
                new Triangulo(9),
                new Triangulo(4.2)
            };

            // When genero el reporte en Castellano
            var reporte = _formateador.GenerarReporte(formas, Idioma.Castellano);

            // Then el reporte contiene "2 Cuadrados"
            Assert.That(reporte, Does.Contain("2 Cuadrados"));
            // And el reporte contiene "2 Círculos"
            Assert.That(reporte, Does.Contain("2 Círculos"));
            // And el reporte contiene "3 Triángulos"
            Assert.That(reporte, Does.Contain("3 Triángulos"));
            // And el reporte contiene "TOTAL"
            Assert.That(reporte, Does.Contain("TOTAL"));
            // And el reporte contiene "7 formas"
            Assert.That(reporte, Does.Contain("7 formas"));
        }

        [TestCase]
        public void GenerarReporteConTrapecioEnItaliano()
        {
            // Given un Trapecio con base mayor 10, base menor 4 y altura 4
            var formas = new List<FormaGeometrica>
            {
                new Trapecio(10, 4, 4)
            };

            // When genero el reporte en Italiano
            var reporte = _formateador.GenerarReporte(formas, Idioma.Italiano);

            // Then el reporte contiene "1 Trapezio"
            Assert.That(reporte, Does.Contain("1 Trapezio"));
            // And el reporte contiene "Area 28"
            Assert.That(reporte, Does.Contain("Area 28"));
        }
    }
}
