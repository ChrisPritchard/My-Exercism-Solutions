# Notes

The first arg passed to a function for these challenges will be in `edi/rdi`
The second arg if it exists will be in `esi/rsi`
The return val will be `eax` (eg set this to `0` for false, `1` for true)

`idiv` divides `edx:eax` by the val of `ecx`, and puts the result in `eax` with the remainder in `edx`

`rep` repeats whatever instruction follows it by the number in `ecx/rcx` (each repeat decrements `ecx` until it is `0`)

`movsb` moves a byte from `esi` to `rdi`