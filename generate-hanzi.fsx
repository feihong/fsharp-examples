open System

let random = Random()
let nums = [for i in [1..8] -> random.Next(1, 100) ]
printfn "Using list comprehension: %A" nums

let nums2 = List.init 8 (fun i -> random.Next(1, 100))
printfn "Using List.init function: %A" nums2
