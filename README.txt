Reduje el Max Size de los Sprites a 128.
Desactivé Generate Physics Shape a todos los sprites ya que no utilizan físicas.
Activé Tight Packing al Sprite Atlas para así ahorrar aún más espacio.
Dividí los Canvas.
Cree un Sprite Atlas e incluí la carpeta "Art" dentro, para que todos los proximos sprites que se agreguen se añadan automaticamente.
A todos los sprites agregados les cambié "Sprite Mode" de Multiple a Single, les saqué filtro y compresión también, ya que la compresión arruina la calidad de los sprites 2D.
Le quite el RayCast Target a todas las imagenes y paneles del HUD, ya que en este proyecto no necesito que sean clickeables.

Utilicé el patrón Strategy para las acciones de los personajes (jugables y npcs), usé también inyección de dependencias.
Dividí las clases con el propósito de cumplir con el principio de Single Responsibility y evitar la God Class, intenté de que las funciones cumplan también.
Escribí mi código teniendo en cuenta el principio Open-Closed. Por ejemplo en PlayerButtons, en lugar de hacer un evento por cada botón de cada Player, decidí crearlos en runtime, solo deben ser 
arrastrados al inspector del correspondiente Player y el script recorrerá las strategies del character para determinar la acción del botón y que texto lleva.
No utilicé singletons.
Seguí PascalCase para clases y métodos, camelCase para variables privadas.
Intenté que mi código en general sea homogeneo.
Los valores de los personajes (HP, velocidad, daño, rango) se configuran desde el Inspector de Unity mediante, evitando valores hardcodeados.
