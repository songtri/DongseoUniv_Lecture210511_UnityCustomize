using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class GUILayoutExample : EditorWindow
{
	[MenuItem("Tools/GUILayout Example", priority = 2)]
	static void ShowWindow()
	{
		var window = EditorWindow.GetWindow<GUILayoutExample>(utility: true, title: "GUILayout Example");
		window.Show();
	}

	private int gridSelection;
	private bool toggleValue;
	private string textAreaValue = "Text area";
	private string textFieldValue = "text field";

	private float progress = 0f;

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(50, 250, 500, 300), new GUIStyle(GUI.skin.button));
		GUILayout.Box("This is Box", GUILayout.Width(100f), GUILayout.Height(50f));
		if (GUILayout.Button("This is Button"))
		{
			//EditorUtility.DisplayDialog("Dialog", "Button clicked", "Close");
			if (EditorUtility.DisplayDialog("Dialog", "Button clicked", "Ok", "Cancel"))
			{
				var step = 0.1f;
				for (float t = 0; t < 3; t += step)
				{
					if (EditorUtility.DisplayCancelableProgressBar("Cancelable", "Doing some work...", t / 3))
						break;
					// Normally, some computation happens here.
					// This example uses Sleep.
					Thread.Sleep((int)(step * 1000.0f));
				}
				EditorUtility.ClearProgressBar();
			}
			else
			{
				string returned = EditorUtility.OpenFilePanel("OpenFile", Application.dataPath, "*.*");
				Debug.Log(returned);
			}
		}
		GUILayout.EndArea();
		GUILayout.Label("This is label");
		gridSelection = GUILayout.SelectionGrid(gridSelection, new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4", "Grid 5" }, 3);
		GUILayout.Label($"Selected Grid: {gridSelection}");

		GUILayout.BeginHorizontal();
		GUILayout.Button("Button 1", GUILayout.Width(100f));
		GUILayout.Space(10f);
		GUILayout.Button("Button 2", GUILayout.Width(100f));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Button("Button 3", GUILayout.Width(100f));
		GUILayout.FlexibleSpace();
		GUILayout.Button("Button 4", GUILayout.Width(100f));
		GUILayout.EndHorizontal();

		textAreaValue = GUILayout.TextArea(textAreaValue);
		textFieldValue = GUILayout.TextField(textFieldValue);

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		GUILayout.Label("Styled Text", style);

		GUISkin skin = Resources.Load<GUISkin>("GUISkin example");
		GUILayout.Button("Skinned Button", new GUIStyle(skin.GetStyle("Button")));

		GUILayout.Label("EditorStyles example", EditorStyles.boldLabel);
	}
}
