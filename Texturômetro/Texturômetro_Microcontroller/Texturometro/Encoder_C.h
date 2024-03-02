#ifndef Encoder_h
#define Encoder_h

#include  <Arduino.h>

#define A 18
#define B 19

void configEncoder ();
double getPosicao();
double getVel();  
void a1();
void a2();
void zeraCont();

const int pprEncoder = 1200;
const double passoEncoder = 5;
static volatile long contadorEncoder = 0;

static volatile double millisAnt = 0;
static volatile double posAnt = 0; 

#endif
