# asm_to_bin.py

# Define opcodes for ASM SOL-8 instructions
opcodes = {
    'NOP': 0x00,
    'LOAD': 0x01,
    'ADD': 0x02,
    'SUB': 0x03,
    'MUL': 0x04,
    'JMP': 0x05,
    'DIV': 0x06,
    'JMPZ': 0x07,
    'JMPNZ': 0x08,
    'AND': 0x09,
    'OR': 0x0A,
    'XOR': 0x0B,
    'POKE': 0x0C,
    'READ': 0x0D,
    'INC': 0x0E,
    'DEC': 0x0F,
    'CMP': 0x10,
    'JMPF': 0x11,
    'CALL': 0x12,
    'RET': 0x13,
    'POKEA': 0x14,
    'HALT': 0xFF,
}

def assemble(input_asm_file, output_bin_file):
    symbol_table = {}
    machine_code = bytearray()
    pending_labels = []

    with open(input_asm_file, 'r') as f:
        lines = f.readlines()

    # First pass to collect labels
    for line_num, line in enumerate(lines):
        line = line.strip()
        if line.startswith(';') or not line:
            continue

        tokens = line.split()
        if tokens[0].endswith(':'):
            label = tokens[0][:-1]
            symbol_table[label] = len(machine_code)
            tokens.pop(0)  # Remove label for further processing
            lines[line_num] = ' '.join(tokens)  # Update the line without the label

        if tokens and tokens[0] in opcodes:
            opcode = opcodes[tokens[0]]
            machine_code.append(opcode)
            for operand in tokens[1:]:
                if operand in symbol_table:
                    machine_code.append(symbol_table[operand])
                elif operand.startswith('0x'):
                    machine_code.append(int(operand, 16))
                elif operand.startswith(';'):
                    break
                else:
                    try:
                        machine_code.append(int(operand))
                    except ValueError:
                        pending_labels.append((len(machine_code), operand))
                        machine_code.append(0)  # Placeholder for the label

    # Resolve pending labels
    for index, label in pending_labels:
        if label in symbol_table:
            machine_code[index] = symbol_table[label]
        else:
            print(f"Error: Undefined label {label}")

    # Write machine code to binary file
    with open(output_bin_file, 'wb') as fout:
        fout.write(machine_code)

    print(f"Assembled {input_asm_file} to {output_bin_file}")

# Example usage
if __name__ == "__main__":
    input_asm_file = input("Enter .asm file path: ")
    output_bin_file = input("Enter output .bin file path: ")
    assemble(input_asm_file, output_bin_file)
