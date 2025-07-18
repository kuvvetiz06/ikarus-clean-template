"use strict";

// Class definition
var KTThemeMode = function () {
    var menu;
    var callbacks = [];
    var the = this;

    var getMode = function () {
        var mode;

        if (document.documentElement.hasAttribute("data-bs-theme")) {
            return document.documentElement.getAttribute("data-bs-theme");
        } else if (localStorage.getItem("data-bs-theme") !== null) {
            return localStorage.getItem("data-bs-theme");
        } else if (getMenuMode() === "system") {
            return getSystemMode();
        }

        return "light";
    }

    var setMode = function (mode, menuMode) {
        var currentMode = getMode();

        // Reset mode if system mode was changed
        if (menuMode === 'system') {
            if (getSystemMode() !== mode) {
                mode = getSystemMode();
            }
        } else if (mode !== menuMode) {
            menuMode = mode;
        }

        // Set mode to the target document.documentElement
        document.documentElement.setAttribute("data-bs-theme", mode);

        // Store mode value in storage
        localStorage.setItem("data-bs-theme", mode);

        // Update DevExpress CSS based on the theme mode
        updateDevExpressThemeCss(mode);

        // Set active menu item
        var activeMenuItem = menu ? menu.querySelector('[data-kt-element="mode"][data-kt-value="' + menuMode + '"]') : null;
        if (activeMenuItem) {
            localStorage.setItem("data-bs-theme-mode", menuMode);
            setActiveMenuItem(activeMenuItem);
        }

        if (mode !== currentMode) {
            KTEventHandler.trigger(document.documentElement, 'kt.thememode.change', the);
        }
    }

    var getMenuMode = function () {
        if (!menu) {
            return null;
        }

        var menuItem = menu.querySelector('.active[data-kt-element="mode"]');

        if (menuItem && menuItem.getAttribute('data-kt-value')) {
            return menuItem.getAttribute('data-kt-value');
        } else if (document.documentElement.hasAttribute("data-bs-theme-mode")) {
            return document.documentElement.getAttribute("data-bs-theme-mode");
        } else if (localStorage.getItem("data-bs-theme-mode") !== null) {
            return localStorage.getItem("data-bs-theme-mode");
        } else {
            return typeof defaultThemeMode !== "undefined" ? defaultThemeMode : "light";
        }
    }

    var getSystemMode = function () {
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? "dark" : "light";
    }

    var initMode = function () {
        setMode(getMode(), getMenuMode());
        KTEventHandler.trigger(document.documentElement, 'kt.thememode.init', the);
    }

    var getActiveMenuItem = function () {
        return menu.querySelector('[data-kt-element="mode"][data-kt-value="' + getMenuMode() + '"]');
    }

    var setActiveMenuItem = function (item) {
        var menuMode = item.getAttribute("data-kt-value");

        var activeItem = menu.querySelector('.active[data-kt-element="mode"]');

        if (activeItem) {
            activeItem.classList.remove("active");
        }

        item.classList.add("active");
        localStorage.setItem("data-bs-theme-mode", menuMode);
    }

    var handleMenu = function () {
        var items = [].slice.call(menu.querySelectorAll('[data-kt-element="mode"]'));

        items.map(function (item) {
            item.addEventListener("click", function (e) {
                e.preventDefault();

                var menuMode = item.getAttribute("data-kt-value");
                var mode = menuMode;

                if (menuMode === "system") {
                    mode = getSystemMode();
                }

                setMode(mode, menuMode);
            });
        });
    }

    // Function to update the DevExpress CSS based on the theme mode
    var updateDevExpressThemeCss = function (themeMode) {
        var existingLink = document.querySelector('link[href*="dx.material.blue"]');
        if (existingLink) {
            document.head.removeChild(existingLink);
        }

        var devexpressLink = document.createElement('link');
        devexpressLink.rel = 'stylesheet';

        if (themeMode === "dark") {
            devexpressLink.href = "/css/devextreme/dx.material.blue.dark.compact.css"; // Dark theme CSS
        } else {
            devexpressLink.href = "/css/devextreme/dx.material.blue.light.compact.css"; // Light theme CSS
        }        
        document.head.appendChild(devexpressLink);
    }

    return {
        init: function () {
            menu = document.querySelector('[data-kt-element="theme-mode-menu"]');

            initMode();

            if (menu) {
                handleMenu();
            }
        },

        getMode: function () {
            return getMode();
        },

        getMenuMode: function () {
            return getMenuMode();
        },

        getSystemMode: function () {
            return getSystemMode();
        },

        setMode: function (mode) {
            return setMode(mode)
        },

        on: function (name, handler) {
            return KTEventHandler.on(document.documentElement, name, handler);
        },

        off: function (name, handlerId) {
            return KTEventHandler.off(document.documentElement, name, handlerId);
        }
    };
}();

// Initialize app on document ready
KTUtil.onDOMContentLoaded(function () {
    KTThemeMode.init();
});

// Declare KTThemeMode for Webpack support
if (typeof module !== 'undefined' && typeof module.exports !== 'undefined') {
    module.exports = KTThemeMode;
}
