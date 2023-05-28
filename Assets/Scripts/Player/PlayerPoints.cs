using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public TMP_Text pointsCounter;
    public TMP_Text finalPoints;

    private int _totalPoints = 0;
    // Start is called before the first frame update
    private void AddPoints(int points)
    {
        _totalPoints += points;

        finalPoints.SetText("Puntuacion: "+_totalPoints.ToString());
        pointsCounter.SetText(_totalPoints.ToString());

    }
}
