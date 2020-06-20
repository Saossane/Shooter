using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public UnityEvent onDeath; // public car les ennemies doivent pouvoir y accéder 

    [SerializeField] private float speed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject gameOverCanvas;


    private Vector2 direction;
    private Rigidbody2D myRigidbody;
    private Inputs inputs;
    private Camera mainCam; // Référence à la caméra taggée MainCamera

    // Start is called before the first frame update
    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;
        inputs.Player.Shoot.performed += OnShootPerformed;
        myRigidbody = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * (speed * Time.fixedDeltaTime);
        
    }

    /// <summary>
    /// On s’assure que le joueur reste dans les limites de l’espace de jeu
    /// </summary>
    

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        // On instantie un projectile à la position du joueur avec une rotation nulle
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si on rentre dans un ennemi, on lance la fonction Die()
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

    }

    private void OnDestroy()
    {
        inputs.Player.Move.performed -= OnMovePerformed;
        inputs.Player.Move.canceled -= OnMoveCanceled;
        inputs.Player.Shoot.performed -= OnShootPerformed;
    }

    /// <summary>
    /// Se lance lorsque l’on collisionne avec un ennemi ou qu’un ennemi atteint le bord de l’écran
    /// </summary>
    public void Die()
    {
        // Lancement de l’event onDeath
        onDeath.Invoke();
        // Instantiation de l’écran de Game Over
        Instantiate(gameOverCanvas);
        // Destruction de l’objet sur lequel le script est placé
        Destroy(gameObject);
    }
}