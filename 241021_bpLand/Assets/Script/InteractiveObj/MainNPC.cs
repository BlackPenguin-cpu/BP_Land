using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    public List<NpcTextBox.EffectTextInfo> npcTextList;

    protected override void OnInteractionAction()
    {
        NpcStartTexting();
    }

    private async UniTaskVoid NpcStartTexting()
    {
        await NpcTextBox.Instance.StartText(npcTextList);
        NpcTextBox.Instance.ResetText();

        var strs = new List<string>() { "감사합니다", "나가주세요", "싫어요" };
        var actions = new List<Action>() { null, null, null };
        ChooseUI.ChooseSlotInfo(strs, actions);
        await ChooseUI.instance.ChooseSlotInfoAddAndStart(strs, actions);


        OnInteractionOver();
    }
}