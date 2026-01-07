# Old Phone Keypad Decoder

A C# program that converts old phone keypad input (like T9 texting) into readable text.

## Problem Description

Simulate the text input behavior of old phone keypads where you had to press number keys multiple times to type letters AKA [Multi-tap](https://en.wikipedia.org/wiki/Multi-tap).

## Project Structure

```text
OldPhoneKeypad/
├── OldPhoneKeypad.sln                      # solution file
├── README.md                             # this file
├── OldPhoneKeypadProject/                  # main application
│   ├── Program.cs                        # contains PhoneKeypadDecoder Class
│   ├── OldPhoneKeypadProject.csproj
│   ├── bin/
│   └── obj/
└── OldPhoneKeypadTests/                    # unit tests (xUnit)
    ├── UnitTest.cs                       # comprehensive test suite
    ├── OldPhoneKeypadTests.csproj
    ├── bin/
    └── obj/
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Build the Solution

```bash
dotnet build IronSoftTask.sln
```

### Run the Application

```bash
dotnet run --project IronSoftTaskProject/IronSoftTaskProject.csproj
```

Or navigate to the project directory:

```bash
cd IronSoftTaskProject
dotnet run
```

**Example Usage:**

```text
Enter keypad input: 4433555 555666#
Output: HELLO
```

### Run Tests

Run all tests:

```bash
dotnet test
```

Run tests from the test project directory:

```bash
cd IronSoftTaskTests
dotnet test
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

## AI Assistance

What AI helped with:

1. Code improvements - XML documentation, input validation, access modifiers
2. Test expansion - Edge case identification and comprehensive test coverage
3. Documentation - README structure, architectural explanations, complexity analysis
4. Best practices - Error handling patterns, .NET conventions, maintainability

**Chat Transcript:** [View full conversation](https://chatgpt.com/share/695dea35-6e90-8011-a318-ec71e568b782)
