using AdventOfCode.Util;
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

    [ContextMenu("Solve")]
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

    private Tobogan[] tobogans;
    public StringEvent OnHitUpdate;
    public IntEvent OnMoveLine;
    public float LineCheckFrequency;
    public VisualiserDayThree Visualiser;

    public bool Autoplay;

    private float timeSinceLastCheck;
    private long hitCount;
    private int nextLine = 0;

    private void Update()
    {
      if (Autoplay)
      {
        timeSinceLastCheck -= Time.deltaTime * LineCheckFrequency * VisualiserDayThree.SimulationSpeed;
        while (timeSinceLastCheck <= 0f && Autoplay)
        {
          timeSinceLastCheck++;
          CheckNextLine();
        }
      }
    }

    [ContextMenu("Setup")]
    public void SetupCheck()
    {
      hitCount = 0;
      Autoplay = true;
      nextLine = 0;
      tobogans = new Tobogan[Slopes.Length];
      for (int i = 0; i < Slopes.Length; i++)
      {
        tobogans[i] = new Tobogan(Slopes[i], Map);
      }
      Visualiser.Setup(this, Map, tobogans);
    }

    public void CheckNextLine()
    {
      Debug.Log("Moved at " + Time.time);
      OnMoveLine.Invoke(nextLine);

      bool scoreDirty = false;

      foreach (Tobogan tobogan in tobogans)
      {
        if (tobogan.position.y < nextLine)
        {
          scoreDirty = tobogan.Move() || scoreDirty;
        }
      }

      if (scoreDirty)
      {
        hitCount = 1;
        foreach (Tobogan tobogan in tobogans)
        {
          hitCount *= tobogan.collisions;
        }
        OnHitUpdate?.Invoke(hitCount.ToString());
      }
      nextLine++;
      if (nextLine >= Map.Size.y)
      {
        Autoplay = false;
      }
    }
  }
}
