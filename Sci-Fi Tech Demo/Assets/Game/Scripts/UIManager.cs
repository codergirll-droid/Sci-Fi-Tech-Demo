using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private GameObject coinDisplay;
    public void updateAmmo(int count)
    {
        ammoText.text = "Ammo : " + count;
    }

    public void CollectedCoin()
    {
        coinDisplay.SetActive(true);
    }
    public void SpentCoin()
    {
        coinDisplay.SetActive(false);
    }
}
