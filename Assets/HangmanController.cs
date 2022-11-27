using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HangmanController : MonoBehaviour
{
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyboardContainer;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWorld;
    [SerializeField] GameObject restartButton;


    private string word;
    private int incorrectGuesses, correctGuesses;

    public TMP_Text winText;
    public TMP_Text loseText;

    private int win = 0;
    private int lose = 0;

    public int Lose { get => lose; set { lose = value; loseText.text = value.ToString(); } }
    public int Win { get => win; set { win = value; winText.text = value.ToString(); } }

    void Start()
    {
        InitialiseButtons();
        InitialiseGame();
    }

    private void InitialiseButtons()
    {
        for (int i = 65; i <= 90; i++)
        {
            CreateButton(i);
        }
    }

    private void InitialiseGame()
    {
        if (restartButton.activeInHierarchy == false) 
        {
            // Az eredeti állapotba állítjuk az adatokat
            incorrectGuesses = 0;
            correctGuesses = 0;
            foreach (Button child in keyboardContainer.GetComponentsInChildren<Button>())
            {
                child.interactable = true;
            }
            foreach(Transform child in wordContainer.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }
            foreach(GameObject stage in hangmanStages)
            {
                stage.SetActive(false);
            }
            foreach (Image cross in keyboardContainer.GetComponentsInChildren<Image>())
            {
                cross.gameObject.SetActive(false);
            }
            //Új szavak generálása
            word = generateWord().ToUpper();
            foreach (char letter in word)
            {
                var temp = Instantiate(letterContainer, wordContainer.transform);
            }
        }


    }

    private void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }

    private string generateWord()
    {
        string[] wordList = possibleWorld.text.Split("\n");
        string line = wordList[Random.Range(0, wordList.Length - 1)];
        return line.Substring(0, line.Length - 1);
    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for(int i = 0; i < word.Length; i++)
        {
            if (inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;
            }
        }
        if (letterInWord == false)
        {
            incorrectGuesses++;
            hangmanStages[incorrectGuesses - 1].SetActive(true);
        }
        CheckOutcome();
    }
    private void CheckOutcome()
    {
        if (correctGuesses == word.Length) //win
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            //Invoke("InitialiseGame", 3f);
            Win++;
            restartButton.SetActive(true);
        }
        if (incorrectGuesses == hangmanStages.Length) //lose
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            //Invoke("InitialiseGame", 3f);
            Lose++;
            restartButton.SetActive(true);
        }
    }

    public void RestartTheGame()
    {
        restartButton.SetActive(false);
        InitialiseGame();
    }

}
