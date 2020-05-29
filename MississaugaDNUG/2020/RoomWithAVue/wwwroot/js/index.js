new Vue({
  data: {
    name: "Shawn",
    today: new Date(),
    backcolor: "light"
  },
  computed: {
    todayFormatted: function () {
      return this.today.toDateString();
    }
  }
}).$mount("#theSection");