using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireDelay = 0.1f;

    public int maxAmmo = 36;
    public int magSize = 72;
    public int currAmmo;
    public int currMag;
    

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Animator anim;

    private bool isFiring = false;
    private float shotCounter;
    private float rateOfFire = 0.1f;

    public CameraMouse cam;
    public GameObject impactEffect;

    public Image ammoFill;
    public TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        currAmmo = maxAmmo;
        currMag = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currAmmo < maxAmmo)
        {
            anim.SetBool("isReloading", true);
        }

        if(anim.GetBool("isReloading") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            anim.SetBool("isReloading", false);
            currAmmo = maxAmmo;
        }

        if (anim.GetBool("isReloading"))
        {
            ammoFill.fillAmount = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        else
        {
            ammoFill.fillAmount = (float)currAmmo / maxAmmo;
        }

        if (Input.GetButton("Fire1") && currAmmo > 0)
        {
            isFiring = true;
            cam.isFiring = true;
            anim.SetBool("isShooting", true);

        }
        else
        {
            isFiring = false;
            cam.isFiring = false;
            anim.SetBool("isShooting", false);
        }

        if (isFiring)
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                shotCounter = rateOfFire;
                Shoot();
            }
        }
        else
        {
            
            shotCounter -= Time.deltaTime;
        }

        
        ammoText.text = currAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    void Shoot()
    {
        currAmmo -= 1;
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHP target = hit.transform.GetComponent<EnemyHP>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        if (hit.transform.CompareTag("Enemy"))
        {
            GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 0.7f);
        }
    }
}
