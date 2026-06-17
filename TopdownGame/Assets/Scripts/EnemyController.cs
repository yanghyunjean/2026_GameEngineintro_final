using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;

    public float frameTime = 0.15f;

    private SpriteRenderer sr;
    private Sprite[] currentSprites;

    private int frameIndex = 0;
    private float timer = 0f;

    private Vector2 moveDirection;
    private Transform player;

    public float moveSpeed = 2f;
    public float detectRange = 10f;

    public int maxHp = 3;
    private int currentHp;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentHp = maxHp;

        currentSprites = spriteDown;
        sr.sprite = currentSprites[0];

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    //데미지 처리
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    //죽음 처리 + 점수
    private void Die()
    {
        Debug.Log("Enemy 사망");

        GameDataManager.Instance.AddScore(100);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;

            moveDirection = dir;

            UpdateDirectionSprites(dir);

            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }

    private void UpdateDirectionSprites(Vector2 dir)
    {
        Sprite[] targetSprites;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            targetSprites = dir.x > 0 ? spriteRight : spriteLeft;
        }
        else
        {
            targetSprites = dir.y > 0 ? spriteUp : spriteDown;
        }

        if (currentSprites != targetSprites)
        {
            currentSprites = targetSprites;
            frameIndex = 0;
            timer = 0f;
            sr.sprite = currentSprites[0];
        }
    }

    void Update()
    {
        if (moveDirection.sqrMagnitude <= 0.01f)
        {
            frameIndex = 0;
            sr.sprite = currentSprites[frameIndex];
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex++;

            if (frameIndex >= currentSprites.Length)
                frameIndex = 0;

            sr.sprite = currentSprites[frameIndex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player =
                collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}