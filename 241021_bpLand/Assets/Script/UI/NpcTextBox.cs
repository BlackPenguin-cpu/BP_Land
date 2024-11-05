using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;

public class NpcTextBox : MonoBehaviour
{
    public static NpcTextBox Instance { get; private set; }

    public List<string> textList = new List<string>();
    public KoreanTyperDemo_Auto textAuto;

    public void Awake()
    {
        Instance = this;
    }
    public void StartText(List<string> curTextList)
    {
        textList = curTextList;
        StartText();
    }
    public void StartText()
    {
        foreach (var curText in textList)
        {
            textAuto.StartTexting(curText);
        }
    }
}
