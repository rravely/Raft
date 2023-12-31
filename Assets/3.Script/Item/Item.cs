using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemKoreanName;
    public string itemDescription;
    public Sprite icon;
    public bool stackable = true;
    public bool isTool = false;
    public bool isBuildable = false;
    public bool isFoundation = false; //for structure items
    public bool isFish = false;
    public bool isCooked = false;
}