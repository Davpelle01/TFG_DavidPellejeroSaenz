using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject MenuSoundtrack;
    public GameObject GameUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        MenuSoundtrack.SetActive(true);
        GameUI.SetActive(false);
    }
}
