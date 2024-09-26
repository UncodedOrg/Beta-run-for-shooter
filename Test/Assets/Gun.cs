using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Gun : MonoBehaviour
{
       public int damage;
       public float timebetweenshooting;
       public float spread;
       public float range;
       public float reloadtime;
       public float timebetweenshots;
       public int magazinesize;
       public int bulletspertap;
       public bool buttonhold;
       int bulletsleft;
       int bulletsshot;
       public bool shooting;
       public bool readytoshoot;
       public bool reloading;
       public Camera fpscam;
       //public transform attackpoint;
       public RaycastHit rayhit;
       public LayerMask enemy;
       public GameObject bulletHoleGraphic;
       public TextMeshProUGUI shotcounter;

       //public health pain;

       private void Awake()
       {
           bulletsleft = magazinesize;
           readytoshoot = true;
       }

       private void Update()
       {
           MyInput();

           shotcounter.SetText(bulletsleft + " / " + magazinesize);
       }

        private void MyInput()
        {
            if (buttonhold == true){
                shooting = Input.GetKey(KeyCode.Mouse0);
            }else{
                shooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if(Input.GetKeyDown(KeyCode.R) && bulletsleft < magazinesize && !reloading)
            {
                Reload();
            }

            if(readytoshoot && shooting && !reloading && bulletsleft > 0)
            {
                bulletsshot = bulletspertap;
                Shoot();
            }
        }
        
        private void Reload()
        {
           reloading = true;
           Invoke("reloadFinished", timebetweenshooting);
        }

        private void Shoot()
        {
            readytoshoot = false;

            float x = Random.Range(-spread,spread);
            float y = Random.Range(-spread,spread);

            Vector3 direction = fpscam.transform.forward + new Vector3(x, y, 0);

            if(Physics.Raycast(fpscam.transform.position, direction, out rayhit, range, enemy))
            {
                Debug.Log("hit");

                if(rayhit.collider.CompareTag("enemy"))
                {
                    //rayhit.collider.GetComponent<pain>().TakeDamage(damage);
                }
            }

            Instantiate(bulletHoleGraphic, rayhit.point, Quaternion.Euler(0,180,0));


            bulletsleft--;
            bulletsshot--;
            Invoke("ResetShot", timebetweenshooting);

            if(bulletsleft > 0 && buttonhold == true && shooting)
            {
            Invoke("Shoot", timebetweenshots);
            }
        }

        private void ResetShot()
        {
            readytoshoot = true;
        }
        private void reloadFinished()
        {
            bulletsleft = magazinesize;
            reloading = false;
        }

}
