using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using com.cyborgAssets.inspectorButtonPro;

public class MathWhackAMole : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public Text questionNumberText;
    public Button startButton;
    public Button restartButton;
    public Transform[] spawnPositions;
    public GameObject[] numberPrefabs;
    public GameObject correctEffectPrefab;
    public GameObject wrongEffectPrefab;

    // Score Panel
    public GameObject ScoreCardPanel;
    public TMP_Text FinalScoreText;

    // Audio variables  
    public AudioClip correctAudio;
    public AudioClip wrongAudio;
    private AudioSource audioSource;

    private int correctAnswer;
    private int score;
    private int totalQuestions = 20;
    private int currentQuestion = 0;
    private float timeLimit = 3f;
    private float timer;
    private Coroutine currentTimerCoroutine;
    private bool isProcessingAnswer = false;

    public AudioClip celebrateSound;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        restartButton.interactable = false;
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        ScoreCardPanel.SetActive(false); // Hide score panel initially
        UpdateQuestionNumberText();
    }

    private void UpdateQuestionNumberText()
    {
        questionNumberText.text = $"{currentQuestion + 1}/{totalQuestions}";
    }

    [ProButton]
    private void StartGame()
    {
        ScoreCardPanel.SetActive(false); // Hide score panel on start

        score = 0;
        currentQuestion = 0;
        scoreText.text = "0";

        startButton.interactable = false;
        restartButton.interactable = true;

        UpdateQuestionNumberText();
        NextQuestion();
    }

    [ProButton]
    public void RestartGame()
    {
        if (currentTimerCoroutine != null)
        {
            StopCoroutine(currentTimerCoroutine);
        }
        DestroyAllNumbers();

        // Reset everything
        score = 0;
        currentQuestion = 0;
        scoreText.text = "0";
        questionText.text = "0";
        timerText.text = "0";
        questionNumberText.text = "Press Start to Begin";

        startButton.interactable = true;
        restartButton.interactable = false;
        ScoreCardPanel.SetActive(false); // Hide score panel
        isProcessingAnswer = false;
    }

    private void NextQuestion()
    {
        if (currentQuestion >= totalQuestions)
        {
            EndGame();
            return;
        }
        StartCoroutine(ShowNextQuestionWithDelay());
    }

    private IEnumerator ShowNextQuestionWithDelay()
    {
        questionText.text = "...";
        yield return new WaitForSeconds(2f);

        int num1 = Random.Range(7, 10);
        int num2 = Random.Range(7, 10);
        int sum = num1 + num2;
        correctAnswer = sum % 10;

        questionText.text = $"{num1} + {num2} = ?";
        UpdateQuestionNumberText();
        SpawnNumbers();

        timer = timeLimit;
        if (currentTimerCoroutine != null)
        {
            StopCoroutine(currentTimerCoroutine);
        }
        currentTimerCoroutine = StartCoroutine(Timer());
    }

    private void SpawnNumbers()
    {
        DestroyAllNumbers();
        List<int> options = new List<int> { correctAnswer };

        while (options.Count < 4)
        {
            int incorrectAnswer = Random.Range(1, 10);
            if (incorrectAnswer != correctAnswer && !options.Contains(incorrectAnswer))
            {
                options.Add(incorrectAnswer);
            }
        }

        for (int i = 0; i < options.Count; i++)
        {
            int temp = options[i];
            int randomIndex = Random.Range(i, options.Count);
            options[i] = options[randomIndex];
            options[randomIndex] = temp;
        }

        HashSet<int> usedPositions = new HashSet<int>();
        for (int i = 0; i < options.Count; i++)
        {
            int posIndex;
            do
            {
                posIndex = Random.Range(0, spawnPositions.Length);
            } while (usedPositions.Contains(posIndex));

            usedPositions.Add(posIndex);

            if (options[i] < 1 || options[i] > numberPrefabs.Length)
            {
                Debug.LogError($"Invalid option {options[i]} for prefab access.");
                continue;
            }

            GameObject prefabToInstantiate = numberPrefabs[options[i] - 1];
            GameObject instance = Instantiate(prefabToInstantiate, spawnPositions[posIndex].position, prefabToInstantiate.transform.rotation);
            instance.transform.SetParent(spawnPositions[posIndex]);
            instance.tag = options[i].ToString();
        }
    }

    private IEnumerator Timer()
    {
        while (timer > 0 && !isProcessingAnswer)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }

        if (!isProcessingAnswer)
        {
            if (currentQuestion >= totalQuestions - 1) // Stop game if last question is reached
            {
                EndGame();
            }
            else
            {
                score--;
                scoreText.text = score.ToString();
                DestroyAllNumbers();
                currentQuestion++;
                StartCoroutine(ShowNextQuestionWithDelay());
            }
        }
    }

    private void DestroyAllNumbers()
    {
        foreach (Transform position in spawnPositions)
        {
            foreach (Transform child in position)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void CheckAnswer(string numberTag, Vector3 position)
    {
        if (isProcessingAnswer) return;
        isProcessingAnswer = true;

        if (currentTimerCoroutine != null)
        {
            StopCoroutine(currentTimerCoroutine);
        }

        if (int.TryParse(numberTag, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                score++;
                PlayAudio(correctAudio);
                ShowEffect(correctEffectPrefab, position);
            }
            else
            {
                score--;
                PlayAudio(wrongAudio);
                ShowEffect(wrongEffectPrefab, position);
            }

            scoreText.text = score.ToString();
            DestroyAllNumbers();
            currentQuestion++;

            if (currentQuestion >= totalQuestions)
            {
                EndGame();
            }
            else
            {
                StartCoroutine(ShowNextQuestionWithDelay());
            }
        }
        isProcessingAnswer = false;
    }

    private void ShowEffect(GameObject effectPrefab, Vector3 position)
    {
        GameObject effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effectInstance, 1.5f);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void EndGame()
    {
        if (currentTimerCoroutine != null)
        {
            StopCoroutine(currentTimerCoroutine);
        }
        DestroyAllNumbers();

        questionText.text = "Game Over!";
        questionNumberText.text = "Complete!";

        startButton.interactable = true;
        restartButton.interactable = false;

        FinalScoreText.text = $"Final Score: {score}"; // Show final score
        ScoreCardPanel.SetActive(true); // Display the Score Panel

        PlayAudio(celebrateSound); // Play the celebration sound

        isProcessingAnswer = false;
    }

}
