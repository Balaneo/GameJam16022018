using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInventory))]
public class PlayerInventoryEditor : Editor {

	private bool[] showItemSlots = new bool[PlayerInventory.numInventorySlots];

	private SerializedProperty inventoryItemsProperty;
	private SerializedProperty inventoryItemNamesProperty;
	private SerializedProperty inventoryItemDescriptionsProperty;
	private SerializedProperty inventoryItemImagesProperty;
	private SerializedProperty inventoryItemConsumedOnUsesProperty;


	private const string inventoryPropItems = "items";
	private const string inventoryPropItemNames = "itemNames";
	private const string inventoryPropItemDescriptions = "itemDescriptions";
	private const string inventoryPropItemImages = "itemImages";
	private const string inventoryPropItemConsumedOnUses = "itemConsumedOnUses";


	private void OnEnable()
	{
		inventoryItemsProperty = serializedObject.FindProperty (inventoryPropItems);
		inventoryItemNamesProperty = serializedObject.FindProperty (inventoryPropItemNames);
		inventoryItemDescriptionsProperty = serializedObject.FindProperty (inventoryPropItemDescriptions);
		inventoryItemImagesProperty = serializedObject.FindProperty (inventoryPropItemImages);
		inventoryItemConsumedOnUsesProperty = serializedObject.FindProperty (inventoryPropItemConsumedOnUses);
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();

		for (int i = 0; i < PlayerInventory.numInventorySlots; i++)
		{
			ItemSlotGUI (i);
		}

		serializedObject.ApplyModifiedProperties ();
	}

	private void ItemSlotGUI(int index)
	{
		EditorGUILayout.BeginVertical (GUI.skin.box);
		EditorGUI.indentLevel++;

		showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Inventory Item Slot " + index);

		if (showItemSlots [index])
		{
			EditorGUILayout.PropertyField (inventoryItemsProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (inventoryItemNamesProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (inventoryItemDescriptionsProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (inventoryItemImagesProperty.GetArrayElementAtIndex(index));
			EditorGUILayout.PropertyField (inventoryItemConsumedOnUsesProperty.GetArrayElementAtIndex (index));

		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical ();
	}
}
