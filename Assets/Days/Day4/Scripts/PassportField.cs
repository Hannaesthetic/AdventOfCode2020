﻿using System.Text.RegularExpressions;
using UnityEngine;

namespace AdventOfCode.DayFour
{
  [System.Serializable]
  public class PassportField
  {
    /*
     * Regexes so that they appear in the code, not just a serialized scene thing
     * Hair color: \A#[0-9a-f]{6}\Z
     * Eye Color: \A(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)\Z
     * Passport ID: \A[0-9]{9}\Z
     * 
     * Everything else is a Range, a None, or a Height requirement
     */

    public string Name;
    public string FieldID;
    public RequirementType Requirement;
    public string RegexPattern;
    public Vector2Int Range;
    public Vector2Int SecondRange;

    private string content;

    public PassportField()
    {

    }

    public PassportField(PassportField copy)
    {
      Name = copy.Name;
      FieldID = copy.FieldID;
      Requirement = copy.Requirement;
      RegexPattern = copy.RegexPattern;
      Range = copy.Range;
      SecondRange = copy.SecondRange;
    }

    public string GetContent()
    {
      return content;
    }

    public bool IsValid()
    {
      switch(Requirement)
      {
        case RequirementType.None: return true;
        case RequirementType.Regex: return !string.IsNullOrEmpty(content) && Regex.IsMatch(content, RegexPattern);
        case RequirementType.Range: return CheckRange();
        case RequirementType.Height: return CheckHeight();
      }
      return false;
    }

    private bool CheckRange() => CheckSpecificRange(Range, content);

    private bool CheckHeight()
    {
      if (content.EndsWith("cm"))
      {
        string heightString = content.Substring(0, content.Length - 2);
        return CheckSpecificRange(Range, heightString);
      } else if (content.EndsWith("in"))
      {
        string heightString = content.Substring(0, content.Length - 2);
        return CheckSpecificRange(SecondRange, heightString);
      }
      return false;
    }

    private bool CheckSpecificRange(Vector2Int range, string text)
    {
      if (int.TryParse(text, out int value))
      {
        return value >= range.x && value <= range.y;
      }
      return false;
    }

    public void SetContent(string newContent)
    {
      content = newContent;
    }
  }

  public enum RequirementType
  {
    None, Regex, Range, Height
  }
}