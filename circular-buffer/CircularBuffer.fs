module CircularBuffer

type CircleBuffer<'a> = private { size: int; data: 'a list }

let mkCircularBuffer size = { size = size; data = [] }

let clear buffer = mkCircularBuffer buffer.size

let write value buffer = 
    match buffer.size = buffer.data.Length with
    | true -> failwith "Buffer is full"
    | false -> { size = buffer.size; data = buffer.data @ [value] }

let forceWrite value buffer = 
    match buffer.size = buffer.data.Length with
    | true -> { size = buffer.size; data = List.tail buffer.data @ [value] }
    | false -> write value buffer

let read buffer =
    match buffer.data.Length = 0 with
    | true -> failwith "Buffer is empty"
    | false -> (List.head buffer.data, { size = buffer.size; data = List.tail buffer.data })
