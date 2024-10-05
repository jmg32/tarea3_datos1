using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Tree
{
    public class Nodo
    {
        public long PosicionIzquierda { get; set; }
        public long PosicionDerecha { get; set; }
        public int Clave { get; set; }
        public string Valor { get; set; }
        public bool Eliminado { get; set; }  

        public Nodo(int clave, string valor)
        {
            Clave = clave;
            Valor = valor;
            PosicionIzquierda = -1;
            PosicionDerecha = -1;
            Eliminado = false;  
        }
    }



}
