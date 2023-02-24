using UnityEngine;

[CreateAssetMenu (fileName = "Char_", menuName = "ScriptableObject/Character")]
public class CharacterLifeData : ScriptableObject
{
    [Tooltip("Max character life")]
    public int fullLife;
    [Tooltip("recovery time between damage")]
    public float timeBetweenDamage;
    [Tooltip("Make character invulnarable during an amount of time in the timeBetweenDamage atribute")]
    public bool invulnarableOnDamage = true;
}

