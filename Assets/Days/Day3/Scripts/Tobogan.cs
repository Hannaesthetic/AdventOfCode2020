using AdventOfCode.DayThree;
using System.Dynamic;
using UnityEngine;

public class Tobogan
{
  public delegate void MoveEvent(Vector2Int position, bool hit, int hitCount);
  public event MoveEvent OnMove;

  private Vector2Int Velocity;
  public Vector2Int position { get; private set; }
  public int collisions { get; private set; }
  private ToboganMap map;
  

  public Tobogan(Vector2Int velocity, ToboganMap newMap)
  {
    position = Vector2Int.zero;
    Velocity = velocity;
    map = newMap;
  }

  public bool Move()
  {
    bool hit = false;
    position = new Vector2Int((position.x + Velocity.x) % map.Size.x, position.y + Velocity.y);
    if (map.HitTree(position.x, position.y))
    {
      collisions++;
      hit = true;
    }
    OnMove?.Invoke(position, hit, collisions);
    return hit;
  }
}
