$request = "https://api.github.com/repos/exercism/csharp/contents/exercises"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$result = Invoke-WebRequest $request | ConvertFrom-Json
$names = $result | Where-Object { $_.type -eq "dir" } | Select-Object -ExpandProperty name

$names | ForEach-Object {
    Invoke-Expression "exercism d -t csharp -e $_"
}