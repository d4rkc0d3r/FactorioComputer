RAMCLEAR


MOV 2 @1
MOV 1 @2
.OL:ADD @2 2
DIV @2 2 @3 | MOV 3 @4
MOD @2 @4 @5



.IL:JE @5 0 .OL
ADD @4 2
MOD @2 @4 @5
JL @4 @3 .IL



MOV @2 @1 | JMP .OL
