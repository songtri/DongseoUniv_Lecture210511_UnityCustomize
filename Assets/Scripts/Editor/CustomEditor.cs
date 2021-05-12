using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class CustomEditor : EditorWindow
{
	[MenuItem("Tools/Character Data Editor")]
	static void ShowWindow()
	{
		var window = EditorWindow.GetWindow<CustomEditor>();
		window.Show();
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
		deleteList.Clear();
	}

	private void OnGUI()
	{
		GUILayout.Label("Select charater to edit");
		selectedCharacter = EditorGUILayout.Popup(selectedCharacter, existingCharacterList, GUILayout.Width(300f));
		CharacterDataSO currentCharacter = characterDataList[selectedCharacter];
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

		if (currentAi.aiData.Count == 0)
		{
			GUILayout.Label("Ai data is empty");
		}
		else
		{
			int index = 0;
			foreach (var preset in currentAi.aiData)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Box(index.ToString(), GUILayout.Width(20f));
				GUILayout.Space(5f);
				GUILayout.Label("Target", GUILayout.Width(60f));
				preset.Target = (AiTarget)EditorGUILayout.EnumPopup(preset.Target, GUILayout.Width(80f));
				GUILayout.Space(10f);
				GUILayout.Label("Condition", GUILayout.Width(70f));
				preset.ConditionToCheck = (Condition)EditorGUILayout.EnumPopup(preset.ConditionToCheck, GUILayout.Width(80f));
				GUILayout.Space(10f);
				GUILayout.Label("Value 1", GUILayout.Width(50f));
				preset.ConditionValue1 = EditorGUILayout.IntField(preset.ConditionValue1, GUILayout.MinWidth(100f), GUILayout.MaxWidth(100f));
				GUILayout.Space(10f);
				GUILayout.Label("Value 2", GUILayout.Width(50f));
				preset.ConditionValue2 = EditorGUILayout.IntField(preset.ConditionValue2, GUILayout.MinWidth(100f), GUILayout.MaxWidth(100f));
				GUILayout.Space(10f);
				GUILayout.Label("Behavior", GUILayout.Width(80f));
				preset.Behavior = (Behavior)EditorGUILayout.EnumPopup(preset.Behavior, GUILayout.Width(120f));
				if (preset.Behavior == Behavior.UseSkill)
				{
					GUILayout.Label("BehaviorValue", GUILayout.Width(100f));
					preset.BehaviorValue = EditorGUILayout.Popup(preset.BehaviorValue, currentCharacter.Skills.ToArray(), GUILayout.Width(100f));
				}
				if (GUILayout.Button("-", GUILayout.Width(30f)))
				{
					deleteList.Add(preset);
				}
				GUILayout.EndHorizontal();
				++index;
			}
		}

		foreach (var presetToDelete in deleteList)
		{
			currentAi.aiData.Remove(presetToDelete);
		}

		if (GUILayout.Button("+", GUILayout.Width(50f)))
		{
			currentAi.aiData.Add(new CharacterAI() { Target = AiTarget.None, ConditionToCheck = Condition.None, Behavior = Behavior.None });
		}

		if (GUILayout.Button("Save", GUILayout.Width(200f), GUILayout.Height(50f)))
		{
			currentCharacter.JsonAiData = JsonConvert.SerializeObject(currentAi);
		}
	}
}
