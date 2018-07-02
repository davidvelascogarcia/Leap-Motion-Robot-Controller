using System;//Uso de las librerias del sistema
using System.Collections.Generic;//Uso de las librerias del sistema
using System.ComponentModel;//Uso de las librerias del sistema
using System.Data;//Uso de las librerias del sistema
using System.Drawing;//Uso de las librerias del sistema
using System.Linq;//Uso de las librerias del sistema
using System.Text;//Uso de las librerias del sistema
using System.Threading.Tasks;//Uso de las librerias del sistema
using System.Windows.Forms;//Uso de las librerias del sistema
using Leap;//Uso de las librerias del leap motion
using System.Diagnostics;//Uso de las librerias del sistema
using System.Runtime.InteropServices;//Uso de las librerias del sistema
using System.Speech.Synthesis;//Uso de las librerias del sistema de sintesis de texto a voz
using System.Threading;//Uso de las librerias del sistema


/* 

 Controlador/Aplicación/Recepción/Conversión/Envío para Robot de 6 grados de libertad:

 ANALISIS Y MEJORA DE LA PRECISION

 TFG - TRABAJO DE FIN DE GRADO
 MODULO DE RECONOCIMENTO GESTUAL PARA EL CONTROL DE ROBOT EN TAREAS DE ASISTENCIA

 ALUMNO : DAVID VELASCO GARCIA

 GRADO EN INGENIERÍA EN TECNOLOGÍAS INDUSTRIALES
 INTENSIFICACIÓN EN ELECTRÓNICA INDUSTRIAL Y AUTOMÁTICA

 UNIVERSISDAD CARLOS III DE MADRID
 ESCUELA POLITÉCNICA SUPERIOR

*/

namespace WindowsFormsApplication1//Namespace de la windows form
{
    public partial class Form1 : Form, ILeapEventDelegate
    {

        private Controller controller;//Uso de la clase privada controlador
        private LeapEventListener listener;//Uso de la clase privada escucha del dispositivo Leap Motion
        public Form1()//Form 1 métodos publicos
        {
            InitializeComponent();//Inicialización de la form
            FormBorderStyle = FormBorderStyle.None;//Desactivar bordes de la app, es decir los bordes de maximizar, minimizar y cerrar de windows
            WindowState = FormWindowState.Maximized;//Abrimos la form en modo pantalla completa
            TopMost = true;
            this.controller = new Controller();//Instanciamos un nuevo controlador
            this.listener = new LeapEventListener(this);//Instanciamos un nuevo objeto de escucha
            controller.AddListener(listener);//Asignamos la escucha al controlador

        }

