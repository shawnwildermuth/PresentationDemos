// index.js
Vue.filter("date", function (src) {
  return src.toDateString();
});

new Vue({
  el: "#theApp",
  data: {
    birthday: new Date(),
    name: "Shawn",
    location: "Nashville",
    isBusy: false,
    error: "",
    rooms: [
      {
        title: "Omni Room",
        description: "This is the Omni Room",
        rate: 149.99,
        maxGuests: 3,
        images: [
          { url: "/img/omni-1.jpg" }
        ]
      },
      {
        title: "Varsity Room",
        description: "This is the Varsity Room",
        rate: 129.99,
        maxGuests: 2,
        images: [
          { url: "/img/varsity-1.jpg" }
        ]
      }
    ]

  },
  methods: {
    onBook: function (r) {
      this.isBusy = !this.isBusy;
    },
    onNewRoom: function () {
      this.rooms.push({
        title: "New Room",
        description: "This is the Omni Room",
        rate: 149.99,
        maxGuests: 3,
        images: [
          { url: "/img/omni-1.jpg" }
        ]
      })
    }
  }

});