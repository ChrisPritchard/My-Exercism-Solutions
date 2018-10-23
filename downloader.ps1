# to run for a given language, replace the trackname 'csharp' below with whatever is appropriate (e.g. fsharp)
# this needs to be done in two places: line 4 in the exercise url, and line 10 in the expression invocation

$request = "https://api.github.com/repos/exercism/csharp/contents/exercises"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$result = Invoke-WebRequest $request | ConvertFrom-Json
$names = $result | Where-Object { $_.type -eq "dir" } | Select-Object -ExpandProperty name

$names | ForEach-Object {
    Invoke-Expression "exercism d -t csharp -e $_"
}