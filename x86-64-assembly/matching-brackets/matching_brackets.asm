section .text
global is_paired
is_paired:
    ; rdi contains string to test
    ; rax contains result, true or false
    mov r8, 0           ; brackets
    mov r9, 0           ; braces
    mov r10, 0          ; parentheses
    mov rcx, 0          ; string index
inspect_char:
    movzx r11, byte [rdi + rcx]
    cmp r11, 0
    je finished
    
    cmp r11, 5Bh        ; [
    je add_bracket
    cmp r11, 5Dh        ; ]
    je rem_bracket
    cmp r11, 7Bh        ; {
    je add_brace
    cmp r11, 7Dh        ; }
    je rem_brace
    cmp r11, 50h        ; (
    je add_parens
    cmp r11, 51h        ; )
    je rem_parens

move_next:
    inc rcx
    jmp inspect_char

add_bracket:
    inc r8
    jmp move_next
rem_bracket:
    dec r8
    jmp move_next
add_brace:
    inc r9
    jmp move_next
rem_brace:
    dec r9
    jmp move_next
add_parens:
    inc r10
    jmp move_next
rem_parens:
    dec r10
    jmp move_next

finished:
    add r8, r9
    add r8, r10
    jz valid
    mov rax, 0
    ret
valid:
    mov rax, 1
    ret
