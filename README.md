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

- **Perro**: es una **IA** básica de movimiento, el cual depende la flauta. Cuando la flauta no suena, el perro sigue los pasos del flautista y, por el contrario, cuando suene, el perro huye del él.

- **Ratas**: es una **IA** básica de movimiento, el cual depende únicamente de la flauta. Cuando la flauta, suena las ratas que se encuentren lo suficientemente cerca del sonido siguen al flautista y, cuando no suena, las ratas comienzan a moverse por el escenario de forma aleatoria y errática. 

### 2.2 Escenario

El escenario trata de simular el pueblo de **Hamelin** de manera que hay obstáculos(casas, chozas, edificios...). Las entidades no pueden atravesar los obstáculos, de manera que solo puedan circular por las zonas libres de estos. Las entidades controladas mediante IA no detectan dichos obstáculos, por lo que pueden colisionar con estos sin problemas. 

**Hamelin** se genera automáticamente mediante el algoritmo de **Halton** empleando una base (2,3). Puesto que **Halton** es una función determinista, podremos generar distintos escenarios modificando tanto el número de obstáculos que queremos que tenga **Hamelin** como la base empleada para distribuirlos de forma "pseudoaleatoria". 

Para evitar que durante la huida del perro este se quede atrapado en las esquinas del mapa, implementamos un comportamiento el cual, al acercarse a una de estas, le da un pequeño impulso para que cambie su trayectoria ([véase el punto 3.4](#impulso))

## 3. Implementación de **IA**

Para implementar el comportamiento descrito en el enunciado de la práctica hemos desarollado los siguientes comportamientos:

### 3.1 Script AgentePerro
Maneja el comportamiento del perro. Implementa tanto el comportamiento de seguir al jugador cuando la flauta no está sonando como el comportamiento de huida en caso contrario.

### 3.2 Script AgenteRata
Maneja el comportamiento de las ratas. Implementa el comportamiento seguir de forma similar al perro con la diferencia de que este solo está activo cuando esté sonando la flauta. También implementa el comportamiento merodear, el cual consiste en cada x segundos cambiar la dirección en la que se están desplazando las ratas en el caso de que no esté sonando la flauta o que se encuentren a una distancia lo suficientemente grande para no oirla.

### 3.3 Script Flauta
Maneja todos los aspectos relacionados con el manejo de la Flauta. Se encarga de avisar a todos los agentes cuando empieza o deja de sonar cuando el jugador pulsa o suelta la tecla espacio respectivamente. También se encarga de avisar a las ratas que están a distancia de escuchar la flauta de que deben de empezar a moverse hacia el jugador 

<a name = "impulso"></a> 

### 3.4 Script Impulso
Comportamiento que otorga al perro un determinado impulso cuando este entra en un área concreta para que no se quede atrapado en las esquinas. La dirección del impulso es equivalente a la de la recta que uniría la esquina del escenario en la que se encuentra el perro con la esquina que se encuentra en su diagonal. La magnitud de la fuerza que se aplica sobre el cuerpo del perro es configurable. 

### 3.5 Script Repulsión
Comportamiento para que las ratas no colisionen cuando se encuentran en movimiento. El principio que sigue es similar al del impulso implementado para que el perro no colisione con las esquinas. Cuando una rata se acerca demasiado a otra ambas reciben un pequeño impulso en direcciones contrarias para que no se choquen.

### 3.6 Script Inicializacion
Maneja tanto la creación de todas las entidades del sistema (jugador, perro y ratas), además de ser el encargado de colocar y generar los obstáculos de **Hamelin** mediante la secuencia de **Halton**. Tanto el número de ratas generado, como el número de obstáculos del escenario como las bases empleadas para **Halton** son configurables a través del editor de la escena.  


## 4 Referencias
- Pseudocódigo del libro: [**AI for Games, Third Edition**](https://ebookcentral.proquest.com/lib/universidadcomplutense-ebooks/detail.action?docID=5735527) de **Millington**

- Código de [**Federico Peinado**](https://github.com/federicopeinado) habilitado para la asignatura.
