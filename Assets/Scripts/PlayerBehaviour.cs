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
    private Rigidbody myRigidbody;
    private Inputs inputs;
    private Camera mainCam; // Référence à la caméra taggée MainCamera

    // Start is called before the first frame update
    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;
        myRigidbody = GetComponent<Rigidbody>();
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
        ClampPosition();
    }

    /// <summary>
    /// On s’assure que le joueur reste dans les limites de l’espace de jeu
    /// </summary>
    private void ClampPosition()
    {
        // On récupère la distance entre la camera et le joueur
        var zDistance = Mathf.Abs(transform.position.z - mainCam.transform.position.z);
        // On assigne à cette variable la position dans le monde du coin inférieur gauche de la caméra
        var leftBottomCorner = mainCam.ScreenToWorldPoint(new Vector3
        {
            x = 0,
            y = 0,
            z = zDistance
        });
        // On assigne à cette variable la position dans le monde du coin supérieur droit de la caméra
        var rightTopCorner = mainCam.ScreenToWorldPoint(new Vector3
        {
            x = mainCam.pixelWidth,
            y = mainCam.pixelHeight,
            z = zDistance
        });
        var oldPosition = myRigidbody.position;
        // On vient limiter la position en x et y pour qu’elle soit dans les limites du champ de vision de la caméra
        var newPosition = new Vector3
        {
            x = Mathf.Clamp(oldPosition.x, leftBottomCorner.x, rightTopCorner.x),
            y = Mathf.Clamp(oldPosition.y, leftBottomCorner.y, rightTopCorner.y),
            z = oldPosition.z
        };
        myRigidbody.position = newPosition;
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