using Xunit;
using IronSoftTaskProject;

public class IronSoftTaskUnitTests
{
    [Fact]
    public void Test_Backspace()
    {
        string input = "22*2#";
        string expected = "A";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_Space()
    {
        string input = "2 2#";
        string expected = "AA";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_SinglePress()
    {
        string input = "2#";
        string expected = "A";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_TriplePress()
    {
        string input = "222#";
        string expected = "C";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_CombinedWithPause()
    {
        string input = "222 2 22#";
        string expected = "CAB";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultipleLetters()
    {
        string input = "2 2 2#";
        string expected = "AAA";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceInMiddle()
    {
        string input = "4433555*#";
        string expected = "HE";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceAtStart()
    {
        string input = "*2#";
        string expected = "A";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WithSpaceCharacter()
    {
        string input = "999666880277733#";
        string expected = "YOU ARE";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultipleBackspaces()
    {
        string input = "7777333 **#";
        string expected = "";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_TURING()
    {
        string input = "8 88777444666*664#";
        string expected = "TURING";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_HELLO_WithZero()
    {
        string input = "44 33 555 555 6660#";
        string expected = "HELLO ";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_HELLO_WORLD()
    {
        string input = "4433555 555666096667775553#";
        string expected = "HELLO WORLD";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void Test_EmptyInput()
    {
        string input = "#";
        string expected = "";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_OnlyBackspaces()
    {
        string input = "***#";
        string expected = "";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceAfterLetter()
    {
        string input = "2*#";
        string expected = "";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_PauseSingleDigit()
    {
        string input = "2 3#";
        string expected = "AD";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultiplePauses()
    {
        string input = "2   22#";
        string expected = "AB";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_LongCycle()
    {
        string input = "777777777#"; // key 7 has 4 letters, pressed 9 times
        string expected = "P";       // 9 % 4 = 1 → P
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ZeroAndBackspace()
    {
        string input = "2 20*2#"; // 2 → A, space, 2 → A, 0 → " ", * deletes space, 2 → A
        string expected = "AAA";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_InvalidCharactersIgnored()
    {
        string input = "2a3!#"; // letters and punctuation ignored
        string expected = "AD";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_NoHashAtEnd()
    {
        string input = "4433";
        string expected = "HE";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_SpacesAtEnd()
    {
        string input = "22 2  ";
        string expected = "BA";
        string result = Program.OldPhonePad(input);
        Assert.Equal(expected, result);
    }
}
