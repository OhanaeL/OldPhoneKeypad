# Old Phone Keypad Decoder

A C# program that converts old phone keypad input (like T9 texting) into readable text.

## Problem Description

Simulate the text input behavior of old phone keypads where you had to press number keys multiple times to type letters AKA [Multi-tap](https://en.wikipedia.org/wiki/Multi-tap).

## Project Structure

```text
IronSoftTask/
├── IronSoftTask.sln                      # solution file
├── README.md                             # this file
├── IronSoftTaskProject/                  # main application
│   ├── Program.cs                        # contains PhoneKeypadDecoder Class
│   ├── IronSoftTaskProject.csproj
│   ├── bin/
│   └── obj/
└── IronSoftTaskTests/                    # unit tests (xUnit)
    ├── UnitTest.cs                       # comprehensive test suite
    ├── IronSoftTaskTests.csproj
    ├── bin/
    └── obj/
```

## Program Design

### Architecture

`PhoneKeypadDecoder` - Core decoding logic, independent of UI
`Program` - Console I/O wrapper

### Assumptions

- Input is a sequence of digits.
- ' '(space) indicates a pause, instead of an actual space in output.
- '#' character indicates the end of a message, ignoring the rest of the input.

### Full Process

Two buffers:

- `output` - Final decoded message
- `sequence` - Tracks consecutive presses of same digit

Two helper methods:

- `AppendCharacter` - Append character using sequence as an input to the end of the output
- `RemoveCharacter`- Remove character from the end of the output

Processing Flow:

- Repeated digit - Accumulate in sequence
- Different digit - Convert the accumulated sequence of previous digits to character using modulo cycling, append to output
- ` ` (Space) - Pause, finalize accumulated sequence
- `*` - Finalize accumulated sequence, backspace last character
- `#` - Finalize accumulated sequence, return result

### Complexity

- Time: O(n) - Single pass
- Space: O(n) - Output scales with input
