// index.js
var vue = new Vue({
  data: {
    name: "Shawn"
  },
  methods: {
    addToName() {
      this.name = this.name + "_";
    }
  }
}).$mount("#theApp");