module Leap

let leapYear (year: int): bool = 
    let mod4 = year % 4 = 0
    let mod100 = year % 100 = 0
    let mod400 = year % 400 = 0
    mod4 && (not mod100 || mod400)
