# 🎮 Connect Four - Blazor Web Application

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

A modern, fully-featured Connect Four game built with **Blazor Server** and **.NET 8**, showcasing interactive web development, state management, and responsive design principles.

## 🚀 Live Demo

> **Note:** This application runs locally. Follow the [How to Run](#-how-to-run) section to get started.

## ✨ Features

### 🎯 Core Gameplay

- **Classic Connect Four Mechanics**: Traditional 7×6 grid gameplay
- **Intelligent Win Detection**: Automatically identifies horizontal, vertical, and diagonal four-in-a-row patterns
- **Draw Detection**: Recognizes when the board is full with no winner
- **Real-time Turn Management**: Seamless player switching between Red and Yellow
- **Column Full Indicators**: Visual feedback for columns that are at capacity

### 📊 Game Statistics

- **Persistent Win Tracking**: Maintains win counts across multiple games
- **Move History Log**: Complete audit trail of all moves with player and column information
- **Session Statistics**: Track competitive performance during gameplay sessions

### 🎨 User Experience

- **Smooth Animations**: Realistic piece drop physics and winning celebration effects
- **Responsive Design**: Fully functional on desktop, tablet, and mobile devices
- **Visual Feedback**: Clear indicators for current player, winning pieces, and game state
- **Intuitive Controls**: One-click column selection and easy game reset options
- **Accessibility**: Keyboard navigation support and clear visual hierarchy

## 🛠️ Technology Stack

| Technology           | Purpose                                              |
| -------------------- | ---------------------------------------------------- |
| **.NET 8.0**         | Latest framework with performance improvements       |
| **Blazor Server**    | Real-time, interactive UI with server-side rendering |
| **C# 12**            | Modern language features and game logic              |
| **Razor Components** | Modular, reusable UI components                      |
| **CSS3**             | Custom animations and responsive layouts             |
| **ASP.NET Core**     | Web server and application hosting                   |

## 📁 Project Structure

```
ConnectFour/
├── Components/
│   ├── Board.razor              # Game board component with grid layout
│   ├── App.razor                # Root application component
│   ├── Pages/
│   │   ├── Game.razor          # Main game page (/connectfour)
│   │   ├── Game.razor.css      # Game-specific styling
│   │   ├── Home.razor          # Landing page
│   │   └── ...                 # Additional pages
│   └── Layout/
│       ├── MainLayout.razor    # Primary layout wrapper
│       └── NavMenu.razor       # Navigation component
├── GameState.cs                 # Core game logic and state management
├── Program.cs                   # Application entry point and configuration
├── ConnectFour.csproj          # Project file with dependencies
└── wwwroot/                     # Static assets (CSS, images)
```

## 🚀 How to Run

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/kendychae/ConnectFour.git
   cd ConnectFour
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Run the application**

   ```bash
   dotnet run
   ```

4. **Open in browser**
   - Navigate to `https://localhost:5001` or the URL shown in terminal
   - Click **"Connect Four"** in the navigation menu

### Build for Production

```bash
dotnet publish -c Release -o ./publish
```

## 🎮 How to Play

1. **Starting**: Red player goes first
2. **Making a Move**: Click any column to drop your piece
3. **Winning**: Connect four pieces horizontally, vertically, or diagonally
4. **New Game**: Click "New Game" to play again (statistics persist)
5. **Reset Stats**: Clear all win counts with "Reset Win Stats"

### Game Rules

- Players alternate turns (Red → Yellow → Red → ...)
- Pieces fall to the lowest available position in the selected column
- First player to connect four wins immediately
- If the board fills without a winner, the game ends in a draw

## 🧠 Technical Highlights

### State Management

- **Centralized game state** using a singleton `GameState` class
- **Event-driven architecture** with `EventCallback` for component communication
- **Immutable win tracking** with record types

### Algorithm Implementation

- **Efficient win detection** using directional scanning (O(1) per move)
- **Bidirectional search** for all four win conditions
- **Optimized board representation** with 2D arrays

### UI/UX Design

- **CSS Grid** for responsive board layout
- **Keyframe animations** for piece drops and win celebrations
- **Hover effects** and visual feedback for better interactivity
- **Mobile-first responsive design** with breakpoints

## 📚 Learning Outcomes

This project demonstrates proficiency in:

- ✅ Blazor component architecture and lifecycle
- ✅ Server-side rendering with SignalR
- ✅ C# object-oriented programming and SOLID principles
- ✅ State management patterns in modern web applications
- ✅ Responsive CSS design and animations
- ✅ Event handling and inter-component communication
- ✅ Algorithm design and optimization
- ✅ Git version control and documentation

## 🤝 Contributing

This is a portfolio project, but feedback and suggestions are welcome! Feel free to open an issue or submit a pull request.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

## 👨‍💻 Author

**Kendy Chae**

- GitHub: [@kendychae](https://github.com/kendychae)
- Portfolio: [Your Portfolio URL]

## 🙏 Acknowledgments

- Built as part of BYU-Idaho CSE 325 - .NET Software Development
- Inspired by the classic Connect Four board game
- Powered by the ASP.NET Core and Blazor communities

---

⭐ **If you found this project interesting, please consider giving it a star!**
