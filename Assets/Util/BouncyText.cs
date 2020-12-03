using TMPro;
using UnityEngine;

namespace AdventOfCode.Util {
  public class BouncyText : MonoBehaviour
  {
    public TextMeshProUGUI TextBox;
    public float DefaultScale;
    public float BumpVelocity;
    public float ScaleTime;
    public float Ceiling;

    private float scale;
    private float velocity;

    private void Update()
    {
      scale = Mathf.SmoothDamp(scale, 1f, ref velocity, ScaleTime);
      if (scale >= Ceiling)
      {
        scale = Ceiling;
        velocity = 0f;
      }
      transform.localScale = Vector3.one * scale * DefaultScale;
    }

    public void SetText(string text)
    {
      TextBox.SetText(text);
      velocity = BumpVelocity;
    }
  }
}