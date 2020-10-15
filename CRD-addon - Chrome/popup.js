chrome.tabs.query({
    active: true,
    currentWindow: true
},
    function (tabs) {
    console.log(tabs.length);
    let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
    console.log(tab.url);
    if (tab.url.includes('crunchyroll.com')) {
        //var crunchyroll = chrome.tabs.executeScript({
        //    code: 'document.getElementsByClassName("episode")[0].href;'
        //});

        //crunchyroll.then(onExecuted, onError);

        chrome.tabs.executeScript(null, {
            //code: 'document.getElementsByClassName("episode")[0].href;'
            code: 'document.getElementsByClassName("episode").length;'
        },
            function (results) {

            if (results > 0) {

                onExecuted(results);
            } else {
                onError("error")
            }

        });

    } else if (tab.url.includes('funimation.com')) {
        chrome.tabs.executeScript(null, {
            //code: 'document.getElementsByClassName("trackVideo")[0].href'
            code: 'document.getElementsByClassName("trackVideo").length;'
        },
            function (results) {

            if (results > 0) {
                FunimationSuccess(results);

            } else {
                FunimationError(results);

            }

        });

    } else {

        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_enable_select").hidden = true;
        document.getElementById("btn_add_mass").hidden = true;
        document.getElementById("btn_select_all").hidden = true;
        document.getElementById("btn_select_none").hidden = true;
        document.getElementById("btn_enable_funimation_select").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
    }
});

document.getElementById('btn_enable_select').addEventListener('click', () => {

    chrome.tabs.executeScript(null, {
        code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject.js",document.head.appendChild(script);'
    });

    //browser.tabs.executeScript({
    //   code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject.js",document.head.appendChild(script);'
    //}); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0

    document.getElementById("btn_add_mass").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_enable_funimation_select").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;
});

document.getElementById('btn_select_all').addEventListener('click', () => {
    chrome.tabs.query({
        active: true,
        currentWindow: true
    },
        function (tabs) {
        console.log(tabs.length);
        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);
        if (tab.url.includes('crunchyroll.com')) {

            chrome.tabs.executeScript(null, {
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#f78c25",document.getElementsByClassName("episode")[i].classList.add("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            chrome.tabs.executeScript(null, {
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#f78c25",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.add("CRD-Selected");'
            });

        } else {}
    });

});

document.getElementById('btn_select_none').addEventListener('click', () => {

    chrome.tabs.query({
        active: true,
        currentWindow: true
    },
        function (tabs) {
        console.log(tabs.length);
        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);
        if (tab.url.includes('crunchyroll.com')) {

            chrome.tabs.executeScript(null, {
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#ffffff",document.getElementsByClassName("episode")[i].classList.remove("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            chrome.tabs.executeScript(null, {
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#ffffff",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.remove("CRD-Selected");'
            });

        } else {}
    });

});

document.getElementById('btn_add').addEventListener('click', () => {
    chrome.tabs.executeScript(null, {
        code: "document.getElementsByClassName('no-js')[0].innerHTML;"
    },
        function (results) {

        if (results !== null) {

            document.getElementById("btn_add").disabled = true;
            document.getElementById("btn_add").style.background = "#c9c9c9"
                const form = document.createElement('form');
            form.method = 'post';
            form.action = "http://127.0.0.1/post";
            const hiddenField = document.createElement('input');
            hiddenField.type = 'hidden';
            hiddenField.name = "HTMLSingle";
            hiddenField.value = results;
            form.appendChild(hiddenField);

            document.body.appendChild(form);
            form.submit();

            setTimeout(function () {
                document.getElementById("btn_add").style.background = "#ff8000"
            }, 10000);
            setTimeout(function () {
                document.getElementById("btn_add").disabled = false;
            }, 10000);

        } else {
            add_one_error(results);

        }

    });

});

document.getElementById('btn_add_funimation').addEventListener('click', () => {

    chrome.tabs.query({
        active: true,
        currentWindow: true
    },
        function (tabs) {
        document.getElementById("btn_add_funimation").disabled = true;
        document.getElementById("btn_add_funimation").style.background = "#c9c9c9"

            console.log(tabs.length);
        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);
        const form = document.createElement('form');
        form.method = 'post';
        form.action = "http://127.0.0.1/post";

        const hiddenField2 = document.createElement('input');
        hiddenField2.type = 'hidden';
        hiddenField2.name = "FunimationURL";
        hiddenField2.value = tab.url;
        form.appendChild(hiddenField2);

        document.body.appendChild(form);
        form.submit();
        setTimeout(function () {
            document.getElementById("btn_add_funimation").style.background = "#ff8000"
        }, 10000);
        setTimeout(function () {
            document.getElementById("btn_add_funimation").disabled = false;
        }, 10000);
    });
});

