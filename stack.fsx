(*
Reference:
https://viralfsharp.com/2012/02/11/implementing-a-stack-in-f/

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

  //interface IEnumerable<'T> with
  //  member this.GetEnumerator() =
  //    let s = seq { for n in list do yield n }
  //    s.GetEnumerator()
  //
  //interface IEnumerable with
  //  member this.GetEnumerator () =
  //    (this :> IEnumerable<'T>).GetEnumerator() :> IEnumerator
