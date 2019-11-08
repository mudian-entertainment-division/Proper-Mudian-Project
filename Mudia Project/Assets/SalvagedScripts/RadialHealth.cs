using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialHealth : MonoBehaviour
{
    public Image radialIcon;
    public float curHealth, maxHealth;
    // Start is called before the first frame update
    void HealthChange()
    {
        float amount = Mathf.Clamp01(curHealth / maxHealth);
        radialIcon.fillAmount = amount;

    }

    // Update is called once per frame
    void Update()
    {
        HealthChange();
    }
}
