section .text
global find
find:
    
    ; rdi contains the array to search
    ; rsi is its length
    ; rdx is the value to search for

    mov r8, 0                       ; low

    movzx r9, sil
    dec r9                          ; high (final index of array) 

    cmp r9, 0
    jl invalid                      ; check for an empty array

    movzx r10, dl                   ; target

test_mid:
    mov rax, r9
    sub rax, r8

    shr rax, 1                      ; divide by 2 by shifting right by 1
    add rax, r8                     ; mid value

    movzx r11, byte [rdi+(rax*4)]
    cmp r11, r10                 
    je finished
    jl move_higher                  ; value at rax is lower than target, so search upper bound
    jmp move_lower                  ; value at rax is higher than target, so search lower bound
move_lower:
    mov r9, rax
    dec r9
    cmp r9, r8
    jl invalid
    jmp test_mid
move_higher:
    mov r8, rax
    inc r8
    cmp r8, r9
    jg invalid
    jmp test_mid
invalid:
    mov rax, -1
finished:
    ret
