using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Guia3_Pt132129
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private bool estado = false; //Nos servirá como variable de control para saber el estado de la simulación
        int[] arreglo_numeros; //Definimos un arreglo de enteros, que contendra los datos a ordenar
        Button[] arreglo; //Definimos un arreglo de botones, que nos ayudara para la simulacion
        Numero Dato = new Numero();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try {

                int num = Convert.ToInt32(txtNumero.Text);
                Dato.Insertar_Dato(num); //Se agrega al objeto "Datos"
                arreglo_numeros = Dato.getArreglo();   //Se sacan los arreglos del objeto "Datos"

                arreglo = Dato.Arreglo_Botones();

            }
            catch
            {
                MessageBox.Show("Solo se admiten numeros enteros");

            }

            estado = true; //Cambiamos el valor de la variable de control para la simulacion
            tabPage1.Refresh();
            txtNumero.Text = "";
            txtNumero.Focus();

        }



        public void ShellSort(ref int[] arreglo, ref Button[] Arreglo_Numeros) {
           
            Stopwatch crono = new Stopwatch();
            crono.Start();

            int j, inc;

            inc = arreglo.Length / 2;

            while (inc >= 1)
            {

                for (int i = inc; i < arreglo.Length; i++)
                {

                    int v = arreglo[i];

                    j = i - inc;

                    while (j >= 0 && arreglo[j] > v)
                    {

                        arreglo[j + inc] = arreglo[j];

                        Intercambio(ref Arreglo_Numeros, j + inc, j);

                        j = j - inc;

                    }

                    arreglo[j + inc] = v;
                }
                inc = inc / 2;

            }

            crono.Stop();
            MessageBox.Show("El ordenamiento ha tardado: " + Convert.ToString(crono.Elapsed.Milliseconds) + " milisegundos");

        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            if (estado) {
                Point xy = new Point(50, 70); //Metodo de la libreria drawing

                try
                { //Funcion que dibujara los datos en la simulacion
                    Dibujar_Arreglo(ref arreglo, xy, ref tabPage1);
                }
                catch { 
                    
                }
                estado = false;
            }
        }

    public void Dibujar_Arreglo(ref Button[] Arreglo, Point xy, ref TabPage t)
        {
            for (int i = 1; i < Arreglo.Length; i++)
            {
                Arreglo[i].Location = xy;
                t.Controls.Add(Arreglo[i]);
                xy += new Size(70, 0);
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //Cambiamos la apariencia del cursos al modo espera
            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            ShellSort(ref arreglo_numeros, ref arreglo);

            this.Cursor = Cursors.Default; //Retornamos la apariencia del cursor al modo por defecto

            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
        }

        public void Intercambio(ref Button[] boton, int a, int b)
        {
            string temp = boton[a].Text;
            Point pa = boton[a].Location;
            Point pb = boton[b].Location;
            int diferencia = pa.X - pb.X;
            int x = 10;
            int y = 10;
            int t = 50;

            while (y != 70)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(0, 10);
                boton[b].Location += new Size(0, -10);
                y += 10;
            }

            while (x != diferencia + 10)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(-10, 0);
                boton[b].Location += new Size(10, 0);
                x += 10;
            }
            y = 0;
            while (y != -60)
            {
                Thread.Sleep(t);
                boton[a].Location += new Size(0, -10);
                boton[b].Location += new Size(0, +10);
                y -= 10;
            }
            boton[a].Text = boton[b].Text;
            boton[b].Text = temp;
            boton[b].Location = pb;
            boton[a].Location = pa;
            estado = true;
            tabPage1.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //Pausar cursor

            //Cambio de estado de controlles
            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            //Llamamos al metodo
            InsertionSort(ref arreglo_numeros, ref arreglo);
            this.Cursor = Cursors.Default;

            //Cambio de estado de controlles
            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
            txtNumero.Focus();

        }

        public void InsertionSort(ref int[] arreglo_Numeros, ref Button[] arreglo)
        {
            Stopwatch crono = new Stopwatch();
            crono.Start();

            for (int i = 0; i < arreglo.Length; i++)
            {
                int temp = arreglo_Numeros[i];
                int j = i - 1;
                while ((j >= 0) && (arreglo_Numeros[j] > temp))
                {

                    arreglo_Numeros[j + 1] = arreglo_Numeros[j];
                    Intercambio(ref arreglo, j + 1, j);
                    j--;

                }
                arreglo_Numeros[j + 1] = temp;

            }

            crono.Stop();
            MessageBox.Show("El ordenamiento ha tardado: " + Convert.ToString(crono.Elapsed.Milliseconds) + " milisegundos");
        }

        public void SelectionSort(ref int[] arreglo_Numeros, ref Button[] arreglo)
        {
            Stopwatch crono = new Stopwatch();
            crono.Start();

            int temp = 0;
            int pos_min;

            for (int i = 0; i < arreglo.Length - 1; i++)
            {
                pos_min = i; //set pos_min to the current index of array

                for (int j = i + 1; j < arreglo.Length; j++)
                {
                    // We now use 'CompareTo' instead of '<'
                    if (arreglo_Numeros[j].CompareTo(arreglo_Numeros[pos_min]) < 0)
                    {
                        //pos_min will keep track of the index that min is in, this is needed when a swap happens
                        pos_min = j;
                    }
                }

                //if pos_min no longer equals i than a smaller value must have been found, so a swap must occur
                if (pos_min != i)
                {
                    temp = arreglo_Numeros[i];

                    arreglo_Numeros[i] = arreglo_Numeros[pos_min];

                    arreglo_Numeros[pos_min] = temp;

                    Intercambio(ref arreglo, pos_min, i);
                }
            }

            

            crono.Stop();
            MessageBox.Show("El ordenamiento ha tardado: " + Convert.ToString(crono.Elapsed.Milliseconds) + " milisegundos");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //Pausar cursor

            //Cambio de estado de controlles
            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            //Llamamos al metodo
            SelectionSort(ref arreglo_numeros, ref arreglo);
            this.Cursor = Cursors.Default;

            //Cambio de estado de controlles
            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
            txtNumero.Focus();
        }



    }
}
