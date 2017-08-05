Vue.use(VeeValidate);

var app = new Vue({
  el: "main",
  data: {
    name: "Shawn",
    birthdate: new Date(),
    isBusy: false,
    products: [],
    company: {
      contactName: "Phil"
    }
  },
  filters: {
    shortDate: function (val) {
      return val.toDateString();
    }
  },
  created: function () {
    axios.get("/api/products")
      .then(function (result) {
        this.products = result.data;
      }.bind(this), function () {
        console.log("Bad things happen to good developers.");
      })
  },
  methods: {
    onSubmit: function () {
      //this.isBusy = !this.isBusy;
      //alert(JSON.stringify(this.company));
      // axios.post(...)
      //this.products.push({ title: "Another Product" });
      this.$validator.validateAll().then(function (success) {
        if (!success) {
          return false;
        } else {
          //axios.post(...);
        }
      });
    }
  }
});