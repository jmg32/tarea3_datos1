using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Tree
{
    using System;
    using System.Collections.Generic;

    public class BSTree
    {
        private ArchivoManager archivoManager;
        private long posicionRaiz;
        private List<long> vacantes; // Lista de posiciones vacantes

        public BSTree(string pathArchivo)
        {
            archivoManager = new ArchivoManager(pathArchivo);
            posicionRaiz = -1; // Inicialmente, el árbol está vacío
            vacantes = new List<long>(); // Inicializamos la lista de vacantes
        }

        // Método para insertar un nuevo nodo en el árbol
        public void Insertar(int clave, string valor)
        {
            Nodo nuevoNodo = new Nodo(clave, valor);

            if (posicionRaiz == -1)
            {
                // El árbol está vacío, insertamos el primer nodo en la raíz
                posicionRaiz = EscribirNodo(nuevoNodo);
            }
            else
            {
                InsertarRecursivo(posicionRaiz, nuevoNodo);
            }
        }

        private void InsertarRecursivo(long posicionActual, Nodo nuevoNodo)
        {
            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);

            Console.WriteLine($"Insertar nodo con clave {nuevoNodo.Clave}. Nodo actual clave: {nodoActual.Clave}, Posición: {posicionActual}");

            if (nuevoNodo.Clave < nodoActual.Clave)
            {
                if (nodoActual.PosicionIzquierda == -1)
                {
                    // Insertamos en la posición izquierda vacante
                    long posicionNueva = EscribirNodo(nuevoNodo);
                    nodoActual.PosicionIzquierda = posicionNueva;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual); // Actualizamos el nodo padre
                    Console.WriteLine($"Nodo insertado a la izquierda de {nodoActual.Clave} en la posición {posicionNueva}");
                }
                else
                {
                    InsertarRecursivo(nodoActual.PosicionIzquierda, nuevoNodo);
                }
            }
            else if (nuevoNodo.Clave > nodoActual.Clave)
            {
                if (nodoActual.PosicionDerecha == -1)
                {
                    // Insertamos en la posición derecha vacante
                    long posicionNueva = EscribirNodo(nuevoNodo);
                    nodoActual.PosicionDerecha = posicionNueva;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual); // Actualizamos el nodo padre
                    Console.WriteLine($"Nodo insertado a la derecha de {nodoActual.Clave} en la posición {posicionNueva}");
                }
                else
                {
                    InsertarRecursivo(nodoActual.PosicionDerecha, nuevoNodo);
                }
            }
            else
            {
                Console.WriteLine("Clave duplicada. No se puede insertar.");
            }
        }



        // Método para escribir el nodo, reutilizando posiciones vacantes si es posible
        private long EscribirNodo(Nodo nuevoNodo)
        {
            // Nos aseguramos de que el nuevo nodo no esté marcado como eliminado
            nuevoNodo.Eliminado = false;

            // Si hay vacantes, reutilizamos una posición vacante
            if (vacantes.Count > 0)
            {
                long posicionVacante = vacantes[0];
                vacantes.RemoveAt(0);
                Console.WriteLine($"Reutilizando posición vacante: {posicionVacante}");

                // Sobreescribimos en la posición vacante y aseguramos que el nodo no esté eliminado
                archivoManager.ActualizarNodo(posicionVacante, nuevoNodo);
                return posicionVacante;
            }

            // Si no hay vacantes, escribimos el nuevo nodo al final del archivo
            long nuevaPosicion = archivoManager.EscribirNodo(nuevoNodo);
            Console.WriteLine($"Escribiendo nuevo nodo en la posición: {nuevaPosicion}");
            return nuevaPosicion;
        }





        // Método para buscar un nodo en el árbol
        public Nodo Buscar(int clave)
        {
            return BuscarRecursivo(posicionRaiz, clave);
        }

        private Nodo BuscarRecursivo(long posicionActual, int clave)
        {
            if (posicionActual == -1) return null;

            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);
            Console.WriteLine($"Buscando clave {clave}. Nodo actual clave: {nodoActual.Clave}, Posición: {posicionActual}");

            if (nodoActual.Eliminado)
            {
                Console.WriteLine($"Nodo con clave {nodoActual.Clave} está eliminado.");
                return null;  // Si el nodo está marcado como eliminado, no lo devolvemos
            }

            if (clave == nodoActual.Clave)
            {
                Console.WriteLine($"Nodo con clave {clave} encontrado en la posición {posicionActual}");
                return nodoActual;
            }
            else if (clave < nodoActual.Clave)
            {
                Console.WriteLine($"Buscando a la izquierda de {nodoActual.Clave}");
                return BuscarRecursivo(nodoActual.PosicionIzquierda, clave);
            }
            else
            {
                Console.WriteLine($"Buscando a la derecha de {nodoActual.Clave}");
                return BuscarRecursivo(nodoActual.PosicionDerecha, clave);
            }
        }


        // Método para eliminar un nodo del árbol
        public bool Eliminar(int clave)
        {
            return EliminarRecursivo(posicionRaiz, clave);
        }

        private bool EliminarRecursivo(long posicionActual, int clave)
        {
            if (posicionActual == -1) return false;

            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);

            if (clave < nodoActual.Clave)
            {
                return EliminarRecursivo(nodoActual.PosicionIzquierda, clave);
            }
            else if (clave > nodoActual.Clave)
            {
                return EliminarRecursivo(nodoActual.PosicionDerecha, clave);
            }
            else
            {
                // Nodo encontrado
                if (nodoActual.Eliminado)
                {
                    Console.WriteLine("El nodo ya ha sido eliminado.");
                    return false;
                }

                // Si el nodo es una hoja, simplemente lo eliminamos
                if (nodoActual.PosicionIzquierda == -1 && nodoActual.PosicionDerecha == -1)
                {
                    nodoActual.Eliminado = true;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);
                    vacantes.Add(posicionActual); // Añadir la posición a la lista de vacantes
                    return true;
                }

                // Si tiene un solo hijo
                if (nodoActual.PosicionIzquierda == -1 || nodoActual.PosicionDerecha == -1)
                {
                    long posicionHijo = nodoActual.PosicionIzquierda != -1
                        ? nodoActual.PosicionIzquierda
                        : nodoActual.PosicionDerecha;

                    Nodo nodoHijo = archivoManager.LeerNodo(posicionHijo);
                    nodoActual.Clave = nodoHijo.Clave;
                    nodoActual.Valor = nodoHijo.Valor;
                    nodoActual.PosicionIzquierda = nodoHijo.PosicionIzquierda;
                    nodoActual.PosicionDerecha = nodoHijo.PosicionDerecha;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);

                    vacantes.Add(posicionHijo); // Añadir la posición del nodo hijo a la lista de vacantes
                    return true;
                }

                // Si tiene dos hijos, encontramos el sucesor inorden
                long sucesorPosicion = ObtenerSucesorInorden(nodoActual.PosicionDerecha);
                Nodo sucesor = archivoManager.LeerNodo(sucesorPosicion);
                nodoActual.Clave = sucesor.Clave;
                nodoActual.Valor = sucesor.Valor;
                archivoManager.ActualizarNodo(posicionActual, nodoActual);

                // Eliminamos el sucesor inorden
                return EliminarRecursivo(nodoActual.PosicionDerecha, sucesor.Clave);
            }
        }

        // Método para encontrar el sucesor inorden (el nodo más pequeño del subárbol derecho)
        private long ObtenerSucesorInorden(long posicionActual)
        {
            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);
            while (nodoActual.PosicionIzquierda != -1)
            {
                posicionActual = nodoActual.PosicionIzquierda;
                nodoActual = archivoManager.LeerNodo(posicionActual);
            }
            return posicionActual;
        }
    }
}
