var episodeCount = document.getElementsByClassName("episode-card").length;
var i;
for (i = 0; i < episodeCount; i++) {
    var old_element = document.getElementsByClassName("episode-card")[i];
    var new_element = old_element.cloneNode(true);
    old_element.parentNode.replaceChild(new_element, old_element);
	document.getElementsByClassName("episode-card")[i].classList.remove('transparent')
    document.getElementsByClassName("episode-card")[i].setAttribute('href', "javascript:" + document.getElementsByClassName("episode-card")[i].href);
    document.getElementsByClassName("episode-card")[i].setAttribute('onclick', 'this.classList.contains("CRD-Selected")?(this.classList.remove("CRD-Selected"),this.style.background="#000000",this.style.borderStyle="none",this.style.opacity="1"):(this.classList.add("CRD-Selected"),this.style.background="#400099",this.style.borderStyle="solid", this.style.borderColor="#400099", this.style.borderWidth = "10px");');

}
