#include "Encoder_C.h"
#include <Arduino.h>


void configEncoder(){
    pinMode(A,INPUT_PULLUP);
    pinMode(B,INPUT_PULLUP);
    digitalWrite(A,1);
    digitalWrite(B,1);
    attachInterrupt(digitalPinToInterrupt(A),a1,FALLING);
    attachInterrupt(digitalPinToInterrupt(B),a2,FALLING);
}

double getPosicao() {
    return ((double)contadorEncoder*passoEncoder)/(double)pprEncoder;
}

double getVel(){
  double _pos = getPosicao();
  double _time = millis();
  double _vel = ((_pos-posAnt)*1000)/(_time-millisAnt);
  posAnt = _pos;
  millisAnt = _time;
  
  return _vel;
}

void a1(){
  if(!digitalRead(B)) {
  contadorEncoder++;
  }else{
  contadorEncoder--;
  }
}

void a2(){
   if(!digitalRead(A)){
  contadorEncoder--;
  }else{
  contadorEncoder++;
  }  
}

void zeraCont(){
  contadorEncoder=0;
}
