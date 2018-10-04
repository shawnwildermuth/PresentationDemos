var app = new Vue({
  el: "#theForm",
  data: {
    sender: {}
  },
  methods: {
    onSend() {
      alert(JSON.stringify(this.sender));
    }
  }
});