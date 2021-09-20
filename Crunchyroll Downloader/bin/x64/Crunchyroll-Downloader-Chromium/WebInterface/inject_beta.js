var episodeCount = document.getElementsByClassName("c-playable-card__link").length;
var i;
for (i = 0; i < episodeCount; i++) {
    var old_element = document.getElementsByClassName("c-playable-card__link")[i];
    var new_element = old_element.cloneNode(true);
    old_element.parentNode.replaceChild(new_element, old_element);
    document.getElementsByClassName("c-playable-card__link")[i].setAttribute('href', "javascript:" + document.getElementsByClassName("c-playable-card__link")[i].href);
    document.getElementsByClassName("c-playable-card__link")[i].setAttribute('onclick', 'this.classList.contains("CRD-Selected")?(this.classList.remove("CRD-Selected"),this.style.background="#000000",this.style.opacity="0"):(this.classList.add("CRD-Selected"),this.style.background="#f78c25",this.style.opacity="0.5");');

}
