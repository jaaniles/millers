using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InventoryDict : SerializableDictionary<Item, int> { }

public class SerializableDictionaryExample : MonoBehaviour
{
    // The dictionaries can be accessed throught a property
    [SerializeField]
    InventoryDict m_InventoryDict;
    public IDictionary<Item, int> ResourceDictionary
    {
        get { return m_InventoryDict; }
        set { m_InventoryDict.CopyFrom(value); }
    }
}
