using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    //movement:
    Rigidbody2D rb;
    [SerializeField] float bounceForce;
    public float xVelocity ;

    //spawning other balls
    public int ballSize ;
    [SerializeField] GameObject[] balls;

    Vector3 offset;//for spawning 2 new balls
    [SerializeField] GameObject pop;//explosion or sound
    bool hit;//for hitting one bullet or harpoon

    [SerializeField] GameObject cherry;// bonus object

    private void Start()//init
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xVelocity, 5);
        offset = new Vector3(0.2f, 0,0);
        hit = false;
    }

    private void FixedUpdate()
    {
        // make sure the ball doesn't slow down
        if (rb.velocity.x != xVelocity && rb.velocity.x != -xVelocity) {
            if (rb.velocity.x > 0) { rb.velocity = new Vector2(Math.Abs(xVelocity), rb.velocity.y); }
            else { rb.velocity = new Vector2(-Math.Abs(xVelocity), rb.velocity.y); }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Collisions
    {
        if (collision.collider.CompareTag("Ground"))//when hit the ground
        {
            BounceUp();
        }
        else if (collision.collider.CompareTag("Wall"))//when hit a wall
        {

            rb.velocity = new Vector2(-xVelocity, rb.velocity.y);
            xVelocity *= -1;
        }
        else if (collision.collider.CompareTag("Player"))//when hit a player (the health script does more)
        {
            BounceUp();
        }
        else if (collision.collider.CompareTag("Harpoon")) //when hit a harpoon, the bullet also tagged as harpoon
        {
            if (!hit)//hit only one bullet or harpoon, without it 2 bullets might hit the same ball
            {
                BallIsHit();
            }
        }
    void BallIsHit() {
            hit = true;
            MakePopSound(); //can be an explotion
            LevelScript.score += 100;
            LevelScript.UpdateScore();
            if (ballSize > 1)
            {
                //create 2 balls
                GameObject leftball = Instantiate(balls[ballSize - 2], transform.position - offset, transform.rotation);
                Instantiate(balls[ballSize - 2], transform.position + offset, transform.rotation);
                //set the left ball to the left
                BallScript leftballBS = leftball.GetComponent<BallScript>();
                if (leftballBS != null) { leftballBS.xVelocity = -Math.Abs(xVelocity); }
            }
            SpawnACherry();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    void MakePopSound() {
        GameObject newpop = Instantiate(pop , transform.position , Quaternion.identity); //+100 ui
        Destroy(newpop, 2);// destroy the explosion or pop sound after 2 seconds
    }

    void BounceUp() {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }
    void SpawnACherry() {
        //randomly spawn a cherry that gives you extra points if you take it
        if (Random.Range(0 , 10) > 8)
        { 
            GameObject go= Instantiate(cherry , transform.position , Quaternion.identity);
            Destroy(go , 6);// destroy the cherry after 6 seconds
        }

    }

}
