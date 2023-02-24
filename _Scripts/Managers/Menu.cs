using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    public int time, spawnTime;

    // Start is called before the first frame update
    void Update()
    {
       /*  if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            time = UIManager.instance.playTime;
            spawnTime = UIManager.instance.spawnEnemiesTime;
        } */
    }
    void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy (gameObject);
		}
    }
}
