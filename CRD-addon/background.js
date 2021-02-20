browser.webRequest.onHeadersReceived.addListener(
    details => {
    browser.test.log(`onHeadersReceived ${JSON.stringify(details)}\n`);
    if (!details.fromCache) {
        //console.log("not cached");
        browser.test.sendMessage("statusCode", details.statusCode);
        const mime = details.responseHeaders.find(header => {
            return header.value && header.name === "Server";
        });
        if (mime) {
            //console.log(mime.value);
           browser.runtime.sendMessage({
                Server: mime.value
		   });

        } else {
            details.responseHeaders.push({
                name: "Content-Security-Policy",
                value: "upgrade-insecure-requests",
            });
        }

        return {
            responseHeaders: details.responseHeaders,
        };
    } else {
        console.log("cached");
    }
}, {
    urls: ["http://127.0.0.1/*"],
},
    ["blocking", "responseHeaders"]);
