// contact.js
var vm = new Vue({
  el: "#theForm",
  data: {
    message: {}
  },
  methods: {
    onSave: function () {
      alert(JSON.stringify(this.message));
      
    }
  }
});