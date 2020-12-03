using AdventOfCode.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventOfCode.DayThree
{
  public class VisualiserDayThree : MonoBehaviour
  {
    public Vector2 GridSize;
    public GameObject DotPrefab;
    public GameObject TreePrefab;
    public GameObject FacePrefab;
    public GameObject TextPrefab;
    public Transform TextParent;
    public BouncyText FinalScoreText;
    public Color[] Colors;
    public float GameSpeed = 1f;

    public static float SimulationSpeed = 1f;

    private List<Face> faces;
    private int nextColor;

    private void Update()
    {
      SimulationSpeed = GameSpeed;
    }

    public void Setup(SolutionDayThree solution, ToboganMap map, Tobogan[] tobogans)
    {
      solution.OnMoveLine += MoveCamera;
      SetupMap(map);
      faces = new List<Face>();
      for (int i = 0; i < tobogans.Length; i++)
      {
        SetupTobogan(tobogans[i]);
      }
      solution.OnHitUpdate += UpdateScoreText;
    }

    public void UpdateScoreText(string value)
    {
      FinalScoreText.SetText(value);
    }

    public void MoveCamera(int line)
    {
      Vector3 newPosition = transform.position;
      newPosition.y = GridToWorld(new Vector2(0f, line)).y;
      transform.position = newPosition;
    }

    private void SetupMap(ToboganMap map)
    {
      for (int y = 0; y < map.Size.y; y++)
      {
        for (int x = 0; x < map.Size.x; x++)
        {
          Vector3 position = GridToWorld(new Vector2(x, y));
          Instantiate(map.HitTree(x, y) ? TreePrefab : DotPrefab, position, Quaternion.identity);
        }
      }
    }

    private void SetupTobogan(Tobogan tobogan)
    {
      GameObject textObj = Instantiate(TextPrefab, TextParent);
      BouncyText text = textObj.GetComponent<BouncyText>();

      GameObject obj = Instantiate(FacePrefab);
      Face face = obj.GetComponent<Face>();
      face.Visualiser = this;
      face.text = text;
      face.SetColor(Colors[nextColor]);
      nextColor = (nextColor + 1) % Colors.Length;
      
      tobogan.OnMove += face.OnStateUpdate;
      faces.Add(face);
    }

    public Vector3 GridToWorld(Vector2 position)
    {
      Vector2 target = GridSize;
      target.Scale(position);
      return target;
    }
  }
}
