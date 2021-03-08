using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbol_AVL
{
    class AVL
    {
        public int valor;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public AVL NodoPadre;
        public int altura;
        public Rectangle prueba;
        private DibujaAVL arbol;
        public AVL() { }

        public DibujaAVL Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }

        public AVL(int valorNuevo, AVL izquierdo, AVL derecho, AVL padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }
        //sirva para insertar un nuevo valor en el arbol
        public AVL insertar(int valorNuevo, AVL Raiz)
        {
            if (Raiz == null)
                Raiz = new AVL(valorNuevo, null, null, null);
            else if (valorNuevo < Raiz.valor)
            {
                Raiz.NodoIzquierdo = insertar(valorNuevo, Raiz.NodoIzquierdo);
            }
            else if (valorNuevo > Raiz.valor)
            {
                Raiz.NodoDerecho = insertar(valorNuevo, Raiz.NodoDerecho);
            }
            else
            {

                MessageBox.Show("Valor Existente en el Arbol", "Error", MessageBoxButtons.OK);
            }

            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if (valorNuevo < Raiz.NodoIzquierdo.valor)
                    Raiz = RotacionIzquierdaSimple(Raiz);
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);

            }

            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo > Raiz.NodoDerecho.valor)
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz);

            }

            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;
            return Raiz;


        }

        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(AVL Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }

        AVL nodoE, nodoP;
        public AVL Eliminar(int valorEliminar, ref AVL Raiz)
        {
            if (Raiz != null)
            {
                if (valorEliminar < Raiz.valor)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if (valorEliminar > Raiz.valor)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        AVL NodoElimar = Raiz;
                        if (NodoElimar.NodoDerecho == null)
                        {
                            Raiz = NodoElimar.NodoIzquierdo;
                            if (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                if (valorEliminar < nodoE.valor)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }
                            if (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar > nodoE.NodoDerecho.valor)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                    nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if (NodoElimar.NodoIzquierdo == null)
                            {
                                Raiz = NodoElimar.NodoDerecho;
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    AVL AuxiliarNodo = null;
                                    AVL Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    NodoElimar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                }
                                else
                                {
                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {
                                        AVL AuxiliarNodo = null;
                                        AVL Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        while (Auxiliar.NodoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        NodoElimar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }
                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0)
                                        {
                                            AVL AuxiliarNodo = null;
                                            AVL Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            NodoElimar = Auxiliar;
                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }


            }
            else 
            {
                MessageBox.Show("Nodo Inexistente en el arbol", "Error", MessageBoxButtons.OK);
            }
            return nodoP;

        }

        private static AVL RotacionIzquierdaSimple(AVL K2)
        {
            AVL k1 = K2.NodoIzquierdo;
            K2.NodoIzquierdo = k1.NodoDerecho;
            k1.NodoDerecho = K2;
            K2.altura = max(Alturas(K2.NodoIzquierdo), Alturas(K2.NodoDerecho)) + 1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), K2.altura) + 1;
            return k1;
        }

        private static AVL RotacionDerechaSimple(AVL K1)
        {
            AVL k2 = K1.NodoDerecho;
            K1.NodoDerecho=k2.NodoIzquierdo;
            k2.NodoIzquierdo = K1;
            K1.altura = max(Alturas(K1.NodoIzquierdo), Alturas(K1.NodoDerecho)) + 1;
            k2.altura = max(Alturas(k2.NodoDerecho), K1.altura) + 1;
            return k2;
        }
        private static AVL RotacionIzquierdaDoble(AVL K3)
        {
            K3.NodoIzquierdo = RotacionDerechaSimple(K3.NodoIzquierdo);
            return RotacionIzquierdaSimple(K3);
        }
        private static AVL RotacionDerechaDoble(AVL K1)
        {
            K1.NodoDerecho = RotacionDerechaSimple(K1.NodoDerecho);
            return RotacionDerechaSimple(K1);
        }
        public  int getAltura (AVL nodoActual)
        {
            if (nodoActual == null)
            {
                return 0;
            }else
            {
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));

            }
        }

        public void buscar(int valorBuscar, AVL Raiz)
        {
            if (Raiz != null)
            {
                if (valorBuscar < Raiz.valor)
                {
                    buscar(valorBuscar, Raiz.NodoIzquierdo);
                }
                else
                {
                    if(valorBuscar> Raiz.valor)
                    {
                        buscar(valorBuscar, Raiz.NodoDerecho);
                    }

                }
            }else
            {
                MessageBox.Show("Valor no Encontrado", "Error", MessageBoxButtons.OK);
            }
        }


        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistaionV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodo(ref int xmin,int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistaionV); 
            }
            if((NodoIzquierdo!=null)&& (NodoDerecho!=null))
            {
                xmin += DistanciaH;
            }

            if (NodoDerecho != null) { NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistaionV); }

            if (NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                {
                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);
                }else
                {
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if (NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                NodoDerecho.CoordenadaX = CoordenadaX + 40;
                CoordenadaX = aux2;

            }
            else
            {
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        public void DibujarRamas(Graphics grafo, Pen lapiz)
        {
            if (NodoIzquierdo != null)
            {
                grafo.DrawLine(lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, lapiz);
            }
            if(NodoDerecho!= null)
            {
                grafo.DrawLine(lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, lapiz);
            }
        }

        public void DibujarNodo(Graphics grafo,Font fuente , Brush Relleno,Brush RellenoFuente,Pen Lapiz,int dato,Brush encuentro)
        {
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            if (valor == dato)
            {
                grafo.FillEllipse(encuentro, rect);
                
            }else
            {
                grafo.FillEllipse(encuentro, rect);
                grafo.FillEllipse(Relleno, rect);
            }
            grafo.DrawEllipse(Lapiz, rect);
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);

            if(NodoIzquierdo!= null)
            {
                NodoIzquierdo.DibujarNodo(grafo, fuente, Brushes.YellowGreen, RellenoFuente, Lapiz, dato, encuentro);
            }
            if (NodoDerecho != null)
            {
                NodoDerecho.DibujarNodo(grafo, fuente, Brushes.Yellow, RellenoFuente, Lapiz, dato, encuentro);
            }
        }

        public void Colorear(Graphics grafo ,Font fuente,Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 20), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            //prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;

            grafo.DrawEllipse(Lapiz, rect);
            grafo.FillEllipse(Brushes.PaleVioletRed, rect);
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);
        }



    }
}
