using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] projectiles;
    private float _timer;
    public float timeBeforeSpawn;
    //public GameObject projectilePrefab;
    public TextMeshProUGUI distanceUI;
    public TextMeshProUGUI scoreUI;
    public int killCount;
    private float _distance;

    private void Update()
    {
        scoreUI.text = "Score: " + killCount;
        _timer += Time.deltaTime;
        _distance += Time.deltaTime;
        if (_timer >= timeBeforeSpawn)
        {
            _timer = 0;
            int ranNum = Random.Range(0, 3);
            int ranNumb = Random.Range(0, 2);
            Instantiate(projectiles[ranNumb], spawnPoints[ranNum].transform.position, projectiles[ranNumb].transform.rotation);
        }

        distanceUI.text = "Distance: " + _distance.ToString("F2");
    }
}
