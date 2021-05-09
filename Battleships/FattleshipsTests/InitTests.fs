module InitTests

open Domain
open Xunit
open FsUnit

let isCoordWithinBoard coord =
    coord >= 1 && coord <= 10

let isShipWithinBoard ship =
    ship.Coordinates
    |> List.map (fun x -> x.Coordinate)
    |> List.map (fun x -> (isCoordWithinBoard x.Col && isCoordWithinBoard x.Row))
    |> List.fold (fun x y -> x && y) true

[<Fact>]
let ``should init single ship correctly``() =
    let ships = Init.InitGame [ShipLength 1;ShipLength 2]
    
    List.length ships |> should equal 2
    
    List.map isShipWithinBoard ships
    |> List.fold (fun x y -> x && y) true
    |> should equal true
    
[<Fact>]
let ``should init single ship correctly2``() =
    let ships = Init.InitGame [ShipLength 3;ShipLength 2; ShipLength 5]
    
    List.length ships |> should equal 3
    
    List.map isShipWithinBoard ships
    |> List.fold (fun x y -> x && y) true
    |> should equal true
    
let mulByTwoAddOneAndDivideBy x y =
    (x * 2 + 1) / y


let isAnyOverlapping coord coords =
    coords
    |> List.where (fun x -> x = coord)
    |> List.length > 1

[<Fact>]
let ``ships should not overlap`` () =
    let ships = Init.InitGame [ShipLength 5; ShipLength 4; ShipLength 3]
    
    let coords = ships
                |> List.map(fun x -> x.Coordinates |> List.map(fun y -> y.Coordinate))
                |> List.concat
                
    coords
    |> List.map (fun x -> isAnyOverlapping x coords)
    |> List.fold(fun x y -> x || y) false
    |> should equal false 
    