        delegate void LeapEventDelegate(string EventName);//Eventos del Leap Motion
        public void LeapEventNotification(string EventName)//Función de notificación y casos de uso del Leap Motion
        {
            if (!this.InvokeRequired)//En caso de que se cumpla

            {
                switch (EventName)//Switch de casos de eventos
                {
                    case "onInt"://Caso OnINT

                        break;//Salida
                    case "onConnect"://Caso onConnect

                        connectHandler();//Llamada a función connectHandler

                        break;//Salida
                    case "onFrame"://Caso onFrame

                        ;
                        detectGesture(this.controller.Frame());//Llamada a función de detección de gesto
                      
                        detectFingers(this.controller.Frame());//Llamada a función de detección de dedos de la mano


                        break;//Salida
                }
            }
            else//En caso contrario de no cumplir la condición
            {
                BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });//Inicia el invoke
            }
        }

        public void connectHandler()//Función publica coonectHandler
        {
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);//Hablita en el controlador el gesto de movimiento en circulo en el plano
            this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);//Habilita al controlador el movimiento de tap sobre el dispotitivo
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);//Hablitita al controlador el movimiento de gesto Swipe
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);//Habilita por último al controlador el gesto de screen tap
        }

        public void detectGesture(Leap.Frame frame)//Función publica de detección de gesto con los frames recibidos del Leap Motion
        {
            GestureList gestures = frame.Gestures();//Intanciación del objeto gestos con la llamada de frames
            for (int i = 0; i < gestures.Count(); i++) //Bucle for para recorrer la cantidad de gestos recibidos
            {
                Gesture gesture = gestures[i];//Aignación de las posiciones del array segun se recorre el array de  gestos
                switch (gesture.Type)//Switch de caos del tipo de gesto 
                {

                    case Gesture.GestureType.TYPE_CIRCLE://Si se recibe el gesto de circulo
                        richTextBox1.AppendText("Circle detected!" + Environment.NewLine);//Se muestra el texto por pantalla y se pasa a la siguiente linea del cuadro de muestras de texto por pantalla
                        break;//Salida
                    case Gesture.GestureType.TYPE_SCREEN_TAP://Si se recibe el gesto de screen tap
                        richTextBox1.AppendText("Screen tap!" + Environment.NewLine);//Se muestra el texto por pantalla y se pasa a la siguiente linea del cuadro de muestras de texto por pantalla
                        break;//Salida
                    case Gesture.GestureType.TYPE_KEY_TAP://Si se recibe el gesto de key tap
                        richTextBox1.AppendText("Key tap!" + Environment.NewLine);//Se muestra el texto por pantalla y se pasa a la siguiente linea del cuadro de muestras de texto por pantalla
                        break;//Salida
                    case Gesture.GestureType.TYPE_SWIPE://Si se recibe el gesto de swipe
                        richTextBox1.AppendText("Swipe detected!" + Environment.NewLine);//Se muestra el texto por pantalla y se pasa a la siguiente linea del cuadro de muestras de texto por pantalla
                        break;//Salida
                }

            }
        }

        public void detectHandPosition(Leap.Frame frame)//Función de detección de la posición de la mano
        {
            HandList allHands = frame.Hands;//Empleamos cualquiera de las dos manos
            foreach (Hand hand in allHands)//Para cada muestra
            {

                //******************************
                //Control vía YAW, PITCH y ROLL
                //******************************

                //Recibimos los parametros de posición en YAW, PITCH y ROLL

                float pitch = hand.Direction.Pitch;//Almacenamos los grados en la dirección de la mano pitch, que serán los grados en la dirección en radianes
                float yaw = hand.Direction.Yaw;//Almacenamos los grados en la dirección de la mano yaw, que serán los grados en la dirección en radianes
                float roll = hand.PalmNormal.Roll;//Almacenamos los grados en la dirección de la mano roll, que serán los grados en la dirección en radianes

                //Convertimos los grados de radianes a grados sexagesimales

                double degPitch = pitch * (180 / Math.PI);//Convertimos los grados de radianes a grados sexagesimales
                double degYaw = yaw * (180 / Math.PI);//Convertimos los grados de radianes a grados sexagesimales
                double degRoll = roll * (180 / Math.PI);//Convertimos los grados de radianes a grados sexagesimales

                //Convertimos los grados de tipo double a entero

                int intPitch = (int)degPitch;//Convertimos los grados de tipo double a entero
                int intYaw = (int)degYaw;//Convertimos los grados de tipo double a entero
                int intRoll = (int)degRoll;//Convertimos los grados de tipo double a entero

                float grab = hand.GrabStrength;//Almacenamos la fuerza de la mano que equivale a valores de 1 con mano cerrada a 0 con mano abierta 

                txtPitch.Text = intPitch.ToString();//Se convierte a tipo String y muestra por pantalla en el cuadro de texto
                txtYaw.Text = intYaw.ToString();//Se convierte a tipo String y muestra por pantalla en el cuadro de texto
                txtRoll.Text = intRoll.ToString();//Se convierte a tipo String y muestra por pantalla en el cuadro de texto
                txtGrab.Text = grab.ToString();//Se convierte a tipo String y muestra por pantalla en el cuadro de texto

            }
        }

        public void detectFingers(Leap.Frame frame)//Funcion publica de deteccion de dedos
        {
            foreach (Finger finger in frame.Fingers)//Para cada dedo
            {
                richTextBox2.AppendText("Finger ID: " + finger.Id + Environment.NewLine +//Mostrar en nueva linea el id
                                        "Finger Type: " + finger.Type + Environment.NewLine +//Mostrar en nueva linea el tipo
                                        "Finger Length: " + finger.Length + Environment.NewLine +//Mostrar en nueva linea longitud
                                        "Finger Width: " + finger.Width + Environment.NewLine);//Mostrar en nueva linea anchura

                foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))//Para cada hueso
                {
                    Bone bone = finger.Bone(boneType);//Asignar dedos
                    richTextBox3.AppendText("Bone Type: " + bone.Type + Environment.NewLine +//Mostar en cada nueva linea el tipo
                                            "Bone Length: " + bone.Length + Environment.NewLine +//Mostar en cada nueva linea la longitud
                                            "Bone Width: " + bone.Width + Environment.NewLine +//Mostar en cada nueva linea la anchura
                                            "Previous Joint: " + bone.PrevJoint + Environment.NewLine +//Mostar en cada nueva linea la union previa
                                            "Next Joint: " + bone.NextJoint + Environment.NewLine +//Mostar en cada nueva linea la siguiente union
                                            "Direction: " + bone.Direction + Environment.NewLine);//Mostar en cada nueva linea la direccion

                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            controller.Dispose();//Llamada a la funcion dispose del controlador
        }
        private void label2_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();//Inicio del reloj
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void label8_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;//Quitar pantalla completa
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Indice - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button4_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Fase 1 - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }
        [DllImport("user32.dll")]//Dll de software del sistema
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        private void button6_Click(object sender, EventArgs e)
        {
            Process p = Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//herramienta de diagostico
            Thread.Sleep(500);//Espera
            p.StartInfo.CreateNoWindow = true;//Embebida
            SetParent(p.MainWindowHandle, this.Handle);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();//Cerrar app
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToShortTimeString();//Mostrar fecha
            label1.Text = DateTime.Now.ToString("dd/MM/yyyy");//Formato
            if (i == 0)//Un acceso
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciar sintesis

                //Dispositivo de audio predeterminado
                synth.SetOutputToDefaultAudioDevice();

                //Sintesis de texto a voz

                synth.Speak("El módulo de optimización y mejora de resultados, para obtener las posiciones del  analisis de la mano, ha sido iniciado");
                synth.Speak("Por favor, situe las manos sobre el dispositivo leap motion para obtener y mostrar los resultados del análisis");
                i++;//Restringir el acceso
            }
        }

        private void button4_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Inicio\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button10_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Analisis de informacion gestual\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void button6_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            Process p = Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//herramienta de diagnostico
            Thread.Sleep(500);//Espera
            p.StartInfo.CreateNoWindow = true;//Embebida
            SetParent(p.MainWindowHandle, this.Handle);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Application.Exit();//Cerrar app
        }

        private void button15_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Informacion del proyecto\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button14_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Ejemplos de uso\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button8_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Conversion\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button11_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Control de robot via modulo\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button12_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Simulacion de control de robot\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button13_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Control de robot prototipo\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }
    }


    //*************************************************
    //Funciones publicas del dispositivo Leap Motion
    //*************************************************

    public interface ILeapEventDelegate//Llamada a la funcion
    {
        void LeapEventNotification(string EventName);//Llamada a la funcion
    }
    public class LeapEventListener : Listener//Clase publica de evento de escucha
    {
        ILeapEventDelegate eventDelegate;
        public LeapEventListener(ILeapEventDelegate delegateObjet)//Llamada a la funcion
        {
            this.eventDelegate = delegateObjet;
        }
        public override void OnInit(Controller controller)//Llamada a la funcion
        {
            this.eventDelegate.LeapEventNotification("onInit");//Notificacion de evento del dispositivo Leap Motion onInt

        }
        public override void OnConnect(Controller controller)//Llamada a la funcion
        {
            this.eventDelegate.LeapEventNotification("onConnect");//Notificacion de evento del dispositivo Leap Motion onConnect
        }
        public override void OnFrame(Controller controller)//Llamada a la funcion
        {
            this.eventDelegate.LeapEventNotification("onFrame");//Notificacion de evento del dispositivo Leap Motion onFrame
        }
        public override void OnExit(Controller controller)//Llamada a la funcion
        {
            this.eventDelegate.LeapEventNotification("onExit");//Notificacion de evento del dispositivo Leap Motion onExit
        }
        public override void OnDisconnect(Controller controller)//Llamada a la funcion
        {
            this.eventDelegate.LeapEventNotification("onDisconnect");//Notificacion de evento del dispositivo Leap Motion onDisconect
        }
    }
}



