module Domain 
    type Coordinate = { Row: int; Col: int }

    type ShipCoordinate = { Coordinate: Coordinate; IsHit: bool }

    type Ship =
        { Coordinates: ShipCoordinate list
          Id: int }


    type RoundResult =
        | InvalidInput
        | Miss
        | Hit
        | Sink

    type NextRound =   { Ships: Ship list
                         RoundResult: RoundResult }

    type UserInput = UserInput of string

    type ShipLength = ShipLength of int