using System.Collections;
using UnityEngine;

public enum GameState{
    menu, inGame, gamePaused, gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; //TRUCO DEL SINGLETON
    public GameState currentGameState;

    void Awake(){
        sharedInstance = this;
    }

    void Start(){
        currentGameState = GameState.menu;  
    }

    void Update()
    {
        if (Input.GetButtonDown("s")){
            if (currentGameState != GameState.inGame)
            {
                StartGame();
            }
            
        }    
    }

    //Use this for start the game
    public void StartGame(){
        PlayerMovement.sharedInstance.StartGame();
        ChangeGameState(GameState.inGame);
    }

    //Use this for end the game
    public void GameOver(){
        ChangeGameState(GameState.gameOver);
    }

    //Use this for pause the game
    public void PauseGame(){
        ChangeGameState(GameState.gamePaused);
    }

    //Call it when the player decides to finish and return to the Main Menu
    public void BackToMainMenu(){
        ChangeGameState(GameState.menu);
    }

    void ChangeGameState(GameState newGameState){
        if (newGameState == GameState.menu){
            //La escena de Unity deberá mostrar el menú principal
            currentGameState = GameState.menu;

        } else if (newGameState == GameState.inGame){
            //la escena de Unity debe configurarse para mostrar el juego en si.
            currentGameState = GameState.inGame;

        } else if (newGameState == GameState.gamePaused){
            //La escena de Unity debe mostrar la ventana de juego pausado
            currentGameState = GameState.gamePaused;

        } else if (newGameState == GameState.gameOver){
            //La escena de Unity mostrará la ventana de Game Over.
            currentGameState = GameState.gameOver;
        }

    }
    
}
