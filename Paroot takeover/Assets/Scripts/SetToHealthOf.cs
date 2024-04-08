using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToHealthOf : MonoBehaviour
{
    public Slider healthSlider;
    public Health playerHealth;
    private void Update()
    {
        healthSlider.value = playerHealth.health;
    }
}
