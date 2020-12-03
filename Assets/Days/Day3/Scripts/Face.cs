using AdventOfCode.Util;
using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayThree
{
  public class Face : MonoBehaviour
  {
    public float MoveSpeed;
    public float MoodSpeed;
    public float CheerSpeed;


    private float smile = 1f;
    public Disc MainDisc;
    public Line[] lines;

    public VisualiserDayThree Visualiser;
    public BouncyText text;

    private Vector3 targetPosition;
    private Vector3 velocity;
    private float targetMood;
    private float moodVelocity;

    private void Update()
    {
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, MoveSpeed, Mathf.Infinity, Time.deltaTime * VisualiserDayThree.SimulationSpeed);

      targetMood = Mathf.Clamp(targetMood + CheerSpeed * Time.deltaTime, -1f, 1f);
      smile = Mathf.SmoothDamp(smile, targetMood, ref moodVelocity, MoodSpeed, Mathf.Infinity, Time.deltaTime * VisualiserDayThree.SimulationSpeed);

      lines[1].Start = lines[0].End = new Vector2(-0.1f, -0.06f * smile);
      lines[2].End = lines[1].End = new Vector2(0, -0.08f * smile);
      lines[2].Start = lines[3].End = new Vector2(0.1f, -0.06f * smile);
    }

    public void OnStateUpdate(Vector2Int position, bool hit, int hitCount)
    {
      if (hit)
      {
        targetMood = -1f;
        text.SetText(hitCount.ToString());
      }
      targetPosition = Visualiser.GridToWorld(position);
    }

    public void SetColor(Color color)
    {
      MainDisc.Color = color;
      text.SetColor(color);
    }
  }
}