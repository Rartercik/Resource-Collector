using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    [SerializeField] ResourceReceiver _receiver;
    [SerializeField] GameObject _endGameWindow;

    private void Start()
    {
        _receiver.SetOnRecieveAction(CheckIfGameEnd);
    }

    private void CheckIfGameEnd()
    {
        if (_receiver.HasAllComponents())
            EndGame();
    }

    private void EndGame()
    {
        _endGameWindow.SetActive(true);
    }
}
