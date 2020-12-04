using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.Util
{
  public class Follower : MonoBehaviour
  {
    public Vector3 Offset;
    public Transform Target;

    private void Update()
    {
      transform.position = Target.position + Offset;
      transform.rotation = Target.rotation;
    }
  }
}