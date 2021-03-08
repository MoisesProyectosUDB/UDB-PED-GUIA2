using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbol_AVL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int cont = 0;
        int dato = 0;
        int datb = 0;
        int cont2 = 0;
        int  opcion = 0; //Variable opcion para saber  en que orden se quiere ver la cadena
        DibujaAVL arbolAVL = new DibujaAVL(null);
        DibujaAVL arbolAVL_Letra = new DibujaAVL(null);
        Graphics g;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            arbolAVL.DibujarArbol(g, this.Font, Brushes.White, Brushes.Black, Pens.White, datb, Brushes.Black);
            datb = 0;

            if (pintarR == 1)
            {
                arbolAVL.colorear(g, this.Font, Brushes.Black, Brushes.Yellow, Pens.Blue, arbolAVL.Raiz, radioButton3.Checked, radioButton2.Checked, radioButton1.Checked);
                pintarR = 0;
            }
            if (pintarR == 2)
            {
                arbolAVL.colorearB(g, this.Font, Brushes.White, Brushes.Red, Pens.White, arbolAVL.Raiz, int.Parse(textBox1.Text));
                pintarR = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Valor Obligatorio");
            }
            else
            {
                //validamos que este seleccionado un orden desde el inicio.

                try {
                    dato = int.Parse(textBox1.Text);
                    arbolAVL.Insertar(dato);
                    textBox1.Clear();
                    textBox1.Focus();
                    label2.Text = arbolAVL.Raiz.getAltura(arbolAVL.Raiz).ToString();
                    cont++;
                    //se agrega logica para que en cada agregar reoredana  la cadena segun el radiobuton seleccionado
                    opcion = radioButton1.Checked == true ? 1 : radioButton2.Checked == true ? 2 : radioButton3.Checked == true ? 2 : 0;
                    textBox2.Clear();
                    textBox2.Text = arbolAVL.MostrarOrdenSelc(opcion);
                   
                    Refresh();
                    Refresh();
                
                }catch (Exception ex) 
                {
                    errorProvider1.SetError(textBox1, "Debe ser Numerico");
                }
            }
        }
        int pintarR = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Valor Obligatorio");
            }else
            {
                try {
                    datb = int.Parse(textBox1.Text);
                    arbolAVL.buscar(datb);
                    pintarR = 2;
                    Refresh();
                    textBox1.Clear();
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox1, "Debe Ser Numerico");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Valor Obligatorio");
            }
            else
            {
                try
                {
                    dato = int.Parse(textBox1.Text);
                    textBox1.Clear();
                    arbolAVL.Eliminar(dato);
                    label2.Text = arbolAVL.Raiz.getAltura(arbolAVL.Raiz).ToString();
                    Refresh();
                    Refresh();
                    cont2++;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox1, "Debe Ser Numerico");
                }
            }
            Refresh(); Refresh(); Refresh();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                errorProvider1.Clear();
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(textBox1, "Valor Obligatorio");
                }
                else
                {
                    try
                    {

                        dato = int.Parse(textBox1.Text);
                        if (dato > 0)
                        {
                            arbolAVL.Insertar(dato);
                            textBox1.Clear();
                            textBox1.Focus();
                            label2.Text = arbolAVL.Raiz.getAltura(arbolAVL.Raiz).ToString();
                            cont++;
                            Refresh();
                            Refresh();
                        }
                        else
                        {
                            errorProvider1.SetError(textBox1, "Debe ser un numero Mayor que 0");
                        }
                    }catch (Exception ex)
                    {
                        errorProvider1.SetError(textBox1, "Debe ser Numerico");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Text = arbolAVL.MostrarOrdenSelc(1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Text = arbolAVL.MostrarOrdenSelc(2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Text = arbolAVL.MostrarOrdenSelc(3);
        }
    }
}
