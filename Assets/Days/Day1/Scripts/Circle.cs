using Shapes;
using TMPro;
using UnityEngine;

namespace AdventOfCode.DayOne
{
  public class Circle : MonoBehaviour
  {
    public Disc disc;
    public TextMeshProUGUI NumberText;
    public float MoveTime;
    public AnimationCurve AppearCurve;

    public int value { get; private set; }
    public Vector2 AppearTimeRange;
    public Vector2 AppearDurationRange;

    private Vector3 targetPosition;
    private Vector3 velocity;
    private float appearTime;
    private float appearDuration;
    private float spawnTime;

    private void Start()
    {
      spawnTime = Time.time;
      appearTime = Random.Range(AppearTimeRange.x, AppearTimeRange.y);
      appearDuration = Random.Range(AppearDurationRange.x, AppearDurationRange.y);
      UpdateScale();
    }

    private void Update()
    {
      UpdateScale();
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, MoveTime);
    }

    private void UpdateScale()
    {
      float radius = Mathf.Clamp01(Mathf.InverseLerp(appearTime, appearTime + appearDuration, Time.time - spawnTime));
      transform.localScale = Vector3.one * AppearCurve.Evaluate(radius);
    }

    public void SetValue(int newValue)
    {
      value = newValue;
      NumberText.SetText(newValue.ToString());
    }

    public void SetPosition(Vector3 position, bool instant)
    {
      targetPosition = position;
      if (instant)
      {
        transform.position = position;
      }
    }
  }
}
