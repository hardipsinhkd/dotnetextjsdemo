Ext.define("ExtJSDemo.view.group.GroupListModel", {
  extend: "Ext.app.ViewModel",
  alias: "viewmodel.groupList",
  fields: ["name"],
  stores: {
    groupList: {
      fields: ["id", "name"],
      proxy: {
        type: "ajax",
        url: "https://localhost:7000/api/Groups",
      },
      autoLoad: true,
    },
  },
});
