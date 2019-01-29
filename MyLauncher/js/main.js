// Your code here!
(function mainModule() {
    "use strict";

    var systemNavManager = Windows.UI.Core.SystemNavigationManager.getForCurrentView();
    function startApp() {
        var addNewPost = document.getElementById('add-new-post');
        var openNewsFolder = document.getElementById('open-folder');
        var viewWebsite = document.getElementById('open-website');
        var uploadMedia = document.getElementById('add-image');
        var openApp = document.getElementById('open-app');
        var openRpnFolder = document.getElementById('open-cloud');

        addNewPost.addEventListener('click', launch('http://rpnradio.com/wp-admin/post-new.php', false));
        openNewsFolder.addEventListener('click', launch('news.lnk', false));
        viewWebsite.addEventListener('click', launch('http://rpnradio.com', false));
        uploadMedia.addEventListener('click', launch('hhttp://rpnradio.com/wp-admin/upload.php', false));
        openApp.addEventListener('click', launch('rddevtools://', false));
        openRpnFolder.addEventListener('click', launch('rpn.lnk', false));

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

})();
