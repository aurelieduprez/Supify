// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//waveform
var wavesurfer = WaveSurfer.create({
    container: '#waveform',
    barWidth: 5,
    barRadius: 5,
    cursorWidth: 0,
    height: 200,
    hideScrollbar: true,
    partialRender: true
});
//load music
wavesurfer.load('/music/theWeekend.mp3');
wavesurfer.on('ready', function () {
    wavesurfer.setProgressColor("#415a77");
    wavesurfer.setWaveColor("#778da9");
    wavesurfer.play();
    wavesurfer.setVolume(0.2);
});

// volume cursor
document.querySelector('#volume').value = wavesurfer.backend.getVolume();
var volumeInput = document.querySelector('#volume');
var onChangeVolume = function (e) {
    wavesurfer.setVolume(e.target.value);
    console.log(e.target.value);
};
volumeInput.addEventListener('input', onChangeVolume);
