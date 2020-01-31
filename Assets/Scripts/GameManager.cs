using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    private float spawnRate = 1.0f;
    private int score;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator SpawnTarget()
    {
        // Hierdoor spawnene er object als je de game start
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Dit zorgt ervoor dat de score wordt opgetelt
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        // Hierdoor kan je de gameover en restart button zien
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Dit zorgt ervoor dat de scene opnieuw wordt geladen als je op restart klikt
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {

        isGameActive = true;
        spawnRate /= difficulty;

        //dit start het spawn script en het blijft loopen
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);

        // Dit zorgt ervoor dat de title screen weggaat
        titleScreen.gameObject.SetActive(false);
    }
}