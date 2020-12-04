using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayFour
{
  public class SolutionDayFour : MonoBehaviour
  {
    public static float SimulationSpeed;
    
    public PassportField[] Fields;
    [TextAreaAttribute(3, 10)] public string UnparsedData;
    public VisualiserDayFour Visualiser;
    public float SimSpeed = 1f;
    public float CheckInterval = 5f;

    private string[] splitUnparsedData;
    private List<Passport> passports;
    private int validPassports;

    private int nextIndex;
    private float timeToNextCheck;
    private bool done;


    private void Start()
    {
      Setup();
    }

    private void Update()
    {
      if (done)
      {
        return;
      }
      SimulationSpeed = SimSpeed;
      timeToNextCheck += SimSpeed * Time.deltaTime;
      if (nextIndex >= splitUnparsedData.Length) {
        done = true;
        Visualiser.SetVisualProgress(1f);
      }
      else
      {
        while (timeToNextCheck >= CheckInterval)
        {
          timeToNextCheck -= CheckInterval;
          CheckNext();
        }
        Visualiser.SetVisualProgress(timeToNextCheck / CheckInterval);
      }
    }

    [ContextMenu("Setup")]
    private void Setup()
    {
      SplitData();
      SetupCheck();
    }

    [ContextMenu("Check Next")]
    private void CheckNext()
    {
      CheckEntry(nextIndex++);
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

    [ContextMenu("CheckAll")]
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
      if (Visualiser != null)
      {
        Visualiser.UpdateContent(passport, splitUnparsedData[i]);
      }
      
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