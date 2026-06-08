# README - Refactoring FormaGeometrica

## Autor

**Tomas Lazara** - Desarrollador Senior
[tomafelipelazara@gmail.com](mailto:tomafelipelazara@gmail.com) | [GitHub](https://github.com/TomasLazara/) | [LinkedIn](https://www.linkedin.com/in/tomas-felipe-lazara/)

---

## Enfoque Utilizado

Este proyecto aplica los siguientes métodos para resolver el desafío de refactorización:

- **Análisis AS-IS**: Modelado UML, OCL y diagramas de secuencia del código actual
- **Identificación de problemas**: Análisis de violaciones de diseño y duplicación de código
- **Diseño TO-BE**: Aplicación de polimorfismo y principios SOLID
- **BDD (Behavior-Driven Development)**: Gherkins → Tests unitarios NUnit (1:1)
- **Testing exhaustivo**: Tests unitarios + integración + E2E

---

## Compatibilidad con Tests AS-IS

Los 6 tests originales del código AS-IS fueron **actualizados al nuevo diseño TO-BE** manteniendo **expectativas de HTML idénticas**:

### Cambios en los tests (solo API, mismo comportamiento):

**AS-IS (código viejo):**
```csharp
new FormaGeometrica(FormaGeometrica.Cuadrado, 5)
FormaGeometrica.Imprimir(formas, FormaGeometrica.Castellano)
```

**TO-BE (código refactorizado):**
```csharp
new Cuadrado(5)
new ReporteFormateador().GenerarReporte(formas, Idioma.Castellano)
```

### HTML generado: IDÉNTICO

El HTML producido es **exactamente el mismo** que el código AS-IS:
- Mismo formato: `<h1>...</h1>`, `<br/>`, `TOTAL:<br/>`
- Mismos decimales: coma para castellano/italiano, punto para inglés
- Mismo orden de formas agrupadas por tipo

**Resultado:** Los 6 tests AS-IS pasan exitosamente con el nuevo diseño. ✅

---

## Análisis del Estado Actual (AS-IS)

## 1. Diagnóstico del Problema

### ¿Por qué es lento para el equipo agregar figuras geométricas o idiomas?

**Problema principal**: Una figura nueva modifica múltiples ifs y switches en 5+ ubicaciones.

**Puntos de modificación para agregar una figura**:
1. Agregar constante de tipo (línea 28)
2. Modificar loop de acumulación (líneas 82-102)
3. Modificar CalcularArea (switch líneas 151-158)
4. Modificar CalcularPerimetro (switch líneas 163-170)
5. Modificar TraducirForma (switch líneas 133-146)
6. Agregar variables de acumulación (9 variables adicionales)
7. Agregar llamada a ObtenerLinea

**Riesgos**:
- ❌ Olvidar modificar un lugar → bug en runtime
- ❌ No hay validación de tipo → errores silenciosos
- ❌ Violación Open/Closed Principle

---

## 2. Análisis de Arquitectura

### UML de Clases AS-IS
![UML Clases](Analisis%20UML%20-%20AS%20IS.png)

**Observaciones**:
- God Class: FormaGeometrica tiene 5 responsabilidades
- Sin interfaces ni abstracciones
- Constantes de tipo dentro de la entidad

---

## 3. Análisis de Métodos

### Constructor
![Análisis Constructor](Analisis%20CTOR%20-%20AS%20IS.png)

**Problemas identificados**:
- Tipo puede estar fuera de rango desde el principio (no se valida)
- Ancho → _lado: error de nombre y sin validación de valores

### CalcularArea / CalcularPerimetro
![Análisis Cálculos](Analisis%20Area%20&%20Perimetro%20-%20AS%20IS.png)

**Problemas identificados**:
- No valida valores de _lado
- Excepción no muestra el input de error, solo mensaje genérico
- Falta fail-fast: errores se propagan hacia arriba

---

## 4. Inconsistencias y Duplicación de Código

**Archivo analizado**: `FormaGeometrica.cs` (174 líneas totales)

### Resumen de duplicaciones

| Inconsistencia | Líneas afectadas | % del archivo |
|----------------|------------------|---------------|
| Loop de acumulación (mismo código 3 veces) | 21 | 12.1% |
| Switch duplicado (Área/Perímetro) | 23 | 13.2% |
| Código de traducción (distribuido) | 31 | 17.8% |
| **TOTAL código duplicado/ineficiente** | **75** | **43.1%** |

**Casi la mitad del archivo (43.1%) contiene código duplicado o que podría centralizarse.**

[Ver detalle completo de inconsistencias](./inconsistencias-codigo-as-is.md)

---

## 5. OCL del AS-IS

**Restricciones reales del código** (no ideales, sino las que existen):

```ocl
context FormaGeometrica

  -- INVARIANTES: NO HAY
  -- El código permite _lado negativo, Tipo fuera de rango

context FormaGeometrica::FormaGeometrica(tipo: Integer, ancho: Decimal)
  -- PRE: NINGUNA (no valida parámetros)

  post tipoAsignado: self.Tipo = tipo
  post ladoAsignado: self._lado = ancho

context FormaGeometrica::CalcularArea() : Decimal
  -- PRE: NINGUNA
  post excepcionSiTipoInvalido:
    self.Tipo not in Set{1, 2, 3} implies OclIsInvalid()
```

---

## 6. Análisis de Tests

**6 tests existentes** - Todos de integración (no unitarios)

---

## Conclusión del Análisis AS-IS

El código actual funciona pero presenta múltiples problemas de diseño que dificultan su extensibilidad y mantenibilidad:

1. **43.1% de código duplicado**
2. **Sin validaciones** (fail-late en vez de fail-fast)
3. **Alto acoplamiento** (God Class con múltiples responsabilidades)
4. **Tests de integración** sin cobertura unitaria

---

# Diseño TO-BE - Solución con Polimorfismo

## UML de Clases TO-BE

![UML TO-BE](Analisis%20UML%20-%20TO%20BE.png)

---

## Arquitectura del Diseño

### Separación de Responsabilidades

**FormaGeometrica (abstracta)**:
- Responsabilidad única: Cálculo geométrico
- Property abstracta: `Nombre` (identifica la forma para traducción)
- Métodos abstractos: `CalcularArea()`, `CalcularPerimetro()`
- Sin conocimiento de formateo, idiomas o presentación

**Formas Concretas** (4):
- `Cuadrado(decimal lado)` - Nombre: "Cuadrado"
- `Circulo(decimal diametro)` - Nombre: "Circulo"
- `Triangulo(decimal lado)` - Nombre: "Triangulo"
- `Trapecio(decimal baseMayor, decimal baseMenor, decimal altura)` - Nombre: "Trapecio", Isósceles

**ReporteFormateador**:
- Responsabilidad única: Generación de reportes HTML
- Usa `Traductor` para multiidioma
- Sin lógica de cálculo geométrico

**Traductor**:
- Responsabilidad única: Traducción multiidioma
- Maneja castellano, inglés, italiano
- Fallback a clave original si no encuentra traducción

**Idioma (enum)**:
- Type-safe: Castellano, Ingles, Italiano
- Reemplaza magic numbers (int 1, 2)

---

## Aplicación de Principios SOLID

### Single Responsibility Principle (SRP) ✅
Cada clase tiene una sola razón para cambiar:
- Formas: Solo si cambia fórmula matemática
- ReporteFormateador: Solo si cambia formato de reporte
- Traductor: Solo si cambia estrategia de traducción

### Open/Closed Principle (OCP) ✅
Abierto a extensión, cerrado a modificación:
- **Agregar nueva forma**: Crear clase nueva, heredar de `FormaGeometrica`
- **Agregar nuevo idioma**: Agregar valor al enum, actualizar resources
- **NO modificar**: Clases existentes, switches, ifs por tipo

### Dependency Inversion Principle (DIP) ✅
`ReporteFormateador` depende de abstracción (`Traductor`), no implementación concreta.

---

## OCL TO-BE - Restricciones Formales

### Constructores de Formas

```ocl
-- Cuadrado
context Cuadrado::Cuadrado(lado: Decimal)
  pre ladoPositivo:
    lado > 0

  post ladoAsignado:
    self.lado = lado

-- Circulo
context Circulo::Circulo(diametro: Decimal)
  pre diametroPositivo:
    diametro > 0

  post diametroAsignado:
    self._diametro = diametro

-- Triangulo
context Triangulo::Triangulo(lado: Decimal)
  pre ladoPositivo:
    lado > 0

  post ladoAsignado:
    self.lado = lado

-- Trapecio (Isósceles)
context Trapecio::Trapecio(baseMayor: Decimal, baseMenor: Decimal, altura: Decimal)
  pre valoresPositivos:
    baseMayor > 0 and baseMenor > 0 and altura > 0

  pre baseMayorMayorQueBaseMenor:
    baseMayor > baseMenor

  post baseMayorAsignada:
    self.baseMayor = baseMayor

  post baseMenorAsignada:
    self.baseMenor = baseMenor

  post alturaAsignada:
    self.altura = altura
```

---

### Property Nombre en FormaGeometrica

```ocl
-- FormaGeometrica (clase abstracta)
context FormaGeometrica
  inv nombreNoNullNiVacio:
    self.Nombre <> null and self.Nombre.size() > 0

-- Cada clase concreta implementa Nombre como constante
context Cuadrado
  inv nombreCorrecto:
    self.Nombre = "Cuadrado"

context Circulo
  inv nombreCorrecto:
    self.Nombre = "Circulo"

context Triangulo
  inv nombreCorrecto:
    self.Nombre = "Triangulo"

context Trapecio
  inv nombreCorrecto:
    self.Nombre = "Trapecio"
```

---

### Traductor

```ocl
context Traductor::Traducir(clave: String, idioma: Idioma) : String
  pre claveNoNullNiVacia:
    clave <> null and clave.size() > 0

  pre idiomaValido:
    idioma in {Idioma::Castellano, Idioma::Ingles, Idioma::Italiano}

  post siempreDevuelveValor:
    result <> null and result <> ''

  post fallbackAClave:
    -- Si no encuentra traducción, devuelve clave original
    not traduccionEncontrada implies result = clave

context Traductor::TraducirForma(nombreForma: String, cantidad: Integer, idioma: Idioma) : String
  pre nombreNoNullNiVacio:
    nombreForma <> null and nombreForma.size() > 0

  pre cantidadNoNegativa:
    cantidad >= 0

  pre idiomaValido:
    idioma in {Idioma::Castellano, Idioma::Ingles, Idioma::Italiano}

  post siempreDevuelveValor:
    result <> null and result.size() > 0

  post pluralizacionCorrecta:
    (cantidad = 1 implies result.esSingular()) and
    (cantidad <> 1 implies result.esPlural())
```

---

### ReporteFormateador

```ocl
context ReporteFormateador::ReporteFormateador(traductor: Traductor)
  post traductorAsignado:
    traductor = null implies (
      self.traductor <> null and
      -- Log: "Traductor no provisto, usando traductor por defecto"
      true
    )

  post traductorProvisto:
    traductor <> null implies self.traductor = traductor

context ReporteFormateador::GenerarReporte(formas: List<FormaGeometrica>, idioma: Idioma) : String
  pre formasNoNull:
    formas <> null

  pre formasVaciaValida:
    -- Lista vacía es válida (comportamiento AS-IS mantenido)
    true

  pre idiomaValido:
    idioma in {Idioma::Castellano, Idioma::Ingles, Idioma::Italiano}

  post siempreDevuelveValor:
    result <> null and result.size() > 0

  post listaVaciaDevuelveMensaje:
    formas.isEmpty() implies result.contains("vacía" or "empty" or "vuota")
```

---

### CalcularArea y CalcularPerimetro

**Sin restricciones OCL explícitas** - Confiamos en matemática:
- Variables `readonly` validadas en constructor
- Si lado/radio/altura > 0 → área/perímetro > 0 (garantizado matemáticamente)

---

## Validaciones Fail-Fast

Todas las validaciones se realizan en **constructores**:
- `ArgumentException` si parámetros inválidos (≤ 0)
- `ArgumentNullException` si strings null/vacíos
- `ArgumentException` si enum Idioma inválido

**Ventaja**: Errores detectados inmediatamente al crear objetos, no en runtime posterior.

---

## BDD (Behavior-Driven Development)

Los escenarios Gherkin que guían la implementación y testing se encuentran en:
📂 **[/features/](./features/)** - Especificaciones en formato Gherkin

**Metodología**: Cada escenario Gherkin → 1 test unitario NUnit (relación 1:1)

---
