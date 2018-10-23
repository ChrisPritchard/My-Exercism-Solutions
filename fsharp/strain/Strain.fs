module Seq

let keep pred xs = seq { for i in xs do if pred i then yield i }

let discard pred xs = keep (pred >> not) xs
