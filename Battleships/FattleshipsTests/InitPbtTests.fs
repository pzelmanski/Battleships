module InitPbtTests

open FsCheck
open FsCheck.Xunit

open Domain
type TestShipLength = 
    static member genSingleShipLength() = 
      gen { let! i = Gen.choose (1, 6) 
            return ShipLength i }
      |> Arb.fromGen

let isCoordWithinBoard coord =
    coord >= 1 && coord <= 10

let isShipWithinBoard ship =
    ship.Coordinates
    |> List.map (fun x -> x.Coordinate)
    |> List.map (fun x -> (isCoordWithinBoard x.Col && isCoordWithinBoard x.Row))
    |> List.fold (fun x y -> x && y) true
let allShipsWithinBoard ships =
    List.map isShipWithinBoard ships
    |> List.fold (fun x y -> x && y) true 
    

let t1 (len: ShipLength) =
    Init.InitGame [len]
    |> allShipsWithinBoard

[<Properties( Arbitrary=[| typeof<TestShipLength> |] )>]
module CheckInit = 
    [<Property>]
    let ``test test`` (len: ShipLength) =
        t1 len