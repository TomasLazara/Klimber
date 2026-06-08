using System;
using DevelopmentChallenge.Data.Classes;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    /// <summary>
    /// Tests BDD para Feature: Crear instancias de formas geométricas
    /// Mapeo 1:1 con scenarios en 01_crear_formas.feature
    /// </summary>
    [TestFixture]
    public class CrearFormasTests
    {
        #region Cuadrado

        [TestCase]
        public void CrearCuadradoConLadoValido()
        {
            // Given un lado de 5
            var lado = 5;

            // When creo un Cuadrado
            var cuadrado = new Cuadrado(lado);

            // Then el cuadrado se crea exitosamente
            Assert.IsNotNull(cuadrado);
            // And el lado del cuadrado es 5
            Assert.AreEqual(5, lado);
        }

        [TestCase]
        public void CrearCuadradoConLadoInvalido_Cero()
        {
            // Given un lado de 0
            var lado = 0;

            // When intento crear un Cuadrado
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Cuadrado(lado));

            // And el mensaje contiene "lado debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("lado debe ser mayor a 0"));
        }

        [TestCase]
        public void CrearCuadradoConLadoNegativo()
        {
            // Given un lado de -5
            var lado = -5;

            // When intento crear un Cuadrado
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Cuadrado(lado));

            // And el mensaje contiene "lado debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("lado debe ser mayor a 0"));
        }

        #endregion

        #region Círculo

        [TestCase]
        public void CrearCirculoConDiametroValido()
        {
            // Given un diámetro de 6
            var diametro = 6;

            // When creo un Círculo
            var circulo = new Circulo(diametro);

            // Then el círculo se crea exitosamente
            Assert.IsNotNull(circulo);
            // And el diámetro del círculo es 6
            Assert.AreEqual(6, diametro);
        }

        [TestCase]
        public void CrearCirculoConDiametroInvalido_Cero()
        {
            // Given un diámetro de 0
            var diametro = 0;

            // When intento crear un Círculo
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Circulo(diametro));

            // And el mensaje contiene "diámetro debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("diámetro debe ser mayor a 0"));
        }

        [TestCase]
        public void CrearCirculoConDiametroNegativo()
        {
            // Given un diámetro de -2
            var diametro = -2;

            // When intento crear un Círculo
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Circulo(diametro));

            // And el mensaje contiene "diámetro debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("diámetro debe ser mayor a 0"));
        }

        #endregion

        #region Triángulo

        [TestCase]
        public void CrearTrianguloConLadoValido()
        {
            // Given un lado de 4
            var lado = 4;

            // When creo un Triángulo
            var triangulo = new Triangulo(lado);

            // Then el triángulo se crea exitosamente
            Assert.IsNotNull(triangulo);
            // And el lado del triángulo es 4
            Assert.AreEqual(4, lado);
        }

        [TestCase]
        public void CrearTrianguloConLadoInvalido_Cero()
        {
            // Given un lado de 0
            var lado = 0;

            // When intento crear un Triángulo
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Triangulo(lado));

            // And el mensaje contiene "lado debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("lado debe ser mayor a 0"));
        }

        #endregion

        #region Trapecio

        [TestCase]
        public void CrearTrapecioConBasesYAlturaValidas()
        {
            // Given una base mayor de 10, base menor de 4 y altura de 4
            var baseMayor = 10;
            var baseMenor = 4;
            var altura = 4;

            // When creo un Trapecio
            var trapecio = new Trapecio(baseMayor, baseMenor, altura);

            // Then el trapecio se crea exitosamente
            Assert.IsNotNull(trapecio);
            // And la base mayor del trapecio es 10
            Assert.AreEqual(10, baseMayor);
            // And la base menor del trapecio es 4
            Assert.AreEqual(4, baseMenor);
            // And la altura del trapecio es 4
            Assert.AreEqual(4, altura);
        }

        [TestCase]
        public void CrearTrapecioConBaseMayorInvalida()
        {
            // Given una base mayor de 0, base menor de 4 y altura de 4
            var baseMayor = 0;
            var baseMenor = 4;
            var altura = 4;

            // When intento crear un Trapecio
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Trapecio(baseMayor, baseMenor, altura));

            // And el mensaje contiene "baseMayor debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("baseMayor debe ser mayor a 0"));
        }

        [TestCase]
        public void CrearTrapecioConBaseMenorInvalida()
        {
            // Given una base mayor de 10, base menor de 0 y altura de 4
            var baseMayor = 10;
            var baseMenor = 0;
            var altura = 4;

            // When intento crear un Trapecio
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Trapecio(baseMayor, baseMenor, altura));

            // And el mensaje contiene "baseMenor debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("baseMenor debe ser mayor a 0"));
        }

        [TestCase]
        public void CrearTrapecioConAlturaInvalida()
        {
            // Given una base mayor de 10, base menor de 4 y altura de 0
            var baseMayor = 10;
            var baseMenor = 4;
            var altura = 0;

            // When intento crear un Trapecio
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Trapecio(baseMayor, baseMenor, altura));

            // And el mensaje contiene "altura debe ser mayor a 0"
            Assert.That(ex.Message, Does.Contain("altura debe ser mayor a 0"));
        }

        [TestCase]
        public void CrearTrapecioConBaseMenorMayorQueBaseMayor()
        {
            // Given una base mayor de 4, base menor de 10 y altura de 4
            var baseMayor = 4;
            var baseMenor = 10;
            var altura = 4;

            // When intento crear un Trapecio
            // Then se lanza ArgumentException
            var ex = Assert.Throws<ArgumentException>(() => new Trapecio(baseMayor, baseMenor, altura));

            // And el mensaje contiene "baseMayor debe ser mayor que baseMenor"
            Assert.That(ex.Message, Does.Contain("baseMayor debe ser mayor que baseMenor"));
        }

        #endregion
    }
}
