// Push 
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
// Pop 
@LCL
D=M
@0
D=A+M
@R15
M=D
@SP
AM=M-1
D=M
@R15
A=M
M=D
(LOOP_START)
// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@0
D=A
@LCL
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M+D
// Pop 
@LCL
D=M
@0
D=A+M
@R15
M=D
@SP
AM=M-1
D=M
@R15
A=M
M=D
// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M-D
// Pop 
@ARG
D=M
@0
D=A+M
@R15
M=D
@SP
AM=M-1
D=M
@R15
A=M
M=D
// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1

// Push 
@0
D=A
@LCL
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@111
D=A
@SP
A=M
M=D
@SP
M=M+1
// Push 
@333
D=A
@SP
A=M
M=D
@SP
M=M+1
// Push 
@888
D=A
@SP
A=M
M=D
@SP
M=M+1
// Pop 
@SP
AM=M-1
D=M
@Output.8
M=D
// Pop 
@SP
AM=M-1
D=M
@Output.3
M=D
// Pop 
@SP
AM=M-1
D=M
@Output.1
M=D
// Push 
@Output.3
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@Output.1
D=M
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M-D
// Push 
@Output.8
D=M
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M+D

// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
D=M-D
@Output.0
D;JLT
D=0
@Output.1
0;JMP
(Output.0)
D=-1
(Output.1)
@SP
A=M-1
M=D


(IF_TRUE)
// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1

(IF_FALSE)
// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M-D

// Push 
@0
D=A
@ARG
A=M+D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
AM=M-1
D=M
A=A-1
M=M-D

@SP
AM=M-1
D=M
A=A-1
M=M+D

