using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is a scriptable object, used to handle all the different types of ingredients
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject {
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
