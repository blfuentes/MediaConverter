# FlacToMp3Converter

This CLI tool is a basic FLAC to MP3 converter using FFmpegCore.

## Using
### Parameters
* -p / path: Path to the ffmpeg folder (currently defined in `settings.json`)
* -f / folder: Path to the input folder
* -o / output: Path to the output folder
* -c / codec: Desired codec for conversion (192kbps, defaulted to mp3 at 128kbps)
   
   * ogg
   * mp3
   * ac3

### Example
```
.\FlacToMp3Converter.exe -f "D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)" -o "D:\test" -c "ogg"
==== Starting 'FLAC Converter' ====
Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.ogg'

Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.ogg'
...

==== Finished 'FLAC Converter' ====
```