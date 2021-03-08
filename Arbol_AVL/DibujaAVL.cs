
using System;
using System.Drawing;

using System.Threading;
using System.Windows.Forms;

namespace Arbol_AVL
{
    class DibujaAVL
    {
        public AVL Raiz;
        public AVL aux;

        public DibujaAVL()
        {
            aux = new AVL();
        }
        public DibujaAVL(AVL RaizNueva)
        {
            Raiz = RaizNueva;
        }

        public void Insertar(int dato)
        {
            if (Raiz == null)
            {
                Raiz = new AVL(dato, null, null, null);
            }
            else
                Raiz = Raiz.insertar(dato, Raiz);
        }

        public void Eliminar(int dato)
        {
            if (Raiz == null)
            {
                Raiz = new AVL(dato, null, null, null);
            }
            else
                Raiz = Raiz.Eliminar(dato,  ref Raiz);

        }

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistaciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodorecorido(ref int xmin, ref int ymin)
        {
            CoordenadaY = (int)(ymin + Radio / 2);
            CoordenadaX = (int)(xmin + Radio / 2);
            xmin += Radio;
        }

        public void colorear(Graphics grafo,Font fuente, Brush Relleno,Brush RellenoFuente, Pen Lapiz,AVL Raiz, bool post,bool inor,bool preor)
        {
            Brush entorno = Brushes.Red;
            if (inor == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Brushes.Blue, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    Raiz.Colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(500);
                    Raiz.Colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);

                }
            }
            else if (preor == true)
            {
                if (Raiz != null)
                {
                    
                    Raiz.Colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                    Thread.Sleep(500);
                    Raiz.Colorear(grafo, fuente, Brushes.White, Brushes.Black, Pens.Black);
                    colorear(grafo, fuente, Brushes.Blue, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);

                }
            }
            else if (post!= true)
            {
                if (Raiz != null)
                {

                    
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);
                    Raiz.Colorear(grafo, fuente,entorno, RellenoFuente,Lapiz);
                    Thread.Sleep(500);                    
                    Raiz.Colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);


                }
            }
        }

        public void colorearB(Graphics grafo,Font fuente, Brush Relleno,Brush RellenoFuente,Pen Lapiz,AVL Raiz,int busqueda)
        {
            Brush entorno = Brushes.Red;
            if (Raiz != null)
            {
                Raiz.Colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                if(busqueda< Raiz.valor)
                {
                    Thread.Sleep(500);
                    Raiz.Colorear(grafo, fuente, entorno, Brushes.Blue, Lapiz);
                    colorearB(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, busqueda);
                }else
                {
                    if (busqueda > Raiz.valor)
                    {
                        Thread.Sleep(500);
                        Raiz.Colorear(grafo, fuente, entorno, Brushes.Blue, Lapiz);
                        colorearB(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, busqueda);
                    }else
                    {
                        Raiz.Colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                        Thread.Sleep(500);
                    }
                }
            }
        }

        public void DibujarArbol(Graphics grafo,Font fuente,Brush Relleno,Brush RellenoFuente,Pen Lapiz,int Dato,Brush encuentro)
        {
            int x = 100;
            int y = 75;
            if (Raiz == null) return;
            Raiz.PosicionNodo(ref x, y);
            
            Raiz.DibujarRamas(grafo, Lapiz);
            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, Dato, encuentro);
        }

        public int x1 = 100;
        public int y2 = 75;
        public void restablecer_Valores()
        {
            x1 = 100;
            y2 = 75;

        }
        public void buscar(int x)
        {
            if (Raiz == null)
                MessageBox.Show("Arbol VL Vacio", "Error", MessageBoxButtons.OK);
            else
                Raiz.buscar(x, Raiz);
        }

        public string MostrarOrdenSelc(int opcion)
        {
            string cadena = "";
            if (opcion == 0)
            {
                MessageBox.Show("Opcion de Orden No Valida", "Error", MessageBoxButtons.OK);
                return null;
            }
            if (opcion == 1) //Che en Pre orden
            {
                cadena = preorden(this.Raiz, ref cadena);
            }
            if (opcion == 2)//chque en orden
            {
                cadena = inorden(this.Raiz, ref cadena);
            }
            if (opcion == 3)
            {
                cadena = postorden(this.Raiz, ref cadena);
            }

            return cadena;


            
        }
        //Metodo en Ordenn 
        private string inorden(AVL rama, ref string cadena)//recibe  la raiz  y se llama ella misma de tal forma que recorre todo nodos ddel arbol hasta que no encutras mas hijos
        {
            if (rama != null)
            {
                inorden(rama.NodoIzquierdo, ref cadena);

                if (String.IsNullOrEmpty(cadena) == true)
                {
                    cadena += rama.valor;
                }
                else
                {
                    cadena += ", " + rama.valor;
                }

                inorden(rama.NodoDerecho, ref cadena);
            }

            return cadena;
        }

        private string preorden(AVL rama, ref string cadena)// la mismo logica solo cambio el momento donde imprime la raiz
        {
            if (rama != null)
            {
                if (String.IsNullOrEmpty(cadena) == true)
                {
                    cadena += rama.valor;
                }
                else
                {
                    cadena += ", " + rama.valor;
                }

                preorden(rama.NodoIzquierdo, ref cadena);
                preorden(rama.NodoDerecho, ref cadena);
            }

            return cadena;
        }

        private string postorden(AVL rama, ref string cadena)// la mismo logica solo cambio el momento donde imprime la raiz
        {
            if (rama != null)
            {
                postorden(rama.NodoIzquierdo, ref cadena);
                postorden(rama.NodoDerecho, ref cadena);

                if (String.IsNullOrEmpty(cadena) == true)
                {
                    cadena += rama.valor;
                }
                else
                {
                    cadena += ", " + rama.valor;
                }
            }

            return cadena;
        }
    }
}
