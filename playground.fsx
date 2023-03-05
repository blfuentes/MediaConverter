open System.IO
open FFMpegCore

let files = Directory.EnumerateFiles @"D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)"

let options = FFMpegOptions()
options.RootDirectory <- "path to your binaries"
FFMpegOptions.Configure(options)