using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Transform tr;
    private Animation anim;

    public float moveSpeed;
    public float turnSpeed;

    private readonly float initHp = 100.0f;
    public float currentHp;
    private Image hpBar;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie; 
    // Start is called before the first frame update
    IEnumerator Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HPBAR")?.GetComponent<Image>();
        currentHp = initHp;
        DisplayHealth();

        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h =Input.GetAxis("Horizontal");
        float v =Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //asd
        //Debug.Log("h = " + h);
        //Debug.Log("v = " + v);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * moveSpeed*Time.deltaTime);
        tr.Rotate(Vector3.up* turnSpeed*Time.deltaTime*r);

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f) anim.CrossFade("RunF", 0.25f);
        else if (v <= -0.1f) anim.CrossFade("RunB",0.25f);
        else if (v >= 0.1f) anim.CrossFade("RunR", 0.25f);
        else if (v <= -0.1f) anim.CrossFade("RunL", 0.25f);
        else anim.CrossFade("Idle", 0.25f);

    }

    void OnTriggerEnter(Collider coll)
    {
        if (currentHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currentHp -= 10.0f;
            DisplayHealth();
            Debug.Log($"Player hp = {currentHp / initHp}");
            if(currentHp <= 0.0f)
            {
                playerDie();
            }
        }
    }

    void playerDie()
    {
        Debug.Log("player Die~.~");
   
        OnPlayerDie();
        //GameObject.Find("GameMgr").GetComponent<GameManeser>().IsGameOver = true;
        GameManeser.instance.IsGameOver = true;
    }
    void DisplayHealth()
    {
        hpBar.fillAmount = currentHp / initHp;
    }
}
