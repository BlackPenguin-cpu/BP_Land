using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class NameQuestMainObj : MonoBehaviour
{
    public List<GameObject> nameObjects;

    public IEnumerator NameObjectActive(int index)
    {
        var renderer = nameObjects[index].GetComponent<SpriteRenderer>();

        var mainCam = MainCamera.instance;
        mainCam.cameraState = MainCamera.ECameraState.ProductionObject;
        mainCam.targetObj = nameObjects[index];

        yield return new WaitForSeconds(0.5f);

        renderer.DOColor(Color.white, 1).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(2f);

        mainCam.cameraState = MainCamera.ECameraState.OnPlayer;
    }

}
