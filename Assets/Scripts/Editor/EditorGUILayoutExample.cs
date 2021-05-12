using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorGUILayoutExample : EditorWindow
{
	[MenuItem("Tools/EditorGUILayout Example")]
	static void ShowWindow()
	{
		var window = EditorWindow.GetWindow<EditorGUILayoutExample>();
		window.Show();
	}

	private Vector2 scrollPos;
	private Color colorValue;
	private AnimationCurve curveValue;
	private int intValue;
	private int delayedIntValue;
	private float floatValue;
	private bool foldoutValue;
	private bool foldoutGroupValue;
	private Gradient gradientValue = new Gradient();
	private int sliderValue;
	private int maskValue;
	private Object objectValue;
	private int intPopupValue;
	private int popupSelectedIndex;
	private string textAreaValue;
	private string textFieldValue;
	private string tagValue;
	private bool toggleGroupEnabled = false;
	private bool[] toggleValue = new bool[3];
	private bool toggleLeftValue;
	private Vector2 vector2Value;
	private Vector3 vector3Value;

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.button));
		{
			EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window));
			{
				EditorGUILayout.LabelField("Start of BeginVertical");
				EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.button));
				EditorGUILayout.LabelField("Horizontal 1");
				EditorGUILayout.LabelField("Horizontal 2");
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal(new GUIStyle(GUI.skin.button));
				EditorGUILayout.LabelField("Horizontal 1");
				EditorGUILayout.LabelField("Horizontal 2");
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.LabelField("EndVertical");
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window));
			{
				EditorGUILayout.LabelField("Start of BeginVertical");
				EditorGUILayout.LabelField("EndVertical");
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true, GUILayout.Width(600f), GUILayout.Height(200f));
		colorValue = EditorGUILayout.ColorField("ColorField", colorValue);
		curveValue = EditorGUILayout.CurveField("CurveField", curveValue);
		intValue = EditorGUILayout.IntField("IntField", intValue);
		delayedIntValue = EditorGUILayout.DelayedIntField("Delayed IntField", delayedIntValue);
		EditorGUILayout.LabelField($"IntValue: {intValue}, DelayedIntValue: {delayedIntValue}");
		floatValue = EditorGUILayout.FloatField("FloatField", floatValue);
		foldoutValue = EditorGUILayout.Foldout(foldoutValue, "Foldout");
		EditorGUILayout.EndScrollView();

		foldoutGroupValue = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutGroupValue, "Foldout Group");
		if (foldoutGroupValue)
		{
			EditorGUILayout.LabelField("Label 1");
			EditorGUILayout.LabelField("Label 2");
		}
		EditorGUILayout.EndFoldoutHeaderGroup();

		var rect = EditorGUILayout.GetControlRect();
		EditorGUILayout.LabelField($"Control Rect: {rect}");
		gradientValue = EditorGUILayout.GradientField(gradientValue);
		EditorGUILayout.HelpBox("This is HelpBox", MessageType.Info);
		sliderValue = EditorGUILayout.IntSlider("Int Slider", sliderValue, 10, 100);
		maskValue = EditorGUILayout.MaskField("MaskField", maskValue, new string[] { "Mask1", "Mask2", "Mask3" });
		objectValue = EditorGUILayout.ObjectField("ObjectField", objectValue, typeof(Object), true);

		string[] popupOptions = { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5", };
		int[] intOptionValues = { 1, 3, 5, 7, 9 };
		intPopupValue = EditorGUILayout.IntPopup("IntPopup", intPopupValue, popupOptions, intOptionValues);
		EditorGUILayout.LabelField($"IntPopup Value: {intPopupValue}");
		popupSelectedIndex = EditorGUILayout.Popup("Popup", popupSelectedIndex, popupOptions);
		EditorGUILayout.LabelField($"Popup Value: {popupSelectedIndex}");

		textAreaValue = EditorGUILayout.TextArea("Text Area");
		textFieldValue = EditorGUILayout.TextField("Text Field", "Text Field");

		tagValue = EditorGUILayout.TagField("TagField");
		EditorGUILayout.Space(30f);
		toggleGroupEnabled = EditorGUILayout.BeginToggleGroup("Toggle Group", toggleGroupEnabled);
		toggleValue[0] = EditorGUILayout.Toggle("Toggle1", toggleValue[0]);
		toggleValue[1] = EditorGUILayout.Toggle("Toggle2", toggleValue[1]);
		toggleValue[2] = EditorGUILayout.Toggle("Toggle3", toggleValue[2]);
		EditorGUILayout.EndToggleGroup();
		toggleLeftValue = EditorGUILayout.ToggleLeft("ToggleLeft", false);

		vector2Value = EditorGUILayout.Vector2Field("Vector2 Field", vector2Value);
		vector3Value = EditorGUILayout.Vector3Field("Vector3 Field", vector3Value);
	}
}
