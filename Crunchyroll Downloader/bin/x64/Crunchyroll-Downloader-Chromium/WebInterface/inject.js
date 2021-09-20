var episodeCount = document.getElementsByClassName("episode").length;
var i;
for (i = 0; i < episodeCount; i++) {
    document.getElementsByClassName("episode")[i].setAttribute('href', "javascript:" + document.getElementsByClassName("episode")[i].href);
    document.getElementsByClassName("episode")[i].setAttribute('onclick', 'deselect(this.id)')
    //document.getElementsByClassName("episode")[i].style.background = "#f78c25";
    document.getElementsByClassName("episode")[i].setAttribute('id', makeid(8))
    //document.getElementsByClassName("episode")[i].classList.add('CRD-Selected')
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
