RAMCLEAR


MOV 2 @1
MOV 1 @2
.OL:ADD @2 2

MOD @2 3 @5
MOV 3 @4
MOV 9 @3

.IL:JE @5 0 .OL | ADD @4 2
MOD @2 @4 @5
POW @4-1 2 @3
JL @3 @2 .IL



MOV @2 @1 | JMP .OL
