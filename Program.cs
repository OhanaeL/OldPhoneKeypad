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
                AppendCharacter(output, sequence);
                break;
            }

            if (c == ' ')
            {
                AppendCharacter(output, sequence);
                continue;
            }

            if (c == '*')
            {
                AppendCharacter(output, sequence);
                RemoveCharacter(output);
                continue;
            }

            if (sequence.Length > 0 && sequence[0] != c)
                AppendCharacter(output, sequence);

            sequence.Append(c);
        }

        return output.ToString();
    }

    public static void AppendCharacter(StringBuilder output, StringBuilder sequence)
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
    }

    public static void RemoveCharacter(StringBuilder output)
    {
        if (output.Length > 0)
            output.Length--;
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
        Console.WriteLine(OldPhonePad("2#"));                    // A
        Console.WriteLine(OldPhonePad("222#"));                  // C
        Console.WriteLine(OldPhonePad("2222#"));                 // A
        Console.WriteLine(OldPhonePad("2 2 2#"));                // AAA
        Console.WriteLine(OldPhonePad("6665553#"));              // OLD
        Console.WriteLine(OldPhonePad("4433555*#"));             // HE
        Console.WriteLine(OldPhonePad("*2#"));                   // A
        Console.WriteLine(OldPhonePad("999666880277733#"));      // YOU ARE
        Console.WriteLine(OldPhonePad("77773333**#"));           // ""
        Console.WriteLine(OldPhonePad("8 88777444666*664#"));    // TURING
        Console.WriteLine(OldPhonePad("44 33 555 555 6660#"));   // HELLO
        Console.WriteLine(OldPhonePad("4433555 555666096667775553#")); // HELLO WORLD

    }
}
