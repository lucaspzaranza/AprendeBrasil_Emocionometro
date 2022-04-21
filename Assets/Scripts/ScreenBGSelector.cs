using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenBGSelector : MonoBehaviour
{
    public List<GameObject> bearsBG;

    private void OnEnable()
    {
        Invoke(nameof(ActivateSelectedBG), 0.001f);
    }

    private void OnDisable()
    {
        Invoke(nameof(DeactivateBG), 0.001f);
    }

    private void DeactivateBG()
    {
        bearsBG.ForEach(bg =>
        {
            if (bg.activeSelf)
                bg.SetActive(false);
        });
        //bearsBG[UIController.instance.selectedBearIndex].SetActive(false);
    }

    private void ActivateSelectedBG()
    {
        
        bearsBG[UIController.instance.selectedBearIndex].SetActive(true);
    }
}
