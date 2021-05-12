module GameLoop

open Domain


let charToInt c =
    int c - int '0'


let mapInput (UserInput rawInput) : Coordinate option =
    // TODO: Handle incorrect input    
    match String.length rawInput with
    | 2 -> let row = charToInt rawInput.[0]
           let col = int rawInput.[1]
           Some { Row = row; Col = col }
    | _ -> None

type HitOrMiss =
    | Hit
    | Miss

let hitOrMiss (allShips: Ship list) hitCoord : HitOrMiss =
    allShips
    |> List.map (fun x -> x.Coordinates)
    |> List.concat
    |> List.map (fun x -> x.Coordinate)
    |> List.contains hitCoord
    |> function
    | true -> Hit
    | false -> Miss

let markSegmentHit (ship: Ship) hitCoordinate =
    ship.Coordinates
    |> List.where (fun x -> x.Coordinate = hitCoordinate)
    |> List.last
    |> fun x ->
        { Id = ship.Id
          Coordinates =
              (List.where (fun y -> not (y = x)) ship.Coordinates)
              @ [ { Coordinate = x.Coordinate
                    IsHit = true } ] }

let isSink (ship: Ship) =
    ship.Coordinates
    |> List.map (fun x -> x.IsHit)
    |> List.fold (fun x y -> x && y) true

let shoot (allShips: Ship list) (hitCoord: Coordinate) : NextRound =
    hitOrMiss allShips hitCoord
    |> function
    | Miss ->
        { Ships = allShips
          RoundResult = RoundResult.Miss }
    | Hit ->
        // find ship
        // mark segment as hit
        // check if whole ship sinks
        // return hit or sink & modified all ships
        let shipBeforeHit =
            allShips
            |> List.where (fun x -> List.contains hitCoord (x.Coordinates |> List.map (fun y -> y.Coordinate)))
            |> List.last

        let shipAfterHit = markSegmentHit shipBeforeHit hitCoord

        let allShipsAfterHit =
            (allShips
             |> List.where (fun x -> not (x.Id = shipAfterHit.Id)))
            @ [ shipAfterHit ]

        { Ships = allShipsAfterHit
          RoundResult =
              isSink shipAfterHit
              |> function
              | true -> Sink
              | false -> RoundResult.Hit }

let nextRound (ships: Ship list) (userInput: UserInput) : NextRound =
    mapInput userInput
    |> function
    | Some x -> shoot ships x
    | None ->
        { Ships = ships
          RoundResult = InvalidInput }
