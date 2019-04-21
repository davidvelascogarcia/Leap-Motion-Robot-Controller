using System;//Libreria del sistema
using System.Collections.Generic;//Libreria del sistema
using System.ComponentModel;//Libreria del sistema
using System.Data;//Libreria del sistema
using System.Drawing;//Libreria del sistema
using System.Linq;//Libreria del sistema
using System.Text;//Libreria del sistema
using System.Threading.Tasks;//Libreria del sistema
using System.Windows.Forms;//Libreria del sistema
using System.Diagnostics;//Libreria del sistema
using System.Runtime.InteropServices;//Libreria del sistema
using System.Speech.Synthesis;//Libreria del sistema de sintesis de texto a voz
using System.Threading;//Libreria del sistema
using AForge.Video;//Libreria aforge de video
using AForge.Video.DirectShow;//Libreria de aforge de retransmision de video


/* 

 Controlador/Aplicación/Recepción/Conversión/Envío para Robot de 6 grados de libertad:

 SELECCION DEL METODO DE CONTROL DEL ROBOT PROTOTIPO

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
            InitializeComponent();//Inizializacion del componente
            FormBorderStyle = FormBorderStyle.None;//Quitar bordes de la ventana
            WindowState = FormWindowState.Maximized;//Apertura en modo pantalla completa
            TopMost = true;
        }

        private FilterInfoCollection Dispositivos;//Instanciacion de listado de dispositivos de video
        private VideoCaptureDevice FuenteDeVideo;//Instanciacion del objeto de la fuente de video
        private void label1_Click(object sender, EventArgs e)//Funcion de elemnto grafico
        {

        }

        private void button1_Click(object sender, EventArgs e)//Funcion de elemnto grafico
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Fase 1 - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Funcion de elemento grafico
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Fase 2 - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//Abrimos el visualizador de diagnostico
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //Funcion de elemento grafico
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//Funcion de salida de pantalla completa
        {
            if (e.KeyCode == Keys.Escape)//Si se presiona la tecla escape
            {
                FormBorderStyle = FormBorderStyle.Sizable;//Se cierra el modo pantalla completa
                WindowState = FormWindowState.Normal;//Abre modo ventana
                TopMost = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Funcio de elemnto grafico
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //Funcion de elemento garfico
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //Funcion de elemento garfico
        }


        [DllImport("user32.dll")]//Importacion de dll del system para ejecutar embebidas app del sistema windows
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        private void button6_Click(object sender, EventArgs e)
        {

            Process p = Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//Ejecutamos el visualizador de diagnostico
            Thread.Sleep(500);//Espera
            p.StartInfo.CreateNoWindow = true;//Modo embebido
            SetParent(p.MainWindowHandle, this.Handle);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Optimizacion y mejora de la precision\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual

        }


        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Analisis de informacion gestual\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();//Cerramos la app
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Inicio\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            //Elemento garfico
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //Elemento garfico
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            //Elemento garfico
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Elemento garfico
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //Elemento garfico
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)//Incio del reloj del sistema
        {
            label13.Text = DateTime.Now.ToShortTimeString();//Mostramos la fecha y hora
            label12.Text = DateTime.Now.ToString("dd/MM/yyyy");//Con el formato
            if (i == 0)//Bloqueamos el acceso a una sola entrada
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciar objeto de sintesis de texto a voz

                //Selecciona dispositivo por defecto de audio
                synth.SetOutputToDefaultAudioDevice();

                //Sintetiza texto a voz

                synth.Speak("El módulo de control de robot prototipo, esta operativo y funcional");
                synth.Speak("Por favor, seleccione el modo de control que desee, tanto incremental como coordenadas cartesianas");
                synth.Speak("Si desea un control de cámaras, selecione la cámara a mostrar y seleccione conectar, y desconectar si desea parar la transmisión de imagen.");
                i++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)//Inicio de la form
        {
            timer1.Start();//Inicio del reloj
            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);//Asignamos el listado de dispositivos del pc de video
            foreach(FilterInfo x in Dispositivos)
            {
                comboBox1.Items.Add(x.Name);//Asignamos a la box de seleccion
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Informacion del proyecto\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Ejemplos de uso\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Conversion\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Control de robot via modulo\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Simulacion de control de robot\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FuenteDeVideo = new VideoCaptureDevice(Dispositivos[comboBox1.SelectedIndex].MonikerString);//Asignamos la fuente al objeto fuente
            videoSourcePlayer1.VideoSource = FuenteDeVideo;//Asignamos al reproductor
            videoSourcePlayer1.Start();//Iniciamos la transmision
        }

        private void button17_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();//Detenemos la transmision
        }

        private void button18_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Control de robot prototipo incremental telematico\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }

        private void button19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Control de robot prototipo coordenadas cartesianas\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la app del menu seleccionada y cerramos la actual
        }
    }
       
}
