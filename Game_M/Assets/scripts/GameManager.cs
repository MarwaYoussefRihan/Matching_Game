using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

//Declarations

    [SerializeField]
    private Sprite bgImage;

    public List<Button> btns = new List<Button>();
    
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessesIndex, secondGuessIndex;
    private string firstGuessesPuzzle, secondGuessesPuzzle;

    public GameObject GamePopUp;

//Declarations End


//LoadAllImages
    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Images/animals");
    }



// Start is called before the first frame update
    void Start()
    {
        GetButtons();
        AddGamePuzzles();
        AddListeners();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }


//Functions

//add the created Btns to list btns
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("puzzleBtn");

        for( int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }


//AddSomeImgs
    void AddGamePuzzles()
    {
        int counter = btns.Count;
        int index = 0;
        for(int i = 0; i < counter; i++)
        {
            if(index == counter/2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }



    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener( () => PickPuzzle());
        }
    }


    public void PickPuzzle()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessesIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessesPuzzle = gamePuzzles[firstGuessesIndex].name;

            btns[firstGuessesIndex].image.sprite = gamePuzzles[firstGuessesIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessesPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

           // if(firstGuessesPuzzle == secondGuessesPuzzle)
           //  {
           //     print("puzzle match");
           // }
           // else
           // {
           //  print("not matched");
           // }

            StartCoroutine(checkThePuzzleMatch());
        }

    }


    IEnumerator checkThePuzzleMatch()
    {
        if(firstGuessesPuzzle == secondGuessesPuzzle)
        {
            yield return new WaitForSeconds(0.4f);
            btns[firstGuessesIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            //btns[firstGuessesIndex].image.color = new Color(0, 0, 0, 0);
            //btns[firstGuessesIndex].image.color = new Color(0, 0, 0, 0);
            countGuesses++;
            yield return new WaitForSeconds(0.4f);
            CheckTheGameFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            btns[firstGuessesIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
            countGuesses++;

        }
        //yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;

    }


    void CheckTheGameFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            GamePopUp.SetActive(true);
           // print("it took you " + countGuesses);
        }
    }

    public void ExitBtnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void RetryBtnClick()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
		var index = scene.buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }




    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
