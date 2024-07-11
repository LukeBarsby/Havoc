using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public GameObject popup;

    private void OnTriggerEnter(Collider other)
    {
        popup.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        popup.SetActive(false);
    }
}
