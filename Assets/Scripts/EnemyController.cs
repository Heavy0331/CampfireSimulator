using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsTouching = true;
            player.damage(5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsTouching = false;
        }
    }

    private bool PlayerIsTouching { get; set; }
}
