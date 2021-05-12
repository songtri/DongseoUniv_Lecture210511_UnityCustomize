using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/Character Data")]
public class CharacterDataSO : ScriptableObject
{
	public CharactrerClass Class;
	public string Name;
	public List<string> Skills;
	public string JsonAiData;
}
