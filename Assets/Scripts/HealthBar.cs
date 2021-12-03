using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [Header("- - - References - - -")]
    [SerializeField] gameState gameState;
    [SerializeField] GameObject rocket1Effect;
    [SerializeField] GameObject smallRocketEffect;
    [SerializeField] GameObject bigRocketEffect;
    [SerializeField] AudioManager audioManager;

    [Header("- - - Variables - - -")]
    [SerializeField] float continius_damage = 0.2f;
    [SerializeField] float Battery_Heal = 1000f;
    [SerializeField] float AOE_Damage = 250f;
    [SerializeField] Animator shakeAnimator;
    [SerializeField] GameObject particleEffect;

    void Update() 
    {
        gameState.continius_damage(continius_damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("?arpt?");
        if (collision.gameObject.tag=="Energy")
        {
            Debug.Log(Battery_Heal);
            gameState.heal_player(Battery_Heal);
            audioManager.PlayerBatteryPickup();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "AOE") 
        {
            Debug.Log("aoe");
            //gameState.damage_player(200, gameObject.GetChildWithName());
        }
        else if(collision.gameObject.tag == "Rocket1")
        {
            
            gameState.damage_player(200, gameObject.GetComponentInChildren<SpriteRenderer>(), gameObject.GetComponent<Collider2D>());
            shakeAnimator.SetTrigger("Shake1");
            if(rocket1Effect != null)
            {
                GameObject rocketEffect = Instantiate(rocket1Effect, collision.transform.position, collision.transform.rotation);
                GameObject Effect = Instantiate(particleEffect, collision.transform.position, collision.transform.rotation);
            }

            audioManager.PlayRocketHit();
            collision.gameObject.GetComponent<Warning>().destroyWarning();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "smallRocket")
        {
            gameState.damage_player(50, gameObject.GetComponentInChildren<SpriteRenderer>(), gameObject.GetComponent<Collider2D>());
            shakeAnimator.SetTrigger("Shake1");
            if (smallRocketEffect != null)
            {
                GameObject rocketEffect = Instantiate(smallRocketEffect, collision.transform.position, collision.transform.rotation);
            }

            audioManager.PlayRocketHit();
            //collision.gameObject.GetComponent<Warning>().destroyWarning();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BigRocket")
        {
            gameState.damage_player(300, gameObject.GetComponentInChildren<SpriteRenderer>(), gameObject.GetComponent<Collider2D>());
            shakeAnimator.SetTrigger("Shake1");
            if (bigRocketEffect != null)
            {
                GameObject rocketEffect = Instantiate(bigRocketEffect, collision.transform.position, collision.transform.rotation);
            }

            audioManager.PlayRocketHit();
            collision.gameObject.GetComponent<Warning>().destroyWarning();
            Destroy(collision.gameObject);
        }

    }
}
