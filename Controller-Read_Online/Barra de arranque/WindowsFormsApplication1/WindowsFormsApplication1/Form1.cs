using System;//Librerias del sistema
using System.Collections.Generic;//Librerias del sistema
using System.ComponentModel;//Librerias del sistema
using System.Data;//Librerias del sistema
using System.Drawing;//Librerias del sistema
using System.Linq;//Librerias del sistema
using System.Text;//Librerias del sistema
using System.Threading.Tasks;//Librerias del sistema
using System.Windows.Forms;//Librerias del sistema
using System.Diagnostics;//Librerias del sistema
using System.Speech.Synthesis;//Librerias del sistema de sintesis de texto a voz

/* 

 Controlador/Aplicación/Recepción/Conversión/Envío para Robot de 6 grados de libertad:

 BARRA DE PROGRESO DE INICIO

 TFG - TRABAJO DE FIN DE GRADO
 MODULO DE RECONOCIMENTO GESTUAL PARA EL CONTROL DE ROBOT EN TAREAS DE ASISTENCIA

 ALUMNO : DAVID VELASCO GARCIA

 GRADO EN INGENIERÍA EN TECNOLOGÍAS INDUSTRIALES
 INTENSIFICACIÓN EN ELECTRÓNICA INDUSTRIAL Y AUTOMÁTICA

 UNIVERSISDAD CARLOS III DE MADRID
 ESCUELA POLITÉCNICA SUPERIOR

*/

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form//Inicio de la windows form
    {
        public Form1()
        {
            InitializeComponent();//Inicializacion de la form
            SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciacion de objeto de sintesis de voz

           
            synth.SetOutputToDefaultAudioDevice();//Seleccion del dispositivo de audio predeterminado

            //Se sintetiza el teexto a voz, con salida por el altavoz

            synth.Speak("el modulo de reconocimiento gestual para el control de robot en tareas de asistencia va ha iniciarse, porfavor, espere hasta que finalice la barra de progreso");
            


        }

        private void progressBar1_Click(object sender, EventArgs e)//Objeto barra de progreso
        {
            ProgressBar pBar = new ProgressBar();//Instanciacion de la nueva barra
        }

        private void timer1_Tick(object sender, EventArgs e)//Union del temporizador a la barra de progreso
        {
           

            this.progressBar1.Increment(1);//Incremento de 1% de la barra de progreso por ciclo
            
            

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.timer1.Start();//Inicio del temporizador
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);//Incremento de la barra de progreso
            label4.Text = progressBar1.Value + "%";//Muestra en la etiqueta el porcentaje actual de carga
            if (progressBar1.Value == 100)//Cuando la barra llega al 100% de progreso
            {
                timer1.Enabled = false;//Se deshabilita el timmer
               System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Inicio\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
                Visible = false;//Se abre la app y cierra la aplicacion de barra de progreso
                Application.Exit();//Se cierra la app de progreso
            }
        }

        private void Form1_Load(object sender, EventArgs e)//Funcion de carga de la form
        {
            
        }
    }
}
