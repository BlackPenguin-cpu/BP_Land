using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Text Log", menuName = "Scriptable Object/TextLog")]
public class TextLog : ScriptableObject
{
    public List<NpcTextBox.EffectTextInfo> textLogs;

    public ChooseLog nextChooseLog;
}