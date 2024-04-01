
# Wordle Online Server

The Multiplayer Wordle Game API serves as a central server component for facilitating the gameplay and user interactions within a Kotlin-based application. This API is designed to handle various operations related to multiplayer Wordle gameplay, including game sessions, user management, and game logic.

### Functionality:

- Game Sessions Management:

The API manages the creation, tracking, and termination of game sessions. Each session accommodates multiple players engaging in a Wordle game concurrently.
It handles the initialization of game boards, word selection, and scoring mechanisms for each session.

- User Operations:

User registration and authentication functionalities are provided to ensure secure access to the game.
Users can create profiles, manage their preferences, and participate in multiplayer game sessions.

- Gameplay Logic:

Implements the core gameplay logic of Wordle, where players attempt to guess a hidden word within a limited number of tries.
Validates user guesses, checks for correctness, and provides feedback to players regarding their guesses.
Manages the scoring system based on the accuracy and efficiency of guesses made by each player.

- Real-time Communication:

Enables real-time communication between players participating in the same game session.
Utilizes WebSocket or similar technology to facilitate instant updates, such as displaying guesses made by opponents and notifying players of game events.

- Error Handling and Logging:

Implements robust error handling mechanisms to ensure the stability and reliability of the API.
Logs relevant events and errors to assist in debugging and monitoring the system.

- Scalability and Performance:

Designed with scalability in mind to accommodate a growing number of concurrent users and game sessions.
Optimized for performance to minimize latency and provide a smooth gaming experience for all players.

- Security:

Implements security measures such as encryption for sensitive data transmission and secure authentication mechanisms to protect user accounts.
Implements measures to prevent cheating or unauthorized access to game resources.

### Conclusion :

The Multiplayer Wordle Game API serves as a crucial backend component for the Kotlin application, enabling seamless multiplayer Wordle gameplay experiences for users. By managing game sessions, user interactions, and gameplay logic, the API ensures a smooth and engaging gaming experience while prioritizing security, scalability, and performance. With its comprehensive feature set and robust architecture, the API lays the foundation for a successful multiplayer Wordle gaming platform.
## Getting Started :

###  Install .NET Core SDK :

- To run the project, you need to have the .NET Core SDK installed on your computer. If it's not installed, you can download and install it from [here](https://dotnet.microsoft.com/download).

###  Clone the Repository :

- To run the project, you need to clone the codes from GitHub. You can clone the repository by running the following command in a command prompt:

```bash
git clone https://github.com/fatihserhatturan/WordleOnlineServer.git
```


### Run the Project :

- Once you're in the project directory, you can run the project using the .NET Core CLI (Command Line Interface). Use the following command to start the project:

```bash
dotnet run
```

### Access the Server :

- After successfully running the project, you can access the server via a specific port. By default, the server runs on port 5042.

You can access the server by opening your browser or using an HTTP client to navigate to http://localhost:5042.

By following these steps, you can successfully run this project on your computer using .NET Core.




