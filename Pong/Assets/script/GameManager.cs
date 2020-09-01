using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public playercontrol p1;
    public Rigidbody2D p1rigid;
    
    public playercontrol p2;
    public Rigidbody2D p2rigid;
    
    public ballcontrol ball;
    public Rigidbody2D ballrigid;
    public CircleCollider2D ballcollider;
    
    public int maksscore;


    public Trajectory trajectory;

    private bool isDebugWindowShown = false;

    [HideInInspector]
    public bool selesai;
    public GameObject objbuff;
    [HideInInspector]
    public bool siapdispawn = true;
    public float jedamaxspawnbuff, jedaminspawnbuff;
    public Coroutine spawnbuff;

    public int kemungkinanapi;
    public bool api;
    public GameObject efekapi;
    private void Start()
    {
        mulaispawnbuff();
    }

    void mulaispawnbuff() {
       
            spawnbuff = StartCoroutine(korotinespawnbuff());
      
    }

   public void toggleapi(bool input) {
        api = input;
        efekapi.SetActive(input);
    }

    public void kalkulasiapi() {
        int hasil = Random.Range(0, 100);
        if (hasil <= kemungkinanapi) {
            toggleapi(true);
        }
    }

   public IEnumerator korotinespawnbuff()
    {
        while (!selesai)
        {
            while (!siapdispawn)
            {
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(jedaminspawnbuff, jedamaxspawnbuff));
            objbuff.SetActive(true);
            objbuff.GetComponent<ballcontrol>().spawnsebagaibuff();
            siapdispawn = false;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + p1.skor);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + p2.skor);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            p1.resetskor();
            p2.resetskor();
            ball.SendMessage("ulangi", 0.5f, SendMessageOptions.RequireReceiver);
            siapdispawn = true;

        }
        {
            if (p1.skor == maksscore)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
                ball.SendMessage("tengahkanbola", null, SendMessageOptions.RequireReceiver);
                selesai = true;
            }
            else if (p2.skor == maksscore)
            {
                GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
                ball.SendMessage("tengahkanbola", null, SendMessageOptions.RequireReceiver);
                selesai = true;
            }
        }
        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            float ballMass = ballrigid.mass;
            Vector2 ballVelocity = ballrigid.velocity;
            float ballSpeed = ballrigid.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballcollider.friction;

            float impulsePlayer1X = p1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = p1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = p2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = p2.LastContactPoint.tangentImpulse;
            
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            GUI.backgroundColor = oldColor;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }

}
