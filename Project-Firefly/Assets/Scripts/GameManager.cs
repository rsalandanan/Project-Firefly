using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] projectiles;
    public GameObject potion;
    private float _timer;
    private float _timerForPotion;
    public float timeBeforeSpawn;

    public float timeBeforePotionSpawn;
    //public GameObject projectilePrefab;
    public TextMeshProUGUI distanceUI;
    public TextMeshProUGUI scoreUI;
    public int killCount;
    private float _distance;
    private int _ranNum;

    private void Update()
    {
        EnemyAttack();
        PotionDrop();
        scoreUI.text = "Score: " + killCount;
        _distance += Time.deltaTime;
        distanceUI.text = "Distance: " + _distance.ToString("F2");
    }

    private void EnemyAttack()
    {
        _timer += Time.deltaTime;
        if (_timer >= timeBeforeSpawn)
        {
            _timer = 0;
            _ranNum = Random.Range(0, 3);
            int ranNumb = Random.Range(0, 2);
            Instantiate(projectiles[ranNumb], spawnPoints[_ranNum].transform.position, projectiles[ranNumb].transform.rotation);
        }

    }

    private void PotionDrop()
    {
        _timerForPotion += Time.deltaTime;
        if (_timerForPotion >= timeBeforePotionSpawn)
        {
            _timerForPotion = 0;
            Instantiate(potion, spawnPoints[_ranNum].transform.position, quaternion.identity);
        }
    }
}
