# Notes

The first arg passed to a function for these challenges will be in `edi/rdi`
The second arg if it exists will be in `esi/rsi`
The return val will be `eax` (eg set this to `0` for false, `1` for true)

`idiv` divides `edx:eax` by the val of `ecx`, and puts the result in `eax` with the remainder in `edx`

`rep` repeats whatever instruction follows it by the number in `ecx/rcx` (each repeat decrements `ecx` until it is `0`)

`movsb` moves a byte from the *address* referenced by `esi` to the *address* referenced by `rdi`, then increments both (moving the address forward to the next byte). Useful for concatting strings

## Register reference

```
64-bit	32-bit	16-bit	8 high  8-bit   Description
---------------------------------------------------
RAX	    EAX	    AX	    AH	    AL	    Accumulator
RBX	    EBX	    BX	    BH	    BL	    Base
RCX	    ECX	    CX	    CH	    CL	    Counter
RDX	    EDX	    DX	    DH	    DL	    Data (commonly extends the A register)
RSI	    ESI	    SI	    N/A	    SIL	    Source index for string operations
RDI	    EDI	    DI	    N/A	    DIL	    Destination index for string operations
RSP	    ESP	    SP	    N/A	    SPL	    Stack Pointer
RBP	    EBP	    BP	    N/A	    BPL	    Base Pointer (meant for stack frames)
R8	    R8D	    R8W	    N/A	    R8B	    General purpose
R9	    R9D	    R9W	    N/A	    R9B	    General purpose
R10	    R10D	R10W	N/A	    R10B	General purpose
R11	    R11D	R11W	N/A	    R11B	General purpose
R12	    R12D	R12W	N/A	    R12B	General purpose
R13	    R13D	R13W	N/A	    R13B	General purpose
R14	    R14D	R14W	N/A	    R14B	General purpose
R15	    R15D	R15W	N/A	    R15B	General purpose
```

# Compiling and Debugging

Debugging can be done using IDA Freeware

Assembly requires a few directives to become a win exe. For example, assuming single function is 'distance' (from the hamming challenge) and takes two args, add initial code like so:

```assembly
default rel
section .rodata
arg1: db "GGACGGATTCTG", 0  ; used for this specific function
arg2: db "AGGACGGATTCT", 0

section .text
global main                 ; key parts to add from here...
main:
    lea rsi, [arg1]
    lea rdi, [arg2]
    call distance
    ret                     ; to here
distance:
                            ; rest of code. remove original 'global distance'
```

then compile to a executable that can be opened with ida as so (assuming assembly is named `hamming.asm`):

```nasm -f win64 -o win.obj hamming.asm && gcc.exe win.obj -o win.exe```

(note the above is done using wsl, which can run windows exes like gcc.exe)

To do the equiv on linux (and use gdb or r2 or whatever):

```nasm -f elf64 -o lin.o hamming.asm && ld -o lin lin.o && ./lin```