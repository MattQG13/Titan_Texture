#include "LoadCell.h"
#include "VARS.h"
#include "CS5530.h"


#ifdef WITH_ADC_CS5530
  const int CSPinCS5530 = 2;
  const int MOSIPinCS5530 = 3;
  const int MISOPinCS5530 = 4;
  const int SCLKPinCS5530 = 5;
  
  CS5530 myADC (MOSIPinCS5530, MISOPinCS5530, SCLKPinCS5530, CSPinCS5530);
#endif

#ifdef WITH_ADC_HX711
  #define  ADDO  8 //PB0 //8
  #define  ADSK  9 //PB1 //9
#endif


void configADC(){
    #ifdef WITH_ADC_HX711
        pinMode(ADSK, OUTPUT);
        pinMode(ADDO,INPUT_PULLUP);
        digitalWrite(ADSK, 0);
    #endif

    #ifdef WITH_ADC_CS5530
        myADC.Init();
        delay(1000);
    #endif
    tara=0;
    tarar(100);

    TCCR3A = 0;
    TCCR3B = 0;
    
    
    TCCR3B &=  ~(1 << CS30);
    TCCR3B &=  ~(1 << CS31);
    TCCR3B |= (1 << CS32);
    
    OCR3A=624;
    TCNT3 = 0;
  //TIMSK3 |= (1 << OCIE3A);
}



#ifdef WITH_ADC_CS5530
  long double ReadLoad(){
    long valor;
    myADC.LePeso((long *)&valor);
  
    return (((long double)valor) / scale) - tara;
  }
#endif

#ifdef WITH_ADC_HX711
  long double ReadLoad(){
    unsigned long Count = 0;
  
    digitalWrite(ADSK,0); //PORTB &= ~(1 << ADSK);
    
    while (digitalRead(ADDO));//(PINB & (1 << ADDO)));
  
    for (char i = 0; i < 24; i++){
      digitalWrite(ADSK,1);//PORTB |= (1 << ADSK);
      Count = Count << 1;
      digitalWrite(ADSK,0);//PORTB &= ~(1 << ADSK);
      if(digitalRead(ADDO))Count++; //if (PINB & (1 << ADDO)) Count++;
    }
  
    digitalWrite(ADSK,1); //PORTB |= (1 << ADSK);
    Count = Count ^ 0x800000;
    digitalWrite(ADSK,0);//PORTB &= ~(1 << ADSK);
  
    return (((double)Count) / scale) - tara;
  }
#endif

 double calibrar(double val, char n = 15) {
  long double cal = 0;
  for (int i = 0; i < n; i++){
    delay(20);
    cal += (ReadLoad() / n);
  }

  scale *= cal /(-val);
  tarar(100);
  tara += val;
  return scale;
}

void tarar(char n=10) {
  long double _tara = 0;
  for (int i = 0; i < n; i++) {
    delay(20);
    _tara += (ReadLoad() / n);
  }
  tara += _tara;
}
