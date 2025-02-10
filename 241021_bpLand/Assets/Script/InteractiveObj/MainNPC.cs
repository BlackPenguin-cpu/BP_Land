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

        var strs = new List<string>();
        var actions = new List<Action>();
        yield return ChooseUI.Instance.ChooseSlotInfoAddAndStart(strs, actions);


        OnInteractionOver();
    }
}