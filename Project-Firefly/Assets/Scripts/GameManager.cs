using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    private float _timer;
    public float timeBeforeSpawn;
    public GameObject projectilePrefab;
    public TextMeshProUGUI distanceUI;
    private float _distance;

    private void Update()
    {
        _timer += Time.deltaTime;
        _distance += Time.deltaTime;
        if (_timer >= timeBeforeSpawn)
        {
            _timer = 0;
            int ranNum = Random.Range(0, 3);
            Instantiate(projectilePrefab, spawnPoints[ranNum].transform.position, projectilePrefab.transform.rotation);
        }

        distanceUI.text = "Distance: " + _distance.ToString("F2");
    }
}
