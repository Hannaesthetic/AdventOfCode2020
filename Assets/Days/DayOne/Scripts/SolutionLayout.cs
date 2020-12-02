using Shapes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AdventOfCode.DayOne
{
  public class SolutionLayout : MonoBehaviour
  {
    public GameObject CirclePrefab;
    public float Radius;
    public float RadiusVariance;
    public int LayerDistance = 4;
    public Disc ProgressDisc;
    public TextMeshProUGUI FirstNumber;
    public TextMeshProUGUI SecondNumber;
    public TextMeshProUGUI Sum;

    public Vector2 SmoothSpeeds;
    public Color SelectedColor;
    public Color UnselectedColor;
    
    private List<Circle> circles;
    private float targetMin;
    private float targetMax;
    private float smoothMinVelocity;
    private float SmoothMaxVelocity;

    private int firstSelected = -1;
    private int secondSelected = -1;

    private void Start()
    {
      ProgressDisc.AngRadiansEnd = targetMax = Mathf.PI * 2f;
    }

    private void Update()
    {
      ProgressDisc.AngRadiansStart = Mathf.SmoothDamp(ProgressDisc.AngRadiansStart, targetMin, ref smoothMinVelocity, SmoothSpeeds.x);
      ProgressDisc.AngRadiansEnd = Mathf.SmoothDamp(ProgressDisc.AngRadiansEnd, targetMax, ref SmoothMaxVelocity, SmoothSpeeds.y);
    }

    public void UpdateSelected(int first, int second)
    {
      if (first != firstSelected)
      {
        if (firstSelected >= 0)
        {
          circles[firstSelected].disc.Color = UnselectedColor;
        }
        firstSelected = first;
        circles[first].disc.Color = SelectedColor;
      }
      if (second != secondSelected)
      {
        if (secondSelected >= 0)
        {
          circles[secondSelected].disc.Color = UnselectedColor;
        }
        secondSelected = second;
        circles[second].disc.Color = SelectedColor;
      }
    }

    public List<Circle> BuildCircles(int[] numbers)
    {
      circles = new List<Circle>();
      foreach (int num in numbers)
      {
        GameObject obj = Instantiate(CirclePrefab);
        Circle newCircle = obj.GetComponent<Circle>();
        newCircle.SetValue(num);
        circles.Add(newCircle);
      }
      UpdateCirclePositions(true);
      return circles;
    }

    public void SetCheckedPairs(int min, int max)
    {
      targetMin = GetAngle(min);
      targetMax = GetAngle(max);
    }

    public void SetSumNumbers(int min, int max, int sum)
    {
      FirstNumber.SetText(min.ToString());
      SecondNumber.SetText(max.ToString());
      Sum.SetText(sum.ToString());
    }

    public void UpdateCirclePositions(bool instant)
    {
      for (int i = 0; i < circles.Count; i++)
      {
        float angle = GetAngle(i);
        //int layers = LayerDistance * LayerDistance + 1;
        //int circleLayer = ((i % layers) * LayerDistance) % layers;
        int circleLayer = new int[]{ 3, 7, 1, 5, 9, 2, 6, 0, 4, 8}[i % 10];
        float radiusOffset = RadiusVariance * circleLayer / 10f;
        float individualRadius = Radius + radiusOffset;
        Vector3 position = new Vector3(Mathf.Cos(angle) * individualRadius, Mathf.Sin(angle) * individualRadius, 0f);
        circles[i].SetPosition(position, instant);
      }
    }
    private float GetAngle(int index)
    {
      return Mathf.PI * 2f * ((float)index / circles.Count + 0.25f);
    }
  }

}
