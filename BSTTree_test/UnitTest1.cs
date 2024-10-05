using BST_Tree;

namespace BSTTree_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class BSTreeTests
        {
            private string testFilePath;

            [TestInitialize]
            public void Setup()
            {
                // Configurar antes de cada prueba. Crear un archivo de prueba.
                testFilePath = "test_arbol.dat";

                // Asegurarse de que el archivo no exista antes de empezar.
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }

            [TestCleanup]
            public void Cleanup()
            {
                // Limpiar después de cada prueba. Eliminar el archivo de prueba.
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }

            [TestMethod]
            public void InsertarNodo_DebeInsertarNodoRaiz_Correctamente()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);

                // Act
                arbol.Insertar(10, "Valor 10");

                // Assert
                Nodo nodo = arbol.Buscar(10);
                Assert.IsNotNull(nodo);
                Assert.AreEqual(10, nodo.Clave);
                Assert.AreEqual("Valor 10", nodo.Valor);
            }

            [TestMethod]
            public void BuscarNodo_Existente_DebeRetornarNodoCorrecto()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(20, "Valor 20");

                // Act
                Nodo nodo = arbol.Buscar(20);

                // Assert
                Assert.IsNotNull(nodo);
                Assert.AreEqual(20, nodo.Clave);
                Assert.AreEqual("Valor 20", nodo.Valor);
            }

            [TestMethod]
            public void BuscarNodo_NoExistente_DebeRetornarNull()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");

                // Act
                Nodo nodo = arbol.Buscar(30); // Clave no existente

                // Assert
                Assert.IsNull(nodo);
            }

            [TestMethod]
            public void InsertarMultiplesNodos_DebeInsertarCorrectamente()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);

                // Act
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(5, "Valor 5");
                arbol.Insertar(20, "Valor 20");

                // Assert
                var nodo10 = arbol.Buscar(10);
                var nodo5 = arbol.Buscar(5);
                var nodo20 = arbol.Buscar(20);

                Assert.IsNotNull(nodo10);
                Assert.AreEqual(10, nodo10.Clave);

                Assert.IsNotNull(nodo5);
                Assert.AreEqual(5, nodo5.Clave);

                Assert.IsNotNull(nodo20);
                Assert.AreEqual(20, nodo20.Clave);
            }

            [TestMethod]
            public void InsertarNodoDuplicado_DebeMantenerUnicoNodo()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);

                // Act
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(10, "Valor Duplicado");

                // Assert
                Nodo nodo = arbol.Buscar(10);
                Assert.IsNotNull(nodo);
                Assert.AreEqual("Valor 10", nodo.Valor); // Verifica que el valor no cambió
            }
        }
    }
}