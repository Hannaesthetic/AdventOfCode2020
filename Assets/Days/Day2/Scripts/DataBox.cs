using UnityEngine;

namespace AdventOfCode.DayTwo
{

  public class DataBox : MonoBehaviour
  {
    public Shapes.Rectangle rect;
    public float Scale;
    public float Width;

    private VisualiserDayTwo visualiser;
    private float offsetX;
    private float offsetY;
    private int incorrectSide;
    private bool counted;

    public void SetData(int min, int max, int index, VisualiserDayTwo vis)
    {
      rect.Height = Scale * (max - min + 0.5f);
      offsetX = index * Width;
      offsetY = (min + max) / 2f * Scale;
      visualiser = vis;
      incorrectSide = min > 0 ? 1 : (max < 0 ? -1 : 0);
      SetPosition();
    }

    private void Update()
    {
      SetPosition();
      if (!counted)
      {
        counted = visualiser.TryCount(incorrectSide, offsetX);
      }
    }

    private void SetPosition()
    {
      float incorrectOffset = 0f;
      if (incorrectSide != 0)
      {
        incorrectOffset = visualiser.GetOffset(offsetX) * incorrectSide;
      }
      transform.position = new Vector3(offsetX - visualiser.RailPosition, offsetY + incorrectOffset, 0f);
    }
  }
}