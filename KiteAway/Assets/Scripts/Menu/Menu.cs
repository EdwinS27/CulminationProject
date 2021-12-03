using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : SelectCharacter   {
    private GameObject[] characterOptions;
    public int selectedCharacter = 0;
    private void Start() {
        characterOptions = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
            characterOptions[i] = transform.GetChild(i).gameObject;
        foreach(GameObject character in characterOptions)
            character.SetActive(false);
        if(characterOptions[0])
            characterOptions[0].SetActive(true);
    }
    void Update()   {
    }
    public void toggleLeft(){
        characterOptions[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
            selectedCharacter = characterOptions.Length - 1;
        setSelectedCharacter(selectedCharacter);
        characterOptions[selectedCharacter].SetActive(true);
    }
    public void toggleRight(){
        characterOptions[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if(selectedCharacter == characterOptions.Length)
            selectedCharacter = 0;
        setSelectedCharacter(selectedCharacter);
        characterOptions[selectedCharacter].SetActive(true);
    }
    public void confirmButton(){
        SceneManager.LoadScene("KiteAway");
    }
}
