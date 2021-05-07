module Init

open Domain

type ShipDirection =
    | Horizontal
    | Vertical

let rec generateNextSingleCoord (coordsSoFar: Coordinate list) direction lengthLeft : Coordinate list =
    //TODO: If generated incorrect coords, restart from the beginning
    if (lengthLeft = 0) then
        coordsSoFar
    else
        let nextCoords =
            match direction with
            | Horizontal ->
                List.last coordsSoFar
                |> (fun { Row = r; Col = c } -> coordsSoFar @ [ { Row = r; Col = c + 1 } ])
            | Vertical ->
                List.last coordsSoFar
                |> (fun { Row = r; Col = c } -> coordsSoFar @ [ { Row = r + 1; Col = c } ])

        generateNextSingleCoord nextCoords direction (lengthLeft - 1)

let generateCoords (ShipLength shipLength) =
    // TODO: Randomize headPos and generaitonDirection
    let headPos = (1, 1)
    let headCoord = { Row = fst headPos; Col = snd headPos }
    let generationDirection = Horizontal
    generateNextSingleCoord [ headCoord ] generationDirection (shipLength - 1)


let private generateShips (lengths: ShipLength list) : Ship list =
    lengths
    |> List.map generateCoords
    |> List.map
        (fun x ->
            x
            |> List.map (fun c -> { Coordinate = c; IsHit = false }))
    |> List.mapi (fun i c -> { Coordinates = c; Id = i })

let InitGame (shipLengths: ShipLength list) : Ship list = generateShips shipLengths
