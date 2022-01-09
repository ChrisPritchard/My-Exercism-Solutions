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
    
    cmp r11, '['
    je add_start
    cmp r11, ']'
    je test_brackets
    cmp r11, '{'
    je add_start
    cmp r11, '}'
    je test_braces
    cmp r11, '('
    je add_start
    cmp r11, ')'
    je test_parens

move_next:
    inc rcx
    jmp inspect_char

add_start:
    push r11
    jmp move_next

test_brackets:
    pop r12
    cmp r12, '['
    je move_next
    jmp invalid
test_braces:
    pop r12
    cmp r12, '{'
    je move_next
    jmp invalid
test_parens:
    pop r12
    cmp r12, '('
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
