using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayFive
{
  public class SolutionDay5 : MonoBehaviour
  {
    public const char FRONT_CHAR = 'F';
    public const char BACK_CHAR = 'B';
    public const char RIGHT_CHAR = 'R';
    public const char LEFT_CHAR = 'L';
    public const int ROW_COUNT = 7;
    public const int COLUMN_COUNT = 3;

    [TextArea(3, 10)] public string UnparsedData;

    private int[] IDs;

    [ContextMenu("Find Highest")]
    private void FindBiggestID()
    {
      int[] seats = GetOrderedSeatIDs();
      Debug.Log($"Highest SeatID: " + seats[seats.Length - 1]);
    }

    [ContextMenu("Find Seat")]
    private void FindSeat()
    {
      int firstValid = GetID(new Vector2Int(0, (int)Mathf.Pow(2, COLUMN_COUNT)));
      int lastValid = GetID(new Vector2Int((int)Mathf.Pow(2, ROW_COUNT), 0));
      int[] seats = GetOrderedSeatIDs();
      for(int i = 0; i < seats.Length - 1; i++)
      {
        if (seats[i] < firstValid)
        {
          continue;
        } else if (seats[i] > lastValid)
        {
          Debug.Log("Seat not found");
          return;
        }
        if (seats[i + 1] - seats[i] == 2)
        {
          Debug.Log("Seat found: " + (seats[i] + 1));
          return;
        }
      }
    }

    private int[] GetOrderedSeatIDs()
    {
      string[] splitData = ParseAll(UnparsedData);
      IDs = new int[splitData.Length];

      int highestID = -1;

      for (int i = 0; i < IDs.Length; i++)
      {
        IDs[i] = GetID(FindSeat(splitData[i]));
      }

      Array.Sort(IDs);
      return IDs;
    }

    private string[] ParseAll(string data)
    {
      return data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
    }

    private Vector2Int FindSeat(string input)
    {
      string rows = input.Substring(0, ROW_COUNT);
      string columns = input.Substring(ROW_COUNT, COLUMN_COUNT);
      
      rows = rows.Replace(BACK_CHAR, '1').Replace(FRONT_CHAR, '0');
      columns = columns.Replace(LEFT_CHAR, '0').Replace(RIGHT_CHAR, '1');

      int row = Convert.ToInt32(rows, 2);
      int column = Convert.ToInt32(columns, 2);

      return new Vector2Int(row, column);
    }

    private int GetID(Vector2Int seat)
    {
      return seat.x * (int)Mathf.Pow(2, COLUMN_COUNT) + seat.y;
    }
  }
}