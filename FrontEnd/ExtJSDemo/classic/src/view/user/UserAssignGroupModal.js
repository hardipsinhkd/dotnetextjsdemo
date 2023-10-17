var groupstores = Ext.create("Ext.data.Store", {
  fields: ["id", "name"],
  proxy: {
    type: "ajax",
    url: "https://localhost:7000/api/Groups/GetAllForAssigning",
  },
  autoLoad: true,
});

Ext.define("ExtJSDemo.view.user.UserAssignGroupForm", {
  xtype: "userassigngroupform",
  extend: "Ext.form.Panel",
  width: 400,
  height: 200,
  items: [
    {
      xtype: "hiddenfield",
      name: "id",
    },
    {
      xtype: "combo",
      name: "groupId",
      fieldLabel: "Choose Group",
      allowBlank: false,
      maxLength: 50,
      width: 300,
      height: 10,
      store: groupstores,
      queryMode: "local",
      displayField: "name",
      valueField: "id",
      label: "Choose one",
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
            url: "https://localhost:7000/api/Users/AssignGroup",
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

Ext.define("ExtJSDemo.view.user.UserAssignGroupModal", {
  extend: "Ext.window.Window",
  xtype: "userassigngroupmodal",
  title: "Assign Group",
  modal: true,
  layout: "fit",
  width: 400,
  height: 200,
  bodyPadding: 10,
  closeAction: "hide",
  items: [{ xtype: "userassigngroupform" }],
  listeners: {
    show: function (window) {
      groupstores.load();
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
