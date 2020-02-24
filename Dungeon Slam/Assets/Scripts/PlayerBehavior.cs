using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
  enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }

  public Animator an;
  public Transform tr;
  public Grid grid;

  // Adjustable variables
  public float dashSpeed;
  public float stopDistance;
  public float verticalOffset;

  // caching variables
  private float x;
  private float y;

  private Vector3Int gridCoords;


  private Vector3 dash;

  private Direction direction;


  // Actual X and Y that player will try to move to
  private Vector3 dest;

  // Grid X and y that player will try to move to
  private Vector3Int gridDest;

  private bool moving;


  // Start is called before the first frame update
  void Start()
  {
    tr = GetComponent<Transform>();
    tr.SetPositionAndRotation(new Vector3(0.5f, 0.8f, 0), Quaternion.identity);
    an = GetComponent<Animator>();
    direction = Direction.Down;



  }

  // Update is called once per frame
  void Update()
  {
    x = tr.position.x;
    y = tr.position.y;
    gridCoords = grid.WorldToCell(tr.position);

    if (x == dest.x && y == dest.y)
    {
      moving = false;
    }

    if (!moving)
    {

      // Get input
      if (Input.GetKeyDown(KeyCode.W))
      {
        if (direction != Direction.Up)
        {
          tr.localScale = new Vector3(1, 1, 1);
          direction = Direction.Up;
        }
        else
        {
          gridDest.y = gridCoords.y + 1;
          gridDest.x = gridCoords.x;
          moving = true;
        }
      }

      if (Input.GetKeyDown(KeyCode.A))
      {
        if (direction != Direction.Left)
        {
          tr.localScale = new Vector3(1, 1, 1);
          direction = Direction.Left;
        }
        else
        {
          gridDest.x = gridCoords.x - 1;
          gridDest.y = gridCoords.y;
          moving = true;
        }
      }

      if (Input.GetKeyDown(KeyCode.S))
      {
        if (direction != Direction.Down)
        {
          tr.localScale = new Vector3(1, 1, 1);
          direction = Direction.Down;
        }
        else
        {
          gridDest.y = gridCoords.y - 1;
          gridDest.x = gridCoords.x;
          moving = true;
        }
      }

      if (Input.GetKeyDown(KeyCode.D))
      {
        if (direction != Direction.Right)
        {
          direction = Direction.Right;
          tr.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
          gridDest.x = gridCoords.x + 1;
          gridDest.y = gridCoords.y;
          moving = true;
        }
      }

      // Update animation direction
      an.SetInteger("direction", (int)direction);

      // If you moved during this turn
      if (moving)
      {
        // Check if the tile you're moving to is valid
        //if (grid.)

        dest = grid.CellToWorld(gridDest);
        dest.x += grid.cellSize.x / 2;
        dest.y += grid.cellSize.y / 2;
        // Account for vertical offset
        dest.y += verticalOffset;
      }

    }

    // What to do if you are moving
    else
    {
      Move();
    }
  }

  void Move()
  {
    dash = dest - transform.position;
    if (dash.magnitude < stopDistance)
    {
      transform.SetPositionAndRotation(dest, Quaternion.identity);
      moving = false;
    }
    else
    {
      transform.Translate((dest - transform.position).normalized * Time.deltaTime * dashSpeed);
    }
  }
}
