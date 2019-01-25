
//styleHeader();
(function mainModule() {
    "use strict";

    var systemNavManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    function startApp() {
        var dlCv = document.querySelector(".cv");
        if (dlCv !== null) {
            dlCv.addEventListener("click", downloadFile, false);
        }
        
        var dlCv2 = document.querySelector(".cv2");
        if (dlCv2 !== null) {
            dlCv2.addEventListener("click", downloadFile, false);
        }

        // backRequested event
        if (systemNavManager !== null) {
            systemNavManager.addEventListener("backrequested", backRequested, false);
            UpdateBackButtonVisibility();
        }

        var applicationView = Windows.UI.ViewManagement.ApplicationView;

        if (applicationView !== null) {
            var customColors = {
                backgroundColor: { a: 255, r: 80, g: 80, b: 80 },
                foregroundColor: { a: 255, r: 255, g: 255, b: 255 },
                buttonBackgroundColor: { a: 0, r: 80, g: 80, b: 80 },
                buttonForegroundColor: { a: 255, r: 255, g: 255, b: 255 },
                buttonHoverBackgroundColor: { a: 255, r: 84, g: 84, b: 84 },
                buttonHoverForegroundColor: { a: 255, r: 255, g: 255, b: 255 },
                buttonPressedBackgroundColor: { a: 255, r: 132, g: 211, b: 162 },
                buttonPressedForegroundColor: { a: 255, r: 24, g: 60, b: 216 },
                inactiveBackgroundColor: { a: 255, r: 135, g: 141, b: 199 },
                inactiveForegroundColor: { a: 255, r: 132, g: 211, b: 162 },
                buttonInactiveBackgroundColor: { a: 255, r: 135, g: 141, b: 199 },
                buttonInactiveForegroundColor: { a: 255, r: 132, g: 211, b: 162 },
            };

            var titleBar = applicationView.getForCurrentView().titleBar;
            if (titleBar != null) {
                titleBar.buttonBackgroundColor = customColors.buttonBackgroundColor;
                titleBar.buttonForegroundColor = customColors.buttonForegroundColor;
                titleBar.buttonHoverForegroundColor = customColors.buttonHoverForegroundColor;
                titleBar.backgroundColor = customColors.backgroundColor;
                titleBar.foregroundColor = customColors.foregroundColor;
                
            }
        }
    }

    function downloadFile() {
        var file = "files\\David-CV.pdf";

        Windows.ApplicationModel.Package.current.installedLocation.getFileAsync(file).then(
            function (file) {
                var options = new Windows.System.LauncherOptions();
                options.displayApplicationPicker = true;

                // Launch the retrieved file using the default app
                Windows.System.Launcher.launchFileAsync(file, options).then(
                    function (success) {
                        if (success) {
                            // File launched
                        } else {
                            // File launch failed
                        }
                    });
            });
    }

    function backRequested() {
        if (window.location.href.indexOf("/index.html") === -1) {  // not on home page, go back
            // nav back
            window.history.back();
        }
    }

    if (window.addEventListener) {
        window.addEventListener('load', startApp, false);
    }
    
    // Hide  show back button in title bar depending on where you are in the navigation back stack
    function UpdateBackButtonVisibility() {
        if (systemNavManager !== null) {
            if (window.location.href.indexOf("/index.html") === -1) {
                systemNavManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.visible;
            } else {
                systemNavManager.appViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.collapsed;
            };
        }
    }

})();

window.onload = function () {
    document.getElementById("menu-icon").addEventListener("click", openNav, false);
    document.getElementById("close-item").addEventListener("click", closeNav, false);
}

// Add style to selected header
function myFunction() {
    var element = document.getElementById("main-menu");
    element.ADdc("mystyle");
}

/* OPEN NAV */
function openNav() {
    document.getElementById("mySidenav").style.width = "100%";

}

/* CLOSE NAV */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

/* APPS CLICK */
function itemClick(appId) {
    window.open('https://www.microsoft.com/store/apps/' + appId, '_blank');
}

function menuItemClick(station) {
    var url;
    if (station == "batac") {
        url = "http://87.98.130.255:" + 8469;
    }

    document.getElementById("footer").innerHTML =
        "radiostream = \"http:\/\/87.98.130.255:8469\"; artwork = \"http:\/\/html5radios.svnlabs.com\/radio2014\/images\/logo.png\"; width = \"320\";      height = \"200\"; title = \"Test Title\"; artist = \"Test Artist\"; source =      \"shoutcast\"; autoplay = \"true\"; artwork =      \"http:\/\/html5radios.svnlabs.com\/radio2014\/images\/logo.png\";"

}

function includeHTML() {
    var z, i, elmnt, file, xhttp;
    /*loop through a collection of all HTML elements:*/
    z = document.getElementsByTagName("*");
    for (i = 0; i < z.length; i++) {
        elmnt = z[i];
        /*search for elements with a certain atrribute:*/
        file = elmnt.getAttribute("w3-include-html");
        if (file) {
            /*make an HTTP request using the attribute value as the file name:*/
            xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4) {
                    if (this.status == 200) { elmnt.innerHTML = this.responseText; }
                    if (this.status == 404) { elmnt.innerHTML = "Page not found."; }
                    /*remove the attribute, and call this function once more:*/
                    elmnt.removeAttribute("w3-include-html");
                    includeHTML();
                }
            }
            xhttp.open("GET", file, true);
            xhttp.send();
            /*exit the function:*/
            return;
        }
    }
}
