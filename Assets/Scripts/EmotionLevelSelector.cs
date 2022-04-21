using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionLevelSelector : MonoBehaviour
{
    public List<GameObject> fearList;
    public List<GameObject> joyList;
    public List<GameObject> angerList;
    public List<GameObject> calmList;
    public List<GameObject> sadList;

    public EmotionType emoType;

    private void OnEnable()
    {
        fearList[0].SetActive(false);
        StartCoroutine(SelectEmotionBackgroundAndSetActiveCoroutine(true));
    }

    private void OnDisable()
    {
        SelectEmotionBackgroundAndSetActive(false);
    }

    public IEnumerator SelectEmotionBackgroundAndSetActiveCoroutine(bool val)
    {
        while(UIController.instance == null)
        {
            yield return new WaitForEndOfFrame();
        }

        emoType = (EmotionType)UIController.instance.selectedBearIndex;
        int level = (Emocionometro.instance != null)? Emocionometro.instance.selectedValue : 0;

        switch (emoType)
        {
            case EmotionType.Fear:
                fearList[level].SetActive(val);
                break;
            case EmotionType.Joy:
                joyList[level].SetActive(val);
                break;
            case EmotionType.Anger:
                angerList[level].SetActive(val);
                break;
            case EmotionType.Calm:
                calmList[level].SetActive(val);
                break;
            case EmotionType.Sad:
                sadList[level].SetActive(val);
                break;
            default:
                break;
        }
    }

    public void SelectEmotionBackgroundAndSetActive(bool val)
    {
        emoType = (EmotionType)UIController.instance.selectedBearIndex;
        int level = (Emocionometro.instance != null) ? Emocionometro.instance.selectedValue : 0;

        switch (emoType)
        {
            case EmotionType.Fear:
                fearList[level].SetActive(val);
                break;
            case EmotionType.Joy:
                joyList[level].SetActive(val);
                break;
            case EmotionType.Anger:
                angerList[level].SetActive(val);
                break;
            case EmotionType.Calm:
                calmList[level].SetActive(val);
                break;
            case EmotionType.Sad:
                sadList[level].SetActive(val);
                break;
            default:
                break;
        }
    }
}
