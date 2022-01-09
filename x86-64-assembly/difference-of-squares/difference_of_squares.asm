section .text
global square_of_sum
square_of_sum:
    mov rdx, 0
    mov rax, rdi
    inc rax
    imul rax, rdi
    mov r8, 2
    idiv r8
    imul rax, rax
    ret

global sum_of_squares
sum_of_squares:
    mov rdx, 0
    mov rax, rdi
    imul rax, 2
    inc rax
    mov rbx, rdi
    inc rbx
    imul rax, rbx
    imul rax, rdi
    mov r8, 6
    idiv r8
    ret

global difference_of_squares
difference_of_squares:
    call sum_of_squares
    mov r9, rax
    call square_of_sum
    sub rax, r9
    ret
