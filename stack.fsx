(*
Sources:
https://viralfsharp.com/2012/02/11/implementing-a-stack-in-f/
http://markheath.net/post/recursive-sequence-expressions-in-f

*)
open System
open System.Collections
open System.Collections.Generic


type 'a ImmutableStack =
  | Empty
  | StackNode of 'a * 'a ImmutableStack


type Stack<'T>() =
  let mutable head = Empty
  let mutable count = 0

  let rec traverse head =
    seq {
      match head with
      | StackNode(hd, tl) ->
        yield hd
        yield! traverse tl
      | Empty -> ()
    }

  member this.Count
    with get() = count

  member this.IsEmpty
    with get() = count = 0

  member this.Push item =
    count <- count + 1
    head <- StackNode(item, head)

  member this.Pop() =
    match head with
    | StackNode(hd, tl) ->
      count <- count - 1
      head <- tl
      Some hd
    | Empty -> None

  interface IEnumerable<'T> with
    member this.GetEnumerator() =
      let s = traverse head
      s.GetEnumerator()

  interface IEnumerable with
    member this.GetEnumerator () =
      (this :> IEnumerable<'T>).GetEnumerator() :> IEnumerator


let stack = new Stack<int>()
stack.Push 1
stack.Push 2
stack.Push 3

for n in stack do
  printfn "%d" n

printfn "%A" stack   // seq [3; 2; 1]
