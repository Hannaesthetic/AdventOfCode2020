using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdventOfCode.DayThree
{
  [CreateAssetMenu(menuName = "Advent of Code/Day Three/Tobogan Map")]
  public class ToboganMap : ScriptableObject
  {
    [TextAreaAttribute(3, 10)] public string UnparsedData;
    public MapLine[] ParsedData;
    public Vector2Int Size;

    [ContextMenu("Parse data")]
    public void Parse()
    {
      string[] split = UnparsedData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      ParsedData = new MapLine[split.Length];

      // just gonna assume all lines the same length yay
      Size = new Vector2Int(split[0].Length, split.Length);

      for (int i = 0; i < split.Length; i++)
      {
        ParsedData[i] = new MapLine(split[i], Size.x);
      }
      Debug.Log("Map parsed");
    }

    public bool HitTree(int x, int y)
    {
      if (x < 0 || x >= Size.x || y < 0 || y >= Size.y)
      {
        Debug.LogError($"Tree check made out of map bounds: ({x}, {y})");
        return false;
      }
      return ParsedData[y].LineData[x];
    }
  }

  [Serializable]
  public class MapLine
  {
    public const char TreeChar = '#';
    public bool[] LineData;
    
    public MapLine(string data, int size)
    {
      LineData = new bool[size];
      if (size > data.Length)
      {
        Debug.LogError("Map line too short");
        return;
      }
      for (int i = 0; i < size; i++)
      {
        LineData[i] = data[i] == TreeChar;
      }
    }
  }
}
