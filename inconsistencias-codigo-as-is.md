# Inconsistencias en el código encontradas

**Archivo analizado**: `FormaGeometrica.cs` (174 líneas totales)

---

## 1. El loop de acumulación - Hace lo mismo 3 veces
**Líneas**: 82-102 (21 líneas)

```csharp
for (var i = 0; i < formas.Count; i++)
{
    if (formas[i].Tipo == Cuadrado)
    {
        numeroCuadrados++;
        areaCuadrados += formas[i].CalcularArea();
        perimetroCuadrados += formas[i].CalcularPerimetro();
    }
    if (formas[i].Tipo == Circulo)
    {
        numeroCirculos++;
        areaCirculos += formas[i].CalcularArea();
        perimetroCirculos += formas[i].CalcularPerimetro();
    }
    if (formas[i].Tipo == TrianguloEquilatero)
    {
        numeroTriangulos++;
        areaTriangulos += formas[i].CalcularArea();
        perimetroTriangulos += formas[i].CalcularPerimetro();
    }
}
```

Acá vemos que hace lo mismo si es Cuadrado, Círculo o Triángulo: suma 1 al contador, suma el área, suma el perímetro. La única diferencia es en qué variable guarda (cuadrados vs círculos vs triángulos), pero la operación es idéntica.

---

## 2. ObtenerLinea - Duplica la línea para cambiar una palabra
**Líneas**: 122-125 (4 líneas)

```csharp
if (idioma == Castellano)
    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
```

Acá vemos que arma la misma línea en Castellano o Inglés. La única diferencia es que escribe "Perimetro" o "Perimeter", pero el resto (cantidad, área, formato) es exactamente igual. Ya tiene `TraducirForma` que maneja el idioma, podría hacer lo mismo con "Perimetro/Perimeter".

---

## 3. CalcularArea y CalcularPerimetro - Mismo switch duplicado
**Líneas**: 149-171 (23 líneas)

```csharp
// CalcularArea (líneas 149-159)
public decimal CalcularArea()
{
    switch (Tipo)
    {
        case Cuadrado: return _lado * _lado;
        case Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
        case TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
        default: throw new ArgumentOutOfRangeException(@"Forma desconocida");
    }
}

// CalcularPerimetro (líneas 161-171)
public decimal CalcularPerimetro()
{
    switch (Tipo)
    {
        case Cuadrado: return _lado * 4;
        case Circulo: return (decimal)Math.PI * _lado;
        case TrianguloEquilatero: return _lado * 3;
        default: throw new ArgumentOutOfRangeException(@"Forma desconocida");
    }
}
```

Acá vemos que ambos métodos tienen un switch que evalúa el tipo. Es el mismo switch duplicado - uno para área, otro para perímetro. Mismo patrón, mismos casos (Cuadrado/Círculo/Triángulo).

---

## 4. Traducir palabras - Código de i18n distribuido
**Líneas totales dedicadas a traducción**: 31 líneas (17.8% del archivo)

### TraducirForma (líneas 131-147) - 17 líneas:
```csharp
private static string TraducirForma(int tipo, int cantidad, int idioma)
{
    switch (tipo)
    {
        case Cuadrado:
            if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
            else return cantidad == 1 ? "Square" : "Squares";
        case Circulo:
            if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
            else return cantidad == 1 ? "Circle" : "Circles";
        case TrianguloEquilatero:
            if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
            else return cantidad == 1 ? "Triangle" : "Triangles";
    }
    return string.Empty;
}
```

### Lista vacía (líneas 55-58) - 4 líneas:
```csharp
if (idioma == Castellano)
    sb.Append("<h1>Lista vacía de formas!</h1>");
else
    sb.Append("<h1>Empty list of shapes!</h1>");
```

### Header (líneas 64-68) - 5 líneas:
```csharp
if (idioma == Castellano)
    sb.Append("<h1>Reporte de Formas</h1>");
else
    sb.Append("<h1>Shapes report</h1>");
```

### Footer (líneas 110-112) - 3 líneas:
```csharp
sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " +
    (idioma == Castellano ? "formas" : "shapes") + " ");
sb.Append((idioma == Castellano ? "Perimetro " : "Perimeter ") + ...);
```

### ObtenerLinea (líneas 122-125) - 2 líneas (ya contadas arriba)

Acá vemos que el código de traducción está desperdigado por todo el archivo. En total, **31 líneas de 174 (17.8%)** están dedicadas a traducir palabras. Siempre la misma estructura: if castellano → texto ES, else → texto EN.

---

## Resumen de duplicaciones

| Inconsistencia | Líneas afectadas | % del archivo |
|----------------|------------------|---------------|
| Loop de acumulación | 21 | 12.1% |
| Switch duplicado (Área/Perímetro) | 23 | 13.2% |
| Código de traducción (distribuido) | 31 | 17.8% |
| **TOTAL código duplicado/ineficiente** | **75** | **43.1%** |

**Casi la mitad del archivo (43.1%) contiene código duplicado o que podría centralizarse.**
