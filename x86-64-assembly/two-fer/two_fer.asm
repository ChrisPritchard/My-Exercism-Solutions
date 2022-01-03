default rel

section .rodata
msg: db "One for you, one for me.", 0

section .text
global two_fer
two_fer:
    mov rdx, rdi    ; name param
    mov rdi, rsi    ; source and dest buffer
    
    lea rsi, [msg]  ; source for copy (dest will be rdi)
    mov rcx, 8      ; how many bytes to copy 'One for '
    rep movsb       ; copy
    
    test rdx, rdx   ; check if name is empty
    jne name        ; if so skip next bit and render name

    lea rsi, [msg+8]
    mov rcx, 3      ; 'you'
    rep movsb
    jmp finish

name:               ; section copies over a byte at a time unless/until the byte is 0
    mov rsi, rdx
    cmp byte [rsi], 0
    je finish
loop:
    movsb   ; one byte from rsi to rdi. inc rsi
    cmp byte [rsi], 0
    jne loop

finish:
    lea rsi, [msg+11]
    mov rcx, 14         ; ', one for me.'
    rep movsb

    ret
