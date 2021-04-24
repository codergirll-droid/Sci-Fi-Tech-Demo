using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject destructedCrate;

    public void DestroyCreate()
    {
        Instantiate(destructedCrate, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
