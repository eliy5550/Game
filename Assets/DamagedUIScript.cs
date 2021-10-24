using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedUIScript : MonoBehaviour
{   
    //this script disables the hit indicator after 2 seconds.
    float timer;
    void Start()
    {
        timer = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -=Time.deltaTime;//count down
        if (timer <= 0) {
            timer = 2;
            this.gameObject.SetActive(false);
        }
    }
}
