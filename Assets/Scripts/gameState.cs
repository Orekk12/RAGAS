using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameState : MonoBehaviour
{
    [Header("- - - References - - -")]

    [SerializeField] public Slider slider;
    [SerializeField] MenuManager menuManager;

    [Header("- - - Variables - - -")]
    [SerializeField] public bool isPlayerAlive;
    [SerializeField] public float Health=1000;
    [SerializeField] float flashDuration = 0.1f;
    [SerializeField] Color flashColor;
    [SerializeField] Color regularColor;
    [SerializeField] int duration=6;
    [SerializeField] bool godMode = false;
    [SerializeField] float score = 0;



    bool invincible = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        score += Time.deltaTime;
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = true;
        }*/
    }

    public int get_score()
    {
        return (int)score;
    }

    public bool get_isPlayerAlive()
    {
        if (Health<=0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void set_isPlayerAlive(bool b)
    {
        if(true)
        {
            isPlayerAlive = b;
        }
    }
    public void continius_damage(float value)
    {
        if (menuManager.isPaused == false)
        {
            Health -= value;
            slider.value = Health; 
        }  
    }

    public void damage_player(float value,SpriteRenderer obj, Collider2D col)
    {
        if (invincible && !godMode)
        {
            invincible = false;
            Health -= value;
            slider.value = Health;
            StartCoroutine(FlashOnHit(obj,col));
            invincible = true;

        }
    }
    public void heal_player(float value)
    {
        Health += value;
        slider.value = Health;
        if (Health>1000)
        {
            Health = 1000;
        }
    }

    private IEnumerator FlashOnHit(SpriteRenderer obj,Collider2D col)
    {
        //col.enabled = false;
        SpriteRenderer renderer = obj;
        //Debug.Log("PlayerSkills: " + (gameObject) + "is flashing");
        Physics.IgnoreLayerCollision(7, 0);
        int count = 0;
        while (count < duration) { 
            renderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            renderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            count += 1;
        }
        Physics.IgnoreLayerCollision(7, 0,false);
        //col.enabled = true;

    }
}
