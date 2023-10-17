Ext.define("ExtJSDemo.view.user.UserCreateForm", {
  extend: "Ext.form.Panel",
  xtype: "usercreateform",
  width: 400,
  height: 400,
  animateTarget: "myButton",
  items: [
    {
      xtype: "textfield",
      name: "firstName",
      fieldLabel: "First Name",
      allowBlank: false,
      maxLength: 50,
      width: 300,
      height: 10,
    },
    {
      xtype: "textfield",
      name: "lastName",
      fieldLabel: "Last Name",
      allowBlank: false,
      maxLength: 50,
      width: 300,
      height: 10,
    },
    {
      xtype: "textfield",
      name: "emailAddress",
      fieldLabel: "Email",
      vtype: "email",
      allowBlank: false,
      maxLength: 50,
      width: 300,
      height: 10,
    },
    {
      xtype: "textareafield",
      grow: true,
      name: "address",
      fieldLabel: "Address",
      allowBlank: true,
      maxLength: 500,
      width: 300,
      height: 50,
    },
  ],
  buttons: [
    {
      text: "Save",
      handler: function () {
        var formPanel = this.up("form");
        var form = formPanel.getForm();

        if (form.isValid()) {
          var formData = form.getValues();

          Ext.Ajax.request({
            url: "https://localhost:7000/api/Users",
            method: "POST",
            jsonData: formData,
            success: function (response) {
              console.log("Data saved successfully:", response.responseText);
              var grid = Ext.getCmp("UsersGridId");
              var store = grid.getStore();
              store.load();
              formPanel.up("window").close();
            },
            failure: function (response) {
              console.error("API request failed:", response);
            },
          });
        }
      },
    },
  ],
});

Ext.define("ExtJSDemo.view.user.UserCreateModal", {
  extend: "Ext.window.Window",
  xtype: "usercreatemodal",
  title: "Create User",
  modal: true,
  layout: "fit",
  width: 500,
  height: 400,
  bodyPadding: 10,
  closeAction: "hide",
  items: [
    {
      xtype: "usercreateform",
    },
  ],
  listeners: {
    show: function (window) {
      const screenWidth = Ext.getBody().getViewSize().width;
      const windowWidth = window.getWidth();
      const leftPosition = (screenWidth - windowWidth) / 2;
      window.getEl().setLeft(screenWidth);
      window.getEl().animate({
        from: {
          left: screenWidth,
        },
        to: {
          left: leftPosition,
        },
        duration: 700,
        easing: "easeOut",
      });
    },
    beforeclose: function (window) {
      const screenWidth = Ext.getBody().getViewSize().width;
      window.getEl().animate({
        to: {
          left: screenWidth,
        },
        duration: 1000,
        easing: "easeOut",
        callback: function () {
          window.callParent(arguments);
        },
      });
    },
  },
});
