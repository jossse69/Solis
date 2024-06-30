; Sample ASM SOL-8 program with bit shifting
; Load value into the accumulator and perform bit shifts

    LOAD 0x08     ; Load value 8 into accumulator
    SHL 1         ; Shift left by 1 (Accumulator = 16)
    POKEA 0x20    ; Store accumulator value (16) at memory address 0x20

    LOAD 0x20     ; Load value from memory address 0x20 (Accumulator = 16)
    SHR 2         ; Shift right by 2 (Accumulator = 4)

    HALT          ; Halt the program
