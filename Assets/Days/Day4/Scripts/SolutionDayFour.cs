using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayFour
{
  public class SolutionDayFour : MonoBehaviour
  {
    public PassportField[] Fields;
    [TextAreaAttribute(3, 10)] public string UnparsedData;

    private string[] splitUnparsedData;
    private List<Passport> passports;
    private int validPassports;

    private int nextIndex;


    [ContextMenu("CheckAll")]
    private void Solve()
    {
      SplitData();
      SetupCheck();
      CheckAllPassports();
    }

    private void SplitData()
    {
      splitUnparsedData = UnparsedData.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None);
      Debug.Log($"Split data into {splitUnparsedData.Length}");
    }

    private void SetupCheck()
    {
      passports = new List<Passport>();
      validPassports = 0;
      nextIndex = 0;
    }

    private void CheckAllPassports()
    {
      for (int i = 0; i < splitUnparsedData.Length; i++)
      {
        CheckEntry(i);
      }
      Debug.Log($"Finished, found {validPassports} valid");
    }

    private void CheckEntry(int i)
    {
      Passport passport = GeneratePassport(splitUnparsedData[i]);
      CheckPassport(passport);
    }

    private Passport GeneratePassport(string data)
    {
      Passport passport = new Passport(Fields, data);
      passports.Add(passport);
      return passport;
    }

    private void CheckPassport(Passport passport)
    {
      if (passport.IsValid())
      {
        validPassports++;
      }
    }
  }
}