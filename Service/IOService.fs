module IOService

open System.IO
open System

let rec GetDirectoriesWithContent path extension=
    Directory.GetDirectories(path)
    |> Array.collect (fun dir -> 
        let files = Directory.GetFiles(dir, extension)
        if files.Length > 0 then
            [|dir|]
        else
            GetDirectoriesWithContent dir extension
    )

let ReplicateFolderStructure (folderPath: string) (outputFolder: string) =
    let folders = folderPath.Split("\\", StringSplitOptions.RemoveEmptyEntries)
    let finalFolder = 
        Array.fold (fun currentFolder 
                        folder ->
                                let newFolder = Path.Combine(currentFolder, folder)
                                if not (Directory.Exists(newFolder)) then Directory.CreateDirectory(newFolder)|> ignore else ()
                                newFolder
                   ) outputFolder folders
    finalFolder |> ignore