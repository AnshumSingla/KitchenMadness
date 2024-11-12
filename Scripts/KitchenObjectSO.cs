using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This file is for scriptable object. They are used when we need to use a number of objects
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
