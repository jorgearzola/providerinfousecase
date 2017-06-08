//we could use a library like Knockout for two way binding properties

var providerInfoVM = function(){
    this.ProviderInfo = {
        federal_provider_number: $('#federal_provider_number').val(),
        provider_name: $('#provider_name').val(),
        provider_zip_code: $('#provider_zip_code').val()
    }
}

//minor validation of entered text
providerInfoVM.prototype.isNumeric = function (n) {

    var reg = new RegExp(/^\d+$/);
    var res = reg.test(n);

    if (res) {
        return true;
    } else {
        return false;
    }

}



