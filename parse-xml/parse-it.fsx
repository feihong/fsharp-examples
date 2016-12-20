#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
// XmlProvider will not work unless this is loaded.
#r "System.Xml.Linq.dll"

open FSharp.Data

type Results = XmlProvider<"./sample.xml">

let results =
  Http.RequestString (
    "http://thecatapi.com/api/images/get",
    query = ["format", "xml"; "results_per_page", "3"]) |> Results.Parse

for image in results.Data.Images do
  printfn "Id: %s" image.Id
  printfn "SourceUrl: %s" image.SourceUrl
  printfn "URL: %s" image.Url
  printfn "====="
