section .text
global rotate
rotate:
    ; the input string is in rdi
    ; the amount to rotate is in rsi
    ; the output buffer is in rdx

    ; go through string until 0
    ; if string is lower case, rotate by a-z
    ; if string is upper case, rotate by A-Z
    ; if neither, just set output

    mov rcx, 0      ; current string index
rot_char:
    mov r8b, byte [rdi + rcx]
    cmp r8b, 0
    je finished
    cmp r8b, 41h      ; test less than A
    jl copy_to_buffer
    cmp r8b, 5Ah      ; test less than or equal to Z
    jle rot_uppercase
    cmp r8b, 61h      ; test less than a
    jl copy_to_buffer
    cmp r8b, 7Ah     ; test less than or equal to z
    jle rot_lowercase
    jmp copy_to_buffer
rot_uppercase:
    add r8b, sil
    cmp r8b, 5Ah
    jle copy_to_buffer
    sub r8b, 41h
    jmp copy_to_buffer
rot_lowercase:
    add r8b, sil
    cmp r8b, 7Ah
    jle copy_to_buffer
    sub r8b, 61h
    jmp copy_to_buffer
copy_to_buffer:
    mov byte [rdx + rcx], r8b
    inc rcx
    jmp rot_char
finished:
    mov byte [rdx + rcx], 0
    ret
