using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    public PlayerType playerType{get; private set;}
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCharacter(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCharacter(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCharacter(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCharacter(0, -1);
        }
    }
    
    private void MoveCharacter(int movx, int movy)
    {
        var posibleNewPosition = gameController.GetPosibleNewPosition(movx, movy);
        if (gameController.CheckIsValidPosition(posibleNewPosition))
        {
            MoveCharacter(posibleNewPosition);
        }
    }
    
    private void MoveCharacter(Vector2Int newPosition)
    {
        //gameController.characterPosition = newPosition;

        //GameObject gridCell = gameController.grid[gameController.characterPosition.y][gameController.characterPosition.x];
       // transform.SetParent(gridCell.transform);
        transform.localPosition = Vector3.zero;
    }

}
