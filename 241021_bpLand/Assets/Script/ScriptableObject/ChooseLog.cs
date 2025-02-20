using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ChooseUi Log", menuName = "Scriptable Object/ChooseUiLog")]
public class ChooseLog : ScriptableObject
{
    public List<string> chooseTextList;
    public List<TextLog> nextTextLogList;
    public List<Action> LogActions;
}