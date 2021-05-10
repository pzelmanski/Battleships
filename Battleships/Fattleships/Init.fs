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
    
let isColliding coord (allShips : Coordinate list list) =
    allShips
    |> List.concat
    |> isThereAlreadyShipOnCoord coord

// PURE
let rec private generateNextSingleCoord otherShips (coordsSoFar: Coordinate list) direction lengthLeft : Coordinate list option =
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
        if isCoordWithinBoard (List.last nextCoords) && not (isColliding (List.last nextCoords) otherShips)
           then 
            generateNextSingleCoord otherShips nextCoords direction (lengthLeft - 1)
        else
            None

// IMPURE
let rec private generateCoords (allShips: Coordinate list list) (ShipLength shipLength) =
    let r = Random()
    let headPos = (r.Next(1, 10), r.Next(1, 10))
    let headCoord = { Row = fst headPos; Col = snd headPos }
    let generationDirection = match r.Next(1, 2) with
                                | 1 -> Horizontal
                                | 2 -> Vertical
                                | _ -> failwith "direction random out of range"
    match generateNextSingleCoord allShips [ headCoord ] generationDirection (shipLength - 1) with
    | Some x -> allShips@[x]
    | None -> generateCoords allShips (ShipLength shipLength)


let private generateShips (lengths: ShipLength list) : Ship list =
    let x = [] : Coordinate list list
    lengths
    |> List.fold (fun all curr -> generateCoords all curr) x
    |> List.map
        (fun x ->
            x
            |> List.map (fun c -> { Coordinate = c; IsHit = false }))
    |> List.mapi (fun i c -> { Coordinates = c; Id = i })

let InitGame (shipLengths: ShipLength list) : Ship list = generateShips shipLengths

