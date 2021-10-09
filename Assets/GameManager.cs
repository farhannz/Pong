using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player1;
    private Rigidbody2D rb2dPlayer1;

    public PlayerController player2;
    private Rigidbody2D rb2dPlayer2;

    public BallControl ball;
    private Rigidbody2D rb2dBall;
    private CircleCollider2D c2dBall;
    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;
    // Apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;
    public int maxScore;
    void Start()
    {
        rb2dPlayer1 = player1.GetComponent<Rigidbody2D>();
        rb2dPlayer2 = player2.GetComponent<Rigidbody2D>();
        rb2dBall = ball.GetComponent<Rigidbody2D>();
        c2dBall = ball.GetComponent<CircleCollider2D>();
    }
    private void OnGUI()
    {
        // Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // Ketika tombol restart ditekan, reset skor kedua pemain...
            player1.ResetScore();
            player2.ResetScore();

            // ...dan restart game.
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        // Jika pemain 1 menang (mencapai skor maksimal), ...
        if (player1.Score == maxScore)
        {
            // ...tampilkan teks "PLAYER ONE WINS" di bagian kiri layar...
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            // ...dan kembalikan bola ke tengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        // Sebaliknya, jika pemain 2 menang (mencapai skor maksimal), ...
        else if (player2.Score == maxScore)
        {
            // ...tampilkan teks "PLAYER TWO WINS" di bagian kanan layar... 
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            // ...dan kembalikan bola ke tengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
        }
        if (isDebugWindowShown)
        {
            // Simpan nilai warna lama GUI
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            // Simpan variabel-variabel fisika yang akan ditampilkan. 
            float ballMass = rb2dBall.mass;
            Vector2 ballVelocity = rb2dBall.velocity;
            float ballSpeed = rb2dBall.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = c2dBall.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            // Tentukan debug text-nya
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";
            // Tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            // Kembalikan warna lama GUI
            GUI.backgroundColor = oldColor;
           
            trajectory.enabled = !trajectory.enabled;
            // Toggle nilai debug window ketika pemain mengeklik tombol ini.
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
