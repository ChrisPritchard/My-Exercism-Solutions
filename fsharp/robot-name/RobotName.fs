module RobotName

type Robot = { name: string }

let makeName() =
    let letters = ['A'..'Z']
    let random = new System.Random()
    sprintf "%c%c%03i" letters.[random.Next(26)] letters.[random.Next(26)] (random.Next(999))

let mkRobot() = { name = makeName() }

let name robot = robot.name

let reset robot = { robot with name = makeName() }
