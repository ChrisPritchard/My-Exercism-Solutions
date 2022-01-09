section .text
global is_paired
is_paired:
    ; rdi contains string to test
    ; rax contains result, true or false
    ; for each open, push the stack
    ; for each close, pop the stack. if whats popped matches close, good, else false
    ; at the end, test stack pointer is zero

    mov r8, rsp         ; starting stack pos
    mov rcx, 0          ; string index
inspect_char:
    movzx r11, byte [rdi + rcx]
    cmp r11, 0
    je finished
    
    cmp r11, 5Bh        ; [
    je add_start
    cmp r11, 5Dh        ; ]
    je test_brackets
    cmp r11, 7Bh        ; {
    je add_start
    cmp r11, 7Dh        ; }
    je test_braces
    cmp r11, 28h        ; (
    je add_start
    cmp r11, 29h        ; )
    je test_parens

move_next:
    inc rcx
    jmp inspect_char

add_start:
    push r11
    jmp move_next

test_brackets:
    pop r12
    cmp r12, 5Bh
    je move_next
    jmp invalid
test_braces:
    pop r12
    cmp r12, 7Bh
    je move_next
    jmp invalid
test_parens:
    pop r12
    cmp r12, 28h
    je move_next
    jmp invalid

finished:
    cmp r8, rsp
    je valid
invalid:
    mov rax, 0
    mov rsp, r8
    ret
valid:
    mov rax, 1
    mov rsp, r8
    ret
