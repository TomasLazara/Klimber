Feature: Generar reporte de formas geométricas

  # AS-IS: TestResumenListaVacia
  Scenario: Generar reporte con lista vacía en castellano
    Given una lista vacía de formas
    When genero el reporte en Castellano
    Then el reporte contiene "Lista vacía de formas"

  # AS-IS: TestResumenListaVaciaFormasEnIngles
  Scenario: Generar reporte con lista vacía en inglés
    Given una lista vacía de formas
    When genero el reporte en Ingles
    Then el reporte contiene "Empty list of shapes"

  # NUEVO: Italiano (MUST del MoSCoW)
  Scenario: Generar reporte con lista vacía en italiano
    Given una lista vacía de formas
    When genero el reporte en Italiano
    Then el reporte contiene "Elenco vuoto di forme"

  # AS-IS: TestResumenListaConUnCuadrado
  Scenario: Generar reporte con un cuadrado en castellano
    Given un Cuadrado con lado 5
    When genero el reporte en Castellano
    Then el reporte contiene "1 Cuadrado"
    And el reporte contiene "Area 25"
    And el reporte contiene "Perimetro 20"
    And el reporte contiene "TOTAL"
    And el reporte contiene "1 formas Perimetro 20 Area 25"

  # AS-IS: TestResumenListaConMasCuadrados
  Scenario: Generar reporte con múltiples cuadrados en inglés
    Given un Cuadrado con lado 5
    And un Cuadrado con lado 1
    And un Cuadrado con lado 3
    When genero el reporte en Ingles
    Then el reporte contiene "3 Squares"
    And el reporte contiene "Area 35"
    And el reporte contiene "Perimeter 36"
    And el reporte contiene "TOTAL"
    And el reporte contiene "3 shapes Perimeter 36 Area 35"

  # AS-IS: TestResumenListaConMasTipos
  Scenario: Generar reporte con múltiples tipos de formas en inglés
    Given un Cuadrado con lado 5
    And un Cuadrado con lado 2
    And un Círculo con diámetro 3
    And un Círculo con diámetro 2.75
    And un Triángulo con lado 4
    And un Triángulo con lado 9
    And un Triángulo con lado 4.2
    When genero el reporte en Ingles
    Then el reporte contiene "2 Squares"
    And el reporte contiene "2 Circles"
    And el reporte contiene "3 Triangles"
    And el reporte contiene "TOTAL"
    And el reporte contiene "7 shapes"

  # AS-IS: TestResumenListaConMasTiposEnCastellano
  Scenario: Generar reporte con múltiples tipos de formas en castellano
    Given un Cuadrado con lado 5
    And un Cuadrado con lado 2
    And un Círculo con diámetro 3
    And un Círculo con diámetro 2.75
    And un Triángulo con lado 4
    And un Triángulo con lado 9
    And un Triángulo con lado 4.2
    When genero el reporte en Castellano
    Then el reporte contiene "2 Cuadrados"
    And el reporte contiene "2 Círculos"
    And el reporte contiene "3 Triángulos"
    And el reporte contiene "TOTAL"
    And el reporte contiene "7 formas"

  # NUEVO: Trapecio en italiano (MUST)
  Scenario: Generar reporte con trapecio en italiano
    Given un Trapecio con base mayor 10, base menor 4 y altura 4
    When genero el reporte en Italiano
    Then el reporte contiene "1 Trapezio"
    And el reporte contiene "Area 28"
