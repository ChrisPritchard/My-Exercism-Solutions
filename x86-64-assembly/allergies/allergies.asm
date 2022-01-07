section .text
global allergic_to
allergic_to:
    and rdi, rsi
    jz allergic
    jmp not_allergic
allergic:
    mov rax, 1
    ret
not_allergic:
    mov rax, 0
    ret

global list
list:
    ; Provide your implementation here
    ret
