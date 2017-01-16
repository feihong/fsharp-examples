type Game = {
  Name: string
  Platform: string
  Score: int option
}

let chronoTrigger = { Name = "Chrono Trigger"; Platform = "SNES"; Score = Some 5 }
let alteredBeast = { Name = "Altered Beast"; Platform = "Genesis"; Score = Some 1 }
let bonk = { Name = "Bonk"; Platform = "TG16"; Score = Some 2 }
let halo = { Name = "Halo"; Platform = "Xbox"; Score = None }

let getVerdict score =
  match score with
  | 1 -> "execrable"
  | 2 -> "poor"
  | 3 -> "meh"
  | 4 -> "good"
  | 5 -> "great"
  | _ -> "unknown"


let describe game =
  let verdict =
    match (Option.map getVerdict game.Score) with
    | Some x -> x
    | None -> "not played yet"
  sprintf "%s (%s): %s" game.Name game.Platform verdict

for game in [chronoTrigger; alteredBeast; bonk; halo] do
  printfn "%s" <| describe game
