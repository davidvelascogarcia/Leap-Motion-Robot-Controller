/* 

 Controlador Arduino para Robot de 6 grados de libertad:
 ROBOT PROTOTIPO

 TFG - TRABAJO DE FIN DE GRADO
 MODULO DE RECONOCIMENTO GESTUAL PARA EL CONTROL DE ROBOT EN TAREAS DE ASISTENCIA

 ALUMNO : DAVID VELASCO GARCIA

 GRADO EN INGENIERIA EN TECNOLOGIAS INDUSTRIALES
 INTENSIFICACION EN ELECTRONICA INDUSTRIAL Y AUTOMATICA

 UNIVERSISDAD CARLOS III DE MADRID
 ESCUELA POLITECNICA SUPERIOR

*/

#include <Servo.h>//Libreria de control de servores
#include <stdio.h>//Libreria de C
#include <stdlib.h>//Libreria de C, para manejo de ficheros
#include <SD.h>//Libreria de control de tarjetas SD para futuras actualizaciones
#include <string.h>//Liberia de uso de variables de tipo String
#include <LiquidCrystal.h>//Librerias de pantalla LCD 16x02

//Instanciacion de objetos servomotor

Servo miServo1;//Instanciacion de objeto de tipo Servo
Servo miServo2;//Instanciacion de objeto de tipo Servo
Servo miServo3;//Instanciacion de objeto de tipo Servo
Servo miServo4;//Instanciacion de objeto de tipo Servo
Servo miServo5;//Instanciacion de objeto de tipo Servo
Servo miServo6;//Instanciacion de objeto de tipo Servo

//Instanciacion de objeto pantalla LCD

LiquidCrystal lcd(13, 12, 11, 10, 9, 8);//Indicamos los pines PWM de conexiÃ³n

//Variables de control de los servomotores inicializados al equilibrio

int angulo1=90;//Angulo inicial de reposo en equilibrio del servomotor 1
int angulo2=40;//Angulo inicial de reposo en equilibrio del servomotor 2
int angulo3=70;//Angulo inicial de reposo en equilibrio del servomotor 3
int angulo4=70;//Angulo inicial de reposo en equilibrio del servomotor 4
int angulo5=60;//Angulo inicial de reposo en equilibrio del servomotor 5
int angulo6=90;//Angulo inicial de reposo en equilibrio del servomotor 6

//Variables de control de bucles y volcado de informacion

String ejex;//Variable de posicion recibida del eje x
String ejey;//Variable de posicion recibida del eje y
String ejez;//Variable de posicion recibida del eje x
String pinza;//Variable de posicion recibida de la pinza
String total;//Variable de posicion recibida total recopilada
String verti;//Variable de posicion recibida de la inclinacion vertical de la mano
String hori;//Variable de posicion recibida de la inclinacion horizontal de la mano
int control2=0;//Variable de control de salto de valor recibido para cada servo
char conv[3];

//Funcion Setup
void setup(){//Setup del Hardware

  //Asignamos a cada servomotor el pin de la placa al que estan conectados
  
  miServo1.attach(2);//Asiganmos el pin 2 al servo 1
  miServo2.attach(3);//Asignamos el pin 3 al servo 2
  miServo3.attach(4);//Asignamos el pin 4 al servo 3
  miServo4.attach(5);//Asignamos el pin 5 al servo 4
  miServo5.attach(6);//Asignamos el pin 6 al servo 5
  miServo6.attach(7);//Asignamos el pin 7 al servo 6

//Inicializamos el objeto pantalla LCD

lcd.begin(16, 2);
//lcd.clear();

//Inicializar el objeto superior de texto

  lcd.setCursor(0,0);//Cursor fila superior
  lcd.print("Estado:");//Mensaje superior
 // lcd.setCursor(0,2);//Cursor fila inferior

//Ponemos a cero el buffer recibido de cada posicion de cada servo
  
ejex="";//Reseteo del buffer del eje x
ejey="";//Reseteo del buffer del eje y
ejez="";//Reseteo del buffer del eje z
pinza="";//Reseteo del buffer de la pinza
verti="";//Reseteo del buffer de la inclinacion de la mano vertical
hori="";//Reseteo del buffer de la inclinacion horizontal

//Iniciamos el puerto serie a una velocidad de transferencia de 9600 baudios
  Serial.begin(9600);

}//Fin del Hardware Setup


//Funcion ciclica

