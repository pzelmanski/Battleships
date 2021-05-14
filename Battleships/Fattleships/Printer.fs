module Printer

open Domain

let getShipByCoord (allShips: Ship list) coord =
    let shipOnCoordList =
        allShips
        |> List.filter
            (fun x ->
                x.Coordinates
                |> List.map (fun y -> y.Coordinate)
                |> List.map (fun y -> y = coord)
                |> List.fold (fun x y -> x || y) false)

    match List.length shipOnCoordList with
    | 0 -> "X"
    | 1 ->
        let ship = List.last shipOnCoordList

        let isHit =
            ship.Coordinates
            |> List.filter (fun x -> x.Coordinate = coord)
            |> List.last
            |> fun x -> x.IsHit

        if isHit then "-" else string ship.Id
    | _ -> failwith "Two ships on a single coord"

let getBoard (allShips: Ship list) =
    let rows = [ 1 .. 10 ]
    let cols = [ 1 .. 10 ]

    let mutable rowNum = 0

    List.allPairs rows cols
    |> List.map (fun (x, y) -> { Row = y; Col = x })
    |> List.map (fun c -> (+) " " (getShipByCoord allShips c))
    |> List.mapi
        (fun i x ->
            if (i % 10 = 0) then
                rowNum <- rowNum + 1
                $"\n{rowNum}|" + x
            else
                x)
    |> List.fold (+) " "
    |> (+) "   A B C D E F G H I J"

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
