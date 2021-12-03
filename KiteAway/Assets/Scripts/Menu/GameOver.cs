using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : SkillShots  {
    public Text shotsFired;
    public Text shotsConnected;
    public Text shotsMissed;
    public Text accuracy;
    private void Start() {getAndAssignValues();}
    void getAndAssignValues(){
        shotsFired.text = "Shots Fired: " + getShotsFired();
        shotsMissed.text = "Shots Missed: " + getShotsMissed();
        shotsConnected.text = "Shots Connected: " + getShotsConnected();
        // accuracy.text = "Skill Shot Accuracy: " + getShotsConnected() / getShotsFired();
    }
    public void resetGameFromGameOver(){
        resetGame();
        SceneManager.LoadScene("Menu");
    }
    public void endGame(){
        Application.Quit();
    }
}
