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
    inc r8
    jmp move_next

    cmp r11, 5Dh        ; ]
    dec r8
    jmp move_next

    cmp r11, 7Bh        ; {
    inc r9
    jmp move_next

    cmp r11, 7Dh        ; }
    dec r9
    jmp move_next

    cmp r11, 50h        ; (
    inc r10
    jmp move_next

    cmp r11, 51h        ; )
    dec r10
    jmp move_next

move_next:
    inc rcx
    jmp inspect_char

finished:
    add r8, r9
    add r8, r10
    jz valid
    mov rax, 0
    ret
valid:
    mov rax, 1
    ret
