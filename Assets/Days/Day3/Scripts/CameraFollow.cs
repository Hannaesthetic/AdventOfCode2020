using AdventOfCode.DayThree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public float FollowTime;
  public Transform Target;

  private float velocity;

  private void Update()
  {
    Vector3 newPos = transform.position;
    newPos.y = Mathf.SmoothDamp(newPos.y, Target.position.y, ref velocity, FollowTime, Mathf.Infinity, Time.deltaTime * VisualiserDayThree.SimulationSpeed);
    transform.position = newPos;
  }
}
