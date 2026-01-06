using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    // dictionary for mapping digits to letters
    static Dictionary<char, string> NumberKeypad = new Dictionary<char, string>
        {
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" },
            { '0', " " }
        };

    /// <summary>
    /// Decodes a string of old phone keypad input into a text message.
    /// Rules: Digits 2-9 map to letters, 0 = space, * = backspace, # = end, space = pause.
    /// </summary>
    /// <param name="input">The string containing keypad input characters.</param>
    /// <returns>The decoded text message.</returns>
    public static string OldPhonePad(string input)
    {
        StringBuilder output = new StringBuilder();
        StringBuilder sequence = new StringBuilder();

        foreach (char c in input)
        {
            switch (c)
            {
                case ' ':
                    // ' ' indicates a pause, finalize any ongoing sequence and move on
                    AppendCharacter(output, sequence);
                    continue;

                case '*':
                    // '*' indicates a backspace, finalize any ongoing sequence and remove the last character from output
                    AppendCharacter(output, sequence);
                    RemoveCharacter(output);
                    continue;

                case '#':
                    // '#' indicates the end of input, finalize any ongoing sequence and return the output
                    AppendCharacter(output, sequence);
                    return output.ToString();

                default:
                    // for non-special characters, check if they are the same as the current sequence
                    if (sequence.Length > 0 && c != sequence[0])
                        AppendCharacter(output, sequence);
                    sequence.Append(c);
                    break;
            }
        }

        return output.ToString(); // backup, in case there's no '#' at the end
    }

    /// <summary>
    /// Appends the character represented by the current sequence to the output.
    /// </summary>
    public static void AppendCharacter(StringBuilder output, StringBuilder sequence)
    {
        if (sequence.Length > 0)
        {
            char key = sequence[0];
            if (NumberKeypad.TryGetValue(key, out var letters))
            {
                // % to account for the letters cycling
                int index = (sequence.Length - 1) % letters.Length;
                output.Append(letters[index]);
            }
            sequence.Clear();
        }
    }

    /// <summary>
    /// Removes the last character from the output, if any.
    /// </summary>
    public static void RemoveCharacter(StringBuilder output)
    {
        if (output.Length > 0)
            output.Length--;
    }

    public static void Main(string[] _)
    {
        Console.Write("Enter keypad input:");
        string input = Console.ReadLine() ?? string.Empty;
        string result = OldPhonePad(input);
        Console.WriteLine(result);
    }
}