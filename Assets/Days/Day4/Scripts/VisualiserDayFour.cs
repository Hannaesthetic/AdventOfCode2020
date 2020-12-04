using AdventOfCode.Util;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AdventOfCode.DayFour
{
  public class VisualiserDayFour : MonoBehaviour
  {
    public TextMeshProUGUI[] Fields;
    public CheckMark[] FieldChecks;
    public TextMeshProUGUI InputField; 
    public CheckMark FinalCheck;
    public BouncyText Correct;
    public BouncyText Incorrect;
    public float ScoreDelay;
    public Animator Animator;

    private int correctCount;
    private int incorrectCount;

    private void Update()
    {
      Animator.speed = SolutionDayFour.SimulationSpeed;
    }

    public void UpdateContent(Passport passport, string input)
    {
      for (int i = 0; i < passport.Fields.Length; i++)
      {
        Fields[i].SetText(passport.Fields[i].GetContent());
        FieldChecks[i].SetVisible(passport.Fields[i].IsValid());
      }
      InputField.SetText(input);

      bool isValid = passport.IsValid();
      FinalCheck.SetVisible(isValid);
      StartCoroutine(c_UpdateScore(isValid));
      Animator.SetTrigger("Play");
    }

    public void SetVisualProgress(float progress)
    {
      Animator.Play("PassportAnimator", 0, progress);
    }

    private IEnumerator c_UpdateScore(bool positive)
    {
      yield return new WaitForSeconds(ScoreDelay / SolutionDayFour.SimulationSpeed);
      if (positive)
      {
        correctCount++;
        Correct.SetText(correctCount.ToString());
      } else
      {
        incorrectCount++;
        Incorrect.SetText(incorrectCount.ToString());
      }
    }
  }
}
