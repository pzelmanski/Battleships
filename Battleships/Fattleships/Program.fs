// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
// See the 'F# Tutorial' project for more help.

// Define a function to construct a message to print

open Domain

let printBoard allShips =
    let message = Printer.getBoard allShips
    printfn "Board:%s" message
    
let printRoundResult result =
    let message = match result with
                    | InvalidInput -> "Invalid input"
                    | Hit -> "Hit"
                    | Miss -> "Miss"
                    | Sink -> "Sink"
    printfn "Round result: %s" message

[<EntryPoint>]
let main argv =
    let allShips = Init.InitGame [ShipLength 1;ShipLength 1]
    printBoard allShips
    
    
    let input = System.Console.ReadLine() |> UserInput
    let result = GameLoop.nextRound allShips input
    
    printRoundResult result.RoundResult
    printBoard result.Ships
    0 // return an integer exit code
