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

            // Esta prueba verifica que al insertar el primer nodo  en el árbol, se almacena correctamente y se puede recuperar con la función de búsqueda.
            [TestMethod]
            public void InsertarNodoRaiz()
            {
                BSTree arbol = new BSTree(testFilePath);

                arbol.Insertar(10, "Valor 10");

                Nodo nodo = arbol.Buscar(10);
                Assert.IsNotNull(nodo);
                Assert.AreEqual(10, nodo.Clave);
                Assert.AreEqual("Valor 10", nodo.Valor);
            }

            // Esta prueba verifica que la búsqueda de un nodo existente en el árbol devuelve el nodo correcto con la clave y el valor esperados.
            [TestMethod]
            public void BuscarNodoExistente()
            {
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(20, "Valor 20");

                Nodo nodo = arbol.Buscar(20);

                Assert.IsNotNull(nodo);
                Assert.AreEqual(20, nodo.Clave);
                Assert.AreEqual("Valor 20", nodo.Valor);
            }

            // Esta prueba verifica que la búsqueda de un nodo que no existe en el árbol devuelve null, lo cual indica que no se encontró el nodo.
            [TestMethod]
            public void BuscarNodoNoExistente()
            {
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");

                Nodo nodo = arbol.Buscar(30); // Clave no existente

                Assert.IsNull(nodo);
            }

            // Esta prueba verifica que se pueden insertar múltiples nodos en el árbol, y que todos ellos se almacenan y se pueden recuperar correctamente con la búsqueda.
            [TestMethod]
            public void InsertarMultiplesNodos()
            {
                BSTree arbol = new BSTree(testFilePath);

                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(5, "Valor 5");
                arbol.Insertar(20, "Valor 20");

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

            // Esta prueba verifica que al intentar insertar una clave duplicada, el nodo original se mantiene sin cambios y no se inserta una nueva copia
            [TestMethod]
            public void InsertarNodoDuplicado()
            {
                BSTree arbol = new BSTree(testFilePath);

                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(10, "Valor Duplicado");

                Nodo nodo = arbol.Buscar(10);
                Assert.IsNotNull(nodo);
                Assert.AreEqual("Valor 10", nodo.Valor); // Verifica que el valor no cambió
            }

            // Esta prueba verifica que un nodo existente se puede eliminar correctamentey que ya no se puede encontrar en el árbol después de la eliminación.
            [TestMethod]
            public void EliminarNodo_Existente()
            {
                // Arrange
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(5, "Valor 5");
                arbol.Insertar(20, "Valor 20");

                // Act
                bool eliminado = arbol.Eliminar(5);

                // Assert
                Assert.IsTrue(eliminado);
                var nodoEliminado = arbol.Buscar(5);
                Assert.IsNull(nodoEliminado);  // El nodo ya no debería estar disponible
            }

            // Esta prueba verifica que al eliminar un nodo y luego insertar un nuevo nodo, la posición vacante del nodo eliminado se reutiliza correctamente para el nuevo nodo.
            [TestMethod]
            public void InsertarDespuesDeEliminar()
            {
                BSTree arbol = new BSTree(testFilePath);
                arbol.Insertar(10, "Valor 10");
                arbol.Insertar(5, "Valor 5");
                arbol.Insertar(20, "Valor 20");

                bool eliminado = arbol.Eliminar(5); // Elimina el nodo con clave 5
                arbol.Insertar(7, "Valor 7"); // Intenta insertar en la posición vacante

                var nodo7 = arbol.Buscar(7); // Busca el nodo recién insertado
                Assert.IsNotNull(nodo7); // El nodo no debe ser null
                Assert.AreEqual(7, nodo7.Clave); // Verifica que la clave sea 7

                var nodo5 = arbol.Buscar(5);
                Assert.IsNull(nodo5); // Verifica que el nodo 5 sigue eliminado
            }




        }
    }
}