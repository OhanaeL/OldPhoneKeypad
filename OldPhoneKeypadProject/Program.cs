using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhoneKeypad
{
    /// <summary>
    /// PhoneKeypadDecoder provides functionality to decode old phone keypad input into text.
    ///
    /// Allowed input characters:
    ///   - Digits 0-9 (for letters and space)
    ///   - Space (' ') for pause between same-key presses
    ///   - '*' for backspace
    ///   - '#' to send/finish input
    /// Any other character is ignored and treated as a sequence break (like a pause).
    /// </summary>
    public class PhoneKeypadDecoder
    {
        // dictionary for mapping digits to letters
        private static readonly Dictionary<char, string> NumberKeypad = new()
        {
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };
        private static readonly HashSet<char> ValidChars = [.. NumberKeypad.Keys.Concat(['0', ' ', '*', '#'])];
        private const char PAUSE = ' ';
        private const char BACKSPACE = '*';
        private const char SEND = '#';
        private const char SPACE = '0';

        /// <summary>
        /// Decodes a string of old phone keypad input into a text message.
        /// </summary>
        /// <remarks>
        /// <para>- Digits 2-9 map to letters (2 → ABC, 3 → DEF, etc.).</para>
        /// <para>- Digit 0 maps to a space character.</para>
        /// <para>- Repeated presses cycle through the letters (e.g., "222" → "C").</para>
        /// <para>- ' ' (space) indicates a pause to separate repeated keys.</para>
        /// <para>- '*' deletes the last character.</para>
        /// <para>- '#' ends the input sequence.</para>
        /// <para></para>
        /// Examples:
        /// <para>- OldPhonePad("4433555 555666#") => "HELLO"</para>
        /// <para>- OldPhonePad("8 88777444666*664#") => "TURING"</para>
        /// </remarks>
        /// <param name="input">The string containing keypad input characters.</param>
        /// <returns>The decoded text message.</returns>
        public static string OldPhonePad(string input)
        {

            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (!IsValidInput(input))
                throw new ArgumentException($"{nameof(input)} contains invalid characters.", input);

            StringBuilder output = new();
            StringBuilder sequence = new();

            foreach (char c in input)
            {
                switch (c)
                {
                    case PAUSE:
                        // ' ' indicates a pause, finalize any ongoing sequence and move on
                        AppendCharacter(output, sequence);
                        continue;

                    case BACKSPACE:
                        // '*' indicates a backspace, finalize any ongoing sequence and remove the last character from output
                        AppendCharacter(output, sequence);
                        RemoveCharacter(output);
                        continue;

                    case SPACE:
                        // '0' indicates a space character, finalize any ongoing sequence and append space
                        AppendCharacter(output, sequence);
                        output.Append(' ');
                        continue;

                    case SEND:
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

            // backup, in case there's no '#' at the end
            AppendCharacter(output, sequence);
            return output.ToString();
        }

        /// <summary>
        /// Checks if the input contains only valid keypad characters.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <returns>True if input is valid; otherwise, false.</returns>
        private static bool IsValidInput(string input)
        {
            // Only checks for invalid characters; null is handled separately
            return input.All(c => ValidChars.Contains(c));
        }

        /// <summary>
        /// Appends the character represented by the current sequence to the output.
        /// </summary>
        /// <param name="output">The output StringBuilder.</param>
        /// <param name="sequence">The current sequence of identical digits.</param>
        private static void AppendCharacter(StringBuilder output, StringBuilder sequence)
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
        /// <param name="output">The output StringBuilder.</param>
        private static void RemoveCharacter(StringBuilder output)
        {
            if (output.Length > 0)
                output.Length--;
        }
    }

    public class Program
    {
        public static void Main(string[] _)
        {
            Console.WriteLine("Enter keypad input (digits 0-9, space, *, #):");
            string input = Console.ReadLine() ?? string.Empty;
            string result = PhoneKeypadDecoder.OldPhonePad(input);
            Console.WriteLine(result);
        }
    }
}
