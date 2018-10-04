﻿using UnityEngine;

[System.Serializable]
public class DialogObject {

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}