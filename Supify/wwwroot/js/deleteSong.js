var playlistSelect = document.getElementById('playlistId');
var songSelect = document.getElementById('Id');

playlistSelect.onchange = function () {
//from the playlistid...
    var playlistId = this.value;

// empties the song selection if needed
    while (songSelect.options.length > 0) {
        songSelect.remove(0);
    }

    // fetch corresponding songs
    fetch(`/get-songs?id=${playlistId}`, { method: 'GET' })
        .then(response => response.json())
        .then(result => {
            result.value.forEach(song => {

                var option = document.createElement("option");
                option.text = song.name;
                option.value = song.id;

                songSelect.add(option);
            });
        })
        .catch(error => console.log('error', error));
}