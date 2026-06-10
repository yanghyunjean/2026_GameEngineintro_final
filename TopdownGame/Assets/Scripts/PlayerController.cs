using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Sprite[] spriteUp;

    public Sprite[] spriteDown;

    public Sprite[] spriteLeft;

    public Sprite[] spriteRight;

    public float frameTime = 0.15f;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private Vector2 input;

    private Vector2 velocity;

    private Sprite[] currentSprites;

    private int frameIndex = 0;

    private float timer = 0f;

    public int maxHp = 3;

    private int currentHp;

    private bool isInvincible = false;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = spriteDown;

        currentHp = maxHp;

        sr.sprite = currentSprites[0];
    }



    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.01f)
        {
            lastDirection = input.normalized;


            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    ChangeSprites(spriteRight);
                else
                    ChangeSprites(spriteLeft);
            }
            else
            {
                if (input.y > 0)
                    ChangeSprites(spriteUp);
                else
                    ChangeSprites(spriteDown);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        Debug.Log("현재 체력 : " + currentHp);

        if (currentHp <= 0)
        {
            Die();
            return;
        }
        

        StartCoroutine(InvincibleCoroutine());
    }

    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(1.0f);

        isInvincible = false;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ThrowObject();
        }

        
        if (input.sqrMagnitude <= 0.01f)
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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

    }

    private void ThrowObject()
    {
        Debug.Log("발사!");

        GameObject obj =
            Instantiate(projectilePrefab,
            firePoint.position,
            Quaternion.identity);

        Projectile projectile =
            obj.GetComponent<Projectile>();

        projectile.SetDirection(lastDirection);
    }

    private void ChangeSprites(Sprite[] newSprites)
    {
        if (currentSprites == newSprites)
            return;

        currentSprites = newSprites;
        frameIndex = 0;
        timer = 0f;
        sr.sprite = currentSprites[frameIndex];
    }

    private void Die()
    {
        Debug.Log("플레이어 사망");

        SceneManager.LoadScene("TitleScene");
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemObject item = collision GetComponent<ItemObject>();

            score += item GetPoint();

            GameDataManager.instance.playerData.collectedItems.Add(item.GetItem());

            scoreText.text = score.ToString();
            Destroy(collision.gameObject);

            GameDataManager.instance.SaveData(GameDataManager.Instance.playerData);
        }
    }
    */
}