browser.tabs.query({
    currentWindow: true,
    active: true
}).then((tabs) => {

    let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
    console.log(tab.url);
    if (tab.url.includes('crunchyroll.com')) {
        var crunchyroll = browser.tabs.executeScript({
            code: 'document.getElementsByClassName("episode")[0].href;'
        });

        crunchyroll.then(onExecuted, onError);

    } else if (tab.url.includes('funimation.com')) {

        var funimation = browser.tabs.executeScript({

            code: 'document.getElementsByClassName("trackVideo")[0].href'
        });
        funimation.then(FunimationSuccess, FunimationError);

    } else {

        document.getElementById("btn_add").hidden = true;
        document.getElementById("btn_enable_select").hidden = true;
        document.getElementById("btn_add_mass").hidden = true;
        document.getElementById("btn_select_all").hidden = true;
        document.getElementById("btn_select_none").hidden = true;
        document.getElementById("btn_enable_funimation_select").hidden = true;
        document.getElementById("btn_add_funimation").hidden = true;
    }
}, console.error)

document.getElementById('btn_enable_select').addEventListener('click', () => {
    browser.tabs.executeScript({
        code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject.js",document.head.appendChild(script);'
    }); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0

    document.getElementById("btn_add_mass").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_enable_funimation_select").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;
});

document.getElementById('btn_select_all').addEventListener('click', () => {
    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {

        let tab = tabs[0];

        if (tab.url.includes('crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#f78c25",document.getElementsByClassName("episode")[i].classList.add("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#f78c25",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.add("CRD-Selected");'
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
        if (tab.url.includes('crunchyroll.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("episode").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("episode")[i].style.background="#ffffff",document.getElementsByClassName("episode")[i].classList.remove("CRD-Selected");'
            });

        } else if (tab.url.includes('funimation.com')) {

            browser.tabs.executeScript({
                code: 'var i,episodeCount=document.getElementsByClassName("fullEpisodeThumbs").length;for(i=0;i<episodeCount;i++)document.getElementsByClassName("fullEpisodeThumbs")[i].style.background="#ffffff",document.getElementsByClassName("fullEpisodeThumbs")[i].classList.remove("CRD-Selected");'
            });

        } else {}
    }, console.error)
});

document.getElementById('btn_add').addEventListener('click', () => {
    var add_one = browser.tabs.executeScript({
        code: "document.getElementsByClassName('no-js')[0].innerHTML;"
    });
    add_one.then(add_one_ok, add_one_error);

});

document.getElementById('btn_add_funimation').addEventListener('click', () => {
    var add_fun = browser.tabs.executeScript({
        code: "document.getElementsByClassName('no-touchevents')[0].innerHTML;"
    });
    add_fun.then(add_fun_ok, add_one_error);

});

document.getElementById('btn_add_mass').addEventListener('click', () => {

    var add_mass = browser.tabs.executeScript({
        code: 'var i,URLList="";for(i=0;i<document.getElementsByClassName("CRD-Selected").length;i++)URLList+=document.getElementsByClassName("CRD-Selected")[i].getAttribute("href");URLList;'
    });
    add_mass.then(add_mass_ok, add_mass_error);

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
function add_fun_ok(result) {
    browser.tabs.query({
        currentWindow: true,
        active: true
    }).then((tabs) => {
        let tab = tabs[0]; // Safe to assume there will only be one resultconsole.log(tab.url);
        console.log(tab.url);

        const form = document.createElement('form');
        form.method = 'post';
        form.action = "http://127.0.0.1";

        const hiddenField = document.createElement('input');
        hiddenField.type = 'hidden';
        hiddenField.name = "FunimationHTML";
        hiddenField.value = result;
        form.appendChild(hiddenField);

        const hiddenField2 = document.createElement('input');
        hiddenField2.type = 'hidden';
        hiddenField2.name = "FunimationURL";
        hiddenField2.value = tab.url;
        form.appendChild(hiddenField2);

        document.body.appendChild(form);
        form.submit();
    }, console.error)
}

function add_one_ok(result) {

    const form = document.createElement('form');
    form.method = 'post';
    form.action = "http://127.0.0.1";

    const hiddenField = document.createElement('input');
    hiddenField.type = 'hidden';
    hiddenField.name = "HTMLSingle";
    hiddenField.value = result;
    form.appendChild(hiddenField);

    document.body.appendChild(form);
    form.submit();

}

function add_one_error(error) {
    console.log(`Error: ${error}`);
}
function add_mass_ok(result) {

    const form = document.createElement('form');
    form.method = 'post';
    form.action = "http://127.0.0.1";

    const hiddenField = document.createElement('input');
    hiddenField.type = 'hidden';
    hiddenField.name = "HTMLMass";
    hiddenField.value = result;
    form.appendChild(hiddenField);

    document.body.appendChild(form);
    form.submit();

}

function add_mass_error(error) {
    console.log(`Error: ${error}`);
}
//funimation

document.getElementById('btn_enable_funimation_select').addEventListener('click', () => {

    browser.tabs.executeScript({
        code: 'var script=document.createElement("script");script.type="text/javascript",script.src="http://127.0.0.1/inject_funimation.js",document.head.appendChild(script);'
    }); //load script from local CRD Server included in https://github.com/hama3254/Crunchyroll-Downloader-v3.0

    document.getElementById("btn_add_mass").hidden = false;
    document.getElementById("btn_select_all").hidden = false;
    document.getElementById("btn_select_none").hidden = false;
    document.getElementById("btn_enable_select").hidden = true;
    document.getElementById("btn_add").hidden = true;
    document.getElementById("btn_add_funimation").hidden = true;

});

function FunimationSuccess(result) {
    console.log(result[0]);

    if (result[0].includes('javascript:')) {
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
