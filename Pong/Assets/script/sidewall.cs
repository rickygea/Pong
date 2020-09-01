using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sidewall : MonoBehaviour
{
    public GameManager gameManager;
    public playercontrol player;
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        if (anotherCollider.name == "Ball")
        {
            if (!gameManager.api)
            {
                player.tambahskor();
            }

            gameManager.toggleapi(false);
            if (player.skor < gameManager.maksscore)
            {

                anotherCollider.gameObject.SendMessage("ulangi", 2.0f, SendMessageOptions.RequireReceiver);
                gameManager.kalkulasiapi();
            }


        }
        if (anotherCollider.name == "buff")
         {
            gameManager.objbuff.SetActive(false);
            gameManager.siapdispawn = true;
        }
    }
}
