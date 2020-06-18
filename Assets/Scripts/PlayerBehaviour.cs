using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerBehaviour : MonoBehaviour
{
    public UnityEvent onDeath;

    [SerializeField] private float speed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject gameOverCanvas;

    private Rigidbody2D myRigidbody;
    private Inputs inputs;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
