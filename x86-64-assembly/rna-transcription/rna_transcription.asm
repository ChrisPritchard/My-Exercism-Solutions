section .text
global to_rna
to_rna:
    ; source string is in rdi
    ; buffer to complete is in rsi
    mov rcx, 0

exchange:
    movzx r8, byte [rdi+rcx]

    cmp r8, 0
    je finished

    cmp r8, 'G'
    jne cmp_C
    mov byte [rsi+rcx], 'C'
    jmp move_next
cmp_C:
    cmp r8, 'C'
    jne cmp_T
    mov byte [rsi+rcx], 'G'
    jmp move_next
cmp_T:
    cmp r8, 'T'
    jne cmp_A
    mov byte [rsi+rcx], 'A'
    jmp move_next
cmp_A:
    cmp r8, 'A'
    jne move_next
    mov byte [rsi+rcx], 'U'
    jmp move_next

move_next:
    inc rcx
    jmp exchange
finished:
    mov byte [rsi+rcx], 0
    ret
