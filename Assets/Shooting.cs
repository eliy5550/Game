using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] GameObject harpoon , bullet; //gameobjects to instantiate
    GameObject lastShot; // this variable holds the last harpoon you shot
    int ammo = 0; //for bullets
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { // if his space
            Shoot();
        }
    }

    public void Shoot() {
        if (ammo == 0) { //if no bullets shoot harpoon, otherwise shoot bullets
            if (lastShot == null) // you can't shoot 2 harpoons at ones so you can shoot if this gameobject does not exist
            {
                Vector3 positionForHarpoon = new Vector3(transform.position.x, transform.position.y - 14, 1);
                lastShot = Instantiate(harpoon, positionForHarpoon, transform.rotation);
            }
        }
        else //shoot bullets
        {
            ammo--;
            Vector3 bullet1pos = transform.position + new Vector3(-0.05f,0.5f,0);
            Vector3 bullet2pos = transform.position + new Vector3(0.05f, 0.5f, 0);
            GameObject b1 = Instantiate(bullet , bullet1pos, Quaternion.identity);
            Instantiate(bullet , bullet2pos, Quaternion.identity);
            //one bullet will go left. the other is set to go right by default
            HarpoonScript hs = b1.GetComponent<HarpoonScript>();
            if (hs) { hs.orientation *= -1; }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)//pickup 3 bullets from the ground
    {
        if (collision.gameObject.CompareTag("Gun")) {
            ammo += 3;
            Destroy(collision.gameObject);
        }
    }
}
