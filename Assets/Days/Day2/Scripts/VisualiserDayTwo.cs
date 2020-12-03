using AdventOfCode.Util;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayTwo
{

  public class VisualiserDayTwo : MonoBehaviour
  {
    public GameObject BoxPrefab;
    public float DiscardStart;
    public float DiscardHeight;
    public AnimationCurve DiscardCurve;
    public float RailSpeed;

    public BouncyText LowText;
    public BouncyText HighText;
    public BouncyText CorrectText;

    public float RailPosition;

    private List<DataBox> Boxes = new List<DataBox>();
    private bool isScrolling;
    private int lowCount;
    private int highCount;
    private int correctCount;

    private void Update()
    {
      if (isScrolling)
      {
        RailPosition += RailSpeed * Time.deltaTime;
      }
    }

    public void ToggleScroll()
    {
      isScrolling = !isScrolling;
    }

    public void AddBox(int min, int max)
    {
      GameObject obj = Instantiate(BoxPrefab);
      DataBox box = obj.GetComponent<DataBox>();
      box.SetData(min, max, Boxes.Count, this);
      Boxes.Add(box);
    }

    public float GetOffset(float offsetX)
    {
      float incorrectOffset = Mathf.Clamp01(Mathf.InverseLerp(0f, DiscardStart, offsetX - RailPosition));
      incorrectOffset = DiscardCurve.Evaluate(1f - incorrectOffset) * DiscardHeight;
      return incorrectOffset;
    }

    public bool TryCount(int side, float offset)
    {
      if (offset - RailPosition <= 0f)
      {
        Increment(side);
        return true;
      }
      return false;
    }

    private void Increment(int side)
    {
      switch(side) {
        case -1: IncrementText(HighText, ref highCount); break;
        case 0: IncrementText(CorrectText, ref correctCount); break;
        case 1: IncrementText(LowText, ref lowCount); break;
      }
    }

    private void IncrementText(BouncyText text, ref int count)
    {
      count++;
      text.SetText(count.ToString());
    }
  }
}