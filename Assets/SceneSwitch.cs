using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {
    private bool Starting;
	
	void Update ()
    {
        Switch();
	}

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Starti());
        }
    }

    IEnumerator Starti()
    {
        if (Starting == false)
        {
            Starting = true;
            yield return new WaitForSeconds(1);
            ChunkManager.screenMoveSpeed = 6f;
            Application.LoadLevel("main");
            Starting = false;
        }
    }
}
