module ConversorService

open System.IO
open FFMpegCore
open FFMpegCore.Enums
open CustomSettings

let getConversionOutputOptions (options: FFMpegArgumentOptions) (codec: string) =
    match codec with
    | "mp3" ->  options.WithAudioBitrate(AudioQuality.Good)
                        .WithAudioCodec(AudioCodec.LibMp3Lame)
                        .SelectStream(0, 0, Channel.Audio) |> ignore
    | "ac3" ->  options.WithAudioBitrate(AudioQuality.Good)
                        .WithAudioCodec(AudioCodec.Ac3)
                        .SelectStream(0, 0, Channel.Audio) |> ignore
    | "ogg" ->  options.WithAudioBitrate(AudioQuality.Good)
                        .WithAudioCodec(AudioCodec.LibVorbis) 
                        .SelectStream(0, 0, Channel.Audio) |> ignore
    | "avi" -> options
                        .WithVideoCodec(VideoCodec.LibX264)
                        .WithAudioCodec(AudioCodec.LibMp3Lame)
                        .UsingMultithreading(true)
                        .UsingThreads(System.Environment.ProcessorCount)
                        .WithAudioBitrate(AudioQuality.Normal)
                        //.WithVideoBitrate(5000) // reduction of ~85%. Around 50% with 25000
                        .WithFastStart()
                        |> ignore
    | _ -> options.WithAudioBitrate(AudioQuality.Normal)
                    .WithAudioCodec(AudioCodec.LibMp3Lame) |> ignore

let getConversioInputOptions (options: FFMpegArgumentOptions) (codec: string) =
    options
        .WithHardwareAcceleration(HardwareAccelerationDevice.Auto)
        .UsingMultithreading(true)
        .UsingThreads(System.Environment.ProcessorCount)
        |> ignore

let processFile (codec: string) (outputfolder: string) (inputext: string) (file:string)=
    async {
        printfn "Processing file '%s' ..." file
        let outputFile = Path.Join([|outputfolder; (Path.GetFileName(file).Replace(inputext, codec)) |]) 
        if outputFile.EndsWith(codec) then
            let inputOptions = fun input -> getConversioInputOptions input codec
            let outputOptions = fun options -> getConversionOutputOptions options codec
            let processor = FFMpegArguments
                                .FromFileInput(file, true, inputOptions)
                                .OutputToFile(outputFile, true, outputOptions)

            processor.ProcessSynchronously() |> ignore
        else
            printfn "Skipping file '%s' as its format is not the expected." outputFile
        printfn "Processed! File saved to '%s' %s" outputFile System.Environment.NewLine
    }

let ProcessFolder (outputfolder: string) (codec: string) (inputext: string) (inputfolder: string * string) =
    let files = Directory.EnumerateFiles (snd inputfolder) |> List.ofSeq
    files |> List.map (processFile codec (Path.Join([|outputfolder; fst inputfolder|])) inputext)  
                                |> Async.Parallel |> Async.RunSynchronously

let Run inputfolder outputfolder input codec =
    // Settings
    let settings = System.Text.Json.JsonSerializer.Deserialize<ConversorSettings>(File.ReadAllText("settings.json"))
    let options = new FFOptions()
    options.BinaryFolder <- settings.ffmpegfolder
    GlobalFFOptions.Configure(options);

    // Processing directories
    let directories = inputfolder :: (IOService.GetDirectoriesWithContent inputfolder $"*.{input}" |> List.ofSeq)
    let pathSets = directories |> List.map(fun d -> (d.Replace(inputfolder, ""), d))
    pathSets |> List.iter(fun p -> IOService.ReplicateFolderStructure (fst p) outputfolder) // create folder structure
    pathSets |> List.map (ProcessFolder outputfolder codec input) |> ignore