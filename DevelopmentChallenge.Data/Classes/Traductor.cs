using System;
using System.Globalization;
using System.Resources;
using DevelopmentChallenge.Data.Enums;
using DevelopmentChallenge.Data.Helpers;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Maneja traducciones multiidioma usando archivos de recursos.
    /// </summary>
    public class Traductor
    {
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Crea un traductor con el ResourceManager especificado.
        /// </summary>
        public Traductor()
        {
            _resourceManager = new ResourceManager(
                "DevelopmentChallenge.Data.Resources.Traducciones",
                typeof(Traductor).Assembly);
        }

        /// <summary>
        /// Traduce una clave al idioma especificado.
        /// Si no encuentra traducción, devuelve la clave original (fallback).
        /// </summary>
        /// <param name="clave">Clave a traducir</param>
        /// <param name="idioma">Idioma destino</param>
        /// <returns>Traducción o clave original si no existe</returns>
        /// <exception cref="ArgumentNullException">Si la clave es null</exception>
        /// <exception cref="ArgumentException">Si la clave es vacía o el idioma es inválido</exception>
        public string Traducir(string clave, Idioma idioma)
        {
            if (clave == null)
                throw new ArgumentNullException(nameof(clave));

            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("clave no puede ser vacía", nameof(clave));

            ValidarIdioma(idioma);

            var cultura = CulturaHelper.ObtenerCultura(idioma);
            var traduccion = _resourceManager.GetString(clave, cultura);

            // Fallback: si no encuentra traducción, devuelve clave original
            return traduccion ?? clave;
        }

        /// <summary>
        /// Traduce el nombre de una forma geométrica con pluralización automática.
        /// Usa claves estructuradas en resources: {nombreForma}.Singular / {nombreForma}.Plural
        /// </summary>
        /// <param name="nombreForma">Nombre de la forma (ej: "Cuadrado", "Circle")</param>
        /// <param name="cantidad">Cantidad de formas (determina singular/plural)</param>
        /// <param name="idioma">Idioma destino</param>
        /// <returns>Forma traducida en singular o plural según cantidad</returns>
        /// <remarks>
        /// Ejemplos de claves en resources:
        /// - "Cuadrado.Singular" → "Cuadrado" (es), "Square" (en), "Quadrato" (it)
        /// - "Cuadrado.Plural" → "Cuadrados" (es), "Squares" (en), "Quadrati" (it)
        /// </remarks>
        public string TraducirForma(string nombreForma, int cantidad, Idioma idioma)
        {
            if (nombreForma == null)
                throw new ArgumentNullException(nameof(nombreForma));

            if (string.IsNullOrWhiteSpace(nombreForma))
                throw new ArgumentException("clave no puede ser vacía", nameof(nombreForma));

            if (cantidad < 0)
                throw new ArgumentException("cantidad no puede ser negativa", nameof(cantidad));

            ValidarIdioma(idioma);

            // Construir clave estructurada: NombreForma.Singular o NombreForma.Plural
            string clave = cantidad == 1
                ? nombreForma + ".Singular"     // "Circle.Singular"
                : nombreForma + ".Plural";      // "Circle.Plural"

            // Resources hace TODO: traduce Y pluraliza
            return Traducir(clave, idioma);
        }

        /// <summary>
        /// Valida que el idioma esté dentro del rango válido del enum.
        /// </summary>
        private void ValidarIdioma(Idioma idioma)
        {
            if (!Enum.IsDefined(typeof(Idioma), idioma))
                throw new ArgumentException(string.Format("Idioma inválido: {0}", idioma), nameof(idioma));
        }
    }
}
