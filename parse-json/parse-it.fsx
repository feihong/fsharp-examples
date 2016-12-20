#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data
open FSharp.Data.JsonExtensions

//let results =
//  Http.RequestString (
//    "http://thecatapi.com/api/images/get")

type Results = JsonProvider<"./sample.json">

let results =
  Http.RequestString (
    "http://api.worldbank.org/countries/ca?format=json") |> Results.Parse

//printfn "%A" results.Record
//printfn "%A" results.Array
let obj = results.Array.[0]
printfn "%s" obj.Name
printfn "Coordinates: (%f, %f)" obj.Latitude obj.Longitude
printfn "Region: %s" obj.Region.Value
printfn "Capital: %s" obj.CapitalCity
