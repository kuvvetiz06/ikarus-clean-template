// wwwroot/js/core/HelperService.js
(function (window) {
    window.App = window.App || {};

    App.Helper = {
        generateUUID() {
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = Math.random() * 16 | 0,
                    v = c === 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
        },

        generateGUID() {
            var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
            var result = '';
            for (var i = 0; i < 4; i++) {
                result += chars.charAt(Math.floor(Math.random() * chars.length));
            }
            return result;
        },

        arraySum(array, propName) {
            return (array || []).reduce(function (acc, val) {
                return acc + (val[propName] || 0);
            }, 0);
        },

        formatCurrencyTL(amount) {
            if (typeof amount !== 'number') amount = parseFloat(amount) || 0;
            return new Intl.NumberFormat('tr-TR', {
                style: 'currency',
                currency: 'TRY'
            }).format(amount);
        },

        isJson(str) {
            if (typeof str !== 'string') return false;
            try {
                JSON.parse(str);
                return true;
            } catch (e) {
                return false;
            }
        },

        setText(selector, value) {
            if (typeof selector === 'string') {
                $(selector).text(value);
            }
        },

        setValue(selector, value) {
            if (typeof selector === 'string') {
                $(selector).val(value);
            }
        },

        reloadPage() {
            window.location.reload();
        }
    };

})(window);
