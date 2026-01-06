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
}
