namespace ConnectFour;

/// <summary>
/// Manages the complete state and logic for a Connect Four game.
/// Handles board state, move validation, win detection, and game statistics.
/// </summary>
public class GameState
{
    /// <summary>
    /// Number of rows in the game board (standard Connect Four: 6)
    /// </summary>
    public const int Rows = 6;
    
    /// <summary>
    /// Number of columns in the game board (standard Connect Four: 7)
    /// </summary>
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

    /// <summary>
    /// Gets the current player whose turn it is
    /// </summary>
    public GamePiece CurrentPlayer => (GamePiece)_currentPlayer;
    
    /// <summary>
    /// Gets the winning player, or Empty if no winner yet
    /// </summary>
    public GamePiece WinningPlayer => _winningPlayer;
    
    /// <summary>
    /// Gets the winning play containing the four connected pieces
    /// </summary>
    public WinningPlay WinningPlay => _winningPlay;
    
    /// <summary>
    /// Gets the complete move history for the current game
    /// </summary>
    public List<string> MoveHistory => _moveHistory;
    
    /// <summary>
    /// Gets the total wins for Red player
    /// </summary>
    public int Player1Wins => _player1Wins;
    
    /// <summary>
    /// Gets the total wins for Yellow player
    /// </summary>
    public int Player2Wins => _player2Wins;
    
    /// <summary>
    /// Gets whether the game ended in a draw (board full, no winner)
    /// </summary>
    public bool IsDraw => _isDraw;
    
    /// <summary>
    /// Gets whether the game has ended (either won or drawn)
    /// </summary>
    public bool IsGameOver => _winningPlayer != GamePiece.Empty || _isDraw;

    /// <summary>
    /// Resets the game board to start a new game.
    /// Preserves win statistics across games.
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
    /// Gets the game piece at a specific board position
    /// </summary>
    /// <param name="row">Row index (0-5)</param>
    /// <param name="col">Column index (0-6)</param>
    /// <returns>The game piece at that position, or Empty if out of bounds</returns>
    public GamePiece GetPiece(int row, int col)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            return GamePiece.Empty;
        
        return (GamePiece)_board[row, col];
    }
    
    /// <summary>
    /// Checks if a column is full and cannot accept more pieces
    /// </summary>
    /// <param name="column">Column index to check (0-6)</param>
    /// <returns>True if the column is full, false otherwise</returns>
    public bool IsColumnFull(int column)
    {
        if (column < 0 || column >= Columns)
            return true;
            
        return _board[0, column] != 0;
    }

    /// <summary>
    /// Attempts to play a piece in the specified column for the current player
    /// </summary>
    /// <param name="column">Column to play in (0-6)</param>
    /// <returns>True if the move was successful, false if invalid or game over</returns>
    public bool PlayPiece(int column)
    {
        // Cannot play if game is already over
        if (IsGameOver)
            return false;

        // Validate column bounds
        if (column < 0 || column >= Columns)
            return false;

        // Find the lowest empty row in this column
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (_board[row, column] == 0)
            {
                _board[row, column] = _currentPlayer;
                _moveCount++;
                
                // Record the move in history
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
    /// <summary>
    /// Checks if the last played piece creates a winning connection
    /// </summary>
    /// <param name="row">Row where piece was played</param>
    /// <param name="col">Column where piece was played</param>
    /// <returns>True if this move wins the game</returns>
    private bool CheckForWin(int row, int col)
    {
        // Check all four possible winning directions:
        // - Horizontal (left-right)
        // - Vertical (up-down)
        // - Diagonal descending (top-left to bottom-right)
        // - Diagonal ascending (bottom-left to top-right)
        
        return CheckDirection(row, col, 0, 1) ||   // Horizontal
               CheckDirection(row, col, 1, 0) ||   // Vertical
               CheckDirection(row, col, 1, 1) ||   // Diagonal (\)
               CheckDirection(row, col, 1, -1);    // Diagonal (/)kForWin(int row, int col)
    {
        // Check horizontal
        if (CheckDirection(row, col, 0, 1) || 
            // Check vertical
            CheckDirection(row, col, 1, 0) || 
            // Check diagonal (down-right)
            CheckDirection(row, col, 1, 1) || 
            // Check diagonal (down-left)
            CheckDirection(row, col, 1, -1))
        {
            return true;
        }

    /// <summary>
    /// Checks for four consecutive pieces in a specific direction
    /// </summary>
    /// <param name="row">Starting row position</param>
    /// <param name="col">Starting column position</param>
    /// <param name="rowDir">Row direction (-1, 0, or 1)</param>
    /// <param name="colDir">Column direction (-1, 0, or 1)</param>
    /// <returns>True if four in a row found in this direction</returns>
    private bool CheckDirection(int row, int col, int rowDir, int colDir)
    {
        int count = 1; // Start with the current piece
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
        
        // Four or more in a row = win!
        if (count >= 4)
        {
            // Store the first four pieces of the winning sequence
/// <summary>
/// Represents the three possible states of a board cell
/// </summary>
public enum GamePiece
{
    /// <summary>
    /// Empty cell with no piece
    /// </summary>
    Empty = 0,
    
    /// <summary>
    /// Red player's piece
    /// </summary>
    Red = 1,
    
    /// <summary>
    /// Yellow player's piece
    /// </summary>
    Yellow = 2
}

/// <summary>
/// Represents a winning play consisting of four connected pieces.
/// Immutable record type for thread-safe state management.
/// </summary>
/// <param name="WinningMoves">Array of (Row, Column) tuples representing the winning pieces</param>
public record WinningPlay(params (int Row, int Column)[] WinningMoves)
{
    /// <summary>
    /// Creates an empty winning play (no win yet)
    /// </summary>
    public WinningPlay() : this(Array.Empty<(int, int)>()) { }
    
    /// <summary>
    /// Checks if a specific cell is part of the winning sequence
    /// </summary>
    /// <param name="row">Row to check</param>
    /// <param name="col">Column to check</param>
    /// <returns>True if this cell is one of the four winning pieces</returns>}
}

public enum GamePiece
{
    Empty = 0,
    Red = 1,
    Yellow = 2
}

public record WinningPlay(params (int Row, int Column)[] WinningMoves)
{
    public WinningPlay() : this(Array.Empty<(int, int)>()) { }
    
    public bool Contains(int row, int col)
    {
        return WinningMoves?.Any(m => m.Row == row && m.Column == col) ?? false;
    }
}
