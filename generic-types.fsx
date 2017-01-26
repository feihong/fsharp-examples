type Node<'T> = {
  Value: 'T;
  Next: Node<'T> option;
}

let node1 = {Value = 44; Next = None}
let node2 = {Value = 55; Next = Some node1}
