using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class firecontrol : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    public Transform firepos;
    public AudioClip firesfx;

    private new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Fire();
    }

    void Fire()
    {
        Instantiate(bullet, firepos.position, firepos.rotation);
        audio.PlayOneShot(firesfx, 1.0f);
    }
}