document.getElementById('btn_add_mass').addEventListener('click', () => {

    chrome.tabs.executeScript(null, {
        code: 'var i,URLList="";for(i=0;i<document.getElementsByClassName("CRD-Selected").length;i++)URLList+=document.getElementsByClassName("CRD-Selected")[i].getAttribute("href");URLList;'
    },
        function (results) {

        if (results !== null) {

            document.getElementById("btn_add_mass").disabled = true;
            document.getElementById("btn_add_mass").style.background = "#c9c9c9"
                const form = document.createElement('form');
            form.method = 'post';
            form.action = "http://127.0.0.1/post";
            const hiddenField = document.createElement('input');
            hiddenField.type = 'hidden';
            hiddenField.name = "HTMLMass";
            hiddenField.value = results;
            form.appendChild(hiddenField);

            document.body.appendChild(form);
            form.submit();

            setTimeout(function () {
                document.getElementById("btn_add_mass").style.background = "#ff8000"
            }, 4000);
            setTimeout(function () {
                document.getElementById("btn_add_mass").disabled = false;
            }, 4000);

        } else {
            add_mass_error(results);

        }

    });

});

function onExecuted(result) {
    chrome.tabs.executeScript(null, {
        code: "document.getElementsByClassName('episode')[0].href.includes('javascript:');"
    },
        function (result) {
        //alert(result);
        if (result == 'true') {
            document.getElementById("btn_add").hidden = true;
            document.getElementById("btn_add_mass").hidden = false;
            document.getElementById("btn_select_all").hidden = false;
            document.getElementById("btn_select_none").hidden = false;
            document.getElementById("btn_enable_select").hidden = true;
            document.getElementById("btn_add_funimation").hidden = true;
            document.getElementById("btn_enable_funimation_select").hidden = true;
            console.log(true);
        } else {
            document.getElementById("btn_add").hidden = true;
            document.getElementById("btn_enable_select").hidden = false;
            document.getElementById("btn_add_mass").hidden = true;
            document.getElementById("btn_select_all").hidden = true;
            document.getElementById("btn_select_none").hidden = true;
            document.getElementById("btn_add_funimation").hidden = true;
            document.getElementById("btn_enable_funimation_select").hidden = true;
            console.log(false);
        }
    });

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

}

function add_one_error(error) {
    console.log(`Error: ${error}`);
}

function add_mass_error(error) {
    console.log(`Error: ${error}`);
}
//funimation

document.getElementById('btn_enable_funimation_select').addEventListener('click', () => {

    // browser.tabs.executeScript({
    //     code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject_funimation.js",document.head.appendChild(script);'
    // }); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0
    chrome.tabs.executeScript(null, {
        code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject_funimation.js",document.head.appendChild(script);'
    });

    document.getElementById("btn_add_mass").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;

});

function FunimationSuccess(result) {
    chrome.tabs.executeScript(null, {
        code: "document.getElementsByClassName('trackVideo')[0].href.includes('javascript:');"
    },
        function (result) {
        if (result == 'true') {
            document.getElementById("btn_add").hidden = true;
            document.getElementById("btn_add_mass").hidden = false;
            document.getElementById("btn_select_all").hidden = false;
            document.getElementById("btn_select_none").hidden = false;
            document.getElementById("btn_enable_select").hidden = true;

            document.getElementById("btn_enable_funimation_select").hidden = true;
            document.getElementById("btn_add_funimation").hidden = true;
            console.log(true);
        } else {
            document.getElementById("btn_add").hidden = true;
            document.getElementById("btn_add_funimation").hidden = true;
            document.getElementById("btn_enable_select").hidden = true;
            document.getElementById("btn_add_mass").hidden = true;
            document.getElementById("btn_select_all").hidden = true;
            document.getElementById("btn_select_none").hidden = true;
            document.getElementById("btn_enable_funimation_select").hidden = false;

            console.log(false);
        }
    });
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

}
