using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    // Indicates whether the checker piece is white or black
    public bool IsWhite;
    // Indicates whether the checker piece is a king or not
    public bool IsKing;

    // This function checks if the current piece can make a move by capturing an opponent's piece
    public bool IsForceToMove(CheckerPiece[,] Board, int x, int y)
    {
        // For white or king piece
        if (IsWhite || IsKing)
        {
            // Check if there is an opponent piece on the top left position
            if (x >= 2 && y <= 5)
            {
                CheckerPiece P = Board[x - 1, y + 1];

                // If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    // Check if it is possible to land after the jump
                    if (Board[x - 2, y + 2] == null)
                    {
                        return true;
                    }
                }
            }

            // Check if there is an opponent piece on the top right position
            if (x <= 5 && y <= 5)
            {
                CheckerPiece P = Board[x + 1, y + 1];

                // If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    // Check if it is possible to land after the jump
                    if (Board[x + 2, y + 2] == null)
                    {
                        return true;
                    }
                }
            }
        }
        // For black or king piece
        if (!IsWhite || IsKing)
        {
            // Check if there is an opponent piece on the bottom left position
            if (x >= 2 && y >= 2)
            {
                CheckerPiece P = Board[x - 1, y - 1];

                // If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    // Check if it is possible to land after the jump
                    if (Board[x - 2, y - 2] == null)
                    {
                        return true;
                    }
                }
            }

            // Check if there is an opponent piece on the bottom right position
            if (x <= 5 && y >= 2)
            {
                CheckerPiece P = Board[x + 1, y - 1];

                // If there is a piece, and it is not the same color as ours
                if (P != null && P.IsWhite != IsWhite)
                {
                    // Check if it is possible to land after the jump
                    if (Board[x + 2, y - 2] == null)
                    {
                        return true;
                    }
                }
            }
        }
        // If none of the above conditions are met, return false
        return false;
    }

    // This function checks if the move is valid or not
    public bool ValidMove(CheckerPiece[,] Board, int x1, int y1, int x2, int y2)
    {
        // If you are moving on top of another piece
        if (Board[x2, y2] != null)
        {
            return false;
        }

        int DeltaMove = (int)Mathf.Abs(x1 - x2);
        int DeltaMoveY = y2 - y1;

        // For White Piece
        if (IsWhite || IsKing)
        {
            if (DeltaMove == 1)
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
                    CheckerPiece P = Board[(x1 + x2) / 2, (y1 + y2) / 2];

                    // If there is a piece, and it is not the same color as ours
                    if (P != null && P.IsWhite != IsWhite)
                    {
                        return true;
                    }
                }
            }
        }
        // For Black Piece
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

                    // If there is a piece, and it is not the same color as ours
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
