using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We could have also used interfaces instead of inheritance in both approaches are valid

public class BaseCounter : MonoBehaviour
{
  public virtual void Interact(Player player)
  {
    Debug.LogError("BaseCounter.Interact");
  }
}
