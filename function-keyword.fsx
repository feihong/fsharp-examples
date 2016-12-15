// Use pattern matching to define a fibonacci function.
let rec fib n =
  match n with
  | 0 -> 0
  | 1 -> 1
  | _ -> fib (n - 1) + fib (n - 2)

// The `function` keyword is a good shorthand for defining a function that uses
// pattern matching.
let rec fib' = function
  | 0 -> 0
  | 1 -> 1
  | n -> fib' (n - 1) + fib' (n - 2)

printfn "Using fib: %d" <| fib 8
printfn "Using fib': %d" <| fib' 8
