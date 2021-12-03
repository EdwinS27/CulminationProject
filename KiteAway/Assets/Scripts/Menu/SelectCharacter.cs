using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {
    private static int selectedCharacter = 0;
    public static void setSelectedCharacter(int choice){ selectedCharacter = choice;}
    public static int getSelectedCharacter(){  return selectedCharacter;}
}
