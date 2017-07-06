VAR largestPrime @1
VAR currentCandidate @2
VAR maxDivider @3
VAR currentDivider @4
VAR modResult @5
RAMCLEAR
MOV 2 largestPrime
MOV 1 currentCandidate
OUTER_LOOP:
  ADD currentCandidate 2
  MOV currentCandidate maxDivider
  DIV maxDivider 2 //sqrt would be better, but that is not implemented
  MOV 2 currentDivider
  INNER_LOOP:
    MOV currentCandidate modResult
    MOD modResult currentDivider
    JEZ modResult OUTER_LOOP
    JEG currentDivider maxDivider OUTER_LOOP
    ADD currentDivider 1
    JMP INNER_LOOP
  MOV currentCandidate largestPrime
  JMP OUTER_LOOP
