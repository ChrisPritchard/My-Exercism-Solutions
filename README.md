# My Exercism Solutions

An archive of all my solutions to Exercism <https://exercism.io/> exercises. So far this includes F#, C# and Go.

Exercism is the primary way I learned F#, over the course of a 114 occasionally very grueling exercises. An excellent learning tool :)

In the root of this Repo is a small console app called file-formatter. At the time I added the F# solutions to this repo, the exercism CLI command `exercism r` would copy down all your solutions but seemed to have a bug where it stuck blank lines between the source lines. This file formatter, which I wrote in F# shortly after finishing all the exercises, goes through the files and trims out these extra breaks. Bonus credit for me!

By the time I finished the C# exercises (I knew C# pretty well already, though learned some stuff to my surprise during the track), `exercism r` no longer worked. Instead I wrote a powershell script (the `downloader.ps1` file) that retrieves the exercise lists from the official repo and downloads my solutions to each.
