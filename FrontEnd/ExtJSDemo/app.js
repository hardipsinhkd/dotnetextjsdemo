/*
 * This file launches the application by asking Ext JS to create
 * and launch() the Application class.
 */
Ext.application({
    extend: 'ExtJSDemo.Application',

    name: 'ExtJSDemo',

    requires: [
        // This will automatically load all classes in the ExtJSDemo namespace
        // so that application classes do not need to require each other.
        'ExtJSDemo.*'
    ],

    // The name of the initial view to create.
    mainView: 'ExtJSDemo.view.main.Main'
});
