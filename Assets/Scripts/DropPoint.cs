using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class DropPoint : Inventory
{ 
    public static DropPoint Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
