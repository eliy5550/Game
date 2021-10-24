using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour
{
    //this script was made for the harpoon but with the orientation variable it's good for the bullets too
    public float orientation = 0; //orientation is for the bullets, set on the prefab

    void Update()
    {
        //move up
            transform.position += new Vector3(orientation,10, 0) * Time.deltaTime; //orientation is for the bullets
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ceiling")) //destory me when I hit the ceilings
        {
            Destroy(this.gameObject);
        }
    }

}
