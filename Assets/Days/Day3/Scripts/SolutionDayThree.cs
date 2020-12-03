using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayThree
{
  public class SolutionDayThree : MonoBehaviour
  {
    public const int MovementX = 3;
    public const int MovementY = 1;
    public ToboganMap Map;

    [ContextMenu("Solve Step One")]
    public void SolveStepOne()
    {
      int trees = 0;
      Vector2Int position = Vector2Int.zero;
      do
      {
        if (Map.HitTree(position.x, position.y))
        {
          trees++;
        }
        position.x = (position.x + MovementX) % Map.Size.x;
        position.y += MovementY;
      } while (position.y < Map.Size.y);
      Debug.Log($"Solution complete, hit {trees} trees");
    }
  }
}
