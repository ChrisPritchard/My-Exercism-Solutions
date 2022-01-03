section .text
global leap_year
leap_year:
divide_by_4:
    mov eax, edi    ; load input year from edi into eax
    xor edx, edx    ; empty edx
    mov ecx, 4      ; set divide arg to 4
    idiv ecx        ; divide edx:eax (year) by arg (4), result in eax, rem in edx
    cmp edx, 0      ; check the remainder is 0
    jne set_false   ; if there is a rem then false
                    ; else continue to test div by 100
divide_by_100:
    mov eax, edi    ; load input year from edi into eax
    xor edx, edx    ; empty edx
    mov ecx, 100    ; set divide arg to 100
    idiv ecx        ; divide edx:eax (year) by arg (100), result in eax, rem in edx
    cmp edx, 0      ; check the remainder is 0
    je divide_by_400    ; if there is no rem, test 400 divide
                    ; else continue to true
set_true:
    mov eax, 1
    ret

divide_by_400:
    mov eax, edi    ; load input year from edi into eax
    xor edx, edx    ; empty edx
    mov ecx, 400    ; set divide arg to 400
    idiv ecx        ; divide edx:eax (year) by arg (400), result in eax, rem in edx
    cmp edx, 0      ; check the remainder is 0
    je set_true     ; if so, set true
                    ; else continue to false
set_false:
    mov eax, 0
    ret
