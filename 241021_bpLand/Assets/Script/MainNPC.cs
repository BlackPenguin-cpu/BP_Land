using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNPC : InteractiveObj
{
    public string[] npcTextList;
    protected override void OnInteractionAction()
    {
        StartCoroutine(NPCStartTexting());
    }
    IEnumerator NPCStartTexting()
    {
        foreach (var text in npcTextList)
        {
            NpcTextBox.Instance.StartText(new List<string>() { text });

            yield return new WaitForSeconds(1);
            while (!Input.anyKeyDown)
            {
                yield return 0;
            }
        }
    }
}
