var Port;
var FunCookie;
document.getElementById("btn_add").hidden = true;
document.getElementById("btn_enable_select").hidden = true;
document.getElementById("btn_add_mass").hidden = true;
document.getElementById("btn_select_all").hidden = true;
document.getElementById("btn_select_none").hidden = true;
document.getElementById("btn_enable_funimation_select").hidden = true;
document.getElementById("btn_add_funimation").hidden = true;
document.getElementById("btn_add_mass_funimation").hidden = true;
document.getElementById("btn_add_AoD").hidden = true;

browser.storage.local.get("CRD_Port")
.then(gotPort, NoPort);

function getServerValue(request, sender, sendResponse) {
    if (request.Server == "CRD 1.0") {
        document.getElementById("CRD-Webserver").hidden = false;
        document.getElementById("txtOutput").remove();
        document.getElementById("txtInput").hidden = true;
        document.getElementById("btn_set_port").hidden = true;
        browser.tabs.query({
            currentWindow: true,
            active: true
        }).then((tabs) => {

            let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
            console.log(tab.url);
            if (tab.url.includes('crunchyroll.com')) {
                if (tab.url.includes('beta.crunchyroll.com')) {
                    var crunchyroll = browser.tabs.executeScript({
                        code: 'document.getElementsByClassName("c-playable-card__link")[0].href;'
                    });

                    crunchyroll.then(onExecuted, onError);

                } else {

                    var crunchyroll = browser.tabs.executeScript({
                        code: 'document.getElementsByClassName("episode")[0].href;'
                    });

                    crunchyroll.then(onExecuted, onError);
                }

            } else if (tab.url.includes('funimation.com')) {

                var funimation = browser.tabs.executeScript({

                    code: 'document.getElementsByClassName("trackVideo")[0].href'
                });
                funimation.then(FunimationSuccess, FunimationOldNotFound);
            } else if (tab.url.includes('anime-on-demand.de/anime/')) {

                document.getElementById("btn_add").hidden = true;
                document.getElementById("btn_enable_select").hidden = true;
                document.getElementById("btn_add_mass").hidden = true;
                document.getElementById("btn_select_all").hidden = true;
                document.getElementById("btn_select_none").hidden = true;
                document.getElementById("btn_enable_funimation_select").hidden = true;
                document.getElementById("btn_add_funimation").hidden = true;
                document.getElementById("btn_add_AoD").hidden = true; //false if implemented

            } else {

                document.getElementById("btn_add").hidden = true;
                document.getElementById("btn_enable_select").hidden = true;
                document.getElementById("btn_add_mass").hidden = true;
                document.getElementById("btn_select_all").hidden = true;
                document.getElementById("btn_select_none").hidden = true;
                document.getElementById("btn_enable_funimation_select").hidden = true;
                document.getElementById("btn_add_funimation").hidden = true;
                document.getElementById("btn_add_AoD").hidden = true;
            }
        }, console.error)
    } else {}

}
browser.runtime.onMessage.addListener(getServerValue);

function setItem() {
    console.log("OK");
}
function notsetItem() {
    console.log("Not OK");
}

function gotPort(result) {
    try {
        onStartup(result.CRD_Port.value);
        console.log("Port: " + result.CRD_Port.value)
    } catch (e) {
        onStartup(80);
        console.log("no port")
    }

}

function NoPort(result) {
    onStartup(80);
    console.log("no port")
}

function onStartup(result) {
    Port = result;
    var ifrm = document.createElement("iframe");
    ifrm.src = "http://127.0.0.1:" + Port;
    ifrm.style = "border:0px solid black;";
    ifrm.style.width = "760px";
    ifrm.style.height = "320px";
    ifrm.id = "CRD-Webserver";
    ifrm.hidden = true;
    document.body.appendChild(ifrm);

}

document.getElementById('btn_set_port').addEventListener('click', () => {
    let CRD_Port = {
        value: document.getElementById('txtInput').value
    }

    browser.storage.local.set({
        CRD_Port
    })
    .then(setItem, notsetItem);

    window.close();
});

