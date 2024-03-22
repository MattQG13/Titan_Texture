#include "Motor_C.h"
#include "VARS.h"
#include "Encoder_C.h"

void atualizaMotor(double vel) {
  if (vel > 0.001 || vel < -0.001) {
    TIMSK1 &= ~(1 << OCIE1A);
    
    timerValue = (passo * 1000) / (ppr * (vel > 0 ? vel : -vel) * 2);
    intervalo = (int)(timerValue * 250);
    OCR1A = intervalo;
    TCNT1 = OCR1A - 1;
    TIMSK1 |= (1 << OCIE1A);

    if (vel > 0){
      digitalWrite(direcao,0);
    }
    if (vel < 0) {
      digitalWrite(direcao,1); 
    }
    //PORTB &= ~(1 << enMotor);
    
    digitalWrite(enMotor,0);
  } else {
    //intervalo = (int)(((passo * 1000) / (ppr * 0.001 * 2)) * 250);
    //OCR1A = intervalo;
    TIMSK1 &= ~(1 << OCIE1A);
    digitalWrite(pulso,0);
    digitalWrite(enMotor,0);
  }
}

ISR(TIMER1_COMPA_vect)
{
  TCNT1 = 0;
    
  digitalWrite(pulso, digitalRead(pulso)^1); 
  
  #ifndef WITH_ENCODER
    if (!digitalRead(pulso))contador += digitalRead(direcao)? -dirUP : dirUP;
    posicao = ((double)contador/ppr)*passo;
  #else
    posicao = getPosicao();
  #endif
  
  if(positionLimited){
     if((posicao>finalPosition)^digitalRead(direcao)){
      atualizaMotor(0);
      positionLimited=false;
     }
  }
  if(zerandoMaquina){
    if(filtredload>=zeroMaquinaLoad){
      atualizaMotor(0);
      #ifndef WITH_ENCODER
        contador=0;
      #else
        zeraCont();
      #endif
      zerandoMaquina=false;
      Serial.print("[ZERO]!");
    }
  }
}

void configMotor(){
   pinMode(pulso, OUTPUT);
  pinMode(direcao, OUTPUT);
  pinMode (enMotor, OUTPUT);
  digitalWrite(pulso,0);
  digitalWrite(direcao,0);
  digitalWrite(enMotor,0);

  TCCR1A = 0;
  
  TCCR1B |=  (1 << CS10);
  TCCR1B |=  (1 << CS11);
  TCCR1B &= ~(1 << CS12);

  TCNT1 = 0;
  OCR1A = 0;

}

void rotinaZeroMaquina(double vel, double carga){
    zerandoMaquina=true;
    zeroMaquinaLoad = carga;
    atualizaMotor(-dirUP*abs(vel));
}
