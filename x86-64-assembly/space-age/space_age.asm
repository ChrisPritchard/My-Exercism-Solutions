default rel

section .rodata
seconds: dd 31557600.0
all: dd 0.2408467, 0.61519726, 1.0, 1.8808158, 11.862615, 29.447498, 84.016846, 164.79132, 0

section .text
global age
age:

    lea r8, [all]
    movss xmm1, [r8+rdi*4]
    mulss xmm1, [seconds]

    cvtsi2ss xmm0, rsi
    divss xmm0, xmm1
    ret
