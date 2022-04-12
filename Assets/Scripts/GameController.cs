using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI platform;
    void Start()
    {
        platform.text = Application.platform.ToString();
    }

    void Update()
    {
        
    }
}
