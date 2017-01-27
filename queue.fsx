open System.Collections
open System.Collections.Generic


type 'a DListNode =
  | End
  | Node of 'a NodeInfo
and 'a NodeInfo = {Value: 'a; mutable Next: 'a DListNode}

type 'a DList =
  | Empty
  | Single of 'a
  | List of 'a DListNode * 'a DListNode     // front and rear nodes


type Queue<'T>() =
  let mutable list = Empty

  let rec traverseNode node =
    seq {
      match node with
      | End -> ()
      | Node record ->
        yield record.Value
        yield! traverseNode record.Next
    }

  let rec traverse list =
    seq {
      match list with
      | Empty -> ()
      | Single v -> yield v
      | List (front, _) ->  yield! traverseNode front
    }

  member this.Enqueue newValue =
    match list with
    | Empty ->
      list <- Single newValue
    | Single v ->
      let rear = Node {Value = newValue; Next = End}
      let front = Node {Value = v; Next = rear}
      list <- List (front, rear)
    | List (front, Node rearRecord) ->
      let newRear = Node {Value = newValue; Next = End}
      rearRecord.Next <- newRear
      list <- List (front, newRear)
    | _ -> ()

  member this.Dequeue() =
    match list with
    | Empty ->
      None
    | Single v ->
      list <- Empty
      Some v
    | List (Node frontRecord, rear) ->
      match frontRecord.Next with
      | Node {Next = End; Value = v} ->
        // There are only 2 nodes, so convert to single value.
        list <- Single v
      | newFront ->
        // Make the second node the front node.
        list <- List (newFront, rear)
      Some frontRecord.Value
    | _ -> None

  interface IEnumerable<'T> with
    member this.GetEnumerator() =
      let s = traverse list
      s.GetEnumerator()

  interface IEnumerable with
    member this.GetEnumerator () =
      (this :> IEnumerable<'T>).GetEnumerator() :> IEnumerator


let queue = new Queue<int>()
for i in [1; 2; 3; 4] do
  queue.Enqueue i
printfn "After enqueuing: %A" queue

for i in [1..5] do
  printfn "Dequeued [%A]" <| queue.Dequeue()
printfn "After dequeuing: %A" queue
