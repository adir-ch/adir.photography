(function () {

    'use strict';

    var defaultValues = {
        positions: [{
            name: 'Position'
        }, {
            value: 'tl',
            name: 'Top left'
        }, {
            value: 'tr',
            name: 'Top right'
        }, {
            value: 'br',
            name: 'Bottom right'
        }, {
            value: 'bl',
            name: 'Bottom left'
        }],

        effects: [{
            name: 'Effect'
        }, {
            value: 'slidein',
            name: 'Slide in + fade'
        }, {
            value: 'zoomin',
            name: 'Zoom in'
        }, {
            value: 'fountain',
            name: 'Fountain'
        }, {
            value: 'slidein-spring',
            name: 'Slidein (spring)'
        }],

        methods: [{
            name: 'Method'
        }, {
            value: 'click',
            name: 'Click'
        }, {
            value: 'hover',
            name: 'Hover'
        }],
        actions: [{
            name: 'Fire Main Action?'
        }, {
            value: 'fire',
            name: 'Fire'
        }, {
            value: 'nofire',
            name: 'Don\'t Fire'
        }]
    };


    angular
        .module('MfbButtonMenu', ['ng-mfb-directives'])
        .value('defaultValues', defaultValues)
        .controller('MfbButtonMenuViewModel', MfbButtonMenuViewModel)

    MfbButtonMenuViewModel.$inject = ['defaultValues', '$scope', '$window']

    function MfbButtonMenuViewModel(defaultValues, $scope, $window) {
        var vm = this;
        // //console.log(this);

        vm.showLoginDialog = false;
        vm.positions = defaultValues.positions;
        vm.effects = defaultValues.effects;
        vm.methods = defaultValues.methods;
        vm.actions = defaultValues.actions;

        vm.menuState = 'closed';
        vm.menuButtonClicked = menuButtonClicked;
        vm.setMainAction = setMainAction;
        vm.mainAction = mainAction;

        vm.chosen = {
            effect: 'slidein',
            position: 'br',
            method: 'click',
            action: 'fire'
        };

        vm.buttons = [{
            label: 'Home',
            icon: 'ion-ios-home',
            href: '/gallery'
        }, {
            label: 'Portfolio',
            icon: 'ion-images',
            href: '/gallery/albums'
        }, {
            label: 'About & Contact',
            icon: 'ion-android-mail',
            href: '/home/about'
        }, {
            label: 'Register for updates',
            icon: 'ion-person',
            href: 'login-directive'
        }];

        function menuButtonClicked(href) {
            if (href == "login-directive") {
                //console.log("calling login dialog");
                vm.showLoginDialog = true;
                //this.closeMenu();

            } else {
                $window.location.href = href;
            }

            this.closeMenu();
        }

        function mainAction() {
            ////console.log('Firing Main Action!');
        }

        function setMainAction() {
            if (vm.chosen.action === 'fire') {
                vm.mainAction = mainAction;
            } else {
                vm.mainAction = null;
            }
        }

        $scope.$on("loginPopupDialogFinished", function (event, args) {
            //console.log("reset login dialog status");
            vm.showLoginDialog = false;
        });
    }

    MfbButtonMenuViewModel.prototype.hovered = function () {
        // toggle something on hover.
    };

    MfbButtonMenuViewModel.prototype.toggle = function () {
        this.menuState = this.menuState === 'closed' ? 'open' : 'closed';
    };

    MfbButtonMenuViewModel.prototype.closeMenu = function () {
        this.menuState = 'closed';
    };

}());