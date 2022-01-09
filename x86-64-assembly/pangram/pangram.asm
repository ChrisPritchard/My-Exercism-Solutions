section .text
global is_pangram
is_pangram:
    ; rdi contains input string
    ; return true or false in rax

    ; run through current rsp to start rsp, testing if present
    ; if not, push
    ; at end, test rsb - stack base = 26 (*8, the stack unit size)

    mov r8, rsp     ; initial stack position
    mov rcx, 0      ; string index

test_position:
    movzx r9, byte [rdi+rcx]
    cmp r9, 0
    je finished     ; end of string
    cmp r9, 'A'
    jl move_next
    cmp r9, 'Z'
    jle check_char
    cmp r9, 'a'
    jl move_next
    cmp r9, 'z'
    jg move_next
    sub r9, 32
    jmp check_char
move_next:
    inc rcx
    jmp test_position

check_char:
    mov r10, rsp
test_next:
    cmp r8, r10         ; end of stack so add
    je add_to_stack
    cmp [r10], r9
    je move_next        ; already tracked
    add r10, 8          ; stack grows downwards, so add 8: go back one push
    jmp test_next
add_to_stack:
    push r9
    jmp move_next
    
finished:
    mov r10, r8
    sub r10, rsp     ; find final offset - initial stack (r8) will be much higher, so sub from that the current val
    cmp r10, 26*8
    je valid
    mov rax, 0
    mov rsp, r8     ; restore original stack pointer
    ret
valid:
    mov rax, 1
    mov rsp, r8     ; restore original stack pointer
    ret
