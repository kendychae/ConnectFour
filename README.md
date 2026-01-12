# Connect Four - Blazor Web Application

A fully functional Connect Four game built with Blazor Server as part of CSE 325 Week 4 Assignment.

## Features

### Core Game Functionality
- **Classic Connect Four Gameplay**: Drop colored pieces into a 7-column, 6-row grid
- **Win Detection**: Automatically detects horizontal, vertical, and diagonal wins
- **Interactive UI**: Click on any column to drop your piece
- **Visual Feedback**: Winning pieces are highlighted with special animations
- **Turn Management**: Automatic player switching between Red and Yellow

### Additional Features (Assignment Requirement)
1. **Win Tracker**: Keeps track of consecutive wins for each player across multiple games
   - Displays current win count for Red and Yellow players
   - Persists throughout gaming session
   - Can be reset with "Reset Win Stats" button

2. **Move History**: Complete log of all moves made during the current game
   - Shows which player played in which column
   - Numbered list for easy reference
   - Scrollable history for longer games

### UI/UX Enhancements
- **Responsive Design**: Works on desktop and mobile devices
- **Smooth Animations**: Pieces drop with realistic animation
- **Color-Coded Players**: Clear visual distinction between Red and Yellow
- **Winner Celebration**: Animated announcement when a player wins
- **Clean Controls**: Easy-to-use "New Game" and "Reset Win Stats" buttons

## Technology Stack

- **.NET 8.0**: Latest .NET framework
- **Blazor Server**: Interactive server-side Blazor
- **C#**: Game logic and component code
- **CSS**: Custom styling with animations
- **Razor Components**: Modular component architecture

## Project Structure

ConnectFour/
 Components/
    Board.razor          # Game board component
    Pages/
       Game.razor       # Main game page (/connectfour)
       Game.razor.css   # Game styling
    Layout/
        NavMenu.razor    # Navigation menu
 GameState.cs             # Core game logic and state management
 Program.cs               # Application entry point

## How to Run

1. Ensure you have .NET 8.0 SDK installed
2. Navigate to the project directory
3. Run the application:
   dotnet run
4. Open your browser and navigate to the URL shown in the terminal (typically http://localhost:5299)
5. Click "Connect Four" in the navigation menu to start playing

## How to Play

1. The game starts with the Red player
2. Click on any column to drop your piece
3. Pieces fall to the lowest available position in that column
4. Players alternate turns
5. First player to connect four pieces horizontally, vertically, or diagonally wins!
6. Click "New Game" to play again (win stats are preserved)
7. Click "Reset Win Stats" to clear the win counter

## Learning Objectives Completed

This project demonstrates proficiency in:
- Blazor component development and lifecycle
- State management in Blazor applications
- Event handling and parameter passing between components
- CSS animations and responsive design
- C# game logic implementation
- Interactive render mode in Blazor

## Author

Created for BYU-Idaho CSE 325 - .NET Software Development
Week 4 Assignment: Build Connect Four game with Blazor

## License

Educational project for BYU-Idaho coursework.
