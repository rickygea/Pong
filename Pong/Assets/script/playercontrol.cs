using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public GameManager gamemanager;
    public playercontrol lawan;
    public KeyCode btnatas = KeyCode.W;
    public KeyCode btnbawah = KeyCode.S;
    public float kecepatan = 10.0f;
    public float batas = 9.0f;
    public Rigidbody2D rigid;
    public int skor;
    Vector2 velocity;
    Vector3 position;
    private ContactPoint2D lastContactPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rigid.velocity;
        if (Input.GetKey(btnatas))
        {
            velocity.y = kecepatan;
        }
        else
        {
            if (Input.GetKey(btnbawah))
            {
                velocity.y = -kecepatan;
            }
            else
            {
                velocity.y = 0.0f;
            }
        }
        rigid.velocity = velocity;
         position = transform.position;
        if (position.y > batas)
        {
            position.y = batas;
        }
        else if (position.y < -batas)
        {
            position.y = -batas;
        }
        transform.position = position;
    }

    public void tambahskor() {
        skor++;
    }

    public void resetskor()
    {
        skor = 0;

    }

    public int score() {
        return skor;
    }

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            if (gamemanager.api) {
                lawan.skor = gamemanager.maksscore;
            }
            lastContactPoint = collision.GetContact(0);
        }
        
    }

    public void perpanjangraket(bool perpanjang) {
        if (perpanjang)
        {
            transform.localScale = new Vector3(1f, 4f, 1f);
        }
        else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "buff")
        {
            Debug.Log("ambilbuff");
            gamemanager.objbuff.SetActive(false);
            gamemanager.siapdispawn = true;
            StartCoroutine(buffing());
        }
    }

    IEnumerator buffing() {
        perpanjangraket(true);
        yield return new WaitForSeconds(gamemanager.jedaminspawnbuff);
        perpanjangraket(false);
    }
}
