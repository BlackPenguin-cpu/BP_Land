using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameQuestObj : InteractiveObj
{
    public int NameNumber;
    public NameQuestMainObj obj;
    protected override void OnInteractionAction()
    {
        obj.StartCoroutine(obj.NameObjectActive(NameNumber));
        OnResetInteraction();
        Destroy(gameObject);
    }
}
