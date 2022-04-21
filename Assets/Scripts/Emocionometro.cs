using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emocionometro : MonoBehaviour
{
    public static Emocionometro instance;
    public Slider emocionometroSlider;
    public int selectedValue;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetNewValue()
    {
        selectedValue = (int)emocionometroSlider.value;
    }
}
