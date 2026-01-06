using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    // Dictionary for mapping digits to letters
    static Dictionary<char, string> NumToLetter = new Dictionary<char, string>
        {
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

    public static string OldPhonePad(string input)
    {
        StringBuilder output = new StringBuilder();
        StringBuilder sequence = new StringBuilder();

        foreach (char c in input)
        {
            if (c == '#')
            {
                if (sequence.Length > 0)
                {
                    char key = sequence[0];
                    if (NumToLetter.TryGetValue(key, out var letters))
                    {
                        int index = (sequence.Length - 1) % letters.Length;  // % to account for the letters cycling
                        output.Append(letters[index]);
                    }
                    sequence.Clear();
                }
                break;
            }

            if (c == ' ')
            {
                if (sequence.Length > 0)
                {
                    char key = sequence[0];
                    if (NumToLetter.TryGetValue(key, out var letters))
                    {
                        int index = (sequence.Length - 1) % letters.Length;  // % to account for the letters cycling
                        output.Append(letters[index]);
                    }
                    sequence.Clear();
                }
                continue;
            }

            if (c == '*')
            {
                if (sequence.Length > 0)
                {
                    char key = sequence[0];
                    if (NumToLetter.TryGetValue(key, out var letters))
                    {
                        int index = (sequence.Length - 1) % letters.Length;  // % to account for the letters cycling
                        output.Append(letters[index]);
                    }
                    sequence.Clear();
                }

                if (output.Length > 0)
                    output.Length--;

                continue;
            }

            if (sequence.Length > 0 && sequence[0] != c)
            {
                char key = sequence[0];
                if (NumToLetter.TryGetValue(key, out var letters))
                {
                    int index = (sequence.Length - 1) % letters.Length; // % to account for the letters cycling
                    output.Append(letters[index]);
                }
                sequence.Clear();
            }

            sequence.Append(c);
        }

        return output.ToString();
    }

    public static void Main(string[] args)
    {
        string input = "4433555 555666#";
        string result = OldPhonePad(input);
        Console.WriteLine($"OldPhonePad result: {result}");
        Test();
    }
    public static void Test()
    {
        // Test cases
        Console.WriteLine(OldPhonePad("33#"));
        Console.WriteLine(OldPhonePad("227*#"));
        Console.WriteLine(OldPhonePad("4433555 555666#"));
        Console.WriteLine(OldPhonePad("8 88777444666*664#"));
    }
}
