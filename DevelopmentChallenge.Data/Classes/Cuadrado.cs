using System;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Representa un cuadrado.
    /// </summary>
    public class Cuadrado : FormaGeometrica
    {
        private readonly double _lado;

        /// <summary>
        /// Nombre de la forma.
        /// </summary>
        public override string Nombre
        {
            get { return "Cuadrado"; }
        }

        /// <summary>
        /// Crea un cuadrado con el lado especificado.
        /// </summary>
        /// <param name="lado">Longitud del lado (debe ser mayor a 0)</param>
        /// <exception cref="ArgumentException">Si el lado es menor o igual a 0</exception>
        public Cuadrado(double lado)
        {
            if (lado <= 0)
                throw new ArgumentException("lado debe ser mayor a 0", nameof(lado));

            _lado = lado;
        }

        /// <summary>
        /// Calcula el área del cuadrado.
        /// Fórmula: lado²
        /// </summary>
        public override double CalcularArea()
        {
            return _lado * _lado;
        }

        /// <summary>
        /// Calcula el perímetro del cuadrado.
        /// Fórmula: lado × 4
        /// </summary>
        public override double CalcularPerimetro()
        {
            return _lado * 4;
        }
    }
}
