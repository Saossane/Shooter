using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;
    public PlayerBehaviour player; // Référence au script appliqué sur le joueur

    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        var playerGameObject = GameObject.FindWithTag("Player"); // Permet de trouver un GameObject grâce à son tag
        player = playerGameObject.GetComponent<PlayerBehaviour>(); // On récupère le script d’après le GameObject
        player.onDeath.AddListener(Die); // On ajoute la fonction Die() à la liste des actions à faire à la mort du joueur
    }



    

    private void OnCollisionEnter(Collision other)
    {
        // Si un projectile nous rentre dedans, on détruit l’objet
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}