open System.Timers
open System.Threading

let timer = new System.Timers.Timer(500.0)
timer.AutoReset <- true
timer.Elapsed.Add (fun _ -> printfn "Elapsed!")
timer.Start()

// Sleep for 5 seconds.
Thread.Sleep(5000)
