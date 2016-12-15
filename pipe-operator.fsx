let negate x = x * -1
let square x = x * x
let print x = printfn "The number is: %d" x
let add a b = a + b

//Double-backtick identifiers are handy to improve readability especially
//in unit testing:
let ``square, negate, then print`` x =
    x |> square |> negate |> print

``square, negate, then print`` 3
-4 |> add 10 |> ``square, negate, then print``
