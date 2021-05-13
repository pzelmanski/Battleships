module Printer

open Domain

let getBoard (allShips: Ship list) =
    let rows = [ 1 .. 10 ]
    let cols = [ 1 .. 10 ]

    let allCoords =
        allShips
        |> List.map (fun x -> (x.Coordinates |> List.map (fun c -> c.Coordinate), x.Id))
        |> List.map (fun (x, y) -> x |> List.map (fun c -> (c, y)))
        |> List.concat

    let mutable rowNum = 0

    let p =
        List.allPairs rows cols
        |> List.map (fun (x, y) -> { Row = y; Col = x })
        |> List.map
            (fun c ->
                if (List.contains c (allCoords |> List.map fst)) then
                    List.filter (fun (x, y) -> x = c) allCoords
                    |> List.last
                    |> snd
                    |> string
                    |> (+) " "
                else
                    " X")
        |> List.mapi
            (fun i x ->
                if (i % 10 = 0) then
                    rowNum <- rowNum + 1
                    $"\n{rowNum}" + x
                else
                    x)
    //        List.mapi (fun i x -> printfn $"X: {x}, I: {i}") |> ignore
    p
    |> List.fold (+) " "
    |> (+) "  A B C D E F G H I J"

let printBoard allShips =
    let message = getBoard allShips
    printfn "Board:\n%s" message

let printRoundResult result =
    let message =
        match result with
        | InvalidInput -> "Invalid input"
        | Hit -> "Hit"
        | Miss -> "Miss"
        | Sink -> "Sink"

    printfn "Round result: %s" message
