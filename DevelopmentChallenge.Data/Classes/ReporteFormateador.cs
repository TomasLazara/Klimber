using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DevelopmentChallenge.Data.Enums;

namespace DevelopmentChallenge.Data.Classes
{
    /// <summary>
    /// Genera reportes HTML de formas geométricas en múltiples idiomas.
    /// </summary>
    public class ReporteFormateador
    {
        private readonly Traductor _traductor;

        /// <summary>
        /// Crea un formateador de reportes.
        /// </summary>
        /// <param name="traductor">Traductor a utilizar (opcional, se crea uno por defecto si es null)</param>
        public ReporteFormateador(Traductor traductor = null)
        {
            _traductor = traductor ?? new Traductor();
        }

        /// <summary>
        /// Genera un reporte HTML de las formas geométricas.
        /// </summary>
        /// <param name="formas">Lista de formas a reportar</param>
        /// <param name="idioma">Idioma del reporte</param>
        /// <returns>Reporte en formato HTML</returns>
        public string GenerarReporte(List<FormaGeometrica> formas, Idioma idioma)
        {
            if (formas == null)
                throw new ArgumentNullException(nameof(formas));

            if (!Enum.IsDefined(typeof(Idioma), idioma))
                throw new ArgumentException($"Idioma inválido: {idioma}", nameof(idioma));

            var cultura = ObtenerCultura(idioma);
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                // Lista vacía
                var mensajeVacio = _traductor.Traducir("ListaVacia", idioma);
                sb.Append($"<h1>{mensajeVacio}</h1>");
            }
            else
            {
                // Header
                var header = _traductor.Traducir("ReporteHeader", idioma);
                sb.Append($"<h1>{header}</h1>");

                // Agrupar formas por tipo
                var grupos = formas
                    .GroupBy(f => f.Nombre)
                    .Select(g => new
                    {
                        Nombre = g.Key,
                        Cantidad = g.Count(),
                        AreaTotal = g.Sum(f => f.CalcularArea()),
                        PerimetroTotal = g.Sum(f => f.CalcularPerimetro())
                    })
                    .ToList();

                // Generar líneas por grupo
                foreach (var grupo in grupos)
                {
                    var formaTraducida = _traductor.TraducirForma(grupo.Nombre, grupo.Cantidad, idioma);
                    var labelPerimetro = _traductor.Traducir("Perimetro", idioma);
                    var labelArea = _traductor.Traducir("Area", idioma);

                    var areaFormateada = grupo.AreaTotal.ToString("#.##", cultura);
                    var perimetroFormateado = grupo.PerimetroTotal.ToString("#.##", cultura);

                    sb.Append($"{grupo.Cantidad} {formaTraducida} | {labelArea} {areaFormateada} | {labelPerimetro} {perimetroFormateado} <br/>");
                }

                // Footer - TOTAL
                var totalFormas = grupos.Sum(g => g.Cantidad);
                var totalArea = grupos.Sum(g => g.AreaTotal);
                var totalPerimetro = grupos.Sum(g => g.PerimetroTotal);

                var labelFormas = _traductor.Traducir("Formas", idioma);
                var labelPerimetroFooter = _traductor.Traducir("Perimetro", idioma);
                var labelAreaFooter = _traductor.Traducir("Area", idioma);

                var totalAreaFormateada = totalArea.ToString("#.##", cultura);
                var totalPerimetroFormateado = totalPerimetro.ToString("#.##", cultura);

                sb.Append($"TOTAL:<br/>{totalFormas} {labelFormas} {labelPerimetroFooter} {totalPerimetroFormateado} {labelAreaFooter} {totalAreaFormateada}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Obtiene la cultura correspondiente al idioma para formateo de números.
        /// </summary>
        private CultureInfo ObtenerCultura(Idioma idioma)
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
