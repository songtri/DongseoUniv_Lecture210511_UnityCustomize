using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LiveExample : EditorWindow
{
	[MenuItem("Tools/Live Example", priority = 1)]
	static void OpenWindow()
	{
		GetWindow<LiveExample>();
	}

	private int selectedCharacter = 0;
	private int previousCharacterSelection = -1;
	private List<CharacterDataSO> characterDataList = null;
	private string[] existingCharacterList;
	private List<CharacterAI> deleteList = new List<CharacterAI>();
	private AiData currentAi = null;

	private void OnEnable()
	{
		var data = Resources.LoadAll<CharacterDataSO>("CharacterData");
		characterDataList = data.ToList();
		var names = from n in characterDataList select n.Name;
		existingCharacterList = names.ToArray();
		//deleteList.Clear();
	}

	private void OnGUI()
	{
		GUILayout.Label("Select character to edit");
		selectedCharacter = EditorGUILayout.Popup(selectedCharacter, existingCharacterList.ToArray());
		var currentCharacter = characterDataList[selectedCharacter];

		if (previousCharacterSelection != selectedCharacter)
		{
			if (currentCharacter.JsonAiData == null)
				currentCharacter.JsonAiData = string.Empty;
			currentAi = JsonConvert.DeserializeObject<AiData>(currentCharacter.JsonAiData);
			if (currentAi == null)
				currentAi = new AiData();
			if (currentAi.aiData == null)
				currentAi.aiData = new List<CharacterAI>();
			previousCharacterSelection = selectedCharacter;
		}

		if (currentAi.aiData.Count <= 0)
			GUILayout.Label("Data is empty");
		else
		{
			int index = 0;
			foreach (var ai in currentAi.aiData)
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.Box(index.ToString());
					GUILayout.Label("Target");
					ai.Target = (AiTarget)EditorGUILayout.EnumPopup(ai.Target, GUILayout.Width(80f));
					GUILayout.Label("Condition");
					ai.ConditionToCheck = (Condition)EditorGUILayout.EnumPopup(ai.ConditionToCheck, GUILayout.Width(80f));
					GUILayout.Label("Value1");
					ai.ConditionValue1 = EditorGUILayout.IntField(ai.ConditionValue1, GUILayout.Width(100f));
					GUILayout.Label("Value2");
					ai.ConditionValue2 = EditorGUILayout.IntField(ai.ConditionValue2, GUILayout.Width(100f));
					GUILayout.Label("Behavior");
					ai.Behavior = (Behavior)EditorGUILayout.EnumPopup(ai.Behavior, GUILayout.Width(80f));
					GUILayout.Label("BehaviorValue");
					if (ai.Behavior == Behavior.UseSkill)
					{
						var popupList = currentCharacter.Skills.ToArray();
						ai.BehaviorValue = EditorGUILayout.Popup(ai.BehaviorValue, popupList);
					}
					else
					{
						ai.BehaviorValue = EditorGUILayout.IntField(ai.BehaviorValue, GUILayout.Width(100f));
					}
				}
				GUILayout.EndHorizontal();
				++index;
			}
		}

		if (GUILayout.Button("+", GUILayout.Width(50f), GUILayout.Height(20f)))
		{
			CharacterAI newAi = new CharacterAI();
			currentAi.aiData.Add(newAi);
		}

		if (GUILayout.Button("Save", GUILayout.Width(200f), GUILayout.Height(40f)))
		{
			currentCharacter.JsonAiData = JsonConvert.SerializeObject(currentAi);
		}

		if (GUILayout.Button("Coroutine"))
		{
			EditorCoroutineUtility.StartCoroutine(CoroutineTest(), this);
		}
	}

	IEnumerator CoroutineTest()
	{
		int index = 0;
		while (index < 100)
		{
			Debug.Log(index);
			++index;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
