using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pruebas_Unitarias
{
    public class ListaDoble : ILista
    {
        // Nodo de la lista doblemente enlazada
        private class Nodo
        {
            public int Valor { get; set; }
            public Nodo Anterior { get; set; }
            public Nodo Siguiente { get; set; }

            public Nodo(int valor)
            {
                Valor = valor;
                Siguiente = null;
                Anterior = null;
            }
        }

        private Nodo cabeza;
        private Nodo cola;
        private Nodo nodoMedio;
        private int tamaño;

        public ListaDoble()
        {
            cabeza = null;
            cola = null;
            nodoMedio = null;
            tamaño = 0;
        }

        // Implementar InsertInOrder
        public void InsertInOrder(int Valor)
        {
            Nodo newNodo = new Nodo(Valor);

            // Si la lista está vacía, se inicializa con el nuevo nodo
            if (cabeza == null)
            {
                cabeza = cola = nodoMedio = newNodo;
            }
            else
            {
                // Buscar la posición correcta para insertar en orden ascendente
                Nodo current = cabeza;
                while (current != null && current.Valor < Valor)
                {
                    current = current.Siguiente;
                }

                if (current == null)
                {
                    // Insertar al final si es mayor que todos 
                    cola.Siguiente = newNodo;
                    newNodo.Anterior = cola;
                    cola = newNodo;
                }
                else if (current == cabeza)
                {
                    // Insertar al inicio si es menor que el primer elemento
                    newNodo.Siguiente = cabeza;
                    cabeza.Anterior = newNodo;
                    cabeza = newNodo;
                }
                else
                {
                    // Insertar en el medio
                    newNodo.Anterior = current.Anterior;
                    newNodo.Siguiente = current;
                    current.Anterior.Siguiente = newNodo;
                    current.Anterior = newNodo;
                }
            }

            tamaño++;
            UpdatenodoMedio();
        }

        // Implementar DeleteFirst
        public int DeleteFirst()
        {
            if (cabeza == null) throw new InvalidOperationException("List is empty.");

            int Valor = cabeza.Valor;
            cabeza = cabeza.Siguiente;
            if (cabeza != null)
            {
                cabeza.Anterior = null;
            }
            else
            {
                cola = null;// Si la lista queda vacía
            }

            tamaño--;
            UpdatenodoMedio();
            return Valor;
        }

        // Implementar DeleteLast
        public int DeleteLast()
        {
            if (cola == null) throw new InvalidOperationException("List is empty.");

            int Valor = cola.Valor;
            cola = cola.Anterior;
            if (cola != null)
            {
                cola.Siguiente = null;
            }
            else
            {
                cabeza = null;// Si la lista queda vacía
            }

            tamaño--;
            UpdatenodoMedio();
            return Valor;
        }

        // Implementar DeleteValor
        public bool DeleteValor(int Valor)
        {
            Nodo current = cabeza;
            while (current != null)
            {
                if (current.Valor == Valor)
                {
                    if (current == cabeza)
                    {
                        DeleteFirst();
                    }
                    else if (current == cola)
                    {
                        DeleteLast();
                    }
                    else
                    {
                        // Eliminar el nodo enlazando sus nodos adyacentes
                        current.Anterior.Siguiente = current.Siguiente;
                        current.Siguiente.Anterior = current.Anterior;
                        tamaño--;
                        UpdatenodoMedio();
                    }
                    return true;// Valor encontrado y eliminado
                }
                current = current.Siguiente;
            }
            return false;// Valor no encontrado

        }

        // Implementar GetnodoMedio
        public int GetnodoMedio()
        {
            if (nodoMedio == null)
                throw new InvalidOperationException("La lista está vacía o no inicializada");
            return nodoMedio.Valor;
        }

        // Implementar MergeSorted
        public void MergeSorted(ILista listA, ILista listB, SortDirection direction)
        {
            // Validar que ambas listas no sean nulas
            if (listA == null || listB == null)
            {
                throw new ArgumentException("Las listas no pueden ser null.");
            }

            ListaDoble listaA = listA as ListaDoble;
            ListaDoble listaB = listB as ListaDoble;

            if (listaA == null || listaB == null) throw new ArgumentException("Las listas no pueden ser null.");
            if (listaA.cabeza == null && listaB.cabeza != null) listaA.cabeza = listaB.cabeza;
            if (listaA.cabeza != null && listaB.cabeza == null) return;

            // Insertar los nodos de listaB en listaA en orden
            Nodo nodoB = listaB.cabeza;
            while (nodoB != null)
            {
                listaA.InsertInOrder(nodoB.Valor);
                nodoB = nodoB.Siguiente;
            }

            // Si se requiere en orden descendente, invertir la lista fusionada
            if (direction == SortDirection.Desc)
            {
                listaA.Invert();
            }
        }



        //Implementar Invert
        public void Invert()
        {
            if (cabeza == null) return; // Si la lista está vacía, no hacer nada

            Nodo current = cabeza;
            Nodo temp = null;

            // Intercambiar las referencias de anterior y siguiente en cada nodo
            while (current != null)
            {
                temp = current.Anterior;
                current.Anterior = current.Siguiente;
                current.Siguiente = temp;
                current = current.Anterior; // Después del intercambio, esta referencia apunta al siguiente nodo
            }

            // Actualizar las referencias de cabeza y cola
            temp = cabeza;
            cabeza = cola;
            cola = temp;
        }




        //Implementar UpdateMiddle
        private void UpdatenodoMedio()
        {
            if (tamaño == 0)
            {
                nodoMedio = null;
            }
            else if (tamaño == 1)
            {
                nodoMedio = cabeza;
            }
            else
            {
                int midIndex = tamaño / 2;
                Nodo current = cabeza;
                for (int i = 0; i < midIndex; i++)
                {
                    current = current.Siguiente;
                }
                nodoMedio = current;
            }
        }


        public void PrintList(TestContext testContext)
        {
            Nodo current = cabeza;
            while (current != null)
            {
                testContext.WriteLine(current.Valor.ToString());
                current = current.Siguiente;
            }
            testContext.WriteLine(""); // Para hacer salto de línea
        }


    }

}
