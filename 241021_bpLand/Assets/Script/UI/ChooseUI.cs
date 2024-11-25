using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseUI : MonoBehaviour
{
    public class ChooseSlotInfo
    {
        public string slotName;
        public Action onActiveAction;
    }

    List<ChooseSlotInfo> chooseSlotInfos = new List<ChooseSlotInfo>(4);
    void Start()
    {

    }

    void Update()
    {

    }
}
