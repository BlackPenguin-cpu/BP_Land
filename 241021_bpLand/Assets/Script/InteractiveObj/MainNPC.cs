using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    public List<NpcTextBox.EffectTextInfo> npcTextList;
    protected override void OnInteractionAction()
    {
        StartCoroutine(NPCStartTexting());
    }
    IEnumerator NPCStartTexting()
    {
        NpcTextBox.Instance.StartText(npcTextList);
        yield return null;
    }
}
