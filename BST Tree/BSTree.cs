using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Tree
{
    public class BSTree
    {
        private ArchivoManager archivoManager;
        private long posicionRaiz;

        public BSTree(string pathArchivo)
        {
            archivoManager = new ArchivoManager(pathArchivo);
            posicionRaiz = -1; // Inicialmente, el árbol está vacío
        }

        public void Insertar(int clave, string valor)
        {
            Nodo nuevoNodo = new Nodo(clave, valor);

            if (posicionRaiz == -1)
            {
                // El árbol está vacío, insertamos el primer nodo en la raíz
                posicionRaiz = archivoManager.EscribirNodo(nuevoNodo);
            }
            else
            {
                InsertarRecursivo(posicionRaiz, nuevoNodo);
            }
        }

        private void InsertarRecursivo(long posicionActual, Nodo nuevoNodo)
        {
            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);

            if (nuevoNodo.Clave < nodoActual.Clave)
            {
                if (nodoActual.PosicionIzquierda == -1)
                {
                    // Inserta a la izquierda si no hay nodo
                    nodoActual.PosicionIzquierda = archivoManager.EscribirNodo(nuevoNodo);
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);
                }
                else
                {
                    // Si ya hay un nodo, continuar la inserción recursivamente a la izquierda
                    InsertarRecursivo(nodoActual.PosicionIzquierda, nuevoNodo);
                }
            }
            else if (nuevoNodo.Clave > nodoActual.Clave)
            {
                if (nodoActual.PosicionDerecha == -1)
                {
                    // Inserta a la derecha si no hay nodo
                    nodoActual.PosicionDerecha = archivoManager.EscribirNodo(nuevoNodo);
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);
                }
                else
                {
                    // Si ya hay un nodo, continuar la inserción recursivamente a la derecha
                    InsertarRecursivo(nodoActual.PosicionDerecha, nuevoNodo);
                }
            }
            else
            {
                // Si la clave ya existe, puedes manejarlo según lo que se requiera (p.ej., actualizar el valor)
                Console.WriteLine("Clave duplicada. No se puede insertar.");
            }
        }
        public Nodo Buscar(int clave)
        {
            return BuscarRecursivo(posicionRaiz, clave);
        }

        private Nodo BuscarRecursivo(long posicionActual, int clave)
        {
            if (posicionActual == -1) return null;

            Nodo nodoActual = archivoManager.LeerNodo(posicionActual);

            if (nodoActual.Eliminado)
            {
                return null;  // Si el nodo está marcado como eliminado, no lo devolvemos
            }

            if (clave == nodoActual.Clave)
            {
                return nodoActual;
            }
            else if (clave < nodoActual.Clave)
            {
                return BuscarRecursivo(nodoActual.PosicionIzquierda, clave);
            }
            else
            {
                return BuscarRecursivo(nodoActual.PosicionDerecha, clave);
            }
        }

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

                // Si no tiene hijos, simplemente lo marcamos como eliminado
                if (nodoActual.PosicionIzquierda == -1 && nodoActual.PosicionDerecha == -1)
                {
                    nodoActual.Eliminado = true;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);
                    return true;
                }

                // Si solo tiene un hijo
                if (nodoActual.PosicionIzquierda == -1 || nodoActual.PosicionDerecha == -1)
                {
                    long posicionHijo = (nodoActual.PosicionIzquierda != -1) ? nodoActual.PosicionIzquierda : nodoActual.PosicionDerecha;
                    Nodo nodoHijo = archivoManager.LeerNodo(posicionHijo);

                    // Reemplazar nodo con su hijo
                    nodoActual.Clave = nodoHijo.Clave;
                    nodoActual.Valor = nodoHijo.Valor;
                    nodoActual.PosicionIzquierda = nodoHijo.PosicionIzquierda;
                    nodoActual.PosicionDerecha = nodoHijo.PosicionDerecha;
                    archivoManager.ActualizarNodo(posicionActual, nodoActual);

                    return true;
                }

                // Si tiene dos hijos, buscar el sucesor inorden
                long sucesorPosicion = ObtenerSucesorInorden(nodoActual.PosicionDerecha);
                Nodo sucesor = archivoManager.LeerNodo(sucesorPosicion);
                nodoActual.Clave = sucesor.Clave;
                nodoActual.Valor = sucesor.Valor;
                archivoManager.ActualizarNodo(posicionActual, nodoActual);

                // Eliminar el sucesor inorden (nodo más pequeño del subárbol derecho)
                return EliminarRecursivo(nodoActual.PosicionDerecha, sucesor.Clave);
            }
        }

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
