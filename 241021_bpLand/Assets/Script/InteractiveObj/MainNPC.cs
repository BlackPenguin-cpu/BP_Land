using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    public List<NpcTextBox.EffectTextInfo> npcTextList;

    protected override void OnInteractionAction()
    {
        StartCoroutine(NpcStartTexting());
    }

    private IEnumerator NpcStartTexting()
    {
        yield return NpcTextBox.Instance.StartText(npcTextList);
        yield return null;

        var strs = new List<string>() { "감사합니다", "나가주세요", "싫어요" };
        var actions = new List<Action>() { null, null, null};
        yield return ChooseUI.instance.ChooseSlotInfoAddAndStart(strs, actions);


        OnInteractionOver();
    }
}