# Leap Motion: Robot Controller (C# API)

[![leapmotionRobotController Homepage](https://img.shields.io/badge/leapmotionRobotController-develop-orange.svg)](https://github.com/davidvelascogarcia/leapmotionRobotController/tree/develop/docs) [![Latest Release](https://img.shields.io/github/tag/davidvelascogarcia/tensorflow-cpp-examples.svg?label=Latest%20Release)](https://github.com/davidvelascogarcia/leapmotionRobotController/tags) [![Build Status](https://travis-ci.org/davidvelascogarcia/leapmotionRobotController.svg?branch=develop)](https://travis-ci.org/davidvelascogarcia/leapmotionRobotController)

- [Introducción](#introducción)
- [Listado de programas](#listado-de-programas)
	- [Modos de control](#modos-de-control)
	- [Listado de controladores](#listado-de-controladores)
- [Enlaces de interés](#enlaces-de-interés)

## Introducción

`leapmotionRobotController`, implementación de controlador Leap Motion con publicación de resultados vía `YARP`, control de robot vía `YARP` y control `serial port`. Añadido detección de movimiento, `GUI`, asistente virtual, control por voz. Implementación de módulo en los robot prototipo y robot A.M.O.R (Parque Científico y Tecnológico, Leganés) se realiza la presentación con el uso de ambos robot, operativos en tareas asistenciales, mediante el uso del módulo. Control 6 g.d.l, control telemático e inalámbrico, con incorporación simultanea de control por voz.
Movimiento cartesiano X, Y, Z, así como movimiento rotacional Yaw, Pitch, Roll, junto al control de estado de la pinza en cuestión.

![](https://lh3.googleusercontent.com/dtyw1scpNiuBd87cPYhppHJS0Zyc6I07M88B2-K7aGxYgpquwL9oOyliU_XxuRS0PzHtvo88_Q=w640-h360-p)

Figura 1. Control de robot

```
Universidad Carlos III de Madrid.
Escuela Politécnica Superior.
Grado en Ingeniería en Tecnologías Industriales.
Intensificación en Electrónica Industrial y Automática.
Trabajo de fin de grado.
Módulo de reconocimiento gestual para el control de robot en tareas de asistencia.
```

## Listado de programas

Se ajunta el listo de aplicaciones implementadas en [./programs](./programs).

### Modos de control

Asistencia mediante notificaciones de control e instrucciones de uso.
Modalidades de funcionamiento:

- Control tiempo real cartesiano.
- Control tiempo real velocidades de inclinación.
- Control tiempo real campo de velocidades de inclinación.
- Control por voz.
- Simultanea de control por voz, con un método gestual.

Videos y descargas de aplicaciones y documentación:


- [multimedia: videos](https://www.youtube.com/watch?time_continue=2&v=DU1mztLFsmE)

- [programs: leapmotionRobotController](https://github.com/davidvelascogarcia)

- [docs: tesis](https://www.researchgate.net/publication/319902393_Modulo_de_reconocimiento_gestual_para_control_de_robot_en_tareas_de_asistencia)

### Listado de controladores

Listado de controladores adjuntos:

- Controlador Arduino
- Controlador Windows
- Controlador YARP

## Status

[![Build Status](https://travis-ci.org/davidvelascogarcia/leapmotionRobotController.svg?branch=develop)](https://travis-ci.org/davidvelascogarcia/leapmotionRobotController)

[![Issues](https://img.shields.io/github/issues/davidvelascogarcia/leapmotionRobotController.svg?label=Issues)](https://github.com/davidvelascogarcia/leapmotionRobotController/issues)


## Enlaces de interés

* [multimedia: videos](https://www.youtube.com/watch?time_continue=2&v=DU1mztLFsmE)

* [programs: leapmotionRobotController](https://github.com/davidvelascogarcia)

* [docs: tesis](https://www.researchgate.net/publication/319902393_Modulo_de_reconocimiento_gestual_para_control_de_robot_en_tareas_de_asistencia)
