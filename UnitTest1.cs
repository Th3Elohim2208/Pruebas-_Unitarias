namespace Pruebas_Unitarias
{
    [TestClass]
    public class ListaDobleTests
    {
        private ListaDoble listaA;
        private ListaDoble listaB;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            listaA = new ListaDoble();
            listaB = new ListaDoble();
        }

        // PROBLEMA 1: Mezclar en orden
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMergeSorted_NullListA_ThrowsException()
        {
            listaA = null;
            listaB = new ListaDoble();

            listaB.InsertInOrder(1);
            listaB.InsertInOrder(2);

            listaA.MergeSorted(listaA, listaB, SortDirection.Asc); // Esto debe lanzar una excepción

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMergeSorted_NullListB_ThrowsException()
        {
            listaB = null;
            listaA = new ListaDoble();

            listaA.InsertInOrder(1);
            listaA.InsertInOrder(2);

            listaA.MergeSorted(listaA, listaB, SortDirection.Asc);  // Esto debe lanzar una excepción
           
        }

        [TestMethod]
        public void TestMergeSorted_Asc()
        {
            listaA.InsertInOrder(0);
            listaA.InsertInOrder(2);
            listaA.InsertInOrder(6);
            listaA.InsertInOrder(10);
            listaA.InsertInOrder(25);

            listaB.InsertInOrder(3);
            listaB.InsertInOrder(7);
            listaB.InsertInOrder(11);
            listaB.InsertInOrder(40);
            listaB.InsertInOrder(50);

            listaA.MergeSorted(listaA, listaB, SortDirection.Asc);
            listaA.PrintList(TestContext);

            int[] expected = { 0, 2, 3, 6, 7, 10, 11, 25, 40, 50 };
            foreach (var val in expected)
            {
                Assert.AreEqual(val, listaA.DeleteFirst());
            }
        }

        [TestMethod]
        public void TestMergeSorted_Desc()
        {
            listaA.InsertInOrder(10);
            listaA.InsertInOrder(15);

            listaB.InsertInOrder(9);
            listaB.InsertInOrder(40);
            listaB.InsertInOrder(50);

            listaA.MergeSorted(listaA, listaB, SortDirection.Desc);
            listaA.PrintList(TestContext);

            int[] expected = { 50, 40, 15, 10, 9 };
            foreach (var val in expected)
            {
                Assert.AreEqual(val, listaA.DeleteFirst());
            }
        }

        // PROBLEMA 2: Invertir Lista
        [TestMethod]
        public void TestInvertList()
        {
            listaA.InsertInOrder(1);
            listaA.InsertInOrder(0);
            listaA.InsertInOrder(30);
            listaA.InsertInOrder(50);
            listaA.InsertInOrder(2);

            listaA.Invert();
            listaA.PrintList(TestContext);

            int[] expected = { 50, 30, 2, 1, 0 };
            foreach (var val in expected)
            {
                Assert.AreEqual(val, listaA.DeleteFirst());
            }
        }

        [TestMethod]
        public void TestInvert_SingleElement()
        {
            listaA.InsertInOrder(2);
            listaA.Invert();
            listaA.PrintList(TestContext);
            Assert.AreEqual(2, listaA.DeleteFirst());
        }

        // PROBLEMA 3: Obtener el nodo medio
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetnodoMedio_EmptyList_ThrowsException()
        {
            listaA.GetnodoMedio();
            listaA.PrintList(TestContext);
        }

        [TestMethod]
        public void TestGetnodoMedio_SingleElement()
        {
            listaA.InsertInOrder(1);
            listaA.PrintList(TestContext);
            Assert.AreEqual(1, listaA.GetnodoMedio());
        }

        [TestMethod]
        public void TestGetnodoMedio_MultipleElements()
        {
            listaA.InsertInOrder(0);
            listaA.InsertInOrder(1);
            listaA.InsertInOrder(2);
            listaA.PrintList(TestContext);
            Assert.AreEqual(1, listaA.GetnodoMedio()); // El elemento medio es 1

            listaA.InsertInOrder(3);
            listaA.PrintList(TestContext);
            Assert.AreEqual(2, listaA.GetnodoMedio()); // El elemento medio es 2

            listaA.InsertInOrder(4);
            listaA.PrintList(TestContext);
            Assert.AreEqual(2, listaA.GetnodoMedio()); // El elemento medio sigue siendo 2
        }
    }

}
