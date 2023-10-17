Ext.define("ExtJSDemo.view.user.UserListModel", {
  extend: "Ext.app.ViewModel",
  alias: "viewmodel.userList",
  fields: ["name"],
  stores: {
    userList: {
      fields: [
        "id",
        "firstName",
        "lastName",
        "emailAddress",
        "Address",
        "groupId",
        "groupName",
      ],
      proxy: {
        type: "ajax",
        url: "https://localhost:7000/api/Users",
      },
      autoLoad: true,
    },
  },
});
