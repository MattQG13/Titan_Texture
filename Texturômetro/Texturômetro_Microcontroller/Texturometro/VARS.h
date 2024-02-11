#ifndef variaveis
#define variaveis

#include <Arduino.h>
#include "Filter_MM.h"

#define pulso 10 //PB2 //10 Pulso 
#define direcao 11 //PB3 //11
#define enMotor 12 //PB4 //12


//========DEFINE MODULO ADC========
  //#define WITH_ADC_HX711
  #define WITH_ADC_CS5530
//=================================
//_________________________________
//====DEFINE LEITOR DE POSIÇÃO=====
  //#define WITH_ENCODER
//=================================

//MOTOR==========================


extern long contador;

extern bool positionLimited;
extern double finalPosition;
extern double posicao;
extern double timerValue;
extern int intervalo;
extern bool zerandoMaquina;
extern double zeroMaquinaLoad;
extern double filtredload;

//LOAD CELL=========================================

extern double scale;
extern double load;
extern long double tara;

extern Filter FMM;
extern Filter FFMM;


#endif
