open System.Collections
open System.Collections.Generic


type 'a DLNode =
  | Placeholder
  | Front
  | End
  | Node of 'a NodeInfo
and 'a NodeInfo = {mutable Left: 'a DLNode; Value: 'a; mutable Right: 'a DLNode}

type 'a DLList =
  | Empty
  | Single of 'a
  | List of 'a DLNode * 'a DLNode     // front and rear nodes


type DoublyLinkedList<'T>() =
  let mutable list = Empty

  let rec traverseNode node =
    seq {
      match node with
      | End -> ()
      | Node record ->
        yield record.Value
        yield! traverseNode record.Right
      | _ -> ()
    }

  let rec traverse list =
    seq {
      match list with
      | Empty -> ()
      | Single v -> yield v
      | List (front, _) ->
        yield! traverseNode front
    }

  member this.Push newValue =
    match list with
    | Empty ->
      list <- Single newValue
    | Single v ->
      let frontRecord = {Left = Front; Value = v; Right = Placeholder}
      let front = Node frontRecord
      let rear = Node {Left = front; Value = newValue; Right = End}
      frontRecord.Right <- rear
      list <- List (front, rear)
    | List (front, (Node rearRecord as rear)) ->
      let newRear = Node {Left = rear; Value = newValue; Right = End}
      rearRecord.Right <- newRear
      list <- List (front, newRear)
    | _ -> ()

  member this.Pop() =
    match list with
    | Empty -> None
    | Single v ->
      list <- Empty
      Some v
    | List (front, (Node rearRecord as rear)) ->
      match rearRecord.Left with
      | Node {Left = Front; Value = v} ->
        // There are only 2 nodes, so convert to a single value.
        list <- Single v
      | Node record as newRear ->
        // Make second-to-last node the rear node.
        record.Right <- End
        list <- List (front, newRear)
      | _ -> ()
      Some rearRecord.Value
    | _ -> None

  member this.PushFront newValue =
    match list with
    | Empty ->
      list <- Single newValue
    | Single v ->
      let frontRecord = {Left = Front; Value = newValue; Right = Placeholder}
      let front = Node frontRecord
      let rear = Node {Left = front; Value = v; Right = End}
      frontRecord.Right <- rear
      list <- List (front, rear)
    | List ((Node frontRecord as front), rear) ->
      let newFront = Node {Left = Front; Value = newValue; Right = front}
      frontRecord.Left <- newFront
      list <- List (newFront, rear)
    | _ -> ()

  member this.PopFront() =
    match list with
    | Empty ->
      None
    | Single v ->
      list <- Empty
      Some v
    | List (Node frontRecord, rear) ->
      match frontRecord.Right with
      | Node {Right = End; Value = v} ->
        // There are only 2 nodes, so convert to single value.
        list <- Single v
      | Node record as newFront ->
        // Make second node the front node.
        record.Left <- Front
        list <- List (newFront, rear)
      | _ -> ()
      Some frontRecord.Value
    | _ -> None

  interface IEnumerable<'T> with
    member this.GetEnumerator() =
      let s = traverse list
      s.GetEnumerator()

  interface IEnumerable with
    member this.GetEnumerator () =
      (this :> IEnumerable<'T>).GetEnumerator() :> IEnumerator


let dlist = new DoublyLinkedList<string>()
for s in ["a"; "b"; "c"] do
  dlist.Push s
printfn "After pushing: %A" dlist

for i in [1..4] do
  printfn "Popped [%A]" <| dlist.Pop()
printfn "After popping: %A" dlist

for s in ["a"; "b"; "c"; "d"] do
  dlist.PushFront s
printfn "After pushing to the front: %A" dlist

for i in [1..5] do
  printfn "Popped [%A] from front" <| dlist.PopFront()
printfn "After popping from the front: %A" dlist
