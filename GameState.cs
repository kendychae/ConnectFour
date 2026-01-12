namespace ConnectFour;

public class GameState
{
    public const int Rows = 6;
    public const int Columns = 7;
    
    private readonly int[,] _board = new int[Rows, Columns];
    private int _currentPlayer = 1;
    private int _moveCount = 0;
    private GamePiece _winningPlayer = GamePiece.Empty;
    private WinningPlay _winningPlay = new();
    private readonly List<string> _moveHistory = new();
    
    // Win tracking
    private int _player1Wins = 0;
    private int _player2Wins = 0;

    public GamePiece CurrentPlayer => (GamePiece)_currentPlayer;
    public GamePiece WinningPlayer => _winningPlayer;
    public WinningPlay WinningPlay => _winningPlay;
    public List<string> MoveHistory => _moveHistory;
    public int Player1Wins => _player1Wins;
    public int Player2Wins => _player2Wins;

    public void ResetBoard()
    {
        Array.Clear(_board);
        _currentPlayer = 1;
        _moveCount = 0;
        _winningPlayer = GamePiece.Empty;
        _winningPlay = new();
        _moveHistory.Clear();
    }

    public GamePiece GetPiece(int row, int col)
    {
        if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            return GamePiece.Empty;
        
        return (GamePiece)_board[row, col];
    }

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
                
                // Switch players
                _currentPlayer = _currentPlayer == 1 ? 2 : 1;
                return true;
            }
        }

        return false; // Column is full
    }

    private bool CheckForWin(int row, int col)
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

        return false;
    }

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
