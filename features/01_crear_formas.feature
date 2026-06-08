Feature: Crear instancias de formas geométricas
  Para poder calcular sus propiedades

  # Cuadrado
  Scenario: Crear cuadrado con lado válido
    Given un lado de 5
    When creo un Cuadrado
    Then el cuadrado se crea exitosamente
    And el lado del cuadrado es 5

  Scenario: Crear cuadrado con lado inválido
    Given un lado de 0
    When intento crear un Cuadrado
    Then se lanza ArgumentException
    And el mensaje contiene "lado debe ser mayor a 0"

  Scenario: Crear cuadrado con lado negativo
    Given un lado de -5
    When intento crear un Cuadrado
    Then se lanza ArgumentException
    And el mensaje contiene "lado debe ser mayor a 0"

  # Círculo
  Scenario: Crear círculo con diámetro válido
    Given un diámetro de 6
    When creo un Círculo
    Then el círculo se crea exitosamente
    And el diámetro del círculo es 6

  Scenario: Crear círculo con diámetro inválido
    Given un diámetro de 0
    When intento crear un Círculo
    Then se lanza ArgumentException
    And el mensaje contiene "diámetro debe ser mayor a 0"

  Scenario: Crear círculo con diámetro negativo
    Given un diámetro de -2
    When intento crear un Círculo
    Then se lanza ArgumentException
    And el mensaje contiene "diámetro debe ser mayor a 0"

  # Triángulo
  Scenario: Crear triángulo con lado válido
    Given un lado de 4
    When creo un Triángulo
    Then el triángulo se crea exitosamente
    And el lado del triángulo es 4

  Scenario: Crear triángulo con lado inválido
    Given un lado de 0
    When intento crear un Triángulo
    Then se lanza ArgumentException
    And el mensaje contiene "lado debe ser mayor a 0"

  # Trapecio
  Scenario: Crear trapecio con bases y altura válidas
    Given una base mayor de 10, base menor de 4 y altura de 4
    When creo un Trapecio
    Then el trapecio se crea exitosamente
    And la base mayor del trapecio es 10
    And la base menor del trapecio es 4
    And la altura del trapecio es 4

  Scenario: Crear trapecio con base mayor inválida
    Given una base mayor de 0, base menor de 4 y altura de 4
    When intento crear un Trapecio
    Then se lanza ArgumentException
    And el mensaje contiene "baseMayor debe ser mayor a 0"

  Scenario: Crear trapecio con base menor inválida
    Given una base mayor de 10, base menor de 0 y altura de 4
    When intento crear un Trapecio
    Then se lanza ArgumentException
    And el mensaje contiene "baseMenor debe ser mayor a 0"

  Scenario: Crear trapecio con altura inválida
    Given una base mayor de 10, base menor de 4 y altura de 0
    When intento crear un Trapecio
    Then se lanza ArgumentException
    And el mensaje contiene "altura debe ser mayor a 0"

  Scenario: Crear trapecio con base menor mayor que base mayor
    Given una base mayor de 4, base menor de 10 y altura de 4
    When intento crear un Trapecio
    Then se lanza ArgumentException
    And el mensaje contiene "baseMayor debe ser mayor que baseMenor"
