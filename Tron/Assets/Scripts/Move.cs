using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public KeyCode upKey, downKey, rightKey, leftKey;
    public float speed = 16f;

    private Rigidbody2D rb2d;

    // spawning light walls
    public GameObject wallPrefab;
    private Collider2D wallCollider;
    private Vector2 lastWallPos;

    // Use this for initialization
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.up * speed;

        SpawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKey) && rb2d.velocity.y == 0)
        {
            rb2d.velocity = Vector2.up * speed;
            SpawnWall();
        }
        else if (Input.GetKeyDown(downKey) && rb2d.velocity.y == 0)
        {
            rb2d.velocity = Vector2.down * speed;
            SpawnWall();
        }
        else if (Input.GetKeyDown(leftKey) && rb2d.velocity.x == 0)
        {
            rb2d.velocity = Vector2.left * speed;
            SpawnWall();
        }
        else if (Input.GetKeyDown(rightKey) && rb2d.velocity.x == 0)
        {
            rb2d.velocity = Vector2.right * speed;
            SpawnWall();
        }

        FitColliderBetween(wallCollider, lastWallPos, transform.position);
    }

    void SpawnWall()
    {
        // Save last wall's position
        lastWallPos = transform.position;

        // Spawn a new Lightwall
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wallCollider = g.GetComponent<Collider2D>();
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        // Calculate the Center Position
        co.transform.position = a + (b - a)*0.5f;

        // Scale it (horizontally or vertically)
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }
}