// Push 
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
// Pop 
@0
D=A
@LCL
D=D+M
@R15
M=D
@SP
AM=M-1
D=M
@R15
A=M
M=D
(Output.asm.)
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
@0
D=A
@LCL
D=D+M
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
@0
D=A
@ARG
D=D+M
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
@Output.asm.8
M=D
// Pop 
@SP
AM=M-1
D=M
@Output.asm.3
M=D
// Pop 
@SP
AM=M-1
D=M
@Output.asm.1
M=D
// Push 
@Output.asm.3
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@Output.asm.1
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
@Output.asm.8
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
@Output.asm.0
D;JLT
D=0
@Output.asm.1
0;JMP
(Output.asm.0)
D=-1
(Output.asm.1)
@SP
A=M-1
M=D


(Output.asm.)
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
(Output.asm.)
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
