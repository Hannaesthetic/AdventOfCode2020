using UnityEngine;

namespace AdventOfCode.DayTwo
{
  public class PasswordData
  {
    public char character;
    public int minLength;
    public int maxLength;
    public string password;
    public PasswordData(string input)
    {
      string[] split = input.Split(' ');
      string[] lengths = split[0].Split('-');
      minLength = int.Parse(lengths[0]);
      maxLength = int.Parse(lengths[1]);
      character = split[1][0];
      password = split[2];
    }

    public bool IsValid(out int lowerRange, out int higherRange)
    {
      int count = password.Length - password.Trim(character).Length;
      lowerRange = minLength - count;
      higherRange = maxLength - count;
      return lowerRange <= 0 && higherRange >= 0;
    }
  }
}
