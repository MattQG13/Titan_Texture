#include "Encoder_C.h"
#include <Arduino.h>



void configEncoder(){
    pinMode(A,INPUT_PULLUP);
    pinMode(B,INPUT_PULLUP);
    digitalWrite(A,1);
    digitalWrite(B,1);
    attachInterrupt(digitalPinToInterrupt(A),a1,RISING);
    attachInterrupt(digitalPinToInterrupt(B),a2,RISING);
}

double getPosicao() {
    return ((double)contadorEncoder*passoEncoder)/(double)pprEncoder;
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
