﻿using System;
using System.Diagnostics;
using System.IO;

namespace NotEnoughAV1Encodes
{
    internal class EncodeAudio
    {
        public static void AudioEncode()
        {
            if (MainWindow.audioEncoding == true)
            {
                string audioInput = "";
                string audioMapping = "";
                string audioCodec = "";
                //Creates AudioEncoded Directory in the temp dir
                if (!Directory.Exists(Path.Combine(MainWindow.workingTempDirectory, "AudioEncoded")))
                    Directory.CreateDirectory(Path.Combine(MainWindow.workingTempDirectory, "AudioEncoded"));
                //Audio Encoder --------------------------------------------------------------------------||
                if (MainWindow.audioCodec == "Opus") { audioCodec = "libopus"; }
                if (MainWindow.audioCodec == "Opus 5.1") { audioCodec = "libopus -af channelmap=channel_layout=5.1"; }
                if (MainWindow.audioCodec == "Opus Downmix") { audioCodec = "libopus -ac 2"; }
                if (MainWindow.audioCodec == "AC3") { audioCodec = "ac3"; }
                if (MainWindow.audioCodec == "AAC") { audioCodec = "aac"; }
                if (MainWindow.audioCodec == "MP3") { audioCodec = "libmp3lame"; }
                //----------------------------------------------------------------------------------------||
                //Audio Mapping --------------------------------------------------------------------------||
                if (MainWindow.trackOne == true && MainWindow.detectedTrackOne == true)
                {
                    audioInput += " -c:a:0 " + audioCodec + " -b:a:0 " + MainWindow.audioBitrate + "k";
                    audioMapping += " -map 0:"+ MainWindow.firstTrackIndex + " ";
                }
                if (MainWindow.trackTwo == true && MainWindow.detectedTrackTwo == true)
                {
                    audioInput += " -c:a:1 " + audioCodec + " -b:a:1 " + MainWindow.audioBitrate + "k";
                    audioMapping += " -map 0:" + MainWindow.secondTrackIndex + " ";
                }
                if (MainWindow.trackThree == true && MainWindow.detectedTrackThree == true)
                {
                    audioInput += " -c:a:2 " + audioCodec + " -b:a:2 " + MainWindow.audioBitrate + "k";
                    audioMapping += " -map 0:" + MainWindow.thirdTrackIndex + " ";
                }
                if (MainWindow.trackFour == true && MainWindow.detectedTrackFour == true)
                {
                    audioInput += " -c:a:3 " + audioCodec + " -b:a:3 " + MainWindow.audioBitrate + "k";
                    audioMapping += " -map 0:" + MainWindow.fourthTrackIndex + " ";
                }
                //----------------------------------------------------------------------------------------||
                //Audio Encoding -------------------------------------------------------------------------||
                string ffmpegAudioCommands = "/C ffmpeg.exe -i " + '\u0022' + MainWindow.videoInput + '\u0022' + " -map_metadata -1 -vn -sn -dn" + audioInput + audioMapping + '\u0022' + MainWindow.workingTempDirectory + "\\AudioEncoded\\audio.mkv" + '\u0022';
                SmallScripts.ExecuteFfmpegTask(ffmpegAudioCommands);
                //----------------------------------------------------------------------------------------||
            }
        }
    }
}