using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public Image Blood;
    public float health, maxHealth = 100;
    public float _lerpSpeed = 3;

    private void Update()
    {
        BloodFiller();
    }

    private void BloodFiller()
    {
        Blood.fillAmount = Mathf.Lerp(a: Blood.fillAmount, b: health / maxHealth, t: _lerpSpeed * Time.deltaTime);
    }

    private void AddHealth()
    {
        health += 10;
    }

    private void ReduceHealth()
    {
        health -= 10;
    }
}
