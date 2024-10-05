using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Tree
{
    public class ArchivoManager
    {
        private string pathArchivo;

        public ArchivoManager(string path)
        {
            pathArchivo = path;
        }

        public long EscribirNodo(Nodo nodo)
        {
            using (FileStream fs = new FileStream(pathArchivo, FileMode.Append))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                long posicion = fs.Position;
                writer.Write(nodo.Clave);
                writer.Write(nodo.Valor);
                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
                writer.Write(nodo.Eliminado);  // Escribir el campo Eliminado
                return posicion;

            }
        }

        public Nodo LeerNodo(long posicion)
        {
            using (FileStream fs = new FileStream(pathArchivo, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                fs.Seek(posicion, SeekOrigin.Begin);
                int clave = reader.ReadInt32();
                string valor = reader.ReadString();
                long posicionIzquierda = reader.ReadInt64();
                long posicionDerecha = reader.ReadInt64();
                bool eliminado = reader.ReadBoolean();  // Leer el campo Eliminado
                return new Nodo(clave, valor)
                {
                    PosicionIzquierda = posicionIzquierda,
                    PosicionDerecha = posicionDerecha,
                    Eliminado = eliminado
                };
            }
        }

        public void ActualizarNodo(long posicion, Nodo nodo)
        {
            using (FileStream fs = new FileStream(pathArchivo, FileMode.Open))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                fs.Seek(posicion, SeekOrigin.Begin); // Moverse a la posición especificada en el archivo
                writer.Write(nodo.Clave);            // Escribimos la clave del nodo
                writer.Write(nodo.Valor);            // Escribimos el valor del nodo
                writer.Write(nodo.PosicionIzquierda); // Escribimos la posición del hijo izquierdo
                writer.Write(nodo.PosicionDerecha);  // Escribimos la posición del hijo derecho
                writer.Write(nodo.Eliminado);        // Escribimos el estado de eliminado (asegurándonos de que sea false para nodos reutilizados)
            }
        }


    }



}
