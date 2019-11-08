using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHandler : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerStats
    {
        public string name;
        public int value;
    }
    [Header("Value Variables")]
    public float maxHealth;
    public float maxStamina;
    public float maxMana;
    public float curHealth, healRate;
    public static float curStamina;
    public float curMana;
    public float Armour;
    //public PlayerStats[] stats;
    [Header("Value Variables")]
    public Slider healthBar;
    public Slider staminaBar;
    public Slider manaBar;
    [Header("Damage Effect Variables")]
    public Image damageImage;
    public Image deathImage;
    public AudioClip deathClip;
    public float flashSpeed = 5.0f;
    public Color FlashColor = new Color(1, 0, 0, 0.2f);
    public AudioSource playerAudio;
    public static bool isDead = false;
    bool damaged;
    bool canHeal;
    float healTimer;
    public int selectedIndex, points;
    public struct Stats
    {
        public string statName;
        public int statValue;
        public int tempStat;
    };
    public Stats[] playerStats = new Stats[6];
    public int skinIndex, eyesIndex, armourIndex, hairIndex, clothesIndex, mouthIndex;
    public int[] stats = new int[6];
    public int className;
    public string character;
    public Text text;
    [Header("Check Point")]
    public Transform curCheckPoint;

    //[Header("Save")]
    //public PlayerSaveAndLoad saveAndLoad;
    // Start is called before the first frame update
    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        healRate = 0;
        //skinIndex = Customisation.skinIndex;
        //eyesIndex = Customisation.eyesIndex;
        //hairIndex = Customisation.hairIndex;
        //armourIndex = Customisation.armourIndex;
        //clothesIndex = Customisation.clothesIndex;
        //mouthIndex = Customisation.mouthIndex;
       
       //selectedIndex = Customisation.selectedIndex;
        //points = Customisation.points;

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
        if (staminaBar.value != Mathf.Clamp01(curStamina / maxStamina))
        {
            LoseStamina();
        }
        if (manaBar.value != Mathf.Clamp01(curMana / maxMana))
        {
            LoseMana();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            damaged = true;
            curHealth -= 5;
        }
        
        
        if(damaged && !isDead)
        {
            damageImage.color = FlashColor;
            damaged = false;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
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
    void LoseStamina()
    {
        curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
        staminaBar.value = Mathf.Clamp01(curStamina / maxStamina);
    }
    void LoseMana()
    {
        curMana = Mathf.Clamp(curMana, 0, maxMana);
        manaBar.value = Mathf.Clamp01(curMana / maxMana);
    }
    void Death()
    {
        
        isDead = true;
        text.text = "";
        playerAudio.clip = deathClip;
        playerAudio.Play();
        deathImage.color = FlashColor;
        deathImage.gameObject.GetComponent<Animator>().SetTrigger("IsDead");
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
        curMana = maxMana;
        PlayerHandler.curStamina = maxStamina;
        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;
        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Revive");
        
    }
    void DeathText()
    {
        text.text = "You've fallen in battle...";
    }
    void ReviveText()
    {
        text.text = "But the Battle isn't over yet....";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            curCheckPoint = other.transform;
           // saveAndLoad.Save();
            healRate = 10;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            healRate = 0;
        }
    }
    public void DamagePlayer(float damage)
    {
        damaged = true;
        curHealth -= (damage - Armour);
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
