// contact.js
Vue.use(VeeValidate);

new Vue({
  el: "#theForm",
  data: {
    msg: {}
  },
  methods: {
    onSend() {
      alert(JSON.stringify(this.msg));
    }
  }
});