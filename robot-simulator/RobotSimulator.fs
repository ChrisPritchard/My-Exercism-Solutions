module RobotSimulator

type Direction = North | East | South | West
type Position = int * int

type Robot = { direction: Direction; position: Position }

let create direction position = { direction = direction; position = position }

let turnLeft robot = 
    match robot.direction with
    | North -> { robot with direction = West }
    | East -> { robot with direction = North }
    | South -> { robot with direction = East }
    | West -> { robot with direction = South }

let turnRight robot = 
    match robot.direction with
    | North -> { robot with direction = East }
    | East -> { robot with direction = South }
    | South -> { robot with direction = West }
    | West -> { robot with direction = North }

let advance (robot: Robot) = 
    let (x, y) = robot.position
    match robot.direction with
    | North -> { robot with position = (x, y + 1) }
    | East -> { robot with position = (x + 1, y) }
    | South -> { robot with position = (x, y - 1) }
    | West -> { robot with position = (x - 1, y) }

let instructions (instructions':string) (robot:Robot) = 
    let rec runStep (steps:char[], target:Robot) =
        match Array.tryHead steps with
        | Some 'A' -> runStep (Array.tail steps, advance target)
        | Some 'L' -> runStep (Array.tail steps, turnLeft target)
        | Some 'R' -> runStep (Array.tail steps, turnRight target)
        | _ -> target
    runStep (instructions'.ToCharArray(), robot)
