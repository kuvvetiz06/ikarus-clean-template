window.App = window.App || {};

App.Config = App.Config || {};

App.Config.CurrentCulture = _currentCulture;

App.Config.CultureShortName = App.Config.CurrentCulture.split("-")[0].toString();

App.Config.DefLangObj = {};

for (var i = 0; i < _localizedStringsArray.length; i++) {
    var entry = _localizedStringsArray[i];   
    App.Config.DefLangObj[entry.Name] = entry.Value;
}

App.Config.Session = {
    WebApiUrl: "",   
    AccToken: "",
    TenantID: null,
    UserData: null,
    TenantName: ""
};

App.Config.BuildPageModel = {};