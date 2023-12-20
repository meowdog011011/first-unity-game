using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyScript : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GameScript gameScript;
    public float x = 2;
    public float speed;
    private Vector3 pausePosition = new Vector3(0, 0, 0);
    private Vector2 pauseRigidbodyVelocity = new Vector2(0, 0);

    void Start()
    {
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
        StartCoroutine(Blink());
    }

    void Update()
    {
        if (gameScript.stage == 5)
        {
            if (pausePosition == new Vector3(0, 0, 0))
            {
                pausePosition = transform.position;
            }
            if (pauseRigidbodyVelocity == new Vector2(0, 0))
            {
                pauseRigidbodyVelocity = rigidbody.velocity;
            }
            transform.position = pausePosition;
            rigidbody.velocity = pauseRigidbodyVelocity;
        }
    }

    void FixedUpdate()
    {
        if (gameScript.stage != 0 & gameScript.stage != 4 & gameScript.stage != 5)
        {
            if (x == -2)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            rigidbody.velocity = new Vector3(x, 0) * speed * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Left Platform" & collision.gameObject.name != "Right Platform")
        {
            x *= -1;
        }
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 0f));
        GetComponent<Animator>().enabled = true;
    }
}
