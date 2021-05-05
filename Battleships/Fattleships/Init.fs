module Init

open Domain

let private generateShips (lengths: ShipLength list) : Ship list =
    [ { Coordinates =
            [ { Coordinate = { Row = 1; Col = 1 }
                IsHit = false }
              { Coordinate = { Row = 2; Col = 1 }
                IsHit = false }
             ]
        Id = 1 }
      { Coordinates =
            [ { Coordinate = { Row = 5; Col = 5 }
                IsHit = false } ]
        Id = 2 }
    
    ]


let InitGame (shipLengths: ShipLength list) : Ship list = generateShips shipLengths
