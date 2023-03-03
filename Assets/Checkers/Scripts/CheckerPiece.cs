using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public bool IsWhite;
    public bool IsKing;

    public bool IsForceToMove(CheckerPiece[,] Board, int x, int y)
    {
        if (IsWhite || IsKing)
        {
            //Top Left
            if (x >= 2 && y <= 5)
            {
                CheckerPiece P = Board[x - 1, y + 1];
                
                //If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    //Check if it is possible to land after the jump
                    if (Board[x - 2, y + 2] == null)
                    {
                        return true;
                    }
                }
            }



            //Top Right
            if (x <= 5 && y <= 5)
            {
                CheckerPiece P = Board[x + 1, y + 1];

                //If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    //Check if it is possible to land after the jump
                    if (Board[x + 2, y + 2] == null)
                    {
                        return true;
                    }
                }
            }
        }
        if (!IsWhite || IsKing) 
        {
            //Bottom Left
            if (x >= 2 && y >= 2)
            {
                CheckerPiece P = Board[x - 1, y - 1];

                //If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    //Check if it is possible to land after the jump
                    if (Board[x - 2, y - 2] == null)
                    {
                        return true;
                    }
                }
            }



            //Bottom Right
            if (x <= 5 && y >= 2)
            {
                CheckerPiece P = Board[x + 1, y - 1];

                //If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    //Check if it is possible to land after the jump
                    if (Board[x + 2, y - 2] == null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool ValidMove(CheckerPiece[,] Board, int x1, int y1, int x2, int y2)
    {
        //If you are moving on top of another piece
        if (Board[x2, y2] != null)
        {
            return false;
        }

        int DeltaMove = (int)Mathf.Abs(x1 - x2);
        int DeltaMoveY = y2 - y1;

        //For White Piece
        if (IsWhite || IsKing)
        {
            if (DeltaMove ==1)
            {
                if (DeltaMoveY == 1)
                {
                    return true;
                }
            }

            else if (DeltaMove == 2)
            {
                 if (DeltaMoveY == 2)
                {
                    CheckerPiece P = Board[(x1 + x2) / 2 , (y1 + y2) / 2];

                    if (P != null && P.IsWhite != IsWhite)
                    {
                        return true;
                    }
                }
            }
        }

        //For Black Piece
        if (!IsWhite || IsKing)
        {
            if (DeltaMove == 1)
            {
                if (DeltaMoveY == -1)
                {
                    return true;
                }
            }

            else if (DeltaMove == 2)
            {
                if (DeltaMoveY == -2)
                {
                    CheckerPiece P = Board[(x1 + x2) / 2, (y1 + y2) / 2];

                    if (P != null && P.IsWhite != IsWhite)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
