using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
                if (instance == null)
                {
                    GameObject go = new GameObject("Player");
                    instance = go.AddComponent<Player>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        // Singleton implementation
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        // Get components
        animator = GetComponent<Animator>();
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        // Get PlayerMovement reference
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        // Get Animator from EngineEffect using Find
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Call Move method from PlayerMovement
        if (playerMovement != null)
            playerMovement.Move();
    }

    private void LateUpdate()
    {
        // Update animator IsMoving parameter
        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}

