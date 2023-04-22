# MediaConverter

This CLI tool is a basic Media to MP3/OGG/AC3/AVI converter using FFmpegCore.

## Using
### Parameters
* -p / path: Path to the ffmpeg folder (currently defined in `settings.json`)
* -f / folder: Path to the input folder
* -o / output: Path to the output folder
* -i / input: Extension of the input files
* -c / codec: Desired codec for conversion (192kbps for specified codecs, defaulted to mp3 at 128kbps)
   
   * ogg
   * mp3
   * ac3
   * avi

### Example
```
.\MediaConverter.exe -f "D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)" -o "D:\test" -i "flac" -c "ogg"
==== Starting 'Media Converter' ====
Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\01. Devil's Street.ogg'

Processing file 'D:\torrent\complete\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.flac' ...
Processed! File saved to 'D:\test\Funeral Void - To Forever Misery Bound (2023)\02. Well Of Grief.ogg'
...

==== Finished 'Media Converter' ====
```