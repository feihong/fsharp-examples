(*
Sources:
https://viralfsharp.com/2012/02/11/implementing-a-stack-in-f/
http://markheath.net/post/recursive-sequence-expressions-in-f

*)
open System
open System.Collections
open System.Collections.Generic

type Node<'T> = {
  Value: 'T;
  Tail: Node<'T> option;
}

//type 'a ImmutableStack =
//  | Empty
//  | StackNode of 'a * 'a ImmutableStack


type Stack<'T>() =
  let mutable head: Node<'T> option = None
  let mutable count = 0

  let rec traverse head =
    seq {
      match head with
      | Some node ->
        yield node.Value
        yield! traverse node.Tail
      | None -> ()
    }

  member this.Count
    with get() = count

  member this.IsEmpty
    with get() = count = 0

  member this.Push item =
    count <- count + 1
    match head with
    | Some node ->
      head <- Some {Value = item; Tail = Some node}
    | None ->
      head <- Some {Value = item; Tail = None}

  member this.Pop() =
    match head with
    | Some node ->
      head <- node.Tail
      count <- count - 1
      Some node.Value
    | None -> None

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
