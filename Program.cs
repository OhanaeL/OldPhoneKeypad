using System.Text;

public class Program
{
    // Dictionary for mapping digits to letters
    static Dictionary<char, string> NumToLetter = new Dictionary<char, string>
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


    public static string OldPhonePad(string input)
    {
        StringBuilder output = new StringBuilder();
        StringBuilder sequence = new StringBuilder();

        foreach (char c in input)
        {
            if (sequence.Length == 0 || sequence[0] == c)
            {
                sequence.Append(c);
            }
            else
            {
                char key = sequence[0];
                if (NumToLetter.ContainsKey(key))
                {
                    string letters = NumToLetter[key];
                    int index = (sequence.Length - 1) % letters.Length; // % to account for the letters cycling
                    output.Append(letters[index]);
                }

                // Start a new sequence
                sequence.Clear();
                sequence.Append(c);

                if (c == '#')
                    break;

                // "*" indicates backspace
                if (c == '*')
                {
                    if (output.Length > 0)
                        output.Length--;
                    continue;
                }

            }
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