document.getElementById('btn_add_AoD').addEventListener('click', () => {
    //browser.cookies.getAllCookieStores().then((cookie) => {
    //   browser.cookies.getAll({
    //	name: "_aod_session"
    //	}).then((cookie) => {
    //console.log(cookie)

    //}, console.error)

    var cookies = {};

    cookies.all = url => new Promise(resolve => chrome.cookies.getAll({
                url
            }, resolve));

    console.log(cookies)
});

document.getElementById('btn_enable_select').addEventListener('click', () => {

    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {

        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);

        if (tab.url.includes('beta.crunchyroll.com')) {

            let executing = browser.tabs.executeScript({
                file: "inject_beta.js"
            });
            executing.then(OnChange);

        } else {

            browser.tabs.executeScript({
                code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1:' + Port + '/inject.js",document.head.appendChild(script);'
            }); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0

            document.getElementById("btn_add_mass").hidden = false;
            document.getElementById("btn_select_all").hidden = false;
            document.getElementById("btn_select_none").hidden = false;
            document.getElementById("btn_enable_select").hidden = true;
            document.getElementById("btn_add").hidden = true;
            document.getElementById("btn_enable_funimation_select").hidden = true;
            document.getElementById("btn_add_funimation").hidden = true;
            document.getElementById("btn_add_AoD").hidden = true;

        }
    }, console.error)
});

function OnChange(result) {

    window.close()
}

document.getElementById('btn_select_all').addEventListener('click', () => {
    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {

        let tab = tabs[0];
        if (tab.url.includes('beta.crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("c-playable-card__link").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("c-playable-card__link")[i].style.background="#f78c25",document.getElementsByClassName("c-playable-card__link")[i].style.opacity = "0.5",document.getElementsByClassName("c-playable-card__link")[i].classList.add("CRD-Selected");'
            });

        } else if (tab.url.includes('crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#f78c25",document.getElementsByClassName("episode")[i].classList.add("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#f78c25",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.add("CRD-Selected");'
            });
			
			browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("episode-card").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode-card")[i].style.background="#400099",document.getElementsByClassName("episode-card")[i].classList.add("CRD-Selected"),document.getElementsByClassName("episode-card")[i].style.borderStyle="solid", document.getElementsByClassName("episode-card")[i].style.borderColor="#400099", document.getElementsByClassName("episode-card")[i].style.borderWidth = "10px";'
            });

        } else {}
    }, console.error)

});

document.getElementById('btn_select_none').addEventListener('click', () => {
    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {

        let tab = tabs[0];
        if (tab.url.includes('beta.crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("c-playable-card__link").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("c-playable-card__link")[i].style.background="#000000",document.getElementsByClassName("c-playable-card__link")[i].style.opacity="0",document.getElementsByClassName("c-playable-card__link")[i].classList.remove("CRD-Selected");'
            });

        } else if (tab.url.includes('crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#ffffff",document.getElementsByClassName("episode")[i].classList.remove("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#ffffff",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.remove("CRD-Selected");'
            });
			
			  browser.tabs.executeScript({
                  code: 'var i,episodeCount=document.getElementsByClassName("episode-card").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode-card")[i].style.background="#1e1e1e",document.getElementsByClassName("episode-card")[i].classList.remove("CRD-Selected"),document.getElementsByClassName("episode-card")[i].style.borderStyle="none";'
                  });

        } else {}
    }, console.error)
});

document.getElementById('btn_add').addEventListener('click', () => {

    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {

        let tab = tabs[0];
        if (tab.url.includes('beta.crunchyroll.com')) {
 
			
			add_beta_ok(tab.url)

        } else if (tab.url.includes('crunchyroll.com')) {

            var add_one = browser.tabs.executeScript({
                code: "document.getElementsByClassName('no-js')[0].innerHTML;"
            });
            add_one.then(add_one_ok, add_one_error);

        } else if (tab.url.includes('funimation.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#ffffff",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.remove("CRD-Selected");'
            });

        } else {}
    }, console.error)

});

document.getElementById('btn_add_funimation').addEventListener('click', () => {

    var add_fun = browser.tabs.executeScript({
        code: "document.cookie" //"document.getElementsByClassName('show-details')[0].innerHTML;"
    });
    add_fun.then(add_fun_ok, add_one_error);

});

