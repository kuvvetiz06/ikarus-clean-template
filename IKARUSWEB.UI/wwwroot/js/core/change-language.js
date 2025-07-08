const changeLanguage = (culture, setLanguageUrl) => {

    $.ajax({
        type: 'POST',
        url: setLanguageUrl,
        data: {
            culture: culture,
        },
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {


        }
    });

};