using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Guia3_Pt132129
{
    class Numero
    {
        private int longitud;   //Cantidad de datos a ordenar
        private string[] arreglo = new string[1];
        private Button[] arreglo_botones = new Button[1]; //Definimos un nuevo arreglo de botones

        public Numero() { //Constructor de la clase

            string a = "";      //Variable auxiliar
            arreglo[0] = a; //El texto que desplegara el boton
            arreglo_botones[0] = new Button();
            arreglo_botones[0].Width = 40;      //Definimos el ancho del boton
            arreglo_botones[0].Height = 40;     //Definimos el alto del boton
            arreglo_botones[0].BackColor = Color.DarkCyan;   //Defnimos el color del boton
            arreglo_botones[0].Text = Convert.ToString(a);
            Calcular_Longuitud();
       
        }

        private void Calcular_Longuitud(){  //Con esta funcion determinaremos cuantos datos
            
            longitud = arreglo.Length;      //se van a ordenar
        }

        public int getLongitud() {
            
            return longitud;
        }

        public string[] getArreglo() {

            return arreglo;
        }

        public void Insertar_Dato(string dato) { //Esta funcion la utilizaremos para insertar el 
                                              //Valor digitado por el usuario como texto en el boton

            Array.Resize<string>(ref arreglo, longitud + 1);
            arreglo[longitud] = dato;
            Array.Resize<Button>(ref arreglo_botones, longitud + 1);
            arreglo[longitud] = dato;
            arreglo_botones[longitud] = new Button();
            arreglo_botones[longitud].Height = 50;
            arreglo_botones[longitud].Width = 50;
            arreglo_botones[longitud].BackColor = Color.DarkCyan;
            arreglo_botones[longitud].Text = Convert.ToString(dato);
            Calcular_Longuitud();
        
        }

        public Button[] Arreglo_Botones() {
            return arreglo_botones;
        }

    }
}
