#ifndef MotorController
#define MotorController

#include <Arduino.h>

  void atualizaMotor(double vel);
  void configMotor();
  void rotinaZeroMaquina(double vel, double carga);
  
  const double passo = 5;
  const int ppr = 800;
  const char dirUP = 1;

  static bool zerandoMaquina = false;
static double zeroMaquinaLoad = 0;
static int intervalo = 0;
#endif