document.getElementById('btn_add_mass').addEventListener('click', () => {

    var add_mass = browser.tabs.executeScript({
        code: 'var i,URLList="";for(i=0;i<document.getElementsByClassName("CRD-Selected").length;i++)URLList+=document.getElementsByClassName("CRD-Selected")[i].getAttribute("href");URLList;'
    });
    add_mass.then(add_mass_ok, add_mass_error);

});

document.getElementById('btn_add_mass_funimation').addEventListener('click', () => {

    var add_mass = browser.tabs.executeScript({
        code: 'var i,URLList="";for(i=0;i<document.getElementsByClassName("CRD-Selected").length;i++)URLList+=document.getElementsByClassName("CRD-Selected")[i].getAttribute("href");URLList;'
    });
    add_mass.then(add_mass_fun_ok, add_mass_error);

});

function onExecuted(result) {
    console.log(result[0]);

    if (result[0].includes('javascript:')) {
        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_add_mass").hidden = false;
        document.getElementById("btn_select_all").hidden = false;
        document.getElementById("btn_select_none").hidden = false;
        document.getElementById("btn_enable_select").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
        document.getElementById("btn_enable_funimation_select").hidden = true;
        document.getElementById("btn_add_AoD").hidden = true;
        console.log(true);
    } else {
        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_enable_select").hidden = false;
        document.getElementById("btn_add_mass").hidden = true;
        document.getElementById("btn_select_all").hidden = true;
        document.getElementById("btn_select_none").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
        document.getElementById("btn_enable_funimation_select").hidden = true;
        document.getElementById("btn_add_AoD").hidden = true;

        console.log(false);
    }
}

function onError(error) {
    console.log(`Error: ${error}`);

    document.getElementById("btn_add").hidden = false;
    document.getElementById("btn_add_mass").hidden = true;
    document.getElementById("btn_select_all").hidden = true;
    document.getElementById("btn_select_none").hidden = true;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;
    document.getElementById("btn_enable_funimation_select").hidden = true;
    document.getElementById("btn_add_AoD").hidden = true;

}
function add_fun_ok(result) {
    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {
        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);

        document.getElementById("btn_add_funimation").disabled = true;
        document.getElementById("btn_add_funimation").style.background = "#c9c9c9"

            var xhttp = new XMLHttpRequest();
        xhttp.open("POST", "http://127.0.0.1:" + Port + "/post", true);
        xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhttp.send("FunimationURL=" + tab.url + "&FunimationCookie=" + result);

        setTimeout(function () {
            document.getElementById("btn_add_funimation").style.background = "#ff8000"
        }, 10000);
        setTimeout(function () {
            document.getElementById("btn_add_funimation").disabled = false;
        }, 10000);

    }, console.error)
}

function add_one_ok(result) {

    document.getElementById("btn_add").disabled = true;
    document.getElementById("btn_add").style.background = "#c9c9c9"

        var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "http://127.0.0.1:" + Port + "/post", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send("HTMLSingle=" + result);

    setTimeout(function () {
        document.getElementById("btn_add").style.background = "#ff8000"
    }, 10000);
    setTimeout(function () {
        document.getElementById("btn_add").disabled = false;
    }, 10000);

}

function add_one_error(error) {
    console.log(`Error: ${error}`);
}
function add_mass_ok(result) {

    document.getElementById("btn_add_mass").disabled = true;
    document.getElementById("btn_add_mass").style.background = "#c9c9c9"

        var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "http://127.0.0.1:" + Port + "/post", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send("HTMLMass=" + result);

    setTimeout(function () {
        document.getElementById("btn_add_mass").style.background = "#ff8000"
    }, 10000);
    setTimeout(function () {
        document.getElementById("btn_add_mass").disabled = false;
    }, 10000);

}


function add_beta_ok(result) {

    document.getElementById("btn_add").disabled = true;
    document.getElementById("btn_add").style.background = "#c9c9c9"

        var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "http://127.0.0.1:" + Port + "/post", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send("HTMLMass=" + result);

    setTimeout(function () {
        document.getElementById("btn_add").style.background = "#ff8000"
    }, 10000);
    setTimeout(function () {
        document.getElementById("btn_add").disabled = false;
    }, 10000);

}

