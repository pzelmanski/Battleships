open Domain

let isSink (ship: Ship) =
    List.fold (&&) true (List.map (fun x -> x.IsHit) ship.Coordinates)

let areAllShipsSink (allShips: Ship list) =
    List.map isSink allShips |> List.fold (&&) true

let rec nextRound allShips input roundNumber =
    let result = GameLoop.nextRound allShips input

    printfn $"\n ==== \nRound number %s{string roundNumber} \n === \n"
    Printer.printRoundResult result.RoundResult
    Printer.printBoard result.Ships

    if (areAllShipsSink allShips) then
        exit 0
    else
        let input = System.Console.ReadLine() |> UserInput
        nextRound result.Ships input (roundNumber + 1)

let Play = 
    let allShips =
        Init.InitGame [ ShipLength 5
                        ShipLength 1 ]

    Printer.printBoard allShips


    let input = System.Console.ReadLine() |> UserInput
    nextRound allShips input 1

[<EntryPoint>]
let main argv =
    Play()
    0
