Feature: Traductor multiidioma

  # Traducir claves
  Scenario: Traducir clave existente en castellano
    Given la clave "Cuadrado"
    When traduzco al Castellano
    Then el resultado es "Cuadrado"

  Scenario: Traducir clave existente en inglés
    Given la clave "Cuadrado"
    When traduzco al Ingles
    Then el resultado es "Square"

  Scenario: Traducir clave existente en italiano
    Given la clave "Cuadrado"
    When traduzco al Italiano
    Then el resultado es "Quadrato"

  # Fallback
  Scenario: Traducir clave inexistente con fallback
    Given la clave "ClaveInexistente"
    When traduzco al Italiano
    Then el resultado es "ClaveInexistente"

  # Pluralización
  Scenario: Traducir forma en singular en castellano
    Given la forma "Cuadrado" con cantidad 1
    When traduzco la forma al Castellano
    Then el resultado es "Cuadrado"

  Scenario: Traducir forma en plural en castellano
    Given la forma "Cuadrado" con cantidad 3
    When traduzco la forma al Castellano
    Then el resultado es "Cuadrados"

  Scenario: Traducir forma con cantidad cero en inglés
    Given la forma "Circle" con cantidad 0
    When traduzco la forma al Ingles
    Then el resultado es "Circles"

  # Validaciones
  Scenario: Traducir con clave vacía
    Given una clave vacía
    When intento traducir
    Then se lanza ArgumentException
    And el mensaje contiene "clave no puede ser vacía"

  Scenario: Traducir con clave null
    Given una clave null
    When intento traducir
    Then se lanza ArgumentNullException
