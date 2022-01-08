section .text
global allergic_to
allergic_to:
    mov rax, 1
    mov rcx, rdi    ; fill rcx with the allergen index
    shl rax, cl     ; fill rax with 2^index (cl is the lower 8 bits of rcx)
    and rax, rsi    ; test allergen with person's allergen set
    ret             ; rax will be zero (false) if no match, more than zero otherwise (which counts as true)

global list
list:
                    ; rsi contains allergen set
                    ; rdi is the buffer location
    mov rcx, 0      ; current allergen
    mov rbx, 0      ; size (and latest index)
test_allergen:
    mov rdx, 1
    shl rdx, cl         ; rdx = 2^rcx
    and rdx, rdi        ; test if present in allergen set
    jnz add_allergen    
test_next:
    inc rcx
    cmp rcx, 8
    je finished
    jmp test_allergen
add_allergen:
    mov [rsi+4 + rbx*4], rcx    ; add current allergen to array
    inc rbx
    jmp test_next
finished:
    mov dword [rsi], ebx        ; set array size (dword, so just the int value)
    ret
