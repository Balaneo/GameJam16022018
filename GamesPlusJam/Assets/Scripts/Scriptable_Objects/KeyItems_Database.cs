using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItems_Database
{
	static private List<KeyItems> _items;

	static private bool _isDatabaseLoaded = false;

	static private void ValidateDatabase() 
	{
		if (_items == null)
			_items = new List<KeyItems> ();
		if (!_isDatabaseLoaded)
			LoadDatabase ();
	}

	static public void LoadDatabase() 
	{
		if (_isDatabaseLoaded)
			return;
		_isDatabaseLoaded = true;
		LoadDatabaseForce();

	}

	static public void LoadDatabaseForce() 
	{
		ValidateDatabase ();
		KeyItems[] resources = Resources.LoadAll<KeyItems> (@"KeyItems");
		foreach (KeyItems item in resources) 
		{
			if (!_items.Contains (item)) 
			{
				_items.Add (item);
			}
		}
	}

	static public void ClearDatabase()
	{
		_isDatabaseLoaded = false;
		_items.Clear ();
	}

	static public KeyItems GetItem(int id) 
	{
		ValidateDatabase ();
		foreach (KeyItems item in _items) 
		{
			if (item.Item_ID == id) 
			{
				return ScriptableObject.Instantiate (item) as KeyItems;
			}
		}
		return null;
	}
}
