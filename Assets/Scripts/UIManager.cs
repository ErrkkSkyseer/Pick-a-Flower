using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// UImanager only manage UIs
public class UIManager : MonoBehaviour
{
    //UImanager need some infos from GM
    GameManager gm;

    // References text
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text roseText;
    [SerializeField] TMP_Text violetText;
    [SerializeField] TMP_Text sunflowerText;
    [SerializeField] TMP_Text suzuranText;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //Update UIs
        string s = "Level : " + GameManager.LEVEL;
        levelText.text = s;

        s = "Score \n" + GameManager.SCORE;
        scoreText.text = s;

        roseText.text       = gm.RoseCounter.ToString();
        violetText.text     = gm.VioletCounter.ToString();
        sunflowerText.text  = gm.SunflowerCounter.ToString();
        suzuranText.text    = gm.SuzuranCounter.ToString();
    }
}
