# Texturômetro

* Nome do Projeto: Titan Texture
* Data de Início: 01/07/2023
* Descrição: Software de análise de textura desenvolvido para o projeto de TCC do curso de Engenharia de Controle e Automação

```xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
xxxxxxxxxxxxxxxxxxxxxddddddddxxxxxxxxxxxxxxxxxxxxxxxxxxx 
xxxxxxxxxxxxxxddol:,,,'.'',',,;:codxxxxxxxxxxxxxxxxxxxxx 
xxxxxxxxxxxddlc;..................',coxxxxxxxxxxxxxxxxxx 
xxxxxxxxdoc;'......................''',cdxxxxxxxxxxxxxxx 
xxxxxxdc,...............................':oxxxxxxxxxxxxx 
xxxxdd;.........;,.........................;oxxxxxxxxxxx 
xxxdl...'.......';,..........................:xxxxxxxxxx 
xxdl.............;;'..................     ...,dxxxxxxxx 
xdl.  ..........'::,...............          ..'oxxxxxxx 
do'   ..........';:,...............             .oxxxxxx
d,     .........':ol;,'............              'dxxxxx 
c.     ....''',:ldxxxdllllllllc:,'...             ;xxxxx 
,       ..';:cdxO0000KKKKK000KK00Okkdl:,.. .      .lxxxx 
.      ..,:oddO0KKKKKKKKKKKKKKKKKK000OOxxxc,.      'dxxx 
.     .,:oxxdOKKKKKKKKKKKKKKKKKK00000OOOkOkd:.     .cxxx 
.    .:dxkkkO0KXXKKKKKKKKKKKKKKKKK00OOOOOOOkdc.....',dxx 
l.  .:dxkkOO0KXXXKKKKKKKKKKKKKKKK000OOOOOOOkkd,..lxl:lxx 
ol,..:ddxkkOO0KKKKK00KKKKK000000OOOOOOOOOOkkkkc.cxxl:;xx 
dol;.,ddkkkkkkOOOO000000K0000KK0OO0OOOOOkkkkkkd;lxxxl'ox 
dd:c:;dkkOkkkkO000OOOOOOO0000000kkkkkkxxddxkkkxc;coxo.:x 
ddolocddddxxxdxkO0OOkdxOOxdxdodoolloxkkkkkkOOkxlclcd;.'d 
dddoocooooodddlcclc;;:dkkkddol;,:;.':ldxkOOOOkxo:coo...o 
dddllcoxxxdol;'.,c,;clxkOOkkkkdooooodxO000OOOkxolod:...c 
dool::okOkkdoollolloxxkO00OkOOOOkkkOO00K00OOOkxdddc....: 
oolcc.ckOO0000OOOOOOkkk0KKOOOOO0K000000KK0OOOkxc;,....., 
oclll..dkO0KXKKKK00Okkk0KKOOOOOOO000000KK0OOOkd;.......' 
lllll'.:xkOKKK0000OkkkkO0K0OOOkkxdkkOOOOOOOOkkd,........ 
olcll' .okkOOOOOkxodkkkk0K0OOOOkOxoldxkkkkkkxxd'........ 
lllcc' .,dxxkkkxoclxkOkkO00OOOkkOOkxoclodddxxxl......... 
llllc'...,looooc:cdxxxkO000Okkkkkkkkkxolcdxxxx,......... 
lcllc.....'clc:;cdxxxdddddxkxkOOOkkkkxxdoxkxxl.......... 
c:c:,......'loolllodxxxkkkOOkkxxdd::dkkdxxxdo........... 
.........   .:ddooooc:lllodooocolodkkkkxxdol. .'..'..... 
.......      .'codxxxdodoododdxkOkkkkkkxddl.   ...'..... 
.......      . .'lddddxkOOOOOkkxxdxxkkxdoc.    ......... 
.......      ..   'ldddollooooddxkkkkxol,'.   .....'.... 
........  .. ..     .lxxkkkOOOOOOOkxdl;...     ..'.'.... 
..................    'codxxxxxxxdlc;,.....   .......... 
..............'.  .   ..';::::::;;;;,... ..  ....... ... 
.........''....  ..   ...',;,,,,,,'..... .' ........ ... 
.........'......  .........,;;'............... ....   .. 
.........'.....  . .. ...,,';:;'...........   ....    . 
  ...............  .. .  .,;;:;,'..........  ...		
```

## Protocolo Serial

Padrões de protocolo: 

* __[L;carga;tempo]__ - Valor de leitura da célula de carga e tempo da leitura [g][s]
* __[E;posição;tempo]__ - Valor de posição do encoder e tempo da leitura[mm][s]
* __[LS;valor]__ - Valor do sensor de limite superior [bool] 
* __[LI;valor]__ - Valor do sensor de limite inferior [bool]

* __[M;comando;velocidade]__ - Controle do motor com direção e velocidade [mm/s]
* __[M;comando;velocidade;posição]__ - Controle do motor com direção, velocidade e posição final [mm/s][mm]
  * __UP__ para subir			
  * __DW__ para descer	
  * __S__ de Stop
* __[ZERAR;velocidade;carga]__ - Define velocidade e carga limite para rotira de Zero Máquina [mm/s][g]
* __[ZERO]__ - Define o Zero Máquina do Texturômetro
* __[INITIME]__ - Inicia envio de tempo a partir do envio do comando
* __[TARA]__ - Define a tara da célula de carga
* __[LCC;valor]__ - Define o valor para escala de calibração da célula de carga
* __[CAL;valor]__ - Envia comando para calibração da célula de carga com valor do peso padrão [g]
* __[W;tipo]__ - Define um alarme e o tipo
	* Tipos de alarme:
		* __O__ (Overweight) - Excesso de peso 