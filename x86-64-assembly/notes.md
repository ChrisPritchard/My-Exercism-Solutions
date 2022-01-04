# Notes

The first arg passed to a function for these challenges will be in `edi/rdi`
The second arg if it exists will be in `esi/rsi`
The return val will be `eax` (eg set this to `0` for false, `1` for true)

`idiv` divides `edx:eax` by the val of `ecx`, and puts the result in `eax` with the remainder in `edx`

`rep` repeats whatever instruction follows it by the number in `ecx/rcx` (each repeat decrements `ecx` until it is `0`)

`movsb` moves a byte from the *address* referenced by `esi` to the *address* referenced by `rdi`, then increments both (moving the address forward to the next byte). Useful for concatting strings

## Register reference

<table class="wikitable" border="1">

<tbody><tr>
<th colspan="5"> Monikers
</th>
<th rowspan="2"> Description
</th></tr>
<tr>
<th> 64-bit
</th>
<th> 32-bit
</th>
<th> 16-bit
</th>
<th> 8 high bits of lower 16 bits
</th>
<th> 8-bit
</th></tr>
<tr>
<td> RAX
</td>
<td> EAX
</td>
<td> AX
</td>
<td> AH
</td>
<td> AL
</td>
<td> Accumulator
</td></tr>
<tr>
<td> RBX
</td>
<td> EBX
</td>
<td> BX
</td>
<td> BH
</td>
<td> BL
</td>
<td> Base
</td></tr>
<tr>
<td> RCX
</td>
<td> ECX
</td>
<td> CX
</td>
<td> CH
</td>
<td> CL
</td>
<td> Counter
</td></tr>
<tr>
<td> RDX
</td>
<td> EDX
</td>
<td> DX
</td>
<td> DH
</td>
<td> DL
</td>
<td> Data (commonly extends the A register)
</td></tr>
<tr>
<td> RSI
</td>
<td> ESI
</td>
<td> SI
</td>
<td> N/A
</td>
<td> SIL
</td>
<td> Source index for string operations
</td></tr>
<tr>
<td> RDI
</td>
<td> EDI
</td>
<td> DI
</td>
<td> N/A
</td>
<td> DIL
</td>
<td> Destination index for string operations
</td></tr>
<tr>
<td> RSP
</td>
<td> ESP
</td>
<td> SP
</td>
<td> N/A
</td>
<td> SPL
</td>
<td> Stack Pointer
</td></tr>
<tr>
<td> RBP
</td>
<td> EBP
</td>
<td> BP
</td>
<td> N/A
</td>
<td> BPL
</td>
<td> Base Pointer (meant for stack frames)
</td></tr>
<tr>
<td> R8
</td>
<td> R8D
</td>
<td> R8W
</td>
<td> N/A
</td>
<td> R8B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R9
</td>
<td> R9D
</td>
<td> R9W
</td>
<td> N/A
</td>
<td> R9B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R10
</td>
<td> R10D
</td>
<td> R10W
</td>
<td> N/A
</td>
<td> R10B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R11
</td>
<td> R11D
</td>
<td> R11W
</td>
<td> N/A
</td>
<td> R11B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R12
</td>
<td> R12D
</td>
<td> R12W
</td>
<td> N/A
</td>
<td> R12B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R13
</td>
<td> R13D
</td>
<td> R13W
</td>
<td> N/A
</td>
<td> R13B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R14
</td>
<td> R14D
</td>
<td> R14W
</td>
<td> N/A
</td>
<td> R14B
</td>
<td> General purpose
</td></tr>
<tr>
<td> R15
</td>
<td> R15D
</td>
<td> R15W
</td>
<td> N/A
</td>
<td> R15B
</td>
<td> General purpose
</td></tr></tbody></table>