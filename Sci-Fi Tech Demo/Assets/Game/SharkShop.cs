using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour {

    [SerializeField]
    private AudioClip winSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player!= null)
                {
                    if (player.hasCoin == true)
                    {
                        player.hasCoin = false;
                        UIManager.FindObjectOfType<UIManager>().SpentCoin();
                        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, 1f);
                        player.EnableWeapons();
                    }
                    else
                    {
                        Debug.Log("Get out of the shop!");
                    }
                }
            }
                
            
        }
    }
}
