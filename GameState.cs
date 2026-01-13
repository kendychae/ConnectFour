namespace ConnectFour;

/// <summary>
/// Manages the state and logic for a Connect Four game.
/// Handles piece placement, win detection, draw detection, and game statistics.
/// </summary>
public class GameState
{
    /// <summary>Number of rows in the game board (6 standard)</summary>
    public const int Rows = 6;

    /// <summary>Number of columns in the game board (7 standard)</summary>
    public const int Columns = 7;

    private readonly int[,] _board = new int[Rows, Columns];
    private int _currentPlayer = 1;
    private int _moveCount = 0;
    private GamePiece _winningPlayer = GamePiece.Empty;
    private WinningPlay _winningPlay = new();
    private readonly List<string> _moveHistory = new();
    private bool _isDraw = false;

    // Win tracking across multiple games
    private int _player1Wins = 0;
    private int _player2Wins = 0;

    /// <summary>Gets the current player (Red or Yellow)</summary>
    public GamePiece CurrentPlayer => (GamePiece)_currentPlayer;

    /// <summary>Gets the winning player, or Empty if no winner yet</summary>
    public GamePiece WinningPlayer => _winningPlayer;

    /// <summary>Gets the winning piece positions for highlighting</summary>
    public WinningPlay WinningPlay => _winningPlay;

    /// <summary>Gets the history of all moves in the current game</summary>
    public List<string> MoveHistory => _moveHistory;

    /// <summary>Gets Player 1 (Red) win count across all games</summary>
    public int Player1Wins => _player1Wins;

    /// <summary>Gets Player 2 (Yellow) win count across all games</summary>
    public int Player2Wins => _player2Wins;

    /// <summary>Gets whether the current game ended in a draw</summary>
    public bool IsDraw => _isDraw;

    /// <summary>
    /// Resets the board for a new game while preserving win statistics.
    /// </summary>
    public void ResetBoard()
    {
        Array.Clear(_board);
        _currentPlayer = 1;
        _moveCount = 0;
        _winningPlayer = GamePiece.Empty;
        _winningPlay = new();
        _moveHistory.Clear();
        _isDraw = false;
    }

    /// <summary>
    /// Gets the piece at the specified board position.
    /// </summary>
    /// <param name="row">Row index (0-5, top to bottom)</param>
    /// <param name="col">Column index (0-6, left to right)</param>
    /// <returns>The GamePiece at the position, or Empty if out of bounds</returns>
    public GamePiece GetPiece(int row, int col)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            return GamePiece.Empty;

        return (GamePiece)_board[row, col];
    }

    /// <summary>
    /// Attempts to play a piece in the specified column.
    /// The piece will fall to the lowest available row.
    /// </summary>
    /// <param name="column">The column to play in (0-6)</param>
    /// <returns>True if the move was successful, false if column is full or game is over</returns>
    public bool PlayPiece(int column)
    {
        if (_winningPlayer != GamePiece.Empty)
            return false;

        if (column < 0 || column >= Columns)
            return false;

        // Find the lowest empty row in this column
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (_board[row, column] == 0)
            {
                _board[row, column] = _currentPlayer;
                _moveCount++;

                // Record the move
                string move = $"{(GamePiece)_currentPlayer} plays column {column + 1}";
                _moveHistory.Add(move);

                // Check for winner
                if (CheckForWin(row, column))
                {
                    _winningPlayer = (GamePiece)_currentPlayer;
                    if (_currentPlayer == 1)
                        _player1Wins++;
                    else
                        _player2Wins++;
                    return true;
                }

                // Check for draw (board full)
                if (_moveCount >= Rows * Columns)
                {
                    _isDraw = true;
                    return true;
                }

                // Switch players
                _currentPlayer = _currentPlayer == 1 ? 2 : 1;
                return true;
            }
        }

        return false; // Column is full
    }

    /// <summary>
    /// Checks if the current player has won after placing a piece.
    /// Examines all four possible win directions from the placed piece.
    /// </summary>
    private bool CheckForWin(int row, int col)
    {
        // Check horizontal, vertical, and both diagonals
        if (CheckDirection(row, col, 0, 1) ||
            CheckDirection(row, col, 1, 0) ||
            CheckDirection(row, col, 1, 1) ||
            CheckDirection(row, col, 1, -1))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks for four-in-a-row in a specific direction from the placed piece.
    /// Uses bidirectional scanning for efficient win detection.
    /// </summary>
    private bool CheckDirection(int row, int col, int rowDir, int colDir)
    {
        int count = 1;
        var positions = new List<(int, int)> { (row, col) };

        // Check in positive direction
        for (int i = 1; i < 4; i++)
        {
            int r = row + (rowDir * i);
            int c = col + (colDir * i);

            if (r >= 0 && r < Rows && c >= 0 && c < Columns &&
                _board[r, c] == _currentPlayer)
            {
                count++;
                positions.Add((r, c));
            }
            else
                break;
        }

        // Check in negative direction
        for (int i = 1; i < 4; i++)
        {
            int r = row - (rowDir * i);
            int c = col - (colDir * i);

            if (r >= 0 && r < Rows && c >= 0 && c < Columns &&
                _board[r, c] == _currentPlayer)
            {
                count++;
                positions.Insert(0, (r, c));
            }
            else
                break;
        }

        if (count >= 4)
        {
            _winningPlay = new WinningPlay(
                positions.Take(4).Select(p => (p.Item1, p.Item2)).ToArray()
            );
            return true;
        }

        return false;
    }
}

/// <summary>
/// Represents a game piece on the Connect Four board.
/// </summary>
public enum GamePiece
{
    /// <summary>No piece (empty cell)</summary>
    Empty = 0,
    /// <summary>Player 1's red piece</summary>
    Red = 1,
    /// <summary>Player 2's yellow piece</summary>
    Yellow = 2
}

/// <summary>
/// Represents the winning four-in-a-row positions for visual highlighting.
/// </summary>
/// <param name="WinningMoves">Array of (Row, Column) tuples for the winning pieces</param>
public record WinningPlay(params (int Row, int Column)[] WinningMoves)
{
    /// <summary>Creates an empty WinningPlay (no winner yet)</summary>
    public WinningPlay() : this(Array.Empty<(int, int)>()) { }

    /// <summary>
    /// Checks if a specific board position is part of the winning sequence.
    /// </summary>
    /// <param name="row">Row to check</param>
    /// <param name="col">Column to check</param>
    /// <returns>True if the position is part of the winning four</returns>
    public bool Contains(int row, int col)
    {
        return WinningMoves?.Any(m => m.Row == row && m.Column == col) ?? false;
    }
}
