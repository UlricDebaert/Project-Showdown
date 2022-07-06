using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [Header("Gun")]
    public GameObject gun;
    public Vector3 gunPos;

    [Header("Stats")]
    public int healthPoint;

    [Header("Card Infos")]
    public string characterName;
    public Sprite characterIcon;
    public Sprite powerIcon;

    [Header("Animation")]
    public Sprite baseSprite;
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip jumpAnim;
}
