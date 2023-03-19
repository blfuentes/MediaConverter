module IOService

open System.IO

let rec GetDirectoriesWithContent path extension=
    Directory.GetDirectories(path)
    |> Array.collect (fun dir -> 
        let files = Directory.GetFiles(dir, extension)
        if files.Length > 0 then
            [|dir|]
        else
            GetDirectoriesWithContent dir extension
    )