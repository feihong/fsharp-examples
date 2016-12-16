open System.Diagnostics

let info = ProcessStartInfo()
info.FileName <- "python"
info.Arguments <- "count.py 5"
info.RedirectStandardOutput <- true
info.UseShellExecute <- false

let proc = Process.Start(info)
// Arguments involving function or method applications should be parenthesized.
printfn "Result:\n%s" (proc.StandardOutput.ReadToEnd())
