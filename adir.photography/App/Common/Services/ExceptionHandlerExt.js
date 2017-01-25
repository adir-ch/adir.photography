(function() {

    angular
        .module('AppCommonServices')
        .config(configDecorator);

    function configDecorator($provide) {
        $provide.decorator("$exceptionHandler", exceptionHandlerExt);
    };

    exceptionHandlerExt.$inject = ['$delegate', '$injector'];

    function exceptionHandlerExt($delegate, $injector) {

        // need to return a function, since Angular will call this function
        // when there will be an unhandeled exception
        return function(exception, cause) {
            $delegate(exception, cause);
            console.log("excp delegate: ", exception);

            var WebApiService = $injector.get('WebApiService');
            var GlobalConfigurationService = $injector.get('GlobalConfigurationService');
            WebApiService.apiPost(GlobalConfigurationService.url("clientLogging"), "\"" + exception.message + "\"");
        }
    }
}());