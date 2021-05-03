module Tests

open System
open Xunit

[<Fact>]
let ``My test`` () =
    Assert.True(true)


[<Fact>]
let ``test1`` () =
    Assert.Equal ((Test.addOne 7), 8)
