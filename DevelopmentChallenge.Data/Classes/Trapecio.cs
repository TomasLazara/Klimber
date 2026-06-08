using System;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Representa un trapecio isósceles.
    /// </summary>
    public class Trapecio : FormaGeometrica
    {
        private readonly double _baseMayor;
        private readonly double _baseMenor;
        private readonly double _altura;

        /// <summary>
        /// Nombre de la forma.
        /// </summary>
        public override string Nombre
        {
            get { return "Trapecio"; }
        }

        /// <summary>
        /// Crea un trapecio isósceles con las dimensiones especificadas.
        /// </summary>
        /// <param name="baseMayor">Base mayor (debe ser mayor a 0)</param>
        /// <param name="baseMenor">Base menor (debe ser mayor a 0)</param>
        /// <param name="altura">Altura (debe ser mayor a 0)</param>
        /// <exception cref="ArgumentException">Si algún parámetro es inválido</exception>
        public Trapecio(double baseMayor, double baseMenor, double altura)
        {
            if (baseMayor <= 0)
                throw new ArgumentException("baseMayor debe ser mayor a 0", nameof(baseMayor));

            if (baseMenor <= 0)
                throw new ArgumentException("baseMenor debe ser mayor a 0", nameof(baseMenor));

            if (altura <= 0)
                throw new ArgumentException("altura debe ser mayor a 0", nameof(altura));

            if (baseMayor <= baseMenor)
                throw new ArgumentException("baseMayor debe ser mayor que baseMenor", nameof(baseMayor));

            _baseMayor = baseMayor;
            _baseMenor = baseMenor;
            _altura = altura;
        }

        /// <summary>
        /// Calcula el área del trapecio.
        /// Fórmula: ((baseMayor + baseMenor) / 2) × altura
        /// </summary>
        public override double CalcularArea()
        {
            return (_baseMayor + _baseMenor) / 2 * _altura;
        }

        /// <summary>
        /// Calcula el perímetro del trapecio isósceles.
        /// Fórmula: baseMayor + baseMenor + 2 × ladoInclinado
        /// donde ladoInclinado = √(altura² + ((baseMayor - baseMenor)/2)²)
        /// </summary>
        public override double CalcularPerimetro()
        {
            var diferenciaMediaBases = (_baseMayor - _baseMenor) / 2;
            var ladoInclinado = Math.Sqrt(_altura * _altura + diferenciaMediaBases * diferenciaMediaBases);

            return _baseMayor + _baseMenor + 2 * ladoInclinado;
        }
    }
}
