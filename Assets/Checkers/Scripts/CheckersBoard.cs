using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    // The array to store all the pieces
    public CheckerPiece[,] Pieces = new CheckerPiece[8, 8];

    // Prefabs for the checker pieces
    public GameObject WhitePiecePrefab;
    public GameObject BlackPiecePrefab;

    // The offsets for the board and pieces
    private Vector3 BoardOffset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 PieceOffset = new Vector3(0.5f, 0, 0.5f);

    // The current player (white or black)
    public bool IsWhite;
    // Whether it is currently white's turn
    private bool IsItWhiteTurn;
    // Whether a piece has been killed on this turn
    private bool IsKilled;

    // The currently selected piece
    private CheckerPiece SelectedPiece;
    // A list of pieces that are forced to move (have to jump)
    private List<CheckerPiece> ForcedPieces;

    // The current mouse position over the board
    private Vector2 MouseOver;
    // The starting and ending positions of a move
    private Vector2 StartDrag;
    private Vector2 EndDrag;

    // Initializes the game board
    private void Start()
    {
        // Start with white's turn
        IsItWhiteTurn = true;
        // Initialize the list of forced pieces
        ForcedPieces = new List<CheckerPiece>();
        // Generate the board with pieces
        GenerateBoard();
    }

    // Called once per frame to update the game
    private void Update()
    {
        // Update the mouse position over the board
        UpdateMouseOver();

        // If it is the current player's turn
        {
            // Get the mouse position as board coordinates
            int x = (int)MouseOver.x;
            int y = (int)MouseOver.y;

            // If there is a selected piece, update its position
            if (SelectedPiece != null)
            {
                UpdatePieceDrag(SelectedPiece);
            }

            // If the left mouse button is pressed, select a piece
            if (Input.GetMouseButtonDown(0))
            {
                SelectPiece(x, y);
            }

            // If the left mouse button is released, try to move the selected piece
            if (Input.GetMouseButtonUp(0))
            {
                TryMove((int)StartDrag.x, (int)StartDrag.y, x, y);
            }
        }

    }

    // Updates the mouse position over the board
    private void UpdateMouseOver()
    {
        // If there is no main camera, return
        if (!Camera.main)
        {
            Debug.Log("Unable to find the main camera!");
            return;
        }

        RaycastHit Hit;

        // Cast a ray from the camera to the board to get the mouse position in world coordinates
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 25.0f, LayerMask.GetMask("Board")))
        {
            // Convert the world position to board coordinates
            MouseOver.x = (int)(Hit.point.x - BoardOffset.x);
            MouseOver.y = (int)(Hit.point.z - BoardOffset.z);
        }
        else
        {
            // If the ray doesn't hit the board, set the mouse position to (-1, -1)
            MouseOver.x = -1;
            MouseOver.y = -1;
        }
    }

    private void UpdatePieceDrag(CheckerPiece P)
    {
        // Check if the main camera exists in the scene
        if (!Camera.main)
        {
            Debug.Log("Unable to find the main camera!");
            return;
        }

        // Cast a ray from the mouse position to the game board
        RaycastHit Hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 25.0f, LayerMask.GetMask("Board")))
        {
            // Move the checker piece to the point where the ray hit the board, with an upward offset
            P.transform.position = Hit.point + Vector3.up;
        }
    }

    private void SelectPiece(int x, int y)
    {
        // Check if the given coordinates are out of bounds
        if (x < 0 || x >= 8 || y < 0 || y >= 8)
        {
            return;
        }

        // Get the checker piece at the given coordinates
        CheckerPiece P = Pieces[x, y];

        // Check if the checker piece exists and belongs to the current player
        if (P != null && P.IsWhite == IsWhite)
        {
            // If there are no forced pieces to move, select the current piece and start dragging it
            if (ForcedPieces.Count == 0)
            {
                SelectedPiece = P;
                StartDrag = MouseOver;
            }
            else
            {
                // If there are forced pieces to move, check if the current piece is one of them
                if (ForcedPieces.Find(FP => FP == P) == null)
                {
                    return;
                }

                // If the current piece is a forced piece, select it and start dragging it
                SelectedPiece = P;
                StartDrag = MouseOver;
            }
        }
    }


    // This function tries to move a checker piece from one position to another
    private void TryMove(int x1, int y1, int x2, int y2)
    {
        // First, scan for possible moves to update the list of forced pieces
        ForcedPieces = ScanForPossibleMove();

        // Set the start and end drag positions for the move
        StartDrag = new Vector2(x1, y1);
        EndDrag = new Vector2(x2, y2);

        // Set the selected piece to the piece at the start position
        SelectedPiece = Pieces[x1, y1];

        // Check if the end position is out of bounds
        if (x2 < 0 || x2 >= 8 || y2 < 0 || y2 >= 8)
        {
            // If so, move the selected piece back to the start position and reset the drag and selected piece
            if (SelectedPiece != null)
            {
                // Move the piece back to the start position
                MovePiece(SelectedPiece, x1, y1);
            }

            // Reset the start drag, selected piece, and end drag
            StartDrag = Vector2.zero;
            SelectedPiece = null;
            return;
        }

        // If there is a selected piece
        if (SelectedPiece != null)
        {
            // If the piece hasn't moved, move it back to the start position and reset the drag and selected piece
            if (EndDrag == StartDrag)
            {
                MovePiece(SelectedPiece, x1, y1);
                StartDrag = Vector2.zero;
                SelectedPiece = null;
                return;
            }

            // Check if the move is valid
            if (SelectedPiece.ValidMove(Pieces, x1, y1, x2, y2))
            {
                // Check if the move is a jump
                if (Mathf.Abs(x2 - x1) == 2)
                {
                    // Find the checker piece that was jumped over and remove it from the game
                    CheckerPiece P = Pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (P != null)
                    {
                        Pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                        DestroyImmediate(P.gameObject);
                        IsKilled = true;
                    }
                }

                // If we were supposed to jump over a piece but didn't, move the piece back to the start position and reset the drag and selected piece
                if (ForcedPieces.Count != 0 && !IsKilled)
                {
                    MovePiece(SelectedPiece, x1, y1);
                    StartDrag = Vector2.zero;
                    SelectedPiece = null;
                    return;
                }

                // Otherwise, move the piece to the end position and end the turn
                Pieces[x2, y2] = SelectedPiece;
                Pieces[x1, y1] = null;
                MovePiece(SelectedPiece, x2, y2);
                EndTurn();

            }
            // If the move is not valid, move the piece back to the start position and reset the drag and selected piece
            else
            {
                MovePiece(SelectedPiece, x1, y1);
                StartDrag = Vector2.zero;
                SelectedPiece = null;
                return;
            }
        }
    }

    private void EndTurn()
    {
        //Where we landed during the turn
        int x = (int)EndDrag.x;
        int y = (int)EndDrag.y;

        //Promotion to a king for both Black and White
        if (SelectedPiece != null)
        {
            if (SelectedPiece.IsWhite && !SelectedPiece.IsKing && y == 7)
            {
                SelectedPiece.IsKing = true;
                SelectedPiece.transform.Rotate(Vector3.right * 180);
            }
            else if (!SelectedPiece.IsWhite && !SelectedPiece.IsKing && y == 0)
            {
                SelectedPiece.IsKing = true;
                SelectedPiece.transform.Rotate(Vector3.right * 180);
            }
        }

        // Reset SelectedPiece and StartDrag
        SelectedPiece = null;
        StartDrag = Vector2.zero;

        // If there are more possible moves and we killed a piece, stay on the same player's turn
        if (ScanForPossibleMove(SelectedPiece, x, y).Count != 0 && IsKilled)
        {
            return;
        }

        // Switch the turn to the other player
        IsItWhiteTurn = !IsItWhiteTurn;
        IsWhite = !IsWhite;
        IsKilled = false;

        // Check if the game is over
        CheckVictory();
    }

    private void CheckVictory()
    {

    }

    // This function scans the board to find possible forced moves for a specific checker piece at position (x, y)
    // It takes a CheckerPiece object P and its position (x, y) as arguments
    // It returns a list of CheckerPiece objects that are forced to move
    private List<CheckerPiece> ScanForPossibleMove(CheckerPiece P, int x, int y)
    {
        ForcedPieces = new List<CheckerPiece>();

        // If the piece at position (x, y) is forced to move, add it to the list of forced pieces
        if (Pieces[x, y].IsForceToMove(Pieces, x, y))
        {
            ForcedPieces.Add(Pieces[x, y]);
        }

        return ForcedPieces;
    }

    // This function scans the entire board to find possible forced moves for all pieces of the current player
    // It does not take any arguments
    // It returns a list of CheckerPiece objects that are forced to move
    private List<CheckerPiece> ScanForPossibleMove()
    {
        ForcedPieces = new List<CheckerPiece>();

        //Check All the pieces
        for (int i = 0; i < 8; i++)
        {
            // If the piece at position (i, j) is not null and belongs to the current player
            for (int j = 0; j < 8; j++)
            {
                // If the piece at position (i, j) is forced to move, add it to the list of forced pieces
                if (Pieces[i,j] != null && Pieces[i, j].IsWhite == IsItWhiteTurn)
                {
                    if (Pieces[i, j].IsForceToMove(Pieces, i, j))
                    {
                        ForcedPieces.Add(Pieces[i, j]);
                    }
                }
            }
        }

        return ForcedPieces;
    }

    // This function generates the checkerboard and the pieces and initializes their position
    private void GenerateBoard()
    {

        //Generate White Team
        for (int y = 0; y < 3; y++)
        {
            bool OddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                //Generate Our Piece
                GeneratePiece((OddRow) ? x : x + 1 , y);
            }
        }

        //Generate Black Team
        for (int y = 7; y > 4; y--)
        {
            bool OddRow = (y % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                //Generate Our Piece
                GeneratePiece((OddRow) ? x : x + 1, y);
            }
        }


    }

    // This function generates a checker piece at the specified position and sets its parent transform
    private void GeneratePiece(int x, int y)
    {
        bool isPieceWhite = (y > 3) ? false : true;
        GameObject Go = Instantiate((isPieceWhite) ? WhitePiecePrefab : BlackPiecePrefab ) as GameObject;
        Go.transform.SetParent(transform);
        CheckerPiece P = Go.GetComponent<CheckerPiece>();
        Pieces[x, y] = P;
        MovePiece(P, x, y);

    }

    // This function sets the position of the specified checker piece
    private void MovePiece(CheckerPiece P, int x, int y)
    {
        P.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BoardOffset + PieceOffset;
    }
}
