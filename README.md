# Descripción
Esta es la primera práctica de la asignatura **Inteligencia Artificial para Videojuegos** de la **Universidad Complutense de Madrid**. 

Utilizamos pivotal tracker como herramienta de gestión del proyecto. Puedes ver nuestra organización [aquí](https://www.pivotaltracker.com/n/projects/2490634)

Consiste en la implementación de entidades con inteligencia artificial siguiendo el esquema y la estructura planteada en el enunciado **IAV-Práctica-1.pdf**.

Plantea un ejercicio básico basado en **El flautista de Hamelin**. El usuario controla al flautista, de manera que se puede mover libremente por el escenario. Posee una flauta, la cual podrá tocar o dejar de tocar, de manera que el perro huirá cuando la flauta suene, pues las ratas irán hacia el sonido de la flauta cuando éstas lo detecten. En el caso contrario, cuando la flauta no suene, el perro volverá hacia el flautista y las ratas comenzarán a moverse forma aleatoria y errática por el escenario.

# Documentación técnica del proyecto

El proyecto está implementado con **Unity** y la documentación del código la generamos en español, así como la mayoría de variables, métodos y clases.

## 1 Mecánicas
- **Flechas de dirección**: encargadas del movimiento del jugador

        Flecha arriba -> movimiento en el eje Z positivo
        Flecha abajo -> movimiento en el eje Z negativo
        Flecha derecha -> movimiento positivo en el eje X
        Flecha izquierda -> movimiento negativo en el eje X

- **Barra espaciadora**: sirve para el mecanismo de la flauta

        No pulsado -> la flauta no suena
        Manteniendo pulsado -> la flauta suena

## 2 Entidades y escenario

### 2.1 Entidades
- **Flautista**: es el personaje principal y aldedor del cual gira todo el ejercicio. Es el encargado de tocar la flauta para determinar el comportamiento del perro y de las ratas.

- **Perro**: es una **IA** básica de movimiento, el cual depende tanto de las ratas como de la flauta. Cuando la flauta no suena, el perro sigue los pasos del flautista y, por el contrario, cuando suene, el perro huye de las ratas.

- **Ratas**: es una **IA** básica de movimiento, el cual depende únicamente de la flauta. Cuando la flauta suena las ratas que detecten el sonido siguen al flautista y, cuando no suena, las ratas comienzan a moverse por el escenario de forma aleatoria y errática. 

### 2.2 Escenario

El escenario trata de simular el pueblo de **Hamelin** de manera que hay obstáculos(casas, chozas, edificios...). Las entidades no pueden atravesar los obstáculos, de manera que solo puedan circular por las zonas donde no haya obstáculos. Además, **Hamelin** se genera de forma *pseudoaleatoria* mediante una **IA** que genere el mapa, de manera que no siempre exista el mismo espacio de obstáculos, con el objetivo de probar que la **IA** de las entidades funciona correctamente.

## 3. Implementación de **IA**



## 4 Referencias
- Pseudocódigo del libro: [**AI for Games, Third Edition**](https://ebookcentral.proquest.com/lib/universidadcomplutense-ebooks/detail.action?docID=5735527) de **Millington**

- Código de [**Federico Peinado**](https://github.com/federicopeinado) habilitado para la asignatura.
