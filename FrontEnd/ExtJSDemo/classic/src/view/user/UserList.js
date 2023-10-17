Ext.define("ExtJSDemo.view.user.UserList", {
  extend: "Ext.grid.Panel",
  xtype: "userList",
  controller: "userList",
  viewModel: "userList",
  title: "Users",
  id: "UsersGridId",
  columnLines: true,
  tbar: [
    {
      xtype: "button",
      text: "Create User",
      id: "myButton",
      handler: function () {
        var form = Ext.create("ExtJSDemo.view.user.UserCreateModal");
        form.show();

        // Custom animation using JavaScript
      },
    },
  ],
  columns: [
    { text: "First Name", dataIndex: "firstName", width: 150 },
    { text: "Last Name", dataIndex: "lastName", width: 150 },
    { text: "Email", dataIndex: "emailAddress", width: 150 },
    { text: "Group", dataIndex: "groupName", width: 150 },
    { text: "Address", dataIndex: "address", width: 250 },
    {
      xtype: "widgetcolumn",
      text: "Edit",
      width: 70,
      widget: {
        xtype: "button",
        layout: "hbox",
        text: "Edit",
        handler: function (btn) {
          var record = btn.getWidgetRecord();
          var rec = record.getData();
          var editWindow = Ext.create("ExtJSDemo.view.user.UserEditModal");
          editWindow.down("form").getForm().setValues(rec);
          editWindow.show();
        },
      },
    },
    {
      xtype: "widgetcolumn",
      text: "Delete",
      width: 90,
      widget: {
        xtype: "button",
        layout: "hbox",
        text: "Delete",
        handler: function (button) {
          var record = button.getWidgetRecord();
          var rec = record.getData();
          Ext.Msg.confirm(
            "Confirm",
            "Are you sure you want to delete user?",
            function (btn) {
              if (btn === "yes") {
                Ext.Ajax.request({
                  url: "https://localhost:7000/api/Users/" + rec.id,
                  method: "DELETE",
                  success: function (response) {
                    console.log(
                      "Data deleted successfully:",
                      response.responseText
                    );
                    var grid = Ext.getCmp("UsersGridId");
                    var store = grid.getStore();
                    store.load();
                  },
                  failure: function (response) {
                    console.error("API request failed:", response);
                  },
                });
              }
            }
          );
        },
      },
    },
    {
      xtype: "widgetcolumn",
      text: "Assign Group",
      width: 140,
      widget: {
        xtype: "button",
        layout: "hbox",
        text: "Assign Group",
        handler: function (btn) {
          var record = btn.getWidgetRecord();
          var rec = record.getData();
          var editWindow = Ext.create(
            "ExtJSDemo.view.user.UserAssignGroupModal"
          );
          editWindow.down("form").getForm().setValues(rec);
          editWindow.show();
        },
      },
    },
  ],
  bind: {
    store: "{userList}",
  },
});
