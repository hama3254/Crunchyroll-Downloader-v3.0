var episodeCount = document.getElementsByClassName("fullEpisodeThumbs").length;
var i;
for (i = 0; i < episodeCount; i++) {

    document.getElementsByClassName("trackVideo")[i * 3].setAttribute('href', "javascript:" + document.getElementsByClassName("trackVideo")[i * 3].href);
    document.getElementsByClassName("trackVideo")[i * 3 + 1].setAttribute('href', "javascript:" + document.getElementsByClassName("trackVideo")[i * 3 + 1].href);
    document.getElementsByClassName("trackVideo")[i * 3 + 2].setAttribute('href', "javascript:" + document.getElementsByClassName("trackVideo")[i * 3 + 2].href);

    //document.getElementsByClassName("fullEpisodeThumbs")[i].setAttribute('href', "javascript:" + document.getElementsByClassName("trackVideo")[i].href);

	 document.getElementsByClassName("fullEpisodeThumbs")[i].setAttribute('href', document.getElementsByClassName("trackVideo")[i * 3].href);

    document.getElementsByClassName("fullEpisodeThumbs")[i].setAttribute('onclick', 'deselect(this.id)')
    //document.getElementsByClassName("fullEpisodeThumbs")[i].style.background = "#f78c25";

    document.getElementsByClassName("fullEpisodeThumbs")[i].setAttribute('id', makeid(8))

    //document.getElementsByClassName("fullEpisodeThumbs")[i].classList.add('CRD-Selected')
}

function deselect(clicked_id) {
    var seleceted = document.getElementById(clicked_id).classList.contains('CRD-Selected')

        if (seleceted == true) {
            document.getElementById(clicked_id).classList.remove('CRD-Selected')
            document.getElementById(clicked_id).style.background = "#ffffff";

        } else {
            document.getElementById(clicked_id).classList.add('CRD-Selected')
            document.getElementById(clicked_id).style.background = "#f78c25";

        }

}

function makeid(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}
