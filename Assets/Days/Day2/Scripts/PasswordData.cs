using UnityEngine;

namespace AdventOfCode.DayTwo
{
  public class PasswordData
  {
    public char character;
    public int indexOne;
    public int indexTwo;
    public string password;
    public PasswordData(string input)
    {
      string[] split = input.Split(' ');
      string[] lengths = split[0].Split('-');
      indexOne = int.Parse(lengths[0]);
      indexTwo = int.Parse(lengths[1]);
      character = split[1][0];
      password = split[2];
    }

    public bool IsValidStepOne(out int lowerRange, out int higherRange)
    {
      int count = password.Length - password.Replace(character.ToString(), "").Length;
      lowerRange = indexOne - count;
      higherRange = indexTwo - count;
      return lowerRange <= 0 && higherRange >= 0;
    }

    public bool IsValidStepTwo(out bool firstValid, out bool secondValid)
    {
      firstValid = CheckCharacter(indexOne);
      secondValid = CheckCharacter(indexTwo);
      return firstValid ^ secondValid;
    }

    private bool CheckCharacter(int index)
    {
      int indexFixed = index - 1;
      if (indexFixed < 0 || indexFixed >= password.Length)
      {
        return false;
      }
      return password[indexFixed] == character;
    }
  }
}
