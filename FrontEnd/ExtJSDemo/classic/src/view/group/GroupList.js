Ext.define("ExtJSDemo.view.group.GroupList", {
  extend: "Ext.grid.Panel",
  xtype: "groupList",
  controller: "groupList",
  viewModel: "groupList",
  title: "Groups",
  id: "GroupsGridId",
  columnLines: true,
  tbar: [
    {
      xtype: "button",
      text: "Create Group",
      handler: function () {
        var form = Ext.create("ExtJSDemo.view.group.GroupCreateModal");
        form.show();
      },
    },
  ],
  columns: [
    {
      text: "Name",
      flex: 1,
      dataIndex: "name",
    },
    {
      xtype: "widgetcolumn",
      text: "Edit",
      align: "center",
      width: 150,
      widget: {
        xtype: "button",
        layout: "hbox",
        text: "Edit",
        handler: function (btn) {
          var record = btn.getWidgetRecord();
          var rec = record.getData();
          var editWindow = Ext.create("ExtJSDemo.view.group.GroupEditModal");
          editWindow.down("form").getForm().setValues(rec);
          editWindow.show();
        },
      },
    },
    {
      xtype: "widgetcolumn",
      text: "Delete",
      width: 150,
      widget: {
        xtype: "button",
        layout: "hbox",
        text: "Delete",
        handler: function (button) {
          var record = button.getWidgetRecord();
          var rec = record.getData();
          Ext.Msg.confirm(
            "Confirm",
            "Are you sure you want to delete group?",
            function (btn) {
              if (btn === "yes") {
                Ext.Ajax.request({
                  url: "https://localhost:7000/api/Groups/" + rec.id,
                  method: "DELETE",
                  success: function (response) {
                    console.log(
                      "Data deleted successfully:",
                      response.responseText
                    );
                    var grid = Ext.getCmp("GroupsGridId");
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
  ],
  bind: {
    store: "{groupList}",
  },
});
