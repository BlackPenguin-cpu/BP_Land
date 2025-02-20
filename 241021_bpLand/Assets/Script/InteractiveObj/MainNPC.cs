using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    public List<NpcTextBox.EffectTextInfo> npcTextList;
    public List<NpcTextBox.EffectTextInfo> npcTextList2;

    protected override void OnInteractionAction()
    {
        NpcStartTexting();
    }

    private async UniTask NpcStartTexting()
    {
        await NpcTextBox.Instance.StartText(npcTextList);
        NpcTextBox.Instance.ResetText();

        var strs = new List<string>() { "감사합니다", "나가주세요", "싫어요" };
        var action = new List<Func<UniTask>>();
        var index = await ChooseUI.instance.ChooseSlotInfoAddAndStart(strs);

        action.Add(NpcTextBox.Instance.StartText(npcTextList2));

        await action[index]();
        
        OnInteractionOver();
    }