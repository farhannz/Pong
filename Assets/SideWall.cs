using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    [SerializeField]
    private GameManager gameManager;
    void Start()
    {
        
    }

    // Akan dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding.
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        // Jika objek tersebut bernama "Ball":
        if (anotherCollider.name == "Ball")
        {
            // Tambahkan skor ke pemain
            player.IncrementScore();

            // Jika skor pemain belum mencapai skor maksimal...
            if (player.Score < gameManager.maxScore)
            {
                // ...restart game setelah bola mengenai dinding.
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
