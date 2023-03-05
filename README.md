# FlacToMp3Converter

This CLI tool is a basic FLAC to MP3 converter using FFmpegCore.

## Using
### Parameters
* -p / path: Path to the ffmpeg folder (currently defined in `settings.json`)
* -f / folder: Path to the input folder
* -o / output: Path to the output folder

### Example
```
.\FlacToMp3Converter.exe -f "D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)" -o "D:\test"
==== Starting 'FLAC to MP3 Converter' ====
Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.mp3' processed!

Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.mp3' processed!
...

==== Finished 'FLAC to MP3 Converter' ====
```