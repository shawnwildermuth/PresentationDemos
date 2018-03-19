Vue.use(VeeValidate);

var vm = new Vue({
  el: "#theForm",
  data: {
    sender: {},
    isBusy: false,
    result: ""
  },
  methods: {
    onSend: function () {
      this.$validator.validateAll().then(function (success) {
        if (!success) {
          return false;
        } else {


          this.isBusy = true;
          axios.post("/api/contact", this.sender)
            .then(function (response) {
              this.isBusy = false;
              this.sender = {};
              this.result = "Sent..."
            }.bind(this))
            .catch(function () {
              this.isBusy = false;
              this.result = "Failed to send mail...";
            }.bind(this));
        }
      });
    }
  }
});