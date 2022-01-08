section .text
global allergic_to
allergic_to:
    mov rax, 1
    mov rcx, rdi    ; fill rcx with the allergen index
    shl rax, cl     ; fill rax with 2^index (cl is the lower 8 bits of rcx)
    and rax, rsi    ; test allergen with person's allergen set
    jnz allergic
    jmp not_allergic
allergic:
    mov rax, 1
    ret
not_allergic:
    mov rax, 0
    ret

global list
list:
    mov rsi, rdi    ; put allergen set in rax
    mov rbx, rsi    ; get reference to buffer
    mov rcx, 0      ; index of array
    mov rdi, 0      ; current allergen
test_allergen:
    call allergic_to
    cmp rax, 1
    je add_allergen
test_next:
    cmp rdi, 7
    je finished
    inc rdi
    jmp test_allergen
add_allergen:
    mov [rbx+(rcx*4)], rdi
    inc rcx ; inc size
    jmp test_next
finished:
    mov rdi, rbx
    mov rdi, rcx
    ret


    ; rdi is the allergen set
    ; rsi is the address of the item_list struct
        ; the first element is the size
        ; the second is a list of item values
    ; maintain current index (0)
    ; go through items. if allergic (use call syntax)
    ret
