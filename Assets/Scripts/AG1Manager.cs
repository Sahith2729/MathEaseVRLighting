using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AG1Manager : MonoBehaviour
{
    private int number1;
    private int number2;
    private int correctAnswer;
    
    public GameObject shieldObject;
    public GameObject resetObject;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timerText; // Text for displaying the timer
    public TextMeshProUGUI questionsLeftText; // Text for displaying questions remaining

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioSource audioSource;

    public Material correctMaterial;
    public Material wrongMaterial;
    public Material defaultMaterial;
    public Renderer objectRenderer;

    private float timer;
    private bool timerRunning;

    private int totalQuestions = 20; // Total number of questions in the game
    private int questionsRemaining;

    public static AG1Manager Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        questionsRemaining = totalQuestions; // Initialize remaining questions
        UpdateQuestionsLeftText();
        GenerateQuestion();
        ResetTimer();
        resetObject.SetActive(false);
        
    }

    public void GenerateQuestion()
    {
        if (questionsRemaining > 0)
        {
            number1 = Random.Range(0, 5);
            number2 = Random.Range(0, 6);
            correctAnswer = number1 + number2;

            questionText.text = $"{number1} + {number2} = ?";
            questionsRemaining--;
            UpdateQuestionsLeftText();
        }
        else
        {
            EndGame();
        }
    }

    public void ValidateAnswer(int selectedNumber)
    {
        if (!timerRunning)
        {
            StartTimer();
        }

        if (selectedNumber == correctAnswer)
        {
            audioSource.clip = correctSound;
            ChangeMaterialForASecond();

            Debug.Log("<color=yellow>Correct!</color>");
            GenerateQuestion(); // Generate a new question if questions remain
        }
        else
        {
            audioSource.clip = wrongSound;
            objectRenderer.material = wrongMaterial;

            Debug.Log("<color=red>Wrong answer. Try again!</color>");
        }
        audioSource.Play();
    }

    private void UpdateQuestionsLeftText()
    {
        questionsLeftText.text = $"Questions Left:\n{questionsRemaining}";
    }

    private void EndGame()
    {
        shieldObject.SetActive(true);
        questionText.text = "Game Over!";
        StopTimer();
        resetObject.SetActive(true);
        foreach (Transform child in resetObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        Debug.Log("All questions answered. Game Over!");
    }

    public void ConvertToNumber(string number, string tag)
    {
        if (tag == "Number")
        {
            string objectName = number;
            char firstChar = objectName[0];
            int firstCharAsInt = (int)Char.GetNumericValue(firstChar);
            //Debug.Log($"<color=yellow>GameObject destroyed: {firstCharAsInt}</color>");
            ValidateAnswer(firstCharAsInt);
        }
        
    }

    public void ChangeMaterialForASecond()
    {
        if (objectRenderer != null && correctMaterial != null && defaultMaterial != null)
        {
            StartCoroutine(TemporaryChange());
        }
        else
        {
            Debug.LogError("Please assign all required fields in the Inspector.");
        }
    }

    private IEnumerator TemporaryChange()
    {
        objectRenderer.material = correctMaterial;
        yield return new WaitForSeconds(1);
        objectRenderer.material = defaultMaterial;
    }

    private void ResetTimer()
    {
        timer = 0f;
        timerRunning = false;
        UpdateTimerText();
    }

    private void StartTimer()
    {
        timerRunning = true;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
    }

    private void UpdateTimerText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
    
    public void ResetGame()
    {
        questionsRemaining = totalQuestions; // Reset questions remaining
        UpdateQuestionsLeftText();
        GenerateQuestion(); // Generate the first question
        ResetTimer(); // Reset the timer
        shieldObject.SetActive(false); // Hide the shield object
        questionText.text = $"{number1} + {number2} = ?"; // Reset the question display
        objectRenderer.material = defaultMaterial; // Reset material to default
        resetObject.SetActive(false);
    }
    public void ChangeToGame3Scene()
    {
        SceneManager.LoadScene("Game 3");
    }
}
