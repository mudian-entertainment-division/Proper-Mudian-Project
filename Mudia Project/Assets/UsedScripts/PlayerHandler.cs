using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHandler : MonoBehaviour
{
    
    [Header("Value Variables")]
    public float maxHealth;
    
    public float curHealth, healRate;

    public GameObject m_FrontCheck;

    public Slider healthBar;
   
    [Header("Damage Effect Variables")]
    
    public GameObject deathImage;
    
    
   
   
    public static bool isDead = false;
  
    bool canHeal;
    float healTimer;
   
   
   
    public string character;
    public Text text;
    [Header("Check Point")]
    public Transform curCheckPoint;

    //[Header("Save")]
    //public PlayerSaveAndLoad saveAndLoad;
    // Start is called before the first frame update
    private void Start()
    {
        
        healRate = 0;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0 && !isDead)
        {
            Death();
        }
        if (healthBar.value != Mathf.Clamp01(curHealth/maxHealth))
        {
            LoseHealth();
        }
       
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            curHealth -= 5;
        }
        
        
        
       
        if (!canHeal && curHealth < maxHealth && curHealth > 0)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= 5)
            {
                canHeal = true;
            }
        }
    }
    void LoseHealth()
    {
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        healthBar.value = Mathf.Clamp01(curHealth / maxHealth);

    }
   
    
    void Death()
    {
        
        isDead = true;
        text.text = "";
        deathImage.SetActive(true);


        Invoke("DeathText", 2f);
        Invoke("ReviveText", 6f);
        Invoke("Revive", 9f);
    }
    private void LateUpdate()
    {
        if (canHeal && curHealth < maxHealth && curHealth > 0)
        {
            HealOverTime();
        }

    }
    void Revive()
    {
        text.text = "";
        isDead = false;
        curHealth = maxHealth;
       
       
        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;
        deathImage.SetActive(false);
        
    }
    void DeathText()
    {
        text.text = "Well you've fucked up.";
    }
    void ReviveText()
    {
        text.text = "But I'll give you another chance";
    }
    
    private void OnTriggerEnter(Collider m_FrontCheck)
    {
        if (m_FrontCheck.gameObject.CompareTag("Climb"))
        {
            Movement._gravity = 0;
        }
    }
    private void OnTriggerExit(Collider m_FrontCheck)
    {
        
        if (m_FrontCheck.gameObject.CompareTag("Climb"))
        {
            Movement._gravity = 20;
        }
    }
    public void DamagePlayer(float damage)
    {
        
        
        canHeal = false;
        healTimer = 0;
    }

    public void HealOverTime()
    {
        if (curHealth > 0 && curHealth <= maxHealth && canHeal)
        {
            curHealth += Time.deltaTime * (healRate);
        }
    }
}
