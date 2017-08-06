(*
Source: F# Deep Dives, chapter 2
https://www.manning.com/books/f-sharp-deep-dives
*)
let binomial x n p =
  let fact n =
    match n with
    | _ when n <= 1 -> 1
    | _ -> [1..n] |> Seq.reduce (*)

  let (!) = fact
  let f = float
  [0..x]
  |> Seq.sumBy (fun k ->
    f(!n) /
    (f(!k) * f(!(n - k))) *
    p**f(k) * (1. - p)**f(n - k)
  )
