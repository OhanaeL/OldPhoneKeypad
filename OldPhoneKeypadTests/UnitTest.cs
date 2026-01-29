using Xunit;
using OldPhoneKeypad;

public class OldPhoneKeypadTests
{
    // ==================== Normal Cases ====================

    [Fact]
    public void Test_SinglePress()
    {
        string input = "2#";
        string expected = "A";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_TriplePress()
    {
        string input = "222#";
        string expected = "C";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_CombinedWithPause()
    {
        string input = "222 2 22#";
        string expected = "CAB";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultipleLetters()
    {
        string input = "2 2 2#";
        string expected = "AAA";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_WithSpaceCharacter()
    {
        string input = "999666880277733#";
        string expected = "YOU ARE";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_TURING()
    {
        string input = "8 88777444666*664#";
        string expected = "TURING";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_HELLO_WithZero()
    {
        string input = "44 33 555 555 6660#";
        string expected = "HELLO ";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_HELLO_WORLD()
    {
        string input = "4433555 555666096667775553#";
        string expected = "HELLO WORLD";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_LongCycle()
    {
        string input = "777777777#"; // key 7 has 4 letters, pressed 9 times
        string expected = "P";       // (9-1) % 4 = 0 → P
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    // ==================== Edge Cases ====================

    [Fact]
    public void Test_EmptyInput()
    {
        string input = "#";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_NoHashAtEnd()
    {
        string input = "4433";
        string expected = "HE";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => PhoneKeypadDecoder.OldPhonePad(null!));
    }

    // ==================== Backspace Behavior ====================

    [Fact]
    public void Test_Backspace()
    {
        string input = "22*2#";
        string expected = "A";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceInMiddle()
    {
        string input = "4433555*#";
        string expected = "HE";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceAtStart()
    {
        string input = "*2#";
        string expected = "A";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultipleBackspaces()
    {
        string input = "7777333 **#";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_OnlyBackspaces()
    {
        string input = "***#";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_BackspaceAfterLetter()
    {
        string input = "2*#";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_ZeroAndBackspace()
    {
        string input = "2 20*2#"; // 2 → A, space, 2 → A, 0 → " ", * deletes space, 2 → A
        string expected = "AAA";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    // ==================== Pauses / Zero / Invalid Input ====================

    [Fact]
    public void Test_Space()
    {
        string input = "2 2#";
        string expected = "AA";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_PauseSingleDigit()
    {
        string input = "2 3#";
        string expected = "AD";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MultiplePauses()
    {
        string input = "2   22#";
        string expected = "AB";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_SpacesAtEnd()
    {
        string input = "22 2  ";
        string expected = "BA";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    // ==================== Additional Edge Cases ====================

    [Fact]
    public void Test_CyclingProducesSameOutput()
    {
        string input1 = "222333#";         // 2->A, 22->B, 222->C, 3->D, 33->E, 333->F => CF
        string input2 = "222222333333#";   // 2 six times (cycles: C), 3 six times (cycles: F)
        string expected = "CF";
        string result1 = PhoneKeypadDecoder.OldPhonePad(input1);
        string result2 = PhoneKeypadDecoder.OldPhonePad(input2);
        Assert.Equal(expected, result1);
        Assert.Equal(expected, result2);
    }

    [Fact]
    public void Test_VeryLongInput_Performance()
    {
        string input = new string('2', 1000) + "#";
        string expected = "A"; // pressed 1000 times cycles through A, B, C, so ends with A
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_OnlySpecialCharacters()
    {
        string input = "***   ##";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_MixedValidAndInvalid()
    {
        string input = "2a2b222#";
        Assert.Throws<ArgumentException>(() => PhoneKeypadDecoder.OldPhonePad(input));
    }

    [Fact]
    public void Test_ExcessiveBackspaces()
    {
        string input = "22***#";
        string expected = "";
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_OnlyZeros()
    {
        string input = "0000#";
        string expected = "    "; // old phones interpreted multiple 0 in sequence as multiple spaces
        string result = PhoneKeypadDecoder.OldPhonePad(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_OnlyInvalidCharacters()
    {
        string input = "abc!@#";
        Assert.Throws<ArgumentException>(() => PhoneKeypadDecoder.OldPhonePad(input));
    }
}
