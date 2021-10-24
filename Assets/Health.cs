using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Text livesUI; 
    [SerializeField] GameObject damagedUI; 
    [SerializeField] GameObject pop; 
    [SerializeField] GameObject restart; 

    int lives = 3;
    float safeForSeconds = 0; //After you get hit you will be protected..

    private void Start()
    {
        damagedUI = GameObject.Find("damaged");
        if (damagedUI) { damagedUI.SetActive(false); } //disable the hit indcator
        restart = GameObject.Find("restart");
        if (restart) { restart.SetActive(false); } //disable the restart button
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball") && safeForSeconds <= 0) {//if hit the ball and not safe
            lives--; //damage -1
            UpdateLives(); // for UI
            SayImDamaged(); 
            if (lives <= 0) {
                Die();
            }
            safeForSeconds = 1; //safe for 1 second
        }

        if (collision.collider.CompareTag("Cherry") )
        {
            LevelScript.score += 100;
            LevelScript.UpdateScore();
            GameObject go =  Instantiate(pop , transform.position + new Vector3(0,2,1) , Quaternion.identity);
            Destroy(go , 2);
            Destroy(collision.collider.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (safeForSeconds > 0) { //count time for safety
            safeForSeconds -= Time.deltaTime;
        }
    }

    void UpdateLives() { 
        livesUI.text = "Lives : " + lives;
    }
    public void Die() {
        livesUI.text = "YOU DIED!";
        livesUI.color = Color.red;
        if (restart) { restart.SetActive(true); } //enable the restart button
        Destroy(this.gameObject); // remove player
    }

    void SayImDamaged() {
        if (damagedUI) { damagedUI.SetActive(true); } // activate -1 hit indicator, the indicator will turn himself off
    }
}
