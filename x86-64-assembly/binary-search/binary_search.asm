section .text
global find
find:
    
    ; rdi contains the array to search
    ; rsi is its length
    ; rdx is the value to search for

    mov r8, 0                       ; low
    movzx r9, sil
    dec r9                          ; high (final index of array) 
    movzx r10, dl                    ; target

test_mid:
    mov rax, r9
    sub rax, r8

    shr rax, 1                      ; divide by 2 by shifting right by 1
    add rax, r8                     ; mid value

    cmp byte [rdi+(rax*4)], r10b    ; omg always remember sizes (without 'byte' this tripped me up)
    je finished
    jl move_lower
    jmp move_higher
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
