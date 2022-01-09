section .text
global square
square:
    cmp rdi, 1
    jl invalid
    cmp rdi, 64
    jg invalid
    dec rdi
    mov rcx, rdi
    mov rax, 1
    shl rax, cl
    ret
invalid:
    mov rax, 0
    ret

global total
total:
    mov rdi, 1
    mov r8, 0
add_next:
    cmp rdi, 64
    jg finished
    call square
    add r8, rax
    inc rdi
finished:
    mov rax, r9
    ret
