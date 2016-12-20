open System

//printfn "Local time: %A" DateTime.Now
let now = DateTime.UtcNow
printfn "UTC time: %A" now
printfn "Local time: %A" <| now.ToLocalTime()
