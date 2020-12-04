
using AdventOfCode.Util;
using System;
using UnityEngine;


namespace AdventOfCode.DayFour
{
  public class Passport
  {
    public PassportField[] Fields;

    public Passport(PassportField[] assignedFields, string data)
    {
      SetFields(assignedFields);
      ParseData(data);
    }

    public void SetFields(PassportField[] assignedFields)
    {
      Fields = new PassportField[assignedFields.Length];
      for (int i = 0; i < assignedFields.Length; i++)
      {
        Fields[i] = new PassportField(assignedFields[i]);
      }
    }

    public void ParseData(string unparsed)
    {
      unparsed = unparsed.Replace(Environment.NewLine, " ");
      string[] fields = unparsed.Split(' ');
      foreach (string field in fields)
      {
        string[] sections = field.Split(':');
        foreach (PassportField passportField in Fields)
        {
          if (passportField.FieldID.Equals(sections[0]))
          {
            passportField.SetContent(sections[1]);
          }
        }
      }
    }

    public bool IsValid()
    {
      foreach (PassportField passportField in Fields)
      {
        if (!passportField.IsValid()) {
          return false;
        }
      }
      return true;
    }
  }
}