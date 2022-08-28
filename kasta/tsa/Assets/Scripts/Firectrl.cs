using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Firectrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform FP;
    public AudioClip firesfx;

    private new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) Fire();
    }
    void Fire()
    {
        Instantiate(bullet, FP.position, FP.rotation);
        audio.PlayOneShot(firesfx,1.0f);
    }
}
