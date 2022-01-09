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
    mov r9, 1   ; current square
    mov r8, 0   ; total
add_next:
    cmp r9, 64
    jg finished
    mov rdi, r9
    call square
    add r8, rax
    inc r9      ; move to next square index
    jmp add_next
finished:
    mov rax, r8 ; move final total to output var
    ret
