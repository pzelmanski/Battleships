module GameLoop

open Domain

type MapUserInput = UserInput -> Coordinate


let mapInput (UserInput rawInput) : Coordinate option = Some { Row = 1; Col = 1 }

let shoot (ships: Ship list) (hitCoord: Coordinate) : RoundResult = Miss

let nextRound (ships: Ship list) (userInput: UserInput) : NextRound =
    mapInput userInput
    |> function
    | Some x ->
        { Ships = ships
          RoundResult = shoot ships x }
    | None ->
        { Ships = ships
          RoundResult = InvalidInput }
