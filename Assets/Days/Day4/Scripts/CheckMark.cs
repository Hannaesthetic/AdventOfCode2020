using UnityEngine;

namespace AdventOfCode.DayFour
{
  public class CheckMark : MonoBehaviour
  {
    public AnimationCurve Curve;
    public GameObject Positive;
    public GameObject Negative;
    public float AppearTime;
    public Vector2 AppearDelayVariation;

    private bool showing;
    private float showAmount;

    public void SetVisible(bool positive)
    {
      Positive.SetActive(positive);
      Negative.SetActive(!positive);
      showing = true;
      showAmount = Random.Range(AppearDelayVariation.x, AppearDelayVariation.y);
    }

    public void Hide()
    {
      showing = false;
      showAmount = 0f;
    }

    private void Update()
    {
      if (showing && showAmount < 1f)
      {
        showAmount = Mathf.Min(showAmount + Time.deltaTime * SolutionDayFour.SimulationSpeed / AppearTime, 1f);
      }
      transform.localScale = Vector3.one * Curve.Evaluate(Mathf.Clamp01(showAmount));
    }
  }
}