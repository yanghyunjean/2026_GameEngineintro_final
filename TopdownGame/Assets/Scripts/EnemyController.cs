using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;

    public float moveSpeed = 2f;
    public float detectRange = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;

            rb.MovePosition(
                rb.position + dir * moveSpeed * Time.fixedDeltaTime
            );
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("무언가와 충돌");
            PlayerController player =
                collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
