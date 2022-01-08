section .text
global allergic_to
allergic_to:
    mov rax, 1
    mov rcx, rdi    ; fill rcx with the allergen index
    shl rax, cl     ; fill rax with 2^index (cl is the lower 8 bits of rcx)
    and rax, rsi
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
    ; rdi is the allergen set
    ; rsi is the address of the item_list struct
        ; the first element is the size
        ; the second is a list of item values
    ret