function add_mass_fun_ok(result) {

    document.getElementById("btn_add_mass_funimation").disabled = true;
    document.getElementById("btn_add_mass_funimation").style.background = "#c9c9c9"

        var postdata = result + "&FunimationCookie=" + FunCookie

        var xhttp = new XMLHttpRequest();
    xhttp.open("POST", "http://127.0.0.1:" + Port + "/post", true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send("FunimationMass=" + postdata);

    setTimeout(function () {
        document.getElementById("btn_add_mass_funimation").style.background = "#ff8000"
    }, 10000);
    setTimeout(function () {
        document.getElementById("btn_add_mass_funimation").disabled = false;
    }, 10000);

}

function add_mass_error(error) {
    console.log(`Error: ${error}`);
}
//funimation

document.getElementById('btn_enable_funimation_select').addEventListener('click', () => {
	
	
	var funimation = browser.tabs.executeScript({

                    code: 'document.getElementsByClassName("episode-card")[0].href'
                });
                funimation.then(FunimationNewSelect, FunimationOldSelect);

 

});

function FunimationOldSelect(result) {

       browser.tabs.executeScript({
        code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1:' + Port + '/inject_funimation.js",document.head.appendChild(script);'
    }); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0

    document.getElementById("btn_add_mass").hidden = true;
    document.getElementById("btn_add_mass_funimation").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;
    document.getElementById("btn_add_AoD").hidden = true;
    document.getElementById("btn_enable_funimation_select").hidden = true;

}


function FunimationNewSelect(result) {

       browser.tabs.executeScript({
        file: 'inject_funimation_new.js'
    }); 

    document.getElementById("btn_add_mass").hidden = true;
    document.getElementById("btn_add_mass_funimation").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;
    document.getElementById("btn_add_AoD").hidden = true;
    document.getElementById("btn_enable_funimation_select").hidden = true;

}

function fun_cookie_ok(result) {

    FunCookie = result;

}

function FunimationSuccess(result) {
    console.log(result[0]);

    if (result[0].includes('javascript:')) {
        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_add_mass").hidden = true;
        document.getElementById("btn_add_mass_funimation").hidden = false;
        document.getElementById("btn_select_all").hidden = false;
        document.getElementById("btn_select_none").hidden = false;
        document.getElementById("btn_enable_select").hidden = true;
        document.getElementById("btn_add_AoD").hidden = true;

        document.getElementById("btn_enable_funimation_select").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
        document.getElementById("btn_add_AoD").hidden = true;
        var SaveFunimationCookie = browser.tabs.executeScript({
            code: "document.cookie"
        });
        SaveFunimationCookie.then(fun_cookie_ok, add_mass_error);
        console.log(true);
    } else {
        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
        document.getElementById("btn_enable_select").hidden = true;
        document.getElementById("btn_add_mass").hidden = true;
        document.getElementById("btn_select_all").hidden = true;
        document.getElementById("btn_select_none").hidden = true;
        document.getElementById("btn_enable_funimation_select").hidden = false;
        document.getElementById("btn_add_AoD").hidden = true;
        var SaveFunimationCookie = browser.tabs.executeScript({
            code: "document.cookie"
        });
        SaveFunimationCookie.then(fun_cookie_ok, add_mass_error);
        console.log(false);
    }
}
function FunimationOldNotFound(error) {
	
	var funimation = browser.tabs.executeScript({

                    code: 'document.getElementsByClassName("episode-card")[0].href'
                });
                funimation.then(FunimationSuccess, FunimationError);
}

function FunimationError(error) {
	
	console.log(`Error: ${error}`);

    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_add_mass").hidden = true;
    document.getElementById("btn_select_all").hidden = true;
    document.getElementById("btn_select_none").hidden = true;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add_funimation").hidden = false;
    document.getElementById("btn_enable_funimation_select").hidden = true;
    document.getElementById("btn_add_AoD").hidden = true;
	
	

}
