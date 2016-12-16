open System.Diagnostics

let info = ProcessStartInfo()
info.FileName <- "python"
info.Arguments <- "count.py 5"
info.RedirectStandardOutput <- true
info.UseShellExecute <- false

let proc = Process.Start(info)
printfn "Result:\n%s" (proc.StandardOutput.ReadToEnd())
