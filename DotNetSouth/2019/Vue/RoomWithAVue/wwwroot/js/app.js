// app.js
Vue.filter("date", function (val) {
  return val;
});

var vm = new Vue({
  el: "#theApp",
  data: {
    name: "Shawn",
    theDate: new Date(),
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
      },
      {
        title: "Calanwold",
        description: "This is the Calanwold Room",
        rate: 199.99,
        maxGuests: 3,
        images: [
          { url: "/img/omni-2.jpg" }
        ]
      },
    ]

  },
  computed: {
    nameCount() {
      return this.name.length;
    },
    nameClass: function () {
      return name.length > 5 ? "text-primary": "text-muted";
    }
  },
  methods: {
    bookRoom(room) {
      alert(room.title);
    }
  }
});