// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var wavesurfer = WaveSurfer.create({
    container: '#waveform'
});
wavesurfer.load('/music/theWeekend.mp3');
wavesurfer.on('ready', function () {
    wavesurfer.setProgressColor("#415a77");
    wavesurfer.setWaveColor("#778da9");
    wavesurfer.play();
    wavesurfer.setVolume(0.1);
});