open CommandLine

type options = {
  [<Option('p', "path", Required = false, HelpText = "Bin folder for ffmpeg")>] binfolder : string;
  [<Option('f', "folder", Required = true, HelpText = "Folder with flac files")>] folder : string;
  [<Option('o', "output", Required = true, HelpText = "Output folder")>] output : string;
}

[<EntryPoint>]
let main argv =
    let result = CommandLine.Parser.Default.ParseArguments<options>(argv)
    match result with
    | :? Parsed<options> as parsed -> 
        printfn "==== Starting 'FLAC to MP3 Converter' ===="
        ConversorService.ProcessFolder parsed.Value.folder parsed.Value.output
    | :? NotParsed<options> as notParsed -> 
        printfn "==== Parameters not found ===="
    printfn "==== Finished 'FLAC to MP3 Converter' ===="
    0