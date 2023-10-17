Ext.define("ExtJSDemo.view.group.GroupEditForm", {
  extend: "Ext.form.Panel",
  xtype: "groupeditform",
  width: 350,
  height: 150,
  renderTo: Ext.getBody(),
  items: [
    {
      xtype: "hiddenfield",
      name: "id",
    },
    {
      xtype: "textfield",
      name: "name",
      fieldLabel: "Name",
      allowBlank: false,
      maxLength: 50,
      width: 250,
      height: 10,
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
            url: "https://localhost:7000/api/Groups",
            method: "PUT",
            jsonData: formData,
            success: function (response) {
              var grid = Ext.getCmp("GroupsGridId");
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

Ext.define("ExtJSDemo.view.group.GroupEditModal", {
  extend: "Ext.window.Window",
  xtype: "groupeditmodal",
  title: "Edit Group",
  modal: true,
  layout: "fit",
  width: 400,
  height: 200,
  bodyPadding: 10,
  closeAction: "hide",
  items: [
    {
      xtype: "groupeditform",
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
