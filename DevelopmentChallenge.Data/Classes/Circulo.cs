using System;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Representa un círculo.
    /// </summary>
    public class Circulo : FormaGeometrica
    {
        private readonly double _diametro;

        /// <summary>
        /// Nombre de la forma.
        /// </summary>
        public override string Nombre
        {
            get { return "Circulo"; }
        }

        /// <summary>
        /// Crea un círculo con el diámetro especificado.
        /// </summary>
        /// <param name="diametro">Diámetro del círculo (debe ser mayor a 0)</param>
        /// <exception cref="ArgumentException">Si el diámetro es menor o igual a 0</exception>
        public Circulo(double diametro)
        {
            if (diametro <= 0)
                throw new ArgumentException("diámetro debe ser mayor a 0", nameof(diametro));

            _diametro = diametro;
        }

        /// <summary>
        /// Calcula el área del círculo.
        /// Fórmula: π × (diámetro/2)² = π × radio²
        /// </summary>
        public override double CalcularArea()
        {
            var radio = _diametro / 2;
            return Math.PI * radio * radio;
        }

        /// <summary>
        /// Calcula el perímetro del círculo (circunferencia).
        /// Fórmula: π × diámetro
        /// </summary>
        public override double CalcularPerimetro()
        {
            return Math.PI * _diametro;
        }
    }
}
