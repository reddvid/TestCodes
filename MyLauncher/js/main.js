// Your code here!
(function mainModule() {
    "use strict";

    var systemNavManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    function startApp() {


        var applicationView = Windows.UI.ViewManagement.ApplicationView;
        var applicationViewWindowingMode = Windows.UI.ViewManagement.ApplicationViewWindowingMode;

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
        var compactOptions = Windows.UI.ViewManagement.ViewModePreferences.createDefault(applicationViewWindowingMode.compactOverlay); // viewModePreferences.CreateDefault(ApplicationViewMode.CompactOverlay);
        compactOptions.customSize.height = 500;
        compactOptions.customSize.width = 500;
        applicationView.getForCurrentView().tryEnterViewModeAsync(applicationViewWindowingMode.compactOverlay, compactOptions);
    }

    if (window.addEventListener) {
        window.addEventListener('load', startApp, false);
    }

})();


var addNewPost = document.getElementById('add-new-post');
var openNewsFolder = document.getElementById('open-folder');
var viewWebsite = document.getElementById('open-website');
var uploadMedia = document.getElementById('add-image');
var openApp = document.getElementById('open-app');
var openRpnFolder = document.getElementById('open-cloud');

addNewPost.addEventListener("click", launch, false);
addNewPost.param = 'http://rpnradio.com/wp-admin/post-new.php';

openNewsFolder.addEventListener('click', launch, false);
openNewsFolder.param = 'file:///D:/OneDrive/Work%20(CNN%20PH)/C.RPN%20Website/News/';

viewWebsite.addEventListener('click', launch, false);
viewWebsite.param = 'http://rpnradio.com';

uploadMedia.addEventListener('click', launch, false);
uploadMedia.param = 'http://rpnradio.com/wp-admin/upload.php';

openApp.addEventListener('click', launch, false);
openApp.param = 'rddevtools://';

openRpnFolder.addEventListener('click', openLink, false);
openRpnFolder.param = 'rpn.lnk';


function launch(e) {
    console.log(this.param);
    // Create a Uri object from a URI string 
    var uri = new Windows.Foundation.Uri(this.param);
    // Launch the retrieved file using the default app
    Windows.System.Launcher.launchUriAsync(uri).then(
        function (success) {
            if (success) {
                // URI launched 
            } else {
                // URI launch failed 
            }
        });
}

function openLink(e) {
    var f = this.param;
    Windows.ApplicationModel.Package.current.installedLocation.getFileAsync(f).then(
        function (f) {
            var options = new Windows.System.LauncherOptions();
            options.displayApplicationPicker = true;

            // Launch the retrieved file using the default app
            Windows.System.Launcher.launchFileAsync(f, options).then(
                function (success) {
                    if (success) {
                        // File launched
                    } else {
                        // File launch failed
                    }
                });
        });
}