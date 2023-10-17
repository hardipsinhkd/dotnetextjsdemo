Ext.define("ExtJSDemo.view.group.GroupCreateForm", {
  extend: "Ext.form.Panel",
  xtype: "groupcreateform",
  width: 350,
  height: 150,
  items: [
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
  slideInDuration: 10000, // Adjust the duration as needed
  slideInAnimation: "slide", // Use 'slide' for RTL to LTR animation
  slideOutDuration: 10000, // Adjust the duration as needed
  slideOutAnimation: "slide",
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
            method: "POST",
            jsonData: formData,
            success: function (response) {
              console.log("Data saved successfully:", response.responseText);
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

Ext.define("ExtJSDemo.view.group.GroupCreateModal", {
  extend: "Ext.window.Window",
  xtype: "groupcreatemodal",
  title: "Create Group",
  modal: true,
  layout: "fit",
  width: 400,
  height: 200,
  bodyPadding: 10,
  closeAction: "hide",
  items: [
    {
      xtype: "groupcreateform",
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
