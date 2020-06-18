using System.Collections;
using UnityEngine;


public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minY, maxY;
    [SerializeField] private float minTimeSpawn, maxTimeSpawn;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());// On commence un timer
    }

    // Update is called once per frame
    private IEnumerator Spawner()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            Spawn();
        }
    }
        private void Spawn()
    {
        var position = new Vector3
        {
            x = transform.position.x,
            y = Random.Range(minY, maxY),
            z = 0
        };
        Instantiate(enemy, position, Quaternion.identity);
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
