using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas_Unitarias
{
    public interface ILista
    {
        void InsertInOrder(int Valor);
        int DeleteFirst();
        int DeleteLast();
        bool DeleteValor(int Valor);
        int GetnodoMedio();
        void MergeSorted(ILista listA, ILista listB, SortDirection direction);
    }


    public enum SortDirection
    {
        Asc,
        Desc
    }
}
