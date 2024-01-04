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

* __[L;valor]__
* __[E;valor]__
* __[LS;valor]__
* __[LI;valor]__
* __[M;comando;valor]__
  * __UP__ para subir			
  * __DW__ para descer	
  * __S__ de Stop
* __[ZERO]__ - Define o Zero Máquina do Texturômetro
* __[TARA]__ - Defiine a tara da célula de carga
* __[LCC;valor]__ - Define o valor para calibração da célula de carga
* __[CAL;valor]__ - Calibra célula de carga com base no valor de peso padrão
* __[W;tipo]__ - Define um alarme e o tipo
	* Tipos de alarme:
		* __O__ (Overweight) - Excesso de peso 