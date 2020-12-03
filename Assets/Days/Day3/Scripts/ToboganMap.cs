using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayThree
{
  [CreateAssetMenu(menuName = "Advent of Code/Day Three/Tobogan Map")]
  public class ToboganMap : ScriptableObject
  {
    [TextAreaAttribute(3, 10)] public string UnparsedData;
    public MapLine[] ParsedData;

    [ContextMenu("Parse data")]
    public void Parse()
    {
      string[] split = UnparsedData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      ParsedData = new MapLine[split.Length];

      for (int i = 0; i < split.Length; i++)
      {
        ParsedData[i] = new MapLine(split[i]);
      }
      Debug.Log("Map parsed");
    }
  }

  [Serializable]
  public class MapLine
  {
    public const char TreeChar = '#';
    public bool[] LineData;
    
    public MapLine(string data)
    {
      LineData = new bool[data.Length];
      for (int i = 0; i < data.Length; i++)
      {
        LineData[i] = data[i] == TreeChar;
      }
    }
  }
}
