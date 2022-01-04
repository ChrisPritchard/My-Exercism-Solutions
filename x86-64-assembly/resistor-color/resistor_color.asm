default rel

section .rodata
c0: db "black", 0
c1: db "brown", 0
c2: db "red", 0
c3: db "orange", 0
c4: db "yellow", 0
c5: db "green", 0
c6: db "blue", 0
c7: db "violet", 0
c8: db "grey", 0
c9: db "white", 0
all: dq c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, 0

section .text
global color_code
color_code:
    
    mov r11, rdi        ; save the input string address
    mov r12, -1         ; this will be the length of the string

inc_len:
    inc r12
    cmp byte [rdi+r12], 0
    jne inc_len

    xor r8, r8          ; r8 is our array index and return val
    lea rax, [all]      ; load the array location

test_colour:
    imul r9, r8, 8      ; calculate the array memory offset
    mov rsi, [rax+r9]   ; get the address of the given string
    mov rdi, r11        ; load input string
    mov rcx, r12        ; compare for length of input string
    cld
    repe cmpsb
    jecxz finished
    inc r8              ; move to next index to test
    jmp test_colour

finished:
    mov rax, r8
    ret

global colors
colors:
    lea rax, [all]
    ret
