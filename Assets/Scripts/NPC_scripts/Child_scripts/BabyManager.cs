using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BabyManager : MonoBehaviour
{
    public int totalMissingKids;
    private int kidAtBase = 0;

    public void KidReachedBase()
    {
        kidAtBase++;

        // Show the win screen if all the kids are at the base
        if (kidAtBase >= totalMissingKids)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
