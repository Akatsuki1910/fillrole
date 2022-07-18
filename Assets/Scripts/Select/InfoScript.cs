using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    public void Start()
    {
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
