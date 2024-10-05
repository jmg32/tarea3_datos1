using BST_Tree;

public class Program
{
    public static void Main(string[] args)
    {
        string pathArchivo = "arbol.dat";
        BSTree arbol = new BSTree(pathArchivo);

        // Insertamos varios nodos en el árbol
        arbol.Insertar(10, "Valor 10");
        arbol.Insertar(20, "Valor 20");
        arbol.Insertar(5, "Valor 5");
        arbol.Insertar(15, "Valor 15");
        arbol.Insertar(3, "Valor 3");

        Console.WriteLine("Inserciones completadas.");

        // Prueba de búsqueda
        var nodoEncontrado = arbol.Buscar(15);
        if (nodoEncontrado != null)
            Console.WriteLine($"Nodo encontrado: {nodoEncontrado.Clave} - {nodoEncontrado.Valor}");
        else
            Console.WriteLine("Nodo no encontrado.");
    }

}
