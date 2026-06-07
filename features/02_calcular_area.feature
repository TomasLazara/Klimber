Feature: Calcular área de formas geométricas

  Scenario: Calcular área de cuadrado
    Given un Cuadrado con lado 5
    When calculo el área
    Then el área es 25

  Scenario: Calcular área de círculo
    Given un Círculo con radio 3
    When calculo el área
    Then el área es 28.27

  Scenario: Calcular área de triángulo
    Given un Triángulo con lado 4
    When calculo el área
    Then el área es 6.93

  Scenario: Calcular área de trapecio
    Given un Trapecio con base mayor 10, base menor 4 y altura 4
    When calculo el área
    Then el área es 28
