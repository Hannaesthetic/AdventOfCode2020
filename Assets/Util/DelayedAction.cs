using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AdventOfCode.Util
{
  public class DelayedAction : MonoBehaviour
  {
    public UnityEvent OnTimeExpire;
    public float Delay;
    public bool Loops;

    private void Awake()
    {
      StartCoroutine(c_RunEvent());
    }

    private IEnumerator c_RunEvent()
    {
      do
      {
        yield return new WaitForSeconds(Delay);
        OnTimeExpire.Invoke();
      } while (Loops);
    }
  }
}