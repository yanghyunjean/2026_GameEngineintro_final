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

    public HeartUI heartUI;

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
        heartUI.UpdateHeart(currentHp);
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
        heartUI.UpdateHeart(currentHp);
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

        GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemObject itemObject = collision.GetComponent<ItemObject>();

        EnemyController enemy = collision.GetComponent<EnemyController>();

        if (itemObject != null)
        {
            UseItem(itemObject.itemData);

            Destroy(collision.gameObject);

            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    private void UseItem(ItemData item)
    {
        // 아이템 먹으면 점수 증가
        GameDataManager.Instance.AddScore(50);

        switch (item.itemType)
        {
            case ItemType.Heal:

                currentHp += item.healAmount;

                currentHp = Mathf.Clamp(currentHp, 0, maxHp);

                heartUI.UpdateHeart(currentHp);
                break;

            case ItemType.Speed:

                StartCoroutine(SpeedBoost(item));
                break;

            case ItemType.Invincible:

                StartCoroutine(InvincibleItem(item));
                break;
        }
    }

    private IEnumerator SpeedBoost(ItemData item)
    {
        moveSpeed *= item.speedMultiplier;

        yield return new WaitForSeconds(item.duration);

        moveSpeed /= item.speedMultiplier;
    }

    private IEnumerator InvincibleItem(ItemData item)
    {
        isInvincible = true;

        yield return new WaitForSeconds(item.duration);

        isInvincible = false;
    }
}