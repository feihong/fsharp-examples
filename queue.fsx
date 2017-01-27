open System.Collections
open System.Collections.Generic


type 'a Node =
  | Placeholder
  | Front of 'a FrontInfo
  | Middle of 'a MiddleInfo
  | Rear of 'a RearInfo
and 'a FrontInfo = {Value: 'a; mutable Right: 'a Node}
and 'a MiddleInfo = {mutable Left: 'a Node; Value: 'a; mutable Right: 'a Node}
and 'a RearInfo = {mutable Left: 'a Node; Value: 'a;}

type 'a DoubleLinkedList =
  | Empty
  | Single of 'a
  | List of 'a Node * 'a Node     // front and rear nodes


type Queue<'T>() =
  let mutable count = 0
  let mutable list = Empty

  let rec traverseNode node =
    seq {
      match node with
      | Placeholder -> ()
      | Front record ->
        yield record.Value
        yield! traverseNode record.Right
      | Middle record ->
        yield record.Value
        yield! traverseNode record.Right
      | Rear record ->
        yield record.Value
    }

  let rec traverse list =
    seq {
      match list with
      | Empty -> ()
      | Single v -> yield v
      | List (front, _) ->
        yield! traverseNode front
    }

  member this.Enqueue item =
    count <- count + 1
    match list with
    | Empty ->
      list <- Single item
    | Single v ->
      let (frontRecord : 'T FrontInfo) = {Value = v; Right = Placeholder}
      let front = Front frontRecord
      let rear = Rear {Value = item; Left = front}
      frontRecord.Right <- rear
      list <- List (front, rear)
    | List(front, Rear rearRecord) ->
      let middleRecord = {Value = rearRecord.Value; Left = rearRecord.Left; Right = Placeholder}
      let middle = Middle middleRecord
      let newRear = Rear {Value = item; Left = middle}
      middleRecord.Right <- newRear
      match middleRecord.Left with
      | Front record -> record.Right <- middle
      | Middle record -> record.Right <- middle
      | _ -> ()
      list <- List (front, newRear)
    | _ -> failwith "Unable to enqueue"

  member this.Dequeue() =
    match list with
    | Empty -> None
    | Single v ->
      count <- 0
      list <- Empty
      Some v
    | List(Front frontRecord, rear) ->
      count <- count - 1
      match frontRecord.Right with
      | Rear record ->
        list <- Single record.Value
      | Middle record ->
        let front = Front {Value = record.Value; Right = record.Right}
        list <- List (front, rear)
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


let queue = new Queue<int>()
queue.Enqueue(1)
queue.Enqueue(2)
queue.Enqueue(3)
printfn "%A" queue
for i in [1..4] do
  printfn "%A" <| queue.Dequeue()
