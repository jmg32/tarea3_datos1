﻿using System;
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
                long posicion = fs.Position; // Guardamos la posición actual del archivo
                writer.Write(nodo.Clave);
                writer.Write(nodo.Valor);
                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
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
                return new Nodo(clave, valor)
                {
                    PosicionIzquierda = posicionIzquierda,
                    PosicionDerecha = posicionDerecha
                };
            }
        }

        public void ActualizarNodo(long posicion, Nodo nodo)
        {
            using (FileStream fs = new FileStream(pathArchivo, FileMode.Open))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                fs.Seek(posicion, SeekOrigin.Begin);
                writer.Write(nodo.Clave);
                writer.Write(nodo.Valor);
                writer.Write(nodo.PosicionIzquierda);
                writer.Write(nodo.PosicionDerecha);
            }
        }
    }


}
