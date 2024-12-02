using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemList : ScriptableObject
{
	//public List<EntityType> アイテムリスト; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemEntity> ItemEnt;
}
