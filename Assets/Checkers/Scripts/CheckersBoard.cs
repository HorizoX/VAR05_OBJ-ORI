using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
    public CheckerPiece[,] Pieces = new CheckerPiece[8, 8];
    
    public GameObject WhitePiecePrefab;
    public GameObject BlackPiecePrefab;

    private Vector3 BoardOffset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 PieceOffset = new Vector3(0.5f, 0, 0.5f);

    public bool IsWhite;
    private bool IsItWhiteTurn;
    private bool IsKilled; 

    private CheckerPiece SelectedPiece;
    private List<CheckerPiece> ForcedPieces;

    private Vector2 MouseOver;
    private Vector2 StartDrag;
    private Vector2 EndDrag;

    private void Start()
    {
        IsItWhiteTurn = true;
        ForcedPieces = new List<CheckerPiece>();
        GenerateBoard();
    }

    private void Update()
    {
        UpdateMouseOver();

        //If it is my turn 
        {
            int x = (int)MouseOver.x;
            int y = (int)MouseOver.y;

            if (SelectedPiece != null)
            {
                UpdatePieceDrag(SelectedPiece);
            }

            if (Input.GetMouseButtonDown(0))
            {
                SelectPiece(x, y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                TryMove((int)StartDrag.x , (int)StartDrag.y , x, y);
            }
        }

    }

    private void UpdateMouseOver()
    {

        

        if (!Camera.main)
        {
            Debug.Log("Unable to find the main camera!");
            return;
        }

        RaycastHit Hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 25.0f, LayerMask.GetMask("Board")))
        {

            MouseOver.x = (int)(Hit.point.x - BoardOffset.x);
            MouseOver.y = (int)(Hit.point.z - BoardOffset.z);

        }

        else
        {
            MouseOver.x = -1;
            MouseOver.y = -1;

        }
    }

    private void UpdatePieceDrag(CheckerPiece P)
    {
        if (!Camera.main)
        {
            Debug.Log("Unable to find the main camera!");
            return;
        }

        RaycastHit Hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit, 25.0f, LayerMask.GetMask("Board")))
        {
            P.transform.position = Hit.point + Vector3.up;
        }
    }

    private void SelectPiece(int x, int y)
    {
        //If we are out of bounds

        if (x < 0  || x >= 8 || y < 0 || y >= 8)
        {
            return;
        }

        CheckerPiece P = Pieces[x, y];

        if (P != null && P.IsWhite == IsWhite)
        {
            if (ForcedPieces.Count == 0)
            {
                SelectedPiece = P;
                StartDrag = MouseOver;
            }
            else
            {
                //look for the piece under our forced pieces list
                if (ForcedPieces.Find(FP => FP == P) == null)
                {
                    return;
                }

                SelectedPiece = P;
                StartDrag = MouseOver;
            }
            

        }
    }

    private void TryMove(int x1, int y1, int x2, int y2)
    {

        ForcedPieces = ScanForPossibleMove();
        //Multiplayer Support
        StartDrag = new Vector2(x1, y1);
        EndDrag = new Vector2(x2, y2);
        SelectedPiece = Pieces[x1, y1];

        
        //check if we are out of bounds
        if (x2 < 0 || x2 >= 8 || y2 < 0 || y2 >= 8)
        {
            if (SelectedPiece != null)
            {
                //Start Position
                MovePiece(SelectedPiece, x1, y1);
            }

            StartDrag = Vector2.zero;
            SelectedPiece = null;
            return;
        }

        //If there is a selected piece
        if (SelectedPiece != null)
        {
            //If it has not moved
            if (EndDrag == StartDrag)
            {
                MovePiece(SelectedPiece, x1, y1);
                StartDrag = Vector2.zero;
                SelectedPiece = null;
                return;
            }

            //Check if it is a Valid move
            if (SelectedPiece.ValidMove(Pieces, x1, y1, x2, y2))
            {
                // Did we kill Anything?
                // If This is a jump
                if (Mathf.Abs(x2 - x1) == 2)
                {
                    CheckerPiece P = Pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (P != null)
                    {
                        Pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                        DestroyImmediate(P.gameObject);
                        IsKilled = true;
                    }
                }

                //Were we supoosed to kill anything?
                if (ForcedPieces.Count != 0 && !IsKilled)
                {
                    MovePiece(SelectedPiece, x1, y1);
                    StartDrag = Vector2.zero;
                    SelectedPiece = null;
                    return;
                }

                Pieces[x2, y2] = SelectedPiece;
                Pieces[x1, y1] = null;
                MovePiece(SelectedPiece, x2, y2);


                EndTurn();

           }
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

        SelectedPiece = null;
        StartDrag = Vector2.zero;

        if (ScanForPossibleMove(SelectedPiece, x, y).Count != 0 && IsKilled)
        {
            return;
        }


        IsItWhiteTurn = !IsItWhiteTurn;
        IsWhite = !IsWhite;
        IsKilled = false;
        CheckVictory();

    }

    private void CheckVictory()
    {

    }

    private List<CheckerPiece> ScanForPossibleMove(CheckerPiece P, int x, int y)
    {
        ForcedPieces = new List<CheckerPiece>();

        if (Pieces[x, y].IsForceToMove(Pieces, x, y))
        {
            ForcedPieces.Add(Pieces[x, y]);
        }

        return ForcedPieces;
    }
    private List<CheckerPiece> ScanForPossibleMove()
    {
        ForcedPieces = new List<CheckerPiece>();

        //Check All the pieces
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
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

    private void GeneratePiece(int x, int y)
    {
        bool isPieceWhite = (y > 3) ? false : true;
        GameObject Go = Instantiate((isPieceWhite) ? WhitePiecePrefab : BlackPiecePrefab ) as GameObject;
        Go.transform.SetParent(transform);
        CheckerPiece P = Go.GetComponent<CheckerPiece>();
        Pieces[x, y] = P;
        MovePiece(P, x, y);

    }

    private void MovePiece(CheckerPiece P, int x, int y)
    {
        P.transform.position = (Vector3.right * x) + (Vector3.forward * y) + BoardOffset + PieceOffset;
    }
}
