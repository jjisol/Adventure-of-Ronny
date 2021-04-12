# Adventure_of_Ronny
unity engine game


![ezgif com-gif-maker (4)](https://user-images.githubusercontent.com/37439958/114431938-0c46df80-9bfb-11eb-9080-13bd183f01ad.gif)


# Enemy Movement using NavMeshAgent

```c#
public class EnemyMovement : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	PlayerHealth playerHealth;      // Reference to the player's health.
	HealthController enemyHealth;        // Reference to this enemy's health.
	UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.


	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth= GetComponent <HealthController> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}


	void Update ()
	{
		// If the enemy and the player have health left...
		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (player.position);
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}
	} 
}
```
