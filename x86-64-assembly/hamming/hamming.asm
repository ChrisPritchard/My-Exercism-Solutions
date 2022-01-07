section .text
global distance
distance:
    mov rax, 0
    mov rcx, 0
test_char:
    movzx r8, byte [rsi+rcx]
    movzx r9, byte [rdi+rcx]
    cmp r8, r9
    je go_next
    inc rax
go_next:
    inc rcx
    cmp byte [rsi+rcx], 0
    je finished
    jmp test_char

finished:
    cmp byte [rdi+rcx], 0
    jne bad_data
    ret
bad_data:
    mov rax, -1
    ret
