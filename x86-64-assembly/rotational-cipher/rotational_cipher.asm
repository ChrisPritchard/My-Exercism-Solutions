section .text
global rotate
rotate:
    ; the input string is in rdi
    ; the amount to rotate is in rsi
    ; the output buffer is in rcx

    ; go through string until 0
    ; if string is lower case, rotate by a-z
    ; if string is upper case, rotate by A-Z
    ; if neither, just set output

    mov rdx, 0      ; current string index
rot_char:
    movzx r8, byte [rdi + rdx]
    cmp r8, 0
    je finished
    cmp r8, 65      ; test less than A
    jl copy_to_buffer
    cmp r8, 90      ; test less than or equal to Z
    jle rot_uppercase
    cmp r8, 97      ; test less than a
    jl copy_to_buffer
    cmp r8, 122     ; test less than or equal to z
    jle rot_lowercase
    jmp copy_to_buffer
rot_uppercase:
    add r8, rsi
    cmp r8, 90
    jle copy_to_buffer
    sub r8, 65
    jmp copy_to_buffer
rot_lowercase:
    add r8, rsi
    cmp r8, 122
    jle copy_to_buffer
    sub r8, 97
    jmp copy_to_buffer
copy_to_buffer:
    mov byte [rcx + rdx], r8b
    inc rdx
    jmp rot_char
finished:
    ret
