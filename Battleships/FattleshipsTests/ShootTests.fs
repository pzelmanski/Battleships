module ShootTests

open Xunit
open Domain
open GameLoop
open FsUnit


[<Fact>]
let ``shoot 1`` () =
    let ship =
        { Coordinates =
              [ { Coordinate = { Row = 1; Col = 1 }
                  IsHit = false } ]
          Id = 1 }

    let hit = { Row = 1; Col = 1 }

    shoot [ ship ] hit
    |> fun x -> x.RoundResult
    |> should equal RoundResult.Sink

[<Fact>]
let ``shoot 2`` () =
    let ship =
        { Coordinates =
              [ { Coordinate = { Row = 1; Col = 1 }
                  IsHit = false }
                { Coordinate = { Row = 2; Col = 1 }
                  IsHit = false } ]
          Id = 1 }

    let miss = {Row = 2; Col = 2}
    let hit = { Row = 1; Col = 1 }
    let sink = {Row = 2; Col = 1}
    
    shoot [ship] miss
    |> fun x -> x.RoundResult
    |> should equal RoundResult.Miss
    
    let hitResult = shoot [ ship ] hit
    hitResult
    |> fun x -> x.RoundResult
    |> should equal RoundResult.Hit
    
    shoot hitResult.Ships sink
    |> fun x -> x.RoundResult
    |> should equal RoundResult.Sink