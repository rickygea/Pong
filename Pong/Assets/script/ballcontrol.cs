using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{
    public Rigidbody2D rigid;
     float xforce, yforce;
    public float fixedspeed = 50f;
    float addx, addy, randomx, randomy;
    public bool isbuff;
    private Vector2 trajectoryOrigin;
    // Start is called before the first frame update
    void Start()
    {
        ulangi();
        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void tengahkanbola()
    {
        transform.position = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }

    void gerakkanbola()
    {
        randomx = Random.Range(0, fixedspeed);
        randomy = Random.Range(0, fixedspeed);
        addx = fixedspeed - randomy;
        addy = fixedspeed - randomx;
        Vector2 gaya = new Vector2(((Random.Range(0, 2) < 1.0f) ? -1 : 1) * (randomx + addx), ((Random.Range(0, 2) < 1.0f) ? -1 : 1) * (randomy + addy));
        rigid.AddForce(gaya);
        //float randominisialy = Random.Range(-yforce, yforce);
        //float arahrandom = Random.Range(0, 2);
        
        //if (arahrandom < 1.0f)
        //{
        //    rigid.AddForce(new Vector2(-xforce, randominisialy));
        //}
        //else
        //{
        //    rigid.AddForce(new Vector2(xforce, randominisialy));
        //}
    }
    void ulangi()
    {
        tengahkanbola();
        Invoke("gerakkanbola", 2);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    public void spawnsebagaibuff() {
        ulangi();
    }
}
