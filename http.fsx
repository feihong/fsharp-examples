#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

// Asynchronous API
async {
  let! result2 = Http.AsyncRequestString("http://ipecho.net/plain")
  printfn "Your IP address: %s" result2
} |> Async.RunSynchronously

// Synchronous API
let result =
  Http.RequestString (
    "http://thecatapi.com/api/images/get",
    query = ["format", "xml"; "results_per_page", "3"])
printfn "\nPage content:\n%s" result
