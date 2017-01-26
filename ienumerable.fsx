open System
open System.Collections
open System.Collections.Generic

type MyCollection<'T>() =
  let mutable list : 'T list = []

  member this.Add n =
    list <- List.append list [n]

  interface IEnumerable<'T> with
    member this.GetEnumerator() =
      let s = seq { for n in list do yield n }
      s.GetEnumerator()

  interface IEnumerable with
    member this.GetEnumerator () =
      (this :> IEnumerable<'T>).GetEnumerator() :> IEnumerator


let collection = MyCollection<int>()
collection.Add 1
collection.Add 2
collection.Add 3
collection.Add 444
for n in collection do
  printfn "%d" n
