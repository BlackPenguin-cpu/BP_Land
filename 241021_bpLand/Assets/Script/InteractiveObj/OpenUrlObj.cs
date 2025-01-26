using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrlObj : InteractiveObj
{
    [SerializeField]
    private string url;
    protected override void OnInteractionAction()
    {
        Application.OpenURL(url);
    }
}
