using System;//Uso de las librerias del sistema
using System.Collections.Generic;//Uso de las librerias del sistema
using System.ComponentModel;//Uso de las librerias del sistema
using System.Data;//Uso de las librerias del sistema
using System.Drawing;//Uso de las librerias del sistema
using System.Linq;//Uso de las librerias del sistema
using System.Text;//Uso de las librerias del sistema
using System.Threading.Tasks;//Uso de las librerias del sistema
using System.Windows.Forms;//Uso de las librerias del sistema para windows forms
using Leap;//Uso de las librerias del dispositivo Leap Motion
using System.Diagnostics;//Uso de las librerias del sistema de diagnostico
using System.Runtime.InteropServices;//Uso de las librerias del sistema
using System.Speech.Synthesis;//Uso de las librerias del sistema para sinteis de texto a voz
using System.Threading;//Uso de las librerias del sistema para esperar y progresos
using AForge.Video;//Uso de las librerias de AFORGE para uso y captura de video e imagen
using AForge.Video.DirectShow;//Uso de las librerias de AFORGE para captura de video en directo
using System.Speech.Recognition;//Uso de las librerias del sistema para reconocimiento de voz

/* 

 Controlador/Aplicación/Recepción/Conversión/Envío para Robot de 6 grados de libertad:

 ROBOT CONTROL VÍA COORDENADAS CARTESIANAS CON INCLINACIÓN DE LA MANO VERTICAL Y HORIZONTAL, GARRA Y CONTROL POR VOZ, ASÍ COMO NOTIFICACIONES POR VOZ

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
    public partial class Form1 : Form, ILeapEventDelegate//Uso de la clase publica de eventos del Leap Motion
    {
        //Instanciación de un nuevo objeto de reconocimiento de voz
        
        SpeechRecognitionEngine reconocedor = new SpeechRecognitionEngine();
        
        private Controller controller;//Uso de la clase privada controlador
        private LeapEventListener listener;//Uso de la clase privada escucha del dispositivo Leap Motion
        public Form1()//Form 1 métodos publicos
        {
            //Inicialización de la form
            InitializeComponent();//Inicialización de la form
            //Inicialización de la pantalla
            FormBorderStyle = FormBorderStyle.None;//Desactivar bordes de la app, es decir los bordes de maximizar, minimizar y cerrar de windows
            WindowState = FormWindowState.Maximized;//Abrimos la form en modo pantalla completa
            TopMost = true;
            //Inicialización del Leap Motion
            this.controller = new Controller();//Instanciamos un nuevo controlador
            this.listener = new LeapEventListener(this);//Instanciamos un nuevo objeto de escucha
            controller.AddListener(listener);//Asignamos la escucha al controlador

        }
        //Inicialización de la capturadora de video para monitorización
        private FilterInfoCollection Dispositivos;//Instanciación de dispositivos de la red para gestion de cámaras
        private VideoCaptureDevice FuenteDeVideo;//Instanciación de la fuente de video
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

                        detectGesture(this.controller.Frame());//Llamada a función de detección de gesto
                        detectHandPosition(this.controller.Frame());//Llamada a función de detección de posición de la mano

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

        int gradosx;//Variable global para almacenar los grados en eje X con uso de coordenadas YAW, PITCH y ROLL
        int gradosy;//Variable global para almacenar los grados en eje Y con uso de coordenadas YAW, PITCH y ROLL
        int gradosz;//Variable global para almacenar los grados en eje Z con uso de coordenadas YAW, PITCH y ROLL
        int garra;//Variable gloabl para almacenar los grados de la garra
        int coorx;//Variabale gloabl para almacenar los grados del eje X en coordenadas de uso cartesianas
        int coory;//Variabale gloabl para almacenar los grados del eje Y en coordenadas de uso cartesianas
        int coorz;//Variabale gloabl para almacenar los grados del eje Z en coordenadas de uso cartesianas
        int inclivert;//Variable gloabl para almacenar los grados de la inclinación de la mano vertical
        int inclihor;//Variable gloabl para almacenar los grados de la inclinación de la mano horizontal

        string cartesianas;//String para almacenar el texto a enviar vía pueto serie
        string com4 = "COM5";//String para almacenar el pueto serie por l cual enviar los parametros
        public void detectHandPosition(Leap.Frame frame)//Función de detección de la posición de la mano
        {
            HandList allHands = frame.Hands;//Empleamos cualquiera de las dos manos
            foreach (Hand hand in allHands)//Para cada muestra
            {
                
                int i = 0;//Variable de control
                int intPitch2 = 21;//Variables de inicio en incremental
                int intYaw2 = -10;//Variables de inicio en incremental
                int intRoll2 = -20;//Variables de inicio en incremental

                int garraconversion;//Conversion de la garra

                string x="0";//Puesta a cero en control incremental
                string y="0";//Puesta a cero en control incremental
                string z="0";//Puesta a cero en control incremental

                int espera = 100;//Tiempo de espera

                

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

                
                txtGrab.Text = grab.ToString();//Se convierte a tipo String y muestra por pantalla en el cuadro de texto

                //Se han desactivado los YAW, PITCH y ROLL, al haber sido sustituidos por coordenadas cartesianas que permiten un resultado más preciso, dado que con YAW, PITCH y ROLL, la interrelación entre ello dificultaba el control
               
                //*******************************************

                grab = 10 * grab;//Multiplicamos por 10, para despues convertir a entero y mandar String de entreo por puerto serie

               if (grab == 10)//Si la pinza es 10, es decir esta cerrada
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciamos objeto de sintetizador de taxto a voz 

               
                    synth.SetOutputToDefaultAudioDevice();//Seleccionamos el dispositivo de audio por defecto del ordenador
                    synth.Speak("La pinza ha sido cerrada");//Decimos por el altavoz el texto mostrado
                }

                garraconversion = (int)grab;//Volcamos el valor del grab multiplicado por 10 como entero en la variable

             
                //************************************
                //Interpolaciones lineales en el caso de yaw, pitch y roll
                //************************************
                gradosx = (40 - (10 * intRoll));//Realizamos la ecuación de interpolación lineal del roll a gradosx
                gradosy = (30 + (1 * intPitch));//Realizamos la ecuación de interpolación lineal del pitch a gradosy
                gradosz = (108 + (2 * intYaw));//Realizamos la ecuación de interpolación lineal del yaw a gradosz




                //******************************
                //Control vía coordenadas cartesianas
                //******************************

                //Recibimos los parametros de posición coordenadas cartesianas
                
                
                float a = hand.PalmPosition.z;//Almacenamos la posición de la mano en la coordenada Z
                float b = hand.PalmPosition.x;//Almacenamos la posición de la mano en la coordenada X
                float c = hand.PalmPosition.y;//Almacenamos la posición de la mano en la coordenada Y

                //Convertimos los float a enteros

                int aa = (int)a;//Convertimos el valor a entero y es vocado en un entero
                int bb = (int)b;//Convertimos el valor a entero y es vocado en un entero
                int cc = (int)c;//Convertimos el valor a entero y es vocado en un entero

                //Recibimos el resto de parametros para las inclinaciones de la mano vertical y horizontal

                float d = hand.WristPosition.y; //Recibimos posición de la muñeca en el eje Y
                float dife = c - d;//Restamos la posición del centro de la mano en el eje y a la posicion de la muñeca en el eje y para obtener la inclinación arriba-abajo
                float e = roll - pitch;//Restamos los grados en sexagesimal de la posicion de la mano roll y picth para obtener la orientación de inclinación a derecha e izquierda

                //Mostramos los resultados por pantalla en cuadros de texto

                textBox1.Text = dife.ToString();//Mostramos en el textbox 1 la inclinación vertical

                textBox2.Text = e.ToString();//Mostramos en el textbox 2 la inclinación horizontal

                                   
                txtPitch.Text = aa.ToString();//Mostramos la posición en cartesianas del eje Z
                txtYaw.Text = cc.ToString();//Mostramos la posición en cartesianas del eje Y
                txtRoll.Text = bb.ToString();//Mostramos la posición en cartesianas del eje X

                txtGrab.Text = grab.ToString();//Mostramos la situacion de la mano

                //*********************************
                //Conversión de las inclinaciones
                //*********************************

                int convver = (int)dife;//Convertimos a entero y volcamos
                int convver2 = (int)e;//Convertimos a entero y volcamos


                //********************************************************************
                //Calculos de las interpolaciones lineales en coordenadas cartesianas
                //********************************************************************

                coorx =((((1)*(bb))/5));//Interpolacion de coordenadas del eje X
                coory= (((-41)/5) + (((137) * (cc)) / 1000));//Interpolacion de coordenadas del eje Y
                coorz = ((240 / 10) - (((27) * (aa)) / 200));//Interpolacion de coordenadas del eje Z

            
                //*****************************************************************************************************


                //*****************************************************************************************************
                //Notificaciones de control de posiciones críticas del robot
                //*****************************************************************************************************


                //Cuando se cumplan alguna de las condiciones adjuntas de que alguno de los motores solicita una posicion superior a un limite
                //Se notificará por audio una alarma de un suceso, notificando el angulo en cuestión aunque el servomor tenga restringida tal posicion

                  if (coorx < (-40))
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                    //Se muestra por audio la sintesis del texto de notificacion preprogramado 
                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("El robot, se encuentra en una posición del eje x sentido antihorario, próximo a su limite");
                }
                if (coorx >40)
                {
                    //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                    //Se muestra por audio la sintesis del texto de notificacion preprogramado
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                     
                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak("El robot, se encuentra en una posición del eje x sentido horario, próximo a su limite");
                }

                  if (coory < (0))
                   {
                    //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                    //Se muestra por audio la sintesis del texto de notificacion preprogramado
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                       
                       synth.SetOutputToDefaultAudioDevice();
                       synth.Speak("El robot, se encuentra en una posición del eje y sentido adelantado, próximo a su limite");
                   }
                   if (coory > 60)
                   {

                    //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                    //Se muestra por audio la sintesis del texto de notificacion preprogramado
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                       
                       synth.SetOutputToDefaultAudioDevice();
                       synth.Speak("El robot, se encuentra en una posición del eje y sentido atras, próximo a su limite");
                   }

                  if (coorz < (0))
                  {
                    //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                    //Se muestra por audio la sintesis del texto de notificacion preprogramado
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                      
                      synth.SetOutputToDefaultAudioDevice();
                      synth.Speak("El robot, se encuentra en una posición del eje y sentido adelantado, próximo a su limite");
                  }
                  if (coorz > 50)
                  {
                      //Se instancia un nuevo objeto de sintesis de voz, selecciona el dispositivo de audio por defecto
                      //Se muestra por audio la sintesis del texto de notificacion preprogramado

                      SpeechSynthesizer synth = new SpeechSynthesizer();

                      
                      synth.SetOutputToDefaultAudioDevice();
                      synth.Speak("El robot, se encuentra en una posición del eje z sentido atras, próximo a su limite");
                  }
            
                //************************************************************************************************

                //************************************************************************************************
                //Se almacena concatenando los distintos parametros con el separados ',' en un string para enviar
                //************************************************************************************************

                //**************************
                //Caso de YAW, PITCH y ROLL
                //**************************

                // cartesianas = gradosx + "," + gradosy + "," + gradosz + "," + garra;



                //*******************************************
                //Caso coordenadas cartesianas
                //*******************************************


                cartesianas = coorx + "," + coory + "," + coorz + "," + garra;


                //********************************************

                string prueba ="0"+ gradosx;
                Thread.Sleep(espera);//Realizamos una espera del valor preconfigurado, para esperar al procesamiento del envio previo
                //Escribimos por el puerto serie, de la variable com4, que en verdad es el COM5, a una velocidad de 9600 baudios sin paridad
                System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                try
                {
                    sport.Open();//Abrimos el puerto
                    sport.Write(cartesianas);//Enviamos el valor de la variable que almacena los parametros concatenados
                }
                catch { Exception ex; }
                sport.Close();//Cerramos el puerto de envio
               

                //*************************************
                //Fin del envio
                //*************************************


                //Este apartado es relativo al control telemático vía incremental, que a su vez, se encuentra procesando sin enviar en caso de que se quiera modificar el codigo fuente 
                //y asi pasar de un modo a otro rapidamente en la misma windows form

                //*************************************
                //Control telemático incremental
                //*************************************

                if (i == 0)//Si se cumple la condición de analisis
                {
                    
                    if (intPitch>intPitch2) {//Comparamos con el valor previo para ver si es superior o inferior al mismo
                        y = "6";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }

                    if (intPitch < intPitch2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        y = "4";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }
                    if (intYaw > intYaw2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        x = "3";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }

                    if (intYaw < intYaw2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        x = "1";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }
                    if (intRoll > intRoll2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        z = "9";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }

                    if (intRoll < intRoll2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        z = "7";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }
                    if (intPitch == intPitch2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        y = "5";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }
                    if (intYaw == intYaw2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        x = "2";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }
                    if (intRoll == intRoll2)//Comparamos con el valor previo para ver si es superior o inferior al mismo
                    {
                        z = "8";//Dependiendo de si es superior o inferior se escribe un valor codificado referente a incrementar o decrementar ese angulo
                    }


                    string coordenadasarduino=x+","+y+","+z;//Volcamos el valor concatenando sobre la variable, con los separadores ','

                    //*******************************************************************************************************************************
                    //Envio vía escritura de fichero, se genera un fichero en la ruta y se vuelca el valor, sobrescribiendo lo existente a cada ciclo
                    //********************************************************************************************************************************

                   
                    System.IO.File.WriteAllText(@"C:\cartesianas.txt", cartesianas);

                    i = 1;//Puesta a cero del envio

                  
                }
                if (i == 1)//Copiamos los ultimos valores a las variables de comparación
                {
                    intPitch2 = intPitch;//Copiamos los ultimos valores a las variables de comparación
                    intYaw2 = intYaw;//Copiamos los ultimos valores a las variables de comparación
                    intRoll2 = intRoll;//Copiamos los ultimos valores a las variables de comparación
                    i =0;
                }
              
               
                
                //************************************
            }
        }

        private void label2_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
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

        private void button5_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Fase 2 - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();
            //Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        [DllImport("user32.dll")]//Uso de las librerias de las dll de windows para ejecución de programas de forma embebida, que han sido instalados en el ordenador
        static extern IntPtr SetParent(IntPtr hwc, IntPtr hwp);
        private void button6_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            Process p = Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//Abrimos el visualizador 3D de diagnostico del Leap Motion
            Thread.Sleep(500);//Tiempo de espera
            p.StartInfo.CreateNoWindow = true;//Apertura de forma embebida dentro de la form
            SetParent(p.MainWindowHandle, this.Handle);//Capacidad de mover dentro de la form
        }

        private void button7_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            Application.Exit();//Salir de la app
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            FormBorderStyle = FormBorderStyle.None;//Al ejecutar la tecla escape finaliza el modo pantalla completa pasando a modo ventana
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Indice - copia\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)//Temprizador de la aplicación
        {
            label8.Text = DateTime.Now.ToShortTimeString();//Mostrar la fecha y hora en al etiqueta
            label30.Text = DateTime.Now.ToString("dd/MM/yyyy");//Uso del formato
            if (i == 0)//Condición para ejecutar solo al inicio una vez
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciación de objeto de sistesis de voz

               
                synth.SetOutputToDefaultAudioDevice();//Selecionar dispositivo predeterminado del ordenador

                //Sintesis de texto a voz, con instruciones de uso

                synth.Speak("El módulo de control de robot vía coordenadas cartesianas ha sido iniciado correctamente");
                synth.Speak("Situe las manos sobre el dispositivo leap motion y comience a realizar el control de robot por medios gestuales");
                synth.Speak("Si desea realizar un control de cámaras, por favor seleccione la cámara deseada del listado, y seleccione iniciar, si desea detener la transmision de imagen, seleccione detener.");
                synth.Speak("Si desea realizar un control por voz, por favor, seleccione iniciar bajo la opción control por voz, las órdenes configuradas son");
                synth.Speak("derecha,izquierda,arriba,abajo,atrás,adelante,abrir y cerrar");
                i++;//Bloqueo de volver a ejecutar
            }
        }

        private void button7_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            Application.Exit();//Salir de la app
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Start();
            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);//Asignamos en dispositivos las camaras encontradas en el PC
            foreach (FilterInfo x in Dispositivos)//Para cada uno encontrado
            {
                comboBox1.Items.Add(x.Name);//Añadimos el listado de dispositivos de video encontradis
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Inicio\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Función privada de ejecución de un elemento gráfico de la app
        }

        private void button5_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Optimizacion y mejora de la precision\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();//Abrimos la windows form de la ruta seleccionada y cerramos la actual
        }

        private void button6_Click_1(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            Process p = Process.Start(@"C:\Program Files (x86)\Leap Motion\Core Services\VisualizerApp.exe");//Ejecutamos la herramienta de diagnostico
            Thread.Sleep(500);//Tiempo de espera
            p.StartInfo.CreateNoWindow = true;//Manejo de forma embebida
            SetParent(p.MainWindowHandle, this.Handle);
        }

        private void button15_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            System.Diagnostics.Process.Start(@"C:\Users\david\Documents\Visual Studio 2015\Projects\Informacion del proyecto\WindowsFormsApplication1\WindowsFormsApplication1\bin\Debug\WindowsFormsApplication1.exe");
            Application.Exit();
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

        private void pictureBox3_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {

        }

        private void button19_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            FuenteDeVideo = new VideoCaptureDevice(Dispositivos[comboBox1.SelectedIndex].MonikerString);//Asignamos el selecionado del listado
            videoSourcePlayer1.VideoSource = FuenteDeVideo;//Asignamos la fuente la imagen 
            videoSourcePlayer1.Start();//Iniciamos la muestra del video de camara
        }

        private void button18_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            videoSourcePlayer1.SignalToStop();//Detenemos la muestra de video de la camara
        }

        //***************************************
        //Control de robot vía control por voz
        //***************************************


        string reconoce;//Variable para almacenar el término reconocido
        int j = 0;//Variable de control
        private void button20_Click(object sender, EventArgs e)//Función privada de ejecución de un elemento gráfico de la app
        {
            if (j == 0)//Control de arraque una sola vez, para evitar errores
            {

                reconocedor.LoadGrammar(new DictationGrammar()); //Carga todas la gramaticas de windows
                reconocedor.SetInputToDefaultAudioDevice(); //El programa usara el microfono predeterminado por el sistema
                reconocedor.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(lector);//Asigna
                reconocedor.RecognizeAsync(RecognizeMode.Multiple);//Reconocimento múltiple de palabras
                j++;//Bloqueo de no volver a arrancar 

                SpeechSynthesizer synth = new SpeechSynthesizer();//Instanciación de sistesis de voz

                
                synth.SetOutputToDefaultAudioDevice();//Dsipositivo de sonido predeterminado
                //Mensaje de aviso sistetizado
                synth.Speak("El módulo de control por voz, ha sido correctamente iniciado");
            }
        }

        
        public void lector(object sender, SpeechRecognizedEventArgs e)//Función de reconocimento de palabras
        {
            foreach (RecognizedWordUnit palabra in e.Result.Words)//Analisis para cada palabra recibida
            {
                reconoce = palabra.Text;//Almacenamos el termino recibido en la variable
                label31.Text = palabra.Text;//Mostramos la orden recibida por pantalla

                //**************************************
                //Casos de reconocimiento
                //**************************************

                //Compara el valor recibido en la variable con las ordenes programadas
                
                if (reconoce == "derecha")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                     
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Reduce 20 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    coorx = coorx + 10;
                    if (coorx > (40))
                    {
                        coorx = (40);
                    }

                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                    synth.Speak("El robot se desplazará a la derecha");
                }
                if (reconoce == "izquierda")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Incrementa 20 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot se desplazará a la izquierda");
                    coorx = coorx - 10;
                    if (coorx < (-40))
                    {
                        coorx = -40;
                    }
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                }
                if (reconoce == "atrás")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Reduce 20 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot se desplazará hacia atrás");
                    coorz = coorz - 10;
                    if (coorz < 0)
                    {
                        coorz = 0;
                    }
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                }
                if (reconoce == "adelante")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Incrementa 20 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot se desplazará hacia adelante");
                    coorz = coorz + 10;
                    if (coorz > 50)
                    {
                        coorz = 50;
                    }
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                }

                if (reconoce == "arriba")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Incrementa 20 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot se desplazará hacia arriba");
                    coory = coory + 10;
                    if (coory > 60)
                    {
                        coory = 60;
                    }
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                }
                if (reconoce == "abajo")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Reduce 10 grados a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje
                     
                    synth.SetOutputToDefaultAudioDevice();

                    

                    synth.Speak("El robot se desplazará hacia abajo");
                    coory = coory - 10;
                    if (coory < 0)
                    {
                        coory = 0;
                    }
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();

                }
                if (reconoce == "cerrar" ||reconoce == "cierra")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                   
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Cierra la pinza a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot cerrará la pinza");
                    //garra = ?;
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();

                }
                if (reconoce == "abrir")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                     
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Abre la pinza a la posicion del servomotor en cuestion
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    synth.Speak("El robot abrirá la pinza");
                    //garra = ?;
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();

                }
                if (reconoce == "reinicio" || reconoce == "reinició")
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    
                    synth.SetOutputToDefaultAudioDevice();

                    //Si reconoce el termino seleccionado, instancia un obejto de sintesis de texto, selecciona el altavoz, por defecto
                    //Ajusta los grasos de los servomotores a las posiciones de equilibrio
                    //En caso de superar la posicion limite lo reajusta a su limite en ese sentido y eje

                    coorx = 0;
                    coory = 40;
                    coorz = 0;
                    //garra = ?;
                    //inclivert = 70;
                    //inclihor = 60;
                    cartesianas = coorx + "," + coory + "," + coorz + "," + garra;

                    //Vuelca los valores concatenados en el string
                    //Tras esto, instancia un objeto de comunicacion vía puerto serie por el puerto COM5, de la variable com4, a una velocidad de 9600 baudios
                    //Sin paridad
                    //Abre el puerto, y escribe el valor de las variables concatenadas
                    //Tras esto, cierra el puerto y sintetiza a voz, el texto mostrado

                    System.IO.Ports.SerialPort sport = new System.IO.Ports.SerialPort(com4, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    try
                    {
                        sport.Open();
                        sport.Write(cartesianas);//cartesianas
                    }
                    catch { Exception ex; }
                    sport.Close();
                    synth.Speak("El robot se desplazará a la posición de equilibrio");
                }

                //Tras el ciclo, limpia el buffer y muestra que no se recibe orden, de forma vacio

                reconoce = "vacio";
             
            }
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



