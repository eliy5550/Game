using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Rigidbody2D rb;
    public float xVelocity;
    [SerializeField] float maxXVel;
    //for android input:
    bool holdingLeft;
    bool holdingRight;

    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();
        bool holdingLeft = false; //unity says it's not used but it is through the android buttons
        bool holdingRight = false; //same here
    }

    void Update()
    {
        CheckKeyboardInput(); //can comment out when building for android

       //check if im holding left or right and if I'm not going out of bounds
        if (holdingLeft && !holdingRight && !(transform.position.x < LevelScript.leftborder + 0.75f))
        {
            MoveLeft();
        }
        else if (!holdingLeft && holdingRight && !(transform.position.x > LevelScript.rightborder - 0.75f))
        {
            MoveRight();
        }

        //if in out of bounds somehow, return me.
        if (transform.position.x > LevelScript.rightborder)
        {
            transform.position = new Vector3(LevelScript.rightborder - 0.25f, transform.position.y, transform.position.z);
        }
        else  if (transform.position.x < LevelScript.leftborder)
        {
            transform.position = new Vector3(LevelScript.rightborder + 0.25f, transform.position.y, transform.position.z);
        }

    }

    //adds or subtracts xVelocity to the position
    void MoveLeft()
    {
        transform.position += new Vector3(-xVelocity, 0, 0) * Time.deltaTime; //*(time) for all fps amounts to move at the same speed
    }
    void MoveRight()
    {
        transform.position += new Vector3(xVelocity, 0, 0) * Time.deltaTime; //*(time) for all fps amounts to move at the same speed
    }

    //keyboard input, I could use GetKey but I wanted the code for 
    //the input to be similar to the android
    void CheckKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            holdingLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            holdingLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            holdingRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            holdingRight = false;
        }
    }


    //for the android buttons:
    public void LeftDown()
    {
        holdingLeft = true;
    }

    public void LeftUp()
    {
        holdingLeft = false;
    }

    public void RightDown()
    {
        holdingRight = true;
    }

    public void RightUp()
    {
        holdingRight = false;
    }
}

