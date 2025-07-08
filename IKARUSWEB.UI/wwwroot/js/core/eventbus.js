// wwwroot/js/core/EventBus.js
(function (window) {
    window.App = window.App || {};

    App.EventBus = (function () {
        const _events = {};
        return {
            on(eventName, handler) {
                if (!_events[eventName]) _events[eventName] = [];
                _events[eventName].push(handler);
            },
            off(eventName, handler) {
                if (!_events[eventName]) return;
                _events[eventName] = _events[eventName].filter(h => h !== handler);
            },
            emit(eventName, data) {
                if (!_events[eventName]) return;
                _events[eventName].forEach(h => h(data));
            }
        };
    })();
})(window);
