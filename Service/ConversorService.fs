module ConversorService

open System.IO
open FFMpegCore
open CustomSettings

let processFile (outputfolder: string) (file:string) =
    let workingfolder = Path.Join([|outputfolder; Path.GetDirectoryName(file).Split('\\') |> List.ofArray |> List.rev |> List.head|])

    match File.Exists workingfolder with
    | false -> Directory.CreateDirectory workingfolder |> ignore
    | true -> ()

    printfn "Processing file '%s' ..." file
    let outputFile = Path.Join([|workingfolder; (Path.GetFileName(file).Replace("flac", "mp3")) |]) 
    let processor = FFMpegArguments
                        .FromFileInput(file)
                        .OutputToFile(outputFile)
    processor.ProcessSynchronously() |> ignore
    printfn "Processed! File saved to '%s' %s" outputFile System.Environment.NewLine

let ProcessFolder inputfolder outputfolder=
    let settings = System.Text.Json.JsonSerializer.Deserialize<ConversorSettings>(File.ReadAllText("settings.json"))

    let options = new FFOptions()
    options.BinaryFolder <- settings.ffmpegfolder

    GlobalFFOptions.Configure(options);

    let files = Directory.EnumerateFiles inputfolder |> List.ofSeq
    files |> List.iter (processFile outputfolder)
