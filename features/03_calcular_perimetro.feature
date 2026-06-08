Feature: Calcular perímetro de formas geométricas

  Scenario: Calcular perímetro de cuadrado
    Given un Cuadrado con lado 5
    When calculo el perímetro
    Then el perímetro es 20

  Scenario: Calcular perímetro de círculo
    Given un Círculo con diámetro 6
    When calculo el perímetro
    Then el perímetro es 18.85

  Scenario: Calcular perímetro de triángulo
    Given un Triángulo con lado 4
    When calculo el perímetro
    Then el perímetro es 12

  Scenario: Calcular perímetro de trapecio isósceles
    Given un Trapecio con base mayor 10, base menor 4 y altura 4
    When calculo el perímetro
    Then el perímetro es 24
