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
@output.asm.8
M=D
// Pop 
@SP
AM=M-1
D=M
@output.asm.3
M=D
// Pop 
@SP
AM=M-1
D=M
@output.asm.1
M=D
// Push 
@output.asm.3
D=M
@SP
A=M
M=D
@SP
M=M+1
// Push 
@output.asm.1
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
@output.asm.8
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
