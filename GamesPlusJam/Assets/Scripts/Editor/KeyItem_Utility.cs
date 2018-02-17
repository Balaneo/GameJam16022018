using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KeyItem_Utility
{
	[MenuItem("Assets/Create/ScriptableObject/KeyItem")]
	static public void CreateItem() 
	{
		ScriptableObjectUtility.CreateAsset<KeyItems> ();
	}
}