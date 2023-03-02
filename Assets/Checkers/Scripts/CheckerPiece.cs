using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public bool IsWhite;
    public bool IsKing;
    public bool ValidMove(CheckerPiece[,] Board, int x1, int y1, int x2, int y2)
    {
        //If you are moving on top of another piece
        if (Board[x2, y2] != null)
        {
            return false;
        }

        int DeltaMove = (int)Mathf.Abs(x1 - x2);
        int DeltaMoveY = y1 - y2;

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