void loop(){//Ejecucion de la funcion en bucle infinito
  
    int cadena[12];
    char xx;//Variable de uso de buffer para el caracter recibido
    int i=0;//Variable contador
    
    if(Serial.available()>0){//Si se recibe informacion por el pueto serie, o en su extension seguimos leyendo dado que se lee de caracter en caracter
    //lcd.clear();
    //lcd.write("Recibiendo...");//Mensaje inferior
     
     xx= Serial.read();//Almacenamos el caracter recibido en la variable de tipo caracter
    
    i++;//Incrementamos el contador
    total.concat(xx);//Concatenamos el caracter recibido al String que almacena todo el ciclo recibido
 
    
    if((control2==0)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 0, asignamos el caracter al servo 1, concatenando el caracter recibido
      ejex.concat(xx);//Concatenamos el caracter
  
    }
    if((control2==1)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 1, asignamos el caracter al servo 2, concatenando el caracter recibido
      ejey.concat(xx);//Concatenamos el caracter
   
    }
    if((control2==2)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 2, asignamos el caracter al servo 3, concatenando el caracter recibido
      ejez.concat(xx);//Concatenamos el caracter recibido
    
    }
    if((control2==3)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 3, asignamos el caracter al servo 4, concatenando el caracter recibido
      pinza.concat(xx);//Concatenamos el caracter recibido
    
    }
     if((control2==4)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 4, asignamos el caracter al servo 5, concatenando el caracter recibido
      verti.concat(xx);//Concatenamos el caracter recibido
    
    }
     if((control2==5)&&(xx!=',')){//Si el caracter recibido es distinto de ',', que es el separador de valor de un servomotor a otro y control2  es 5, asignamos el caracter al servo 6, concatenando el caracter recibido
      hori.concat(xx);//Concatenamos el caracter recibido
    
    }
    if(xx==','){//Cuando el caracter recibido es ',' que es el separador, pasamos a almacenar el caracter en el concatenado del siguiente servomotor
      control2=control2+1;//Incrementamos el valor
      
    }
    
     
    }
    else{//Si no se reciben datos o se ha terminado de leer y operar con la cadena recibida
    
  //lcd.print("Desplazando...");//Mensaje inferior
  miServo1.write(angulo1); //Escribimos en el servomor 1, el angulo asignado

 if(ejex.toInt()!=0){//Comprobamos que el valor del string convertido a entero del primer string por el cual se pasa al recibir informacion es distinto de 0, y por lo tanto hemos recibido nueva informacion

  //Volcamos el valor convertido a entero de los buffer en los angulos
  
  angulo1=ejex.toInt();//Convertimos el valor del String concatenado a su angulo asignado
  angulo2=ejey.toInt();//Convertimos el valor del String concatenado a su angulo asignado
  angulo3=ejez.toInt();//Convertimos el valor del String concatenado a su angulo asignado
  angulo6=pinza.toInt();//Convertimos el valor del String concatenado a su angulo asignado
  angulo4=verti.toInt();//Convertimos el valor del String concatenado a su angulo asignado
  angulo5=hori.toInt();}//Convertimos el valor del String concatenado a su angulo asignado
if(angulo5<30){
  angulo5=20;
}else{
  angulo5=60;
}
  //Restringimos los grados de funcionamiento de los servomotores por correcto funcionamiento

angulo1=constrain(angulo1,30,150);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor
angulo2=constrain(angulo2,10,50);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor
angulo3=constrain(angulo3,60,150);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor
angulo4=constrain(angulo4,20,140);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor
angulo5=constrain(angulo5,20,90);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor
angulo4=constrain(angulo4,80,110);//Restringimos los angulos de funcionamiento del servomotor en cuestion por seguridad para un correcto funcionamiento, angulos obtenidos de manera experimental con pruebas de funcionamiento en cada servomotor

//Escribimos los angulos convertidos en los servomotores
 
  miServo1.write(angulo1); //Escribimos en el servomor 1, el angulo asignado
  miServo2.write(angulo2);  //Escribimos en el servomor 2, el angulo asignado

  miServo3.write(angulo3);  //Escribimos en el servomor 3, el angulo asignado

  miServo4.write(angulo4);  //Escribimos en el servomor 4, el angulo asignado

  miServo5.write(angulo5);  //Escribimos en el servomor 5, el angulo asignado

  miServo6.write(angulo6);  //Escribimos en el servomor 6, el angulo asignado

  //Reseteamos las variables de buffer para que se den conlictos con el ciclo posterior

control2=0;//Reseteo de la variable de control de separador
ejex="";//Reseteo del buffer del eje x
ejey="";//Reseteo del buffer del eje y
ejez="";//Reseteo del buffer del eje z
pinza="";//Reseteo del buffer de la pinza
total="";//Reseteo del buffer del string de recepcion total
verti="";//Reseteo del buffer de inclinacion de la mano vertical
hori="";//Reseteo del buffer de inclinacion de la mano horizontal
  
  delay(100);//Retraso de 100 milisegundos entre operacion y operacion de ciclo para evitar solapamientos
    }}//End loop


