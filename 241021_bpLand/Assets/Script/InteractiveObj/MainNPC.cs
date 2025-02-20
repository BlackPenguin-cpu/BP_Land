using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    [SerializeField] private TextLog textLog;

    protected override void OnInteractionAction()
    {
        NpcStartTexting();
    }

    private async UniTask NpcStartTexting()
    {
        var curTextLog = textLog;
        while (true)
        {
            await NpcTextBox.Instance.StartText(curTextLog.textLogs);
            if (!curTextLog.nextChooseLog)
            {
                OnInteractionOver();
                return;
            }

            var chooseLog = curTextLog.nextChooseLog;

            var index = await ChooseUI.instance.ChooseSlotInfoAddAndStart(chooseLog.chooseTextList);

            if (!chooseLog.nextTextLogList[index])
            {
                OnInteractionOver();
                return;
            }

            curTextLog = chooseLog.nextTextLogList[index];
        }
    }
}