using System;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Clase abstracta base para formas geométricas.
    /// Responsabilidad única: Definir contrato para cálculos geométricos.
    /// </summary>
    public abstract class FormaGeometrica
    {
        /// <summary>
        /// Nombre de la forma para identificación y traducción.
        /// </summary>
        public abstract string Nombre { get; }

        /// <summary>
        /// Calcula el área de la forma geométrica.
        /// </summary>
        /// <returns>Área en unidades cuadradas</returns>
        public abstract double CalcularArea();

        /// <summary>
        /// Calcula el perímetro de la forma geométrica.
        /// </summary>
        /// <returns>Perímetro en unidades lineales</returns>
        public abstract double CalcularPerimetro();
    }
}
