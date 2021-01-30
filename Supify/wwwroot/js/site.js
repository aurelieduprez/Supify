// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*waveform
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


/*remove stylesheet with dark theme switch
$(function () {
    var linkEl;
    $("#switch").click(function () {
        if (!linkEl) {
            linkEl = $('<link rel="stylesheet" type="text/css" href="../wwwroot/css/darktheme.css />')
                .appendTo('head')[0];
        }
        else if (linkEl.sheet) {
            linkEl.sheet.disabled = !linkEl.sheet.disabled;
        }
    });
});*/

// Retrive buttons from page
var buttons = {
    back: document.getElementById('btn-back'),
    backward: document.getElementById('btn-backward'),
    playpause: document.getElementById('btn-playpause'),
    forward: document.getElementById('btn-forward'),
    next: document.getElementById('btn-next'),
    mute: document.getElementById('btn-mute')
}

// Variables
var playlistSelect = document.getElementById('playlistId');
var isPlaying = false;

// Create the wave player
var wavesurfer = WaveSurfer.create({
    container: '#waveform',
    barWidth: 5,
    barRadius: 5,
    cursorWidth: 0,
    height: 200,
    hideScrollbar: true,
    partialRender: true
});


// volume cursor
document.querySelector('#volume').value = wavesurfer.backend.getVolume();
var volumeInput = document.querySelector('#volume');
var onChangeVolume = function (e) {
    wavesurfer.setVolume(e.target.value);
    console.log(e.target.value);
};
volumeInput.addEventListener('input', onChangeVolume);

// Play Pause function
function playpause() {
    if (isPlaying) { wavesurfer.pause(); }
    else { wavesurfer.play(); }
    buttons.forward.disabled = !buttons.forward.disabled;
    buttons.backward.disabled = !buttons.backward.disabled;
    isPlaying = !isPlaying;
}
// Play Pause Button
buttons.playpause.addEventListener("click", playpause);

// Mute toggle
buttons.mute.addEventListener("click", () => { wavesurfer.setMute(!wavesurfer.getMute()) });

// Execute every time a song is loaded
wavesurfer.on('ready', function () {
    buttons.playpause.disabled = false;
    wavesurfer.setProgressColor("#415a77");
    wavesurfer.setWaveColor("#778da9");
    wavesurfer.setVolume(0.2);
});

// Forward button
buttons.forward.addEventListener("click", function () { wavesurfer.skipForward(5); });

// Backward button
buttons.backward.addEventListener("click", function () { wavesurfer.skipBackward(5); });

// Function to load a song to the player
function loadToPlayer(path) {
    buttons.backward.disabled = true;
    buttons.forward.disabled = true;
    buttons.playpause.disabled = true;
    isPlaying = false;
    wavesurfer.load(path);
}

// Refresh song list when playlist is changed
playlistSelect.onchange = function () {
    // Retrive the playlist id
    var playlistId = this.value;

    songs = document.getElementById("songs");

    // EMPTY
    while (songs.firstChild) {
        songs.removeChild(songs.firstChild);
    }

    // LOAD AND ADD NEW SONGS
    fetch(`/get-playlist-songs?id=${playlistId}`, { method: 'GET' })
        .then(response => response.json())
        .then(result => {
            result.value.forEach(song => {

                var li = document.createElement("li");
                var p = document.createElement("p");
                var button = document.createElement("button");

                p.className = 'name';
                p.innerText = song.name;

                button.addEventListener("click", () => { loadToPlayer(song.path) });
                button.innerText = 'Load';

                li.appendChild(p);
                li.appendChild(button);

                songs.appendChild(li);
            });
        })
        .catch(error => console.log('error', error));
}