using System.Globalization;
using DevelopmentChallenge.Data.Enums;

namespace DevelopmentChallenge.Data.Helpers
{
    /// <summary>
    /// Helper para operaciones relacionadas con cultura e idioma.
    /// </summary>
    internal static class CulturaHelper
    {
        /// <summary>
        /// Obtiene la cultura correspondiente al idioma para formateo de números.
        /// </summary>
        /// <param name="idioma">Idioma del cual obtener la cultura</param>
        /// <returns>CultureInfo configurada para el idioma</returns>
        public static CultureInfo ObtenerCultura(Idioma idioma)
        {
            switch (idioma)
            {
                case Idioma.Castellano:
                    return CultureInfo.GetCultureInfo("es-AR"); // Coma decimal
                case Idioma.Ingles:
                    return CultureInfo.GetCultureInfo("en-US"); // Punto decimal
                case Idioma.Italiano:
                    return CultureInfo.GetCultureInfo("it-IT"); // Coma decimal
                default:
                    return CultureInfo.InvariantCulture;
            }
        }
    }
}
