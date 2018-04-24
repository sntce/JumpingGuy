
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle, Playing, Ended, Ready};


public class GameController : MonoBehaviour {

    [Range (0f, 0.20f)]
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage platform;
    public GameObject uiIdle;

    public GameState gamestate = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

	private AudioSource musicPlayer;

    // Use this for initialization
    void Start () {
		musicPlayer = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update(){

		bool userAction = Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0);
        //Empieza el juego
        if (gamestate == GameState.Idle && userAction){
            gamestate = GameState.Playing;
            uiIdle.SetActive(false);
            player.SendMessage("UpdateState","Player Run");
            enemyGenerator.SendMessage("StarGenerator");
			musicPlayer.Play();
        }
		//empieza el juego
        else if (gamestate == GameState.Playing){
            Parallax();
        }
		//juego preparado para reiniciarce
        else if (gamestate == GameState.Ready){
			if (userAction){
				RestartGame();
			}
		}
    }

    void Parallax() {
        float finalspeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalspeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalspeed * 4, 0f, 1f, 1f);
    }

	public void RestartGame(){
		SceneManager.LoadScene ("Principal");
	}
}
