Vue.use(VeeValidate);

var app = new Vue({
  el: "#theForm",
  data: {
    sender: {},
    isBusy: false,
    result: ""
  },
  mounted: function () { this.$validator.validate() },
  methods: {
    onSubmit() {

      this.$validator.validateAll().then((success) => {
        if (!success) {
          return false;
        } else {
          this.isBusy = true;
          axios.post("/api/contact", this.sender)
            .then((response) => {
              this.isBusy = false;
              this.sender = {};
              this.result = "Sent";
            })
            .catch(() => {
              this.isBusy = false;
              this.result = "Failed to send mail...";
            });
        }
      });
    }
  }
});