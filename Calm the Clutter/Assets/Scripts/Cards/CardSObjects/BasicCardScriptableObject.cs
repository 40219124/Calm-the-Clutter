using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BasicEffect
{
    public EResource resource;
    public int effect;
}

[CreateAssetMenu(fileName = "NewBasicCard", menuName = "ScriptableObjects/BasicCard", order = 1)]
public class BasicCardScriptableObject : ScriptableObject
{
    public string cardTitle;
    public Sprite cardArt;
    public int cardMana;
    [TextArea(2, 2)]
    public string cardText;
    public List<BasicEffect> effects = new List<BasicEffect>();

}
