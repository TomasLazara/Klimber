using System;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Representa un triángulo equilátero.
    /// </summary>
    public class Triangulo : FormaGeometrica
    {
        private readonly double _lado;

        /// <summary>
        /// Nombre de la forma.
        /// </summary>
        public override string Nombre
        {
            get { return "Triangulo"; }
        }

        /// <summary>
        /// Crea un triángulo equilátero con el lado especificado.
        /// </summary>
        /// <param name="lado">Longitud del lado (debe ser mayor a 0)</param>
        /// <exception cref="ArgumentException">Si el lado es menor o igual a 0</exception>
        public Triangulo(double lado)
        {
            if (lado <= 0)
                throw new ArgumentException("lado debe ser mayor a 0", nameof(lado));

            _lado = lado;
        }

        /// <summary>
        /// Calcula el área del triángulo equilátero.
        /// Fórmula: (√3 / 4) × lado²
        /// </summary>
        public override double CalcularArea()
        {
            return (Math.Sqrt(3) / 4) * _lado * _lado;
        }

        /// <summary>
        /// Calcula el perímetro del triángulo equilátero.
        /// Fórmula: lado × 3
        /// </summary>
        public override double CalcularPerimetro()
        {
            return _lado * 3;
        }
    }
}
