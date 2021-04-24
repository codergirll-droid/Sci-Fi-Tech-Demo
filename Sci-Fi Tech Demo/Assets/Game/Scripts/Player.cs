using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController CharacterController;
    [SerializeField]
    private float speed = 3.5f;
    private float gravity = 9.8f;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private GameObject hitMarkerPrefab;
    [SerializeField]
    private AudioSource weaponSound;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool isReloading = false;

    private UIManager UIManager;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;

        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmo > 0 && Input.GetMouseButton(0))
        {
            Shoot();
        }else
        {
            muzzleFlash.SetActive(false);
            weaponSound.Stop();
        }

        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < 50 && isReloading==false)
        {
            isReloading = true;
            StartCoroutine(reload());
        }



    }
    void Shoot()
    {
            muzzleFlash.SetActive(true);
            currentAmmo--;
            UIManager.updateAmmo(currentAmmo);

            if (weaponSound.isPlaying == false)
            {
                weaponSound.Play();
            }

            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;


            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log(hitInfo.transform.name);
                GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 1f);

            Destructible create = hitInfo.transform.GetComponent<Destructible>();
            if(create != null)
            {
                create.DestroyCreate();
            }

            }
        
    }

    void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);
        CharacterController.Move(velocity * Time.deltaTime);

    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        UIManager.updateAmmo(currentAmmo);
        isReloading = false;
    }

    public void EnableWeapons()
    {
        weapon.SetActive(true);
    }
    
}
