module InitTests

open Domain
open Xunit
open FsUnit

let isWithinBoard coord =
    coord >= 1 && coord <= 10

[<Fact>]
let ``should init single ship correctly``() =
    let ships = Init.InitGame [ShipLength 1;ShipLength 2]
    Assert.Equal(2, List.length ships)
    ships.[0].Coordinates
    |> List.map (fun x -> x.Coordinate)
    |> List.map (fun x -> (isWithinBoard x.Col, isWithinBoard x.Row))
    |> List.map (fun (x, y) -> x && y)
    |> List.fold (fun x y -> x && y) true
    |> should equal true
    
    ships.[1].Coordinates
    |> List.map (fun x -> x.Coordinate)
    |> List.map (fun x -> (isWithinBoard x.Col, isWithinBoard x.Row))
    |> List.map (fun (x, y) -> x && y)
    |> List.fold (fun x y -> x && y) true
    |> should equal true
    
let mulByTwoAddOneAndDivideBy x y =
    (x * 2 + 1) / y
