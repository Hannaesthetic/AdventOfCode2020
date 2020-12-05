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

    public string input;

    [ContextMenu("Find Seat")]
    private void CheckInput()
    {
      Vector2Int seatPosition = FindSeat(input);
      Debug.Log($"Row: {seatPosition.x}, column: {seatPosition.y}, ID: {seatPosition.x * Mathf.Pow(2f, COLUMN_COUNT) + seatPosition.y}");
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
  }
}