using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayThree
{
  public class SolutionDayThree : MonoBehaviour
  {
    public Vector2Int[] Slopes;
    public ToboganMap Map;
    public long TreeProduct;
    public int[] TreeCounts;

    [ContextMenu("Solve Step One")]
    public void Solve()
    {
      TreeCounts = new int[Slopes.Length];
      TreeProduct = 1;

      for (int i = 0; i < Slopes.Length; i++)
      {
        TreeCounts[i] = CheckSlope(Slopes[i]);
        TreeProduct *= TreeCounts[i];
      }

      Debug.Log($"All slopes checked, result: {TreeProduct}");
    }

    public int CheckSlope(Vector2Int slope)
    {
      int trees = 0;
      Vector2Int position = Vector2Int.zero;
      do
      {
        if (Map.HitTree(position.x, position.y))
        {
          trees++;
        }
        position.x = (position.x + slope.x) % Map.Size.x;
        position.y += slope.y;
      } while (position.y < Map.Size.y);
      Debug.Log($"Checked {slope.x}/{slope.y}, hit {trees} trees");
      return trees;
    }
  }
}
