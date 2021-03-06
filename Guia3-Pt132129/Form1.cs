﻿using System;
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
        string[] arreglo_numeros; //Definimos un arreglo de enteros, que contendra los datos a ordenar
        Button[] arreglo; //Definimos un arreglo de botones, que nos ayudara para la simulacion
        Numero Dato = new Numero();
        bool flag = false;
        bool wasNumber = false;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try {

                if (radioButton1.Checked) {

                    //string num = Convert.ToInt32(txtNumero.Text);
                    string num = txtNumero.Text;
                    int i = 0;
                    //verificamos si fue numero
                    if (int.TryParse(num.ToString(), out i))
                    {
                        wasNumber = true;
                        Dato.Insertar_Dato(num); //Se agrega al objeto "Datos"
                        arreglo_numeros = Dato.getArreglo();   //Se sacan los arreglos del objeto "Datos"
                        arreglo = Dato.Arreglo_Botones();
                        
                        //bandera para deshabilitar radiobutton
                        flag = true;
                    }
                    else {
                        MessageBox.Show("No era un numero");
                    }



                }
                else if (radioButton2.Checked)
                {
                    //string num = Convert.ToInt32(txtNumero.Text);
                    string num = txtNumero.Text;
                    int i = 0;
                    //verificamos si fue numero
                    if (!int.TryParse(num.ToString(), out i) && num.ToString() != "" && num.Length<2)
                    {
                        wasNumber = true;
                        Dato.Insertar_Dato(num); //Se agrega al objeto "Datos"
                        arreglo_numeros = Dato.getArreglo();   //Se sacan los arreglos del objeto "Datos"
                        arreglo = Dato.Arreglo_Botones();

                        //bandera para deshabilitar radiobutton
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("No era caracter");
                    }
                
                }

                          
            }
            catch
            {
                MessageBox.Show("Solo se admiten numeros enteros");

            }

            //Deshabilitar radiobutton
            if (flag) {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                
            }

            estado = true; //Cambiamos el valor de la variable de control para la simulacion
            tabPage1.Refresh();
            txtNumero.Text = "";
            txtNumero.Focus();

        }



        public void ShellSort(ref string[] arreglo, ref Button[] Arreglo_Numeros) {
           
            Stopwatch crono = new Stopwatch();
            crono.Start();

            int j, inc;

            inc = arreglo.Length / 2;

            while (inc >= 1)
            {

                for (int i = inc; i < arreglo.Length; i++)
                {

                    string v = arreglo[i];

                    j = i - inc;

                    while (j >= 0 && arreglo[j].CompareTo(v) >0)
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
                xy += new Size(60, 0);
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
            int t = 10;

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

            Stopwatch crono = new Stopwatch();
            crono.Start();

            //Llamamos al metodo
            QuickSort(ref arreglo_numeros, ref arreglo, 0, arreglo_numeros.Length - 1);
            this.Cursor = Cursors.Default;

            crono.Stop();
            MessageBox.Show("El ordenamiento ha tardado: " + Convert.ToString(crono.Elapsed.Milliseconds) + " milisegundos");

            //Cambio de estado de controlles
            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
            txtNumero.Focus();

        }

        public void QuickSort(ref string[] arreglo_Numeros, ref Button[] arreglo, int primero, int ultimo)
        {

            int i, j, central;
            string pivote;
            central = (primero + ultimo) / 2;
            pivote = arreglo_Numeros[central];
            i = primero;
            j = ultimo;

            do
            {
                while (arreglo_Numeros[i].CompareTo(pivote) < 0) i++;
                while (arreglo_Numeros[j].CompareTo(pivote) > 0) j--;

                if (i <= j)
                {
                    string temp;
                    temp = arreglo_Numeros[i];
                    arreglo_Numeros[i] = arreglo_Numeros[j];
                    arreglo_Numeros[j] = temp;
                    Intercambio(ref arreglo, j, i);
                    i++;
                    j--;
                }

            } while (i <= j);

            if (primero < j)
            {
                QuickSort(ref arreglo_Numeros, ref arreglo, primero, j);
            }
            if (i < ultimo)
            {
                QuickSort(ref arreglo_Numeros, ref arreglo, i, ultimo);
            }

        }

        public void MergeSort(ref string[] arreglo_Numeros, ref Button[] arreglo, int startIndex, int endIndex)
        {
            int mid;

            if (endIndex > startIndex)
            {
                mid = (endIndex + startIndex) / 2;
                MergeSort(ref arreglo_Numeros, ref arreglo, startIndex, mid);
                MergeSort(ref arreglo_Numeros, ref arreglo, (mid + 1), endIndex);
                Merge(arreglo_Numeros, startIndex, (mid + 1), endIndex);
            }
        }

        public void Merge(string[] input, int left, int mid, int right)
        {
            //Merge procedure takes theta(n) time
            string[] temp = new string[input.Length];
            int i, leftEnd, lengthOfInput, tmpPos;
            leftEnd = mid - 1;
            tmpPos = left;
            lengthOfInput = right - left + 1;

            //selecting smaller element from left sorted array or right sorted array and placing them in temp array.
            while ((left <= leftEnd) && (mid <= right))
            {
                if (input[left].CompareTo(input[mid]) <= 0)
                {
                    temp[tmpPos++] = input[left++];
                }
                else
                {
                    temp[tmpPos++] = input[mid++];
                }
            }
            //placing remaining element in temp from left sorted array
            while (left <= leftEnd)
            {
                temp[tmpPos++] = input[left++];
            }

            //placing remaining element in temp from right sorted array
            while (mid <= right)
            {
                temp[tmpPos++] = input[mid++];
            }

            //placing temp array to input
            for (i = 0; i < lengthOfInput; i++)
            {
                //Intercambio(ref arreglo, right, i);
                input[right] = temp[right];
                right--;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; //Pausar cursor

            //Cambio de estado de controlles
            btnOrdenar.Enabled = false;
            txtNumero.Enabled = false;
            btnAgregar.Enabled = false;

            Stopwatch crono = new Stopwatch();
            crono.Start();
            //Llamamos al metodo
            MergeSort(ref arreglo_numeros, ref arreglo, 0, arreglo_numeros.Length - 1);
            this.Cursor = Cursors.Default;

            crono.Stop();
            MessageBox.Show("El ordenamiento ha tardado: " + Convert.ToString(crono.Elapsed.Milliseconds) + " milisegundos");

            redibujar(ref arreglo_numeros);

            estado = true; //Cambiamos el valor de la variable de control para la simulacion
            tabPage1.Refresh();
            txtNumero.Text = "";
            txtNumero.Focus();

            //Cambio de estado de controlles
            btnOrdenar.Enabled = true;
            txtNumero.Enabled = true;
            btnAgregar.Enabled = true;
            txtNumero.Focus();
        }

        public void redibujar(ref string[] arreglo_numeros)
        {
            int i = 0;
            int cont = arreglo_numeros.Length;

            for (i = 1; i < cont; i++)
            {
                arreglo[i].Text = arreglo_numeros[i].ToString();
                MessageBox.Show(arreglo_numeros[i].ToString());
            }

            //MessageBox.Show("sali");
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
