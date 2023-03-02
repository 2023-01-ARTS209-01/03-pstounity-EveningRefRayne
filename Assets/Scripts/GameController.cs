using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Make sure there is one and only one Game Controller, for any game controlling needs.
    private static GameController _gameController;
 
    // Run when the game starts
    void Awake()
    {
        if (!_gameController){
            _gameController=this;
        } else {
            GameObject.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
