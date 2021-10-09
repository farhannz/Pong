using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;

    public float xInitialForce;
    public float yInititalForce;
    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rb2d.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInititalForce, yInititalForce);
        // sqrt(x^2 + y^2)
        float xNewForce = Mathf.Sqrt(Mathf.Pow(xInitialForce, 2) + Mathf.Pow(yRandomInitialForce, 2));
        float randomDirection = Random.Range(0, 2);
        if (randomDirection < 1.0f)
        {
            rb2d.AddForce(new Vector2(-xNewForce, yRandomInitialForce));
        }
        else
        {
            rb2d.AddForce(new Vector2(xNewForce, yRandomInitialForce));
        }
    }
    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
