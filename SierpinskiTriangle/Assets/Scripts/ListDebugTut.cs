using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class ListDebugTut : MonoBehaviour
{
    [SerializeField] List<int> list;
    [SerializeField] string[] array;

    private void Awake()
    {
        list.BuildString();
        array.BuildString();
    }
}



