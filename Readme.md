# Battleships game

Cmd - based battleships game.

Very exciting, much addictive!

---

Rules:
```
The program should create a 10x10 grid, and place 
several ships on the grid at random with the following sizes:
    1x Battleship (5 squares)
    2x Destroyers (4 squares)
The player enters or selects coordinates of the form “A5”,
where “A” is the column and “5” is the row, 
to specify a square to target. 
Shots result in hits, misses or sinks. 
The game ends when all ships are sunk.
```

---

## Prerequisities
.NET SDK 5.0.201

## Building
run `dotnet build` in `/Battleships/` directory

## Running tests
run `dotnet test` in `/Battleships/` directory

## Running project
run `dotnet run --project ./Battleships/Battleships.csproj` in `/Battleships/` directory