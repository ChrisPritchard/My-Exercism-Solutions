section .text
global reverse
reverse:
    ; string is in rdi
    ; loop each char pushing onto stack, then loop popping back into array to reverse it
    mov rcx, 0
push_step:
    cmp byte [rdi+rcx], 0
    je next
    movzx r8, byte [rdi+rcx]
    push r8
    inc rcx
    jmp push_step
next:
    mov rcx, 0
pop_step:
    cmp byte [rdi+rcx], 0
    je finished
    pop r8
    mov byte [rdi+rcx], r8b
    inc rcx
    jmp pop_step
finished:
    ret
