using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    private float timer;
    private int Score = 0;
    private bool Ending;
    [SerializeField]
    private Text ScoreField; 

    void Update()
    {
        timer = timer + 1 * Time.deltaTime;
        Debug.Log(Score);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
            if (coll.gameObject.tag == "egg")
            {
                Destroy(coll.gameObject);
                Score++;
            ScoreField.text = "Score =" + " " + Score;
            }
            else if (coll.gameObject.tag == "spike" && timer > 3)
            {
            Endgame();
            }
        
    }

    void Endgame()
    {
        Application.LoadLevel("Menu");
    }

}