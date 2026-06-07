# README - Refactoring FormaGeometrica

## Autor

**Tomas Lazara** - Desarrollador Senior
[tomafelipelazara@gmail.com](mailto:tomafelipelazara@gmail.com) | [GitHub](https://github.com/TomasLazara/Klimber)

---

## Enfoque Utilizado

Este proyecto aplica los siguientes métodos para resolver el desafío de refactorización:

- **Análisis AS-IS**: Modelado UML, OCL y diagramas de secuencia del código actual
- **Identificación de problemas**: Análisis de violaciones de diseño y duplicación de código
- **Diseño TO-BE**: Aplicación de polimorfismo y principios SOLID
- **BDD (Behavior-Driven Development)**: Gherkins → Tests unitarios NUnit (1:1)
- **Testing exhaustivo**: Tests unitarios + integración + E2E

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

**Próximo paso**: Diseño TO-BE con polimorfismo y separación de responsabilidades.
