open CommandLine
open System.Diagnostics

type options = {
  [<Option('p', "path", Required = false, HelpText = "Bin folder for ffmpeg")>] binfolder : string;
  [<Option('f', "folder", Required = true, HelpText = "Folder with flac files")>] folder : string;
  [<Option('c', "codec", Required = true, HelpText = "Codec used for output file")>] codec : string;
  [<Option('o', "output", Required = true, HelpText = "Output folder")>] output : string;
}

[<EntryPoint>]
let main argv =
    let result = CommandLine.Parser.Default.ParseArguments<options>(argv)
    match result with
    | :? Parsed<options> as parsed -> 
        printfn "==== Starting 'FLAC Converter' ===="
        #if DEBUG
        let stopWatch = Stopwatch.StartNew()
        #endif
        ConversorService.Run parsed.Value.folder parsed.Value.output parsed.Value.codec
        #if DEBUG
        let elapsedTime = stopWatch.Elapsed

        let formattedTime = sprintf "%02d:%02d:%02d:%03d" elapsedTime.Hours elapsedTime.Minutes elapsedTime.Seconds elapsedTime.Milliseconds

        printfn "Elapsed time: %s" formattedTime
        #endif
    | :? NotParsed<options> as notParsed -> 
        printfn "==== Parameters not found ===="
    printfn "==== Finished 'FLAC Converter' ===="
    0