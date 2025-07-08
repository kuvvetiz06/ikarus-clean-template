const messageType = {
    Success: 'success',
    Error: 'error',
    RemoveConfirmation: 'info',
    Info: 'info'
};

const dialogIcon = {
    success: 'success',
    error: 'error',
    info: 'info'
};

const defLangObj = App.Config.defLangObj;

/**
 * @param {String} _messageType
 * @param {String} _message
 * @param {Function} _btnOkClickEvent
 * @param {Function} _btnCancelClickEvent
 */
const showMessageDialog = (_messageType, _message = "", _btnOkClickEvent, _btnCancelClickEvent) => {
    let icon = dialogIcon[_messageType.toLowerCase()] || "info";
    let showCancelButton = false;
    let confirmButtonText = defLangObj.Ok;
    let cancelButtonText = defLangObj.Cancel;

    const fallbackMessages = {
        [messageType.Success]: defLangObj.SuccessMessageJS,
        [messageType.Error]: defLangObj.ErrorMessageJS,
        [messageType.RemoveConfirmation]: defLangObj.RemoveConfirmationMessageJS,
        [messageType.Info]: defLangObj.InfoMessageJS
    };

    // Override message if it's a key in defLangObj
    if (_message && defLangObj[_message]) {
        _message = defLangObj[_message];
    }

    // If still empty, set fallback message
    if (!_message) {
        _message = fallbackMessages[_messageType] || "";
    }

    // Customize button text per message type
    switch (_messageType) {
        case messageType.RemoveConfirmation:
            showCancelButton = true;
            confirmButtonText = defLangObj.Confirm;
            break;

        case messageType.Info:
            showCancelButton = true;
            break;
    }

    Swal.fire({
        text: _message,
        icon,
        showCancelButton,
        confirmButtonText,
        cancelButtonText
    }).then(result => {
        if (result.value && typeof _btnOkClickEvent === "function") {
            _btnOkClickEvent();
        } else if (result.isDismissed && typeof _btnCancelClickEvent === "function") {
            _btnCancelClickEvent();
        }
    });
};
