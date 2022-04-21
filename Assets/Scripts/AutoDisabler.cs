using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AutoDisabler : MonoBehaviour
{
    public float fadeDuration = 3f;
    private void OnEnable()
    {
        Disable();
    }

    private void Disable()
    {
        gameObject.GetComponent<Image>().DOFade(0, fadeDuration);
        gameObject.transform.GetChild(0).GetComponent<Text>().DOFade(0, fadeDuration);
        Invoke(nameof(Deactivate), fadeDuration);
    }

    private void Deactivate()
    {
        gameObject.GetComponent<Image>().DOFade(1, 0f);
        gameObject.transform.GetChild(0).GetComponent<Text>().DOFade(1, 0f);
        gameObject.SetActive(false);
    }
}
