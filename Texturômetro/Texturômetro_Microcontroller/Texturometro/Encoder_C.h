#ifndef Encoder_h
#define Encoder_h

#include  <Arduino.h>

#define A 21
#define B 20

void configEncoder ();
static double getPosicao();  
void a1();
void a2();

const int pprEncoder = 1200;
const double passoEncoder = 5;
static long contadorEncoder;

#endif