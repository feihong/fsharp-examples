(*

Range of CJK unified ideographs:
http://jrgraphix.net/research/unicode_blocks.php

*)
open System

let random = Random()
let randomInt a b = random.Next(a, b + 1)
let randomHanzi () = randomInt 0x4e00 0x9fff |> Convert.ToChar


let chars = [for i in [1..8] -> randomHanzi () ]
printfn "Using list comprehension: %A" chars
let chars2 = List.init 8 (fun i -> randomHanzi () )
printfn "Using List.init function: %A" chars2
