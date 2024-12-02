using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseUI : MonoBehaviour
{
    public class ChooseSlotInfo
    {
        public string slotName;
        public Action onActiveAction;
        public ChooseSlotInfo(string name, Action action)
        {
            slotName = name;
            onActiveAction = action;
        }
    }

    [SerializeField]
    private List<TextMeshProUGUI> chooseUiTextList;
    private List<ChooseSlotInfo> chooseSlotInfos = new List<ChooseSlotInfo>(4);

    public void ChooseSlotReset()
    {
        chooseSlotInfos.Clear();
    }
    public void ChooseSlotAdd(string name, Action action)
    {
        chooseSlotInfos.Add(new ChooseSlotInfo(name, action));
    }

    private void Start()
    {
        JoyStick.stickAction+= SelectAxis;
    }

    private void SelectAxis(Vector2 vec)
    {
        
    }
}
