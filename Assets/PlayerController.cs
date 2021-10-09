using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10.0f;
    public float yBoundary = 9.0f;
    private Rigidbody2D rb2d;
    private int score;

    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

   
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = rb2d.velocity;

        if (Input.GetKey(upButton))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }
        rb2d.velocity = vel;

        Vector3 pos = rb2d.position;

        if(pos.y > yBoundary)
        {
            pos.y = yBoundary;
        }
        else if(pos.y < -yBoundary)
        {
            pos.y = -yBoundary;
        }
        transform.position = pos;
    }


    public void IncrementScore()
    {
        score++;
    }
    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get { return score; }
    }
    // Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
    // Untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
}
