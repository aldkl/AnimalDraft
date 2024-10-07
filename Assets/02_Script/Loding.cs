using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loding : MonoBehaviour
{
    public int SceneNum;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GoGame(SceneNum));

    }
    IEnumerator GoGame(int a)
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(a);
    }
}
