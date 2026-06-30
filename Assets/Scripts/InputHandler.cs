using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private void Update()
    {
        if (turnManager.CurrentCharacter is not Player) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            turnManager.TryMoveCurrentCharacter(-1, 0);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            turnManager.TryMoveCurrentCharacter(1, 0);
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            turnManager.TryMoveCurrentCharacter(0, 1);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            turnManager.TryMoveCurrentCharacter(0, -1);
    }
}