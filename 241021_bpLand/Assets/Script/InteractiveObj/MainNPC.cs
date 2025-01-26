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
        NpcTextBox.Instance.StartText(npcTextList);
        yield return null;
    }
}
