using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
	public class Level
    {
        public string LevelText;
        public int Unlocked;
        public bool isInteractable;
    }

    public GameObject levelButton;
    public List<Level> LevelList;
    public Transform Spacer;
    void Start()
    {
        //DeleteAll();
        FillList();
    }
    void FillList()
    {
        foreach(var level in LevelList)
        {
            GameObject newbutton = Instantiate(levelButton) as GameObject;
            LevelButton button = newbutton.GetComponent<LevelButton>();
            button.LevelText.text = level.LevelText;

            if(PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
            {
                level.Unlocked = 1;
                level.isInteractable = true;
            }
            button.unlocked = level.Unlocked;
            button.GetComponent<Button>().interactable = level.isInteractable;
            button.GetComponent<Button>().onClick.AddListener(() => loadLevels("Level" + button.LevelText.text)); 

            newbutton.transform.SetParent(Spacer);
        }
        SaveAll();
    }

    void SaveAll()
    {
 /*       if (PlayerPrefs.HasKey("Level1"))
        {
            return;
        }
        else*/
        {
            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach (GameObject buttons in allButtons)
            {
                LevelButton button = buttons.GetComponent<LevelButton>();
                PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
            }
        }
    }
    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    void loadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }
}
