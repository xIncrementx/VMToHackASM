@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.0
D;JEQ
@File.1
D=0;JMP
(File.0)
D=-1
(File.1)
@SP
AM=M-1
A=A-1
M=D
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.2
D;JEQ
@File.3
D=0
0;JMP
(File.2)
D=-1
(File.3)
@SP
AM=M-1
A=A-1
M=D
@16
D=A
@SP
A=M
M=D
@SP
M=M+1
@17
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.4
D;JEQ
@File.5
D=0
0;JMP
(File.4)
D=-1
(File.5)
@SP
AM=M-1
A=A-1
M=D
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.6
D;JLT
@File.7
D=0
0;JMP
(File.6)
D=-1
(File.7)
@SP
AM=M-1
A=A-1
M=D
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@892
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.8
D;JLT
@File.9
D=0
0;JMP
(File.8)
D=-1
(File.9)
@SP
AM=M-1
A=A-1
M=D
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@891
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.10
D;JLT
@File.11
D=0
0;JMP
(File.10)
D=-1
(File.11)
@SP
AM=M-1
A=A-1
M=D
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.12
D;JGT
@File.13
D=0
0;JMP
(File.12)
D=-1
(File.13)
@SP
AM=M-1
A=A-1
M=D
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@32767
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.14
D;JGT
@File.15
D=0
0;JMP
(File.14)
D=-1
(File.15)
@SP
AM=M-1
A=A-1
M=D
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@32766
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
A=M-1
A=A-1
D=M
A=A+1
D=D-M
@File.16
D;JGT
@File.17
D=0
0;JMP
(File.16)
D=-1
(File.17)
@SP
AM=M-1
A=A-1
M=D
@57
D=A
@SP
A=M
M=D
@SP
M=M+1
@31
D=A
@SP
A=M
M=D
@SP
M=M+1
@53
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
M=M+D
@112
D=A
@SP
A=M
M=D
@SP
M=M+1
@SP
AM=M-1
D=M
A=A-1
M=M-D
@SP
AM=M-1
M=-M
