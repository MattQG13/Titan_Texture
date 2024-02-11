#include "VARS.h"
#include "Filter_MM.h"

long contador = 0;

bool positionLimited = false;
double finalPosition = 0;
double posicao = 0;
double timerValue = 0;
int intervalo = 0;
bool zerandoMaquina = false;
double zeroMaquinaLoad = 0;

//LoadCell 

double filtredload=0;
double scale = 224.419;//-53.8071 ;// -26.90355;  //ESCALA PARA AJUSTE
double load=0;
long double tara  = 0;

 Filter FMM(8, 0.01);
  Filter FFMM (16, 0.5);