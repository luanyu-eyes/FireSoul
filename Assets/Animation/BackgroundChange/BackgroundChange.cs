using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;
    public float time;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetBool("ChangeToBlack", true);
            Invoke("ChangeImage", time);
        }
    }

    [System.Obsolete]
    void ChangeImage()
    {
        if (img1.active is true)
        {
            img1.SetActive(false);
            img2.SetActive(true);
        }
        else if (img2.active is true)
        {
            img2.SetActive(false);
            img3.SetActive(true);
        }

        else if (img3.active is true)
        {
            img3.SetActive(false);
            img4.SetActive(true);
        }
        else img1.SetActive(true);
        anim.SetBool("ChangeToBlack", false);
    }
}
