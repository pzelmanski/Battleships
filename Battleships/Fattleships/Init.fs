module Init

open System
open Domain

type ShipDirection =
    | Horizontal
    | Vertical
    
let isCoordWithinBoard (coord:Coordinate) =
    coord.Row >= 1 && coord.Row <= 10
    && coord.Col >= 1 && coord.Col <= 10

let isThereAlreadyShipOnCoord(coord:Coordinate) allShips =
    allShips
    |> List.where (fun x -> x = coord)
    |> List.length > 0

let rec private generateNextSingleCoord (coordsSoFar: Coordinate list) direction lengthLeft : Coordinate list option =
    if (lengthLeft = 0) then
        Some coordsSoFar
    else
        let nextCoords =
            match direction with
            | Horizontal ->
                List.last coordsSoFar
                |> (fun { Row = r; Col = c } -> coordsSoFar @ [ { Row = r; Col = c + 1 } ])
            | Vertical ->
                List.last coordsSoFar
                |> (fun { Row = r; Col = c } -> coordsSoFar @ [ { Row = r + 1; Col = c } ])
        if isCoordWithinBoard (List.last nextCoords)
//           || isThereAlreadyShipOnCoord (List.last nextCoords)
           then 
            generateNextSingleCoord nextCoords direction (lengthLeft - 1)
        else
            None

let rec private generateCoords (ShipLength shipLength) =
    let r = Random()
    let headPos = (r.Next(1, 10), r.Next(1, 10))
    let headCoord = { Row = fst headPos; Col = snd headPos }
    let generationDirection = match r.Next(1, 2) with
                                | 1 -> Horizontal
                                | 2 -> Vertical
                                | _ -> failwith "direction random out of range"
    match generateNextSingleCoord [ headCoord ] generationDirection (shipLength - 1) with
    | Some x -> x
    | None -> generateCoords (ShipLength shipLength)


let private generateShips (lengths: ShipLength list) : Ship list =
    lengths
    |> List.map generateCoords
    |> List.map
        (fun x ->
            x
            |> List.map (fun c -> { Coordinate = c; IsHit = false }))
    |> List.mapi (fun i c -> { Coordinates = c; Id = i })

let InitGame (shipLengths: ShipLength list) : Ship list = generateShips shipLengths
