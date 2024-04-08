using Dino.Scripts;
using UnityEngine;

public class PlayerController : BasicController
{
    private bool isGrounded;
    private bool isBended;
    private bool isRunning;
    private Transform startTransform;
    
    private Animator animator;
    private Rigidbody2D rigidbody;
    private PolygonCollider2D polygonCollider;
    private PolygonCollider2D bendCollider;
    [SerializeField] private StateManager stateManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        bendCollider = transform.Find("BendCollider").gameObject.GetComponent<PolygonCollider2D>();
        startTransform = transform;
    }

    public override void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                // Выполняем необходимые действия при начале игры
                break;
            case GameState.Running:
                if (!isRunning && isGrounded)
                {
                    animator.Play("Run Animation");
                    isRunning = true;
                }
                break;
            case GameState.GameOver:
                animator.enabled = false;
                rigidbody.bodyType = RigidbodyType2D.Static;
                break;
            case GameState.Restart:
                animator.enabled = true;
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                transform.position = startTransform.position;
                transform.rotation = startTransform.rotation;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obtacle"))
        {
            stateManager.ChangeState(GameState.GameOver);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            isRunning = false;

            animator.Play("Freeze Animation");

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Constants.dinoJumpForce);
        }
    }

    public void Bend()
    {
        if (isGrounded && !isBended)
        {
            isBended = true;
            ChangeColliders();

            animator.Play("Bend Animation");
        }
    }

    public void UnBend()
    {
        if (isGrounded && isBended)
        {
            isBended = false;
            ChangeColliders();
            animator.Play("Run Animation");
        }
    }

    private void ChangeColliders()
    {
        polygonCollider.enabled = !polygonCollider.enabled;
        bendCollider.enabled = !bendCollider.enabled;
    }
}