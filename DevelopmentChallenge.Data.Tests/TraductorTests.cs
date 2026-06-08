using System;
using DevelopmentChallenge.Data.Classes;
using DevelopmentChallenge.Data.Enums;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Tests BDD para Feature: Traductor multiidioma
    /// Mapeo 1:1 con scenarios en 05_traductor.feature
    /// </summary>
    [TestFixture]
    public class TraductorTests
    {
        private Traductor _traductor;

        [SetUp]
        public void SetUp()
        {
            _traductor = new Traductor();
        }

        #region Traducir claves

        [TestCase]
        public void TraducirClaveExistenteEnCastellano()
        {
            // Given la clave "Cuadrado"
            var clave = "Cuadrado";

            // When traduzco al Castellano
            var resultado = _traductor.Traducir(clave, Idioma.Castellano);

            // Then el resultado es "Cuadrado"
            Assert.AreEqual("Cuadrado", resultado);
        }

        [TestCase]
        public void TraducirClaveExistenteEnIngles()
        {
            // Given la clave "Cuadrado"
            var clave = "Cuadrado";

            // When traduzco al Ingles
            var resultado = _traductor.Traducir(clave, Idioma.Ingles);

            // Then el resultado es "Square"
            Assert.AreEqual("Square", resultado);
        }

        [TestCase]
        public void TraducirClaveExistenteEnItaliano()
        {
            // Given la clave "Cuadrado"
            var clave = "Cuadrado";

            // When traduzco al Italiano
            var resultado = _traductor.Traducir(clave, Idioma.Italiano);

            // Then el resultado es "Quadrato"
            Assert.AreEqual("Quadrato", resultado);
        }

        #endregion

        #region Fallback

        [TestCase]
        public void TraducirClaveInexistenteConFallback()
        {
            // Given la clave "ClaveInexistente"
            var clave = "ClaveInexistente";

            // When traduzco al Italiano
            var resultado = _traductor.Traducir(clave, Idioma.Italiano);

            // Then el resultado es "ClaveInexistente"
            Assert.AreEqual("ClaveInexistente", resultado);
        }

        #endregion

        #region Pluralización

        [TestCase]
        public void TraducirFormaEnSingularEnCastellano()
        {
            // Given la forma "Cuadrado" con cantidad 1
            var forma = "Cuadrado";
            var cantidad = 1;

            // When traduzco la forma al Castellano
            var resultado = _traductor.TraducirForma(forma, cantidad, Idioma.Castellano);

            // Then el resultado es "Cuadrado"
            Assert.AreEqual("Cuadrado", resultado);
        }

        [TestCase]
        public void TraducirFormaEnPluralEnCastellano()
        {
            // Given la forma "Cuadrado" con cantidad 3
            var forma = "Cuadrado";
            var cantidad = 3;

            // When traduzco la forma al Castellano
            var resultado = _traductor.TraducirForma(forma, cantidad, Idioma.Castellano);

            // Then el resultado es "Cuadrados"
            Assert.AreEqual("Cuadrados", resultado);
        }

        [TestCase]
        public void TraducirFormaConCantidadCeroEnIngles()
        {
            // Given la forma "Circle" con cantidad 0
            var forma = "Circle";
            var cantidad = 0;

            // When traduzco la forma al Ingles
            var resultado = _traductor.TraducirForma(forma, cantidad, Idioma.Ingles);

            // Then el resultado es "Circles"
            Assert.AreEqual("Circles", resultado);
        }

        #endregion

        #region Validaciones

        [TestCase]
        public void TraducirConClaveVacia()
        {
            // Given una clave vacía
            var clave = "";

            // When intento traducir
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => _traductor.Traducir(clave, Idioma.Castellano));

            // And el mensaje contiene "clave no puede ser vacía"
            Assert.That(ex.Message, Does.Contain("clave no puede ser vacía"));
        }

        [TestCase]
        public void TraducirConClaveNull()
        {
            // Given una clave null
            string clave = null;

            // When intento traducir
            // Then se lanza ArgumentNullException
            Assert.Throws<ArgumentNullException>(() => _traductor.Traducir(clave, Idioma.Castellano));
        }

        #endregion
    }
}
