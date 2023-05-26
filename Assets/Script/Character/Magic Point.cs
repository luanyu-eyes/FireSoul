using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MagicPoint : MonoBehaviour
{
    public Image MP;
    public float magic, maxMagic = 100;
    public float _lerpSpeed = 3;

    private void Update()
    {
        MPFiller();
    }

    private void MPFiller()
    {
        MP.fillAmount = Mathf.Lerp(a: MP.fillAmount, b: magic / maxMagic, t: _lerpSpeed * Time.deltaTime);
    }

    private void AddHealth()
    {
        magic += 10;
    }

    private void ReduceHealth()
    {
        magic -= 10;
    }
}